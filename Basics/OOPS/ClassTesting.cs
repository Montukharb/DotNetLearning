
namespace Basics.OOPS
{
    public class ClassTesting : Object
    {
       public static void disp()
        {

            WriteLine("Disp function");
        }
    }

    internal class ABC : ClassTesting
    {
        
       public  ABC()
        {
            ClassTesting obj = new ClassTesting();

            //obj.disp();
            ClassTesting.disp();
        }


    }
}
