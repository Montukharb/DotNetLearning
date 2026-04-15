

namespace Basics.OOPS
{

    internal class Circle
    {
        internal int circleDiameter { get; set; }
        
         internal void Display(int inputvalue, int[] arr, Tester1 Nesobj)
        {
            circleDiameter = inputvalue;
            WriteLine("Circle Diameter " + circleDiameter);
            string res = Nesobj.NameGetter();
            WriteLine(res);
        }
    }




    public class Tester1
    {
        public Tester1() {

            WriteLine("Nested class constructor");
        }
        public string NameGetter()
        {
            return "Nested class Name Getter return value";
        }

    }

    internal class ObjectPasser
    {
       

        internal ObjectPasser()
        {
            Circle obj = new Circle();
            obj.Display(15, new int[] {10,20,3,0 }, new Tester1() );
        }
      
    }
}
