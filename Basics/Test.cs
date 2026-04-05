namespace Basics.Test
{

    public class Test
    {
        public Test()
        {
            Program p1 = new Program(10, 20);
            int num = 10;
            //num = "dd"; strongley typed 
            var name = "my name lorem";
            //name = 45; //strongley typed + auto check data type and assigned

            dynamic multitelented = null;
            multitelented = 4564;
            multitelented = "this is my string";

            Console.WriteLine("dynmaic data type = "+multitelented);

        }
    };


}