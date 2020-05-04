# CppSharp Test

This project illustrates a memory alignment problem with the 
https://github.com/mono/cppsharp tool at 
the current state at 

    April 25, 2020 commit 0da9c46a7365be7d8b35a60ca4f822b6cbba6a49 
    
This problem only relates to Windows code generation. 

# UPDATE  4 May, 2020 #

This memory alignment problem stems from the Debug/Release modes on Windows. The Release/Debug
mode on Linux has no problems. 

Apparently, you cannot mix
Debug libraries and Release libraries in Windoze because the standard library structures
are different, something that Linux does not do. This disparity means that you must have
either Debug compiled libraries or Release compiled libraries. You better not mix them
especially if you are passing things like std::string across library boundaries. More relevant
to this case, you cannot mix them in crossing the P/INVOKE interface between 
CppSharp generated C# code and its C++ counterpart.

This whole problem was solved by making sure that if you were compiling 
your C++ in Debug mode on Windoze with MVSC++ that you issue the 

    -D_DEBUG
    
flag to the CppSharp CLI command line. Do not issue this flag in the extra cflags, i.e. 
inside the `--A=` CppSharp
option. Note, there is an underbar(_) before DEBUG.  For example, the following would be a
suitable CLI command line for generating Debug oriented code on Windows:

    CppSharp.CLI.exe -D_DEBUG Test.hpp

Note, that this flag may be issued for Linux as well without consequence. The standard libraries 
on Linux are unaffected by this flag in this regard, and the result will not be different. 

The issue for CppSharp problem is https://github.com/mono/CppSharp/issues/1365, and it has
now been CLOSED, on 4-May-2020.

The problems with the size of std::string and even the alignment problems with the
lower problem of wrong offsets for the StringVal internals have also been resolved with 
issuing the `-D_DEBUG` flag. 

Remember to compile your C++ code with the same flag, or not, but be consistent between the two.

**NOTE::** This repository will remain as show and example of using Rake to 
use CppSharp with Linux and Windows configurations and configure them for 
Release vs Debug.

There are now new rake tasks called:

    rake config_debug
    rake config_release

both tasks, clean the generated directories and write a file called `configuration.txt`
containing either `Debug` or `Release`, regenerate the C# and recreate the CMake 
build directory. The `configuration.txt` file is used in deciding to include
the `-D_DEBUG` flag in the call to CppSharp and to generate the proper 
`-DCMAKE_BUILD_TYPE=Release|Debug` option to CMake.

## The Problem ##

The std::string type in Windows is given a size of 32. So, therefore,
a class such as:

    class Test { std::string item1; std:string item2; std::string item3}

will generate FieldOffsets of 0, 32, and 64, respectively for each field. However,
I find that on my Windows VM with VS2019 installed that they may well
be 32 in size, but they are actually 40 bytes apart. That is,
for an instance of Test:

    (&item1 - this) == 0
    (&item2 - this) == 40
    (&item3 - this) == 80

This alignment mismatch obviously screws things up. So, I've illustrated
this problem with a Test class and a program, with some fixes
along the way to get it working.

## Setting up the project ###

This project is JetBrains Rider and VS2019 compatible. The only thing you have 
to do outside of the IDE is generate the code using the CPP Sharp Tool.

### Requirements ###

You need the following tools:
* ruby 2.5.1 or higher
* rake 
* cmake 3.14 or higher (usually comes with vs2019)

You need Ruby and Rake to facilitate the code generation and code
compilation, and automated fixes for the problem. Alternatively, 
you would know how to read Rake/Ruby to get an executable
specification of what needs to be done. 

Set your **CPPSHARP_CLI_EXE** Environment variable to the location of
your CppSharp CLI executable. This is normally at 

    $CPPSHARP_HOME/build/vs2017/lib/Release_x64/CppSharp.CLI.exe

Then `cd CppSharpTest`, which is inside the project directory. Then type:

    rake gen
    rake compile
    rake run

The `rake gen` will call the CppSharp CLI and generate code from the Test.hpp file. 
The Generated code will be in `Generated/win/x64/ALK` directory.

Then `rake compile` will create a cmake-win-build directory and generate
the ALK.dll and Std-symbols.dll. These will be installed in the projects
bin/Debug/netcoreapp3.1 directory so that the main program can load them.

The `rake run` command merely calls `dotnet run`, and this command works in Linux. 

However, on Windows ......  blech!

I find it craps out for some reason. However, the project
may be loaded into VS2019 and run from there without a problem. You must generate
the code using CppSharp first, and then compile it using Cmake, or with vs2019.

At this point, the program (`Main` in `Program.cs`) will fail because of the
misalignment of the itemfields. 

    rake fix_test_class
    
Will rectify this problem by modifying the code in `ALK.cs` to change the 
FieldOffsets of the items for the generated `Test.__Internal` to multiples 
of 40 instead of 32. 

Then, Rebuild/Rerun in vs2019 and the program will complete.

At this point I have illustrated, at least on my Windows VM, of where the main
problem is, is with the alignment. This happens with at least my Windows VM.

The test will fully not pass, however, as the last 4 will have the wrong values.
The failure is because of another problem, which I illustrate below, which is something
that we may want to rectify, but it appears that it will not screw things up badly.

### A smaller, but notable problem ####

Another problem is StringVal which is a generated class in Std.cs. We do not have this
problem on Linux/GCC as its analogous generated code is correct. On Windows, however, 
I find it wrong. On Windows the following code is generated:

        [StructLayout(LayoutKind.Explicit, Size = 32)]
        public unsafe partial struct __Internalc__N_std_S__String_val____N_std_S__Simple_types__C
        {
            [FieldOffset(0)]
            internal global::Std.StringVal.Bxty.__Internal _Bx;

            [FieldOffset(16)]
            internal ulong _Mysize;

            [FieldOffset(24)]
            internal ulong _Myres;
        }
        
 This code leads one to think that you may generate a string by use of
 these internal components, as such:
 
        global::Std.BasicString.__Internalc__N_std_N___cxx11_S_basic_string__C___N_std_S_char_traits__C___N_std_S_allocator__C __ret;
        (((int) __ret._Mypair._Myval2._Mysize) > 15 
             ? global::System.Text.Encoding.UTF8.GetString((byte*) __ret._Mypair._Myval2._Bx._Ptr, (int) __ret._Mypair._Myval2._Mysize) 
             : global::System.Text.Encoding.UTF8.GetString((byte*) __ret._Mypair._Myval2._Bx._Buf, (int) __ret._Mypair._Myval2._Mysize));

 However, the only way it actually works is to change the generated code altering the FieldOffsets to the following:
 
        [StructLayout(LayoutKind.Explicit, Size = 32)]
        public unsafe partial struct __Internalc__N_std_S__String_val____N_std_S__Simple_types__C
        {
          [FieldOffset(8)]
          internal global::Std.StringVal.Bxty.__Internal _Bx;
        
          [FieldOffset(24)]
          internal ulong _Mysize;
        
          [FieldOffset(0)]
          internal ulong _Myres;
        } 
        
To make that fix, you may run:

        rake fix_string_val
        
Be careful. You may only run this once after `rake gen`. 

After this fix is applied, then the last four cases of the Main Program.cs
will succeed.

## LINUX

The whole project seems to run flawlessly in linux. You need to have `mono`, `ruby`, `rake`, `cmake`, `gcc`, 
and `dotnet-sdk` installed, and optionally the Jetbrains Rider IDE. Once inside the `CppSharpTest` directory:

    rake run
    
This command should just generate, compile, and perform a `dotnet run`. 

## Contact Author ###

My name is Dr. Polar Humenn. If you know of any problems or anomalies with this test, or want to point out
what idiot thing I'm doing wrong, or misconception that I am harboring, 
please contact me via github or  at  _polar at syr dot edu_.