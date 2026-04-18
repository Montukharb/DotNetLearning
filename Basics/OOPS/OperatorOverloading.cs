
using Basics.OOPS;
using System.Runtime.Intrinsics.X86;

namespace Basics.OOPS
{

    /*
  Rules of operator overloading
  1. It is only applied classes, struct and record
  2. operator overloading perform by objects only
  3. Always Make function to perform any operator overloading and use operator keyword
  4. function are make only public static
  5. can't overloaded these operators in operator overloading Assignment operators += -= /= *= etc & Logical Operators && || !
  */
   
    public class Vector
    {
        public int x , y;


        //constructor
       

        //creating operator overloading function remember always make public static
        public static Vector operator + (Vector ob1 , Vector ob2)
        {
            Vector objNew = new Vector();
            objNew.x = ob1.x + ob2.x;
            objNew.y = ob1.y + ob2.y;

            return objNew;
        }

        //operator overloading multiply
        public static Vector operator * (Vector ob1 , Vector ob2)
        {
            Vector objNew = new Vector();
            objNew.x = ob1.x * ob2.x;
            objNew.y = ob1.y * ob2.y;

            return objNew;
        }


    }

    internal class OperatorOverloading
    {   
        internal OperatorOverloading()
        {
            Vector obj1 = new Vector();
            Vector obj2 = new Vector();

            obj1.x = 10;
            obj1.y = 20;

            obj2.x = 5;
            obj2.y = 5;
            //operator overloading Addition
            Vector obj3 = obj1 + obj2;
            WriteLine("obj3 x = "+obj3.x + "\nobj3 y = " + obj3.y);
           
            //operator overloading multiply
            Vector obj4 = obj1 * obj2;
            WriteLine("obj4 x = " + obj4.x + "\nobj4 y = " + obj4.y);

            //operator over calling
            operatorOver op = new();
        }

    }

   public class vector2
    {
        public int a1, b1;
       public vector2(int a1,int b1)
        {
            this.a1 = a1;
            this.b1 = b1;
        }
        public static vector2 operator +(vector2 ob ,vector2 ob2)
        {
            //var result1 = ob.a1 + ob2.b1;
            //var result2 = ob.a1 + ob2.b1;
            //return new vector2(result1, result2);
            vector2 objnew = new vector2(ob.a1 + ob2.a1 ,ob.b1 + ob2.b1);
            return objnew;
        }
    }
    public class operatorOver
    {
        public operatorOver()
    {
        vector2 obj2 = new vector2(30, 20);
        vector2 obj3 = new vector2(40, 120);
        vector2 obj4 = obj2 + obj3;
            WriteLine("obj4 a1 = " + obj4.a1 + "\nobj4 b1 = " + obj4.b1);
    }
    }
}


//for run this program create object in your program.cs file write
//OperatorOverloading opover = new OperatorOverloading();