
using Basics.OOPS;
using System.Drawing;
using System.Dynamic;
namespace Basics.OOPS
{
  internal partial  class PartialExercise
    {
       internal void Truck()
        {
            WriteLine("Truck is running");
        }
        internal partial void Car2()
        {
            WriteLine("Car2 is running and it is partial method");
        }
    }
}
namespace Learning.Basics
{
    public class Fruit
    {
       public Fruit()
        {
            WriteLine("Default constructor");
            Secret();
        }

        public void DisplayFruit() { 
            WriteLine("Learning Basics public Fruit class method displayfruit");

        }

        private void Secret()
        {
            WriteLine("Secret method");
        }

        protected internal void FriendSecret()  //full project access required inheride class
        {
            WriteLine("Protected method");
        }

        private protected static void SmallPrivate()
        {
            WriteLine("private protected static method");
        }
    }

    



    class Program : Fruit
    {
        public void disp()
        {
            SmallPrivate();
            
        }
        public static void Main(string[] args)
        {
            Fruit f = new Fruit();
            f.FriendSecret();
            SmallPrivate();     
            //create object Testing class
            A ok = new A();
            //ok.met(); //due to private access modifier did't called

            //create object operatio class
            Operation objOp = new Operation();
           


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

            user = new ExpandoObject();

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
            string? input = ReadLine();
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

            //int result = internalobj.sum(out int a, out int b);

            internalobj.sum(out int a, out int b);
            Console.WriteLine("a = "+a + " b = " + b);
            //Console.WriteLine("Return value = " + result);

            WriteLine(internalobj.sum2(10, 20,out string userName, out int age));
            WriteLine($"user name = {userName} \nUser age = {age}");

            Type datatype = typeof(int);
            WriteLine("data type check = " + datatype);
            Type typ = internalobj.GetType();
            WriteLine(typ);

            int? numb = null;
            string userName3 = "vishal";
            var str = userName3 ?? "sample";
            WriteLine(str);


            string? myname = null;
            int? pow = null;
            WriteLine(pow);

            

            WriteLine(myname);



            Program.count();
            int left = 3;
            WriteLine(3 << left);


            //creating oops object here
            BasicClass obj = new BasicClass();
            BasicClass obj1 = new BasicClass(200.5f, new DateTime());
            BasicClass parmetrised = new BasicClass(obj1);
            obj.DisplayUserDetails();
            References refer = new References();
            Stenrec sten = new Stenrec();
            Jk2 jkObj = new Jk2();

            //deconstructor class object
            DeConstructor deObj = new() {address="rohtak Abc", customerName="Vishal BOB", customerId=141413};
            
            var(cID,CName) = deObj;
            //WriteLine(deObj.customerId);

            WriteLine(cID + CName);

            Polymorphism.EnteryPoint();
            RecordClassAndEnum obRec = new();
            obRec.Show();


            //interface objects
            InterfacesExcercise infex = new();
            infex.CreateJob();



            //abstract class objects

            Working wkobj = new();

            wkobj.MyDisp();
            wkobj.display();
            wkobj.Display2();
            wkobj.GeneralInstruction();


            GChild Gchi = new("Jat boy");
            Books bk = new Books();
            bk.PageQuality("95 - GSM");
           
            DevRun dR = new DevRun();
            //dR.accessAbleMethod();

            //((Iundoable)dR).accessAbleMethod();
            ((Stone)dR).accessAbleMethod();


            //Generics
            GenericsExcercise Gen = new GenericsExcercise();

            //Delegates Excercise object
            DelegatesExcercise deobg = new DelegatesExcercise();


            //yieldExcercise
            YieldExcercise y1 = new();
            //indexer and event object 

            IndexerAndEvents IAE = new();
            IAE[0] = 150;
            IAE[1] = 2000;
            IAE[2] = 3000;

            WriteLine(IAE[0]); 
            WriteLine(IAE[1]);
            WriteLine(IAE[2]);

            PartialExercise PE = new();
            PE.Car();
            PE.Truck();
            PE.Car2();
            PE.LabmdaMethodOP();
            Test t = new Test();
            t.OnRun +=()=> WriteLine("task1");
            t.OnRun += () => WriteLine("task2");
            t.Run();

            //circlepasser object
            ObjectPasser op = new();



        }
        static void count()
        {
            int i = 1;
            int j = 1;
            for (int ii = 0; ii < 10; ii++)
            {
                i = +i;
                j += j;
            }

            WriteLine(i + " and " + j);
            WriteLine($"{3 << 3:B8}");
            object og = "testing";
            string? ogs = og as string;
            WriteLine(ogs?.Length);


            int? ff = 10;

            if(ff is int)
            {
                WriteLine("true integer");
            }
            else
            {
                WriteLine("not integer");
            }

            int number = Random.Shared.Next(minValue: 1, maxValue: 7);
            switch (number)
            {
                case 1:
                    WriteLine("One");
                    break; // Jumps to end of switch statement.
                case 2:
                    WriteLine("Two");
                    goto case 1;
                case 3: // Multiple case section.
                case 4:
                    WriteLine("Three or four");
                    goto case 1;
                case 5:
                    goto v_label;
                default:
                    WriteLine("Default");
                    break;
            }
 

        v_label:

            WriteLine("After label jump1");

            WriteLine("After label jump1");

        
        }

        static void logical()
        {
            bool a = true;
            bool b = false;

            bool c = a && b;
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

       public void sum(out int a, out int b)
        {
            a = b = 10;
            a = a + b;
            b = b + a;
            //return b;
        }



// normal method
     public int sum2(int a , int b , out string name,out int age)
        {
            name = "jOhn";
            age = 11;
            
            return a+b;
            
        }
    }



}