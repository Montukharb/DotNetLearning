using System;

namespace Basics.Test
{
  

    public class TestRunner
    {
        int a, b;

        //NameSpaceTesting n1 = new NameSpaceTesting();
        
        public TestRunner(int x, int y)
        {
            a = x;
            b = y;
            WriteLine(NameSpaceTesting.ink);
        }

        public  void tester()
        {
            Console.WriteLine("Hello, World!");

           

            Console.WriteLine(Environment.CurrentDirectory);
            Console.WriteLine(Environment.OSVersion);
            Console.WriteLine(Environment.MachineName);
            Console.WriteLine(Environment.UserDomainName);

            Console.WriteLine(typeof(TestRunner)?.Namespace ?? "Namespace not set");
            Console.WriteLine(typeof(int));

            var data = nameof(TestRunner);
            Console.WriteLine(data);

            #region normal Testing
            Console.WriteLine(typeof(string));
            Console.WriteLine(typeof(TestRunner));

            var t = typeof(TestRunner);
            Console.WriteLine(nameof(t.Name));
            #endregion

            Console.WriteLine("..................");
            Console.WriteLine(typeof(TestRunner));
            Console.WriteLine(nameof(TestRunner));

            uint @int = 21;
            Console.WriteLine(@int);

            int decimalNotation = 2_000_000;
            int binaryNotation = 0b_0001_1110_1000_0100_1000_0000;
            int hexadecimalNotation = 0x_001E_8480;

            Console.WriteLine($"decimal number = {decimalNotation:N0}");
            Console.WriteLine($"binary number = {binaryNotation:N0}");

            Console.WriteLine($"max int = {int.MaxValue:N0}");
            Console.WriteLine($"max uint = {uint.MaxValue:N0}");

            double a = 0.1;
            double b = 0.2;

            if (a + b == 0.3)
            {
                Console.WriteLine("Equal");
            }
            else
            {
                Console.WriteLine($"{a} + {b} != 0.3");
            }

            Console.WriteLine(sizeof(short));
            Console.WriteLine($"Size of ulong = {sizeof(ulong)}");

            unsafe
            {
                Console.WriteLine(sizeof(Half));
                Console.WriteLine(sizeof(UIntPtr));

                int x = 10;
                int* ptr = &x;

                IntPtr address = (IntPtr)ptr; //for giving address any pointer variable using predefine special IntPtr  keyword
                Console.WriteLine($"Address: {address}");
                Console.WriteLine($"Value: {*ptr}");

                int* ptr2 = &x;
                int** ptr3 = &ptr2;

                //n stand for native int means allocate variable memory size according to system specificatoin 32bit or 64bit etc
                Console.WriteLine($"Pointer: {(nuint)ptr2}");
                Console.WriteLine($"Double Pointer: {(nuint)ptr3}");

                int[] arr = { 10, 20, 30 };
                fixed (int* p = arr)
                {
                    Console.WriteLine($"Array address: {(nuint)p}");
                }
            }


            object pi = 3.14f;
            pi = "name";
            Console.WriteLine(pi);
            
        }
    }
}