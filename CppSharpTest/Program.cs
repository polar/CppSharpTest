using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;
using ALK;
using Microsoft.VisualBasic.CompilerServices;

namespace ALK
{
    partial class Test
    {
        public unsafe string ReturnStringXXX(int x)
        {
#if LINUX
            var __ret = new global::Std.BasicString.__Internalc__N_std_N___cxx11_S_basic_string__C___N_std_S_char_traits__C___N_std_S_allocator__C();
            __Internal.ReturnString(new IntPtr(&__ret), __Instance, x);
            return (((int) __ret._M_string_length) > 15 ? global::System.Text.Encoding.UTF8.GetString((byte*)__ret._M_dataplus._M_p, (int) __ret._M_string_length) : global::System.Text.Encoding.UTF8.GetString((byte*)__ret._M_local_buf, (int) __ret._M_string_length));
#else
            var __ret = new global::Std.BasicString.__Internalc__N_std_S_basic_string__C___N_std_S_char_traits__C___N_std_S_allocator__C();
            __Internal.ReturnString(__Instance, new IntPtr(&__ret), x);
            return (((int) __ret._Mypair._Myval2._Mysize) > 15 ? global::System.Text.Encoding.UTF8.GetString((byte*) __ret._Mypair._Myval2._Bx._Ptr, (int) __ret._Mypair._Myval2._Mysize) : global::System.Text.Encoding.UTF8.GetString((byte*) __ret._Mypair._Myval2._Bx._Buf, (int) __ret._Mypair._Myval2._Mysize));
#endif

        }
    }
}

namespace PolarCppSharpTest
{
    class Program
    {
        static unsafe int Main(string[] args)
        {
            Test test = Test.StaticTest;
            Console.WriteLine("************** C++ *************");
            test.Print();
            try
            {
                Console.WriteLine("************** C# *************");
                Console.WriteLine("******* Properties *******");
                Console.WriteLine("Test Addr = " + test.Addr);
                Console.WriteLine("Test.Item1: " + test.AItem1 + " % " + test.OItem1 + " '" + test.Item1 + "'");
                Console.WriteLine("Test.Item2: " + test.AItem2 + " % " + test.OItem2 + " '" + test.Item2 + "'");
                Console.WriteLine("Test.Item3: " + test.AItem3 + " % " + test.OItem3 + " '" + test.Item3 + "'");
                Console.WriteLine("Test.Item4: " + test.AItem4 + " % " + test.OItem4 + " '" + test.Item4 + "'");
                Console.WriteLine("******* Function Calls *******");
                Console.WriteLine("Test.returnString(1): " + test.ReturnString(1));
                Console.WriteLine("Test.returnString(2): " + test.ReturnString(2));
                Console.WriteLine("Test.returnString(3): " + test.ReturnString(3));
                Console.WriteLine("Test.returnString(4): " + test.ReturnString(4));
                Console.WriteLine("******* Function Calls using StringVal *******");
                Console.WriteLine("Test.returnStringXXX(1): " + test.ReturnStringXXX(1));
                Console.WriteLine("Test.returnStringXXX(2): " + test.ReturnStringXXX(2));
                Console.WriteLine("Test.returnStringXXX(3): " + test.ReturnStringXXX(3));
                Console.WriteLine("Test.returnStringXXX(4): " + test.ReturnStringXXX(4));
                Console.WriteLine("******* END TEST *******");
                Console.WriteLine("Test passes if 'item?' is returned from every item/returnString print");
                test.Print();
            }
            catch (Exception e)
            {
                Console.WriteLine("******* ERROR *******");
                Console.WriteLine(e);
                test.Print();
            }

            Console.WriteLine("******* DONE *******");
            return 0;
        }
    }
}