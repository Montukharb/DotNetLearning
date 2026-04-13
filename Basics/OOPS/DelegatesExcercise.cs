
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

            calculation1(100,200.5f);
            calculation2(456,256.2f);
            
        }
           
        
    }
}
