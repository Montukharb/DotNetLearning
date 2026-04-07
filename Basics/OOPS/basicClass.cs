
using System.Reflection.Emit;

namespace Basics.OOPS
{
    internal class BasicClass
    {
        //field
        internal string name = "Vishal Sharma";
        internal float amount;
        internal DateTime date;

        internal float newAmount;
        internal DateTime newDate;

        //property get and set method;
        internal int age
        {
            get; set
            {

                bool label = true;
                if (label)
                {
                    field = value;
                }

            }
        } = 10;

        #region All types Constructor Demo

        //static constructor run one time when class is loaded

        static BasicClass()
        {
            WriteLine("static Contructor");
        }

        //Normal Constructor
        internal BasicClass()
        {
            WriteLine("Normal Constructor run");
           
        }
       
        //parameterised constructor
        internal BasicClass(string LastName)
        {
            WriteLine("Last name " + LastName);
        }

        //overloading constructor;

        internal BasicClass(float amount,DateTime date)
        {
            this.amount = amount;
            this.date = date;
            WriteLine("Amount = " + amount + "DATE = " + date);
        
        }

        //copyConstructor;
        internal BasicClass(BasicClass objCopy)
        {
            WriteLine(  "Amount = "+objCopy.amount + "Date = " + objCopy.date);
            newAmount = objCopy.amount;
            newDate = objCopy.date;

            WriteLine("After copy constructor set new value");
            WriteLine($"new Name = {newAmount}\nnew Date = {newDate}");
            WriteLine(sizeof(short));

            //normal function call by value
            short first = 11;
            short last = 12;
           


            void swap(short first , short last)
            {
                short temp = first;
                first = last;
                last = temp;

                WriteLine($"first = {first} and last = {last}");
            }
            swap(first, last);
            WriteLine($"first = {first} and last = {last}");

            //swapping using reference;

            short AD = 1990;
            short BC = 2000;

            void SwapUsingRef(ref short AD, ref short BC)
            {
                short temp = AD;
                AD = BC;
                BC = temp;
                WriteLine($"AD = {AD} and BC= {BC}");

            }
            SwapUsingRef(ref AD, ref BC);
            WriteLine($"AD = {AD} and BC= {BC}");

        }










        #endregion











        internal void DisplayUserDetails()
        {
            
            WriteLine($"user details name = {name}\nage = {age}");
            Setup obj = new();
            obj.disp();
        }
    }


    internal class Setup {
     
       internal void disp()
        {
            WriteLine("hello this method from setup class");

        }
    
    
    }
 
}
