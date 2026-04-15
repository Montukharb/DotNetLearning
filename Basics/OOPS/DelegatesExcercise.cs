
namespace Basics.OOPS
{

    internal class DelegatesExcercise
    {
        internal int Square(int val) => val * val;
        internal int qube(int val) => val * val * val;


       
        //A Delegate is an object that points to a method and call it
        //create delegates type
        //accessSpecifier keyword return-type, delegateName , accept only one parameter in any method;
        
        delegate int Transformer(int x);

         Transformer d;
        
        delegate int Transformer2();
        Transformer2 d2;
        
        internal int FirstDisp()
        {
            WriteLine("First Disp");
            return 1;
        }
        internal int SecondDisp()
        {
            WriteLine("Second Disp");
            return 2;
        }

        

        internal DelegatesExcercise() {
            WriteLine("Delegates run");
            d = Square;

            WriteLine("square called by delegate loose coupling = " + d(2));
            d = qube;
            WriteLine("Qube called by delegate loose coupling = " + d(3));

            //also we can assign multiple method in delegates
            //Note: Rull all methods but return value last method assigned;

            d2 = FirstDisp;
            d2 += SecondDisp;
            //d2 += FirstDisp;

            
            int res =  d2();
            WriteLine("Multiple called but return last assigned method return value = " + res);

            //lambda function with delecates
            d = x => x * x;
            WriteLine("Lambda function with delegates example = " + d(12));

            //delegates , Func, Action ke sath hi lambda function use hoga;
            //Func,Action inbuild delegates hai C sharp ke;
            //Func Example
            Func<int, float, float> Calculation = (x1, x2) => x1 + x2;
            WriteLine(Calculation(10,20.5f));

            //Action example action does not return any value
            Action<int, float> calculation1 = (x1, x2) => WriteLine(x1 + " .... " + x2);
            Action<int, float> calculation2 = (x1, x2) =>
            {
                WriteLine("Action Delegate with body example");
                WriteLine(x1 + " .... " + x2);
            };

            var labdafunc = (int x1, int x2) => { return x1 + x2; };
            calculation1(100,200.5f);
            calculation2(456,256.2f);

            //lambda with static
            Func<int,int> Mod = static x => (int)x % mod; 
            Func<int,int,int> Mod2 = static(x, y) => ((int)(x % mod ) + (y % mod));

            WriteLine($"MODULES with static parameter lambda function = {Mod(45)}");
            WriteLine($"MODULES with static parameter lambda function = {Mod2(65,98)}");

            //anonymous method always using with delegates
            d = delegate (int x)
            {
                return x * x;
            };

            WriteLine("Anonymous method using with delegates = " + d(20));

            //Anonymous types = eak temporary class jiska name nahi hota
            var temp = new { FirstRound = "10 shot", SecondRound = "5 shot", ThirdRound = "No energy" };

            WriteLine(temp.FirstRound);

            var temparr = new[]
            {
                new{Name = "John Don", Age = 23, Address = "vasant vihar A-Block Villa no-108 opp:Samsung Company"},
                new{Name = "Dr. Dhum ketu",Age = 100, Address = "Old Kutiya Behind the Dolakpur Killa"}
            };

            foreach(var x in temparr)
            {
                Write($"Name = {x.Name}\nAge = {x.Age}\nAddress = {x.Address}");
            }
            WriteLine();

            //tuple
            var person = ("Bob", 23);
            var (Name, age) = person;

            WriteLine(Name + " " + age);

            var nums = new List<int> { 1, 2, 3, 4, 5 };

            var result = nums.Where(x => x > 3);
            foreach(var x in result)
            {
                WriteLine(x);
            }
        }
        static int mod = 10;
           
        
    }
}
