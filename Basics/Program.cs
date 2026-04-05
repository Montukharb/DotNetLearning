
namespace Learning.Basics
{
    class Program
    {
        public static void Main(string[] args)
        {
            decimal balance = 10.55m;
            Console.WriteLine(balance);
            //bool label = default;
            //Console.WriteLine(label);
            double myDouble = 9.78;
            int myInt = (int)myDouble;    // Manual casting: double to int

            Console.WriteLine(myDouble);   // Outputs 9.78
            Console.WriteLine(myInt);      // Outputs 9


            int myInt2 = 10;
            double myDouble2 = 5.25;
            bool myBool = true;

            Console.WriteLine(Convert.ToString(myInt2));    // convert int to string
            Console.WriteLine(Convert.ToDouble(myInt2));    // convert int to double
            Console.WriteLine(Convert.ToInt32(myDouble2));  // convert double to int
            Console.WriteLine(Convert.ToString(myBool));   // convert bool to string

            //string interpolation 


            string line = $$"""
                lorem ipsum dollar {{myInt}}
                string interpolation introduce in c# 6 {{myBool}}
                """;
            Console.WriteLine(line);


            // Full name
            string name = "John Don";

            // Location of the letter D
            int charPos = name.IndexOf("D");

            // Get last name
            string lastName = name.Substring(charPos);
            string middle = name.Substring(2, 4);
            Console.WriteLine(middle);
            // Print the result
            Console.WriteLine(lastName);
            Console.WriteLine(nameof(name));
            Test t1 = new Test();
        }
        static Program()  //run when class loaded in one time did't run when object is created any time.
        {
            Console.WriteLine("static constructor called");
        }

        public Program(int a, int b)
        {
            Console.WriteLine($"Sum = {a + b}");
        }
    }

    internal class Sample2
    {
        static Sample2()
        {
            Console.WriteLine("Run when class loaded one time");
        }
    }
}