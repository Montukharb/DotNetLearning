
using System.Drawing;
using System.Dynamic;

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
            TestRunner t1 = new TestRunner(10, 20);
            t1.tester();

            dynamic user = "something data";
            user = 1234;
            Console.WriteLine(user);

            user = new  ExpandoObject();

            user.FirstName = "John";
            user.LastName = "Don";
            user.age = 40;

            Console.WriteLine($"First name = {user.FirstName} \nLastName = {user.LastName}\nAge = {user.age}");
            DateTime date = DateTime.Now;
            Console.WriteLine(date);
            date = new(1999,1,24);

            Console.WriteLine(date);
            Point d = new(10,20);

            Console.WriteLine(d);

            //string inp = TryParse(Console.ReadLine());
            Write("Enter number: ");
            string input = ReadLine();
            int num;
            bool success = int.TryParse(input, out num);

            if(success)
            {
                WriteLine($"no : {num}");
            }
            else
            {
                WriteLine($"wrong input expected numberic value your input = {input}");
            }

            var internalobj = new Sample2(100){ userName = "montu kharb" };
        }


      
    }

    internal class Sample2
    {
        void GetValue(out int x)
        {
            x = 10; // compulsory assign
        }

      
        public Sample2(int a)
        {
            int num = 20;
            GetValue(out num);

            Console.WriteLine(num); // 10
            WriteLine($"Constructor value = {a}");
        }
        public required string userName{ get; set; }
        static Sample2()
        {
            Console.WriteLine("Run when class loaded one time");
        }


    }
}