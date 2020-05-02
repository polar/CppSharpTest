using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;
using ALK;

namespace PolarCppSharpTest
{
    class Program
    {
        static unsafe void Main(string[] args)
        {
            Test test = Test.StaticTest;
            Console.WriteLine("************** C++ *************");
            test.Print();
            Console.WriteLine("************** C# *************");
            Console.WriteLine("Test Addr = " + test.Addr);
            Console.WriteLine("Test.Item1: " + test.AItem1 + " % " + test.OItem1 + " '" + test.Item1 + "'");
            test.Print();
            Console.WriteLine("Test.Item2: " + test.AItem2 + " % " + test.OItem2 + " '" + test.Item2 + "'");
            test.Print();
            Console.WriteLine("Test.Item3: " + test.AItem3 + " % " + test.OItem3 + " '" + test.Item3 + "'");
            test.Print();
            Console.WriteLine("Test.Item4: " + test.AItem4 + " % " + test.OItem4 + " '" + test.Item4 + "'");
            test.Print();
            Console.WriteLine("Test.returnString(1): " + test.ReturnString(1));
            test.Print();
            Console.WriteLine("Test.returnString(1): " + test.ReturnString(2));
            test.Print();
            Console.WriteLine("Test.returnString(1): " + test.ReturnString(3));
            test.Print();
            Console.WriteLine("Test.returnString(1): " + test.ReturnString(4));
            test.Print();
        }
    }
}