
namespace Basics.OOPS
{
    internal class References
    {
        internal References()
        {
            Per obj = new Per();
            obj.customerName = "LoremIPsum";

            //Per obj2 = new Per();

            Per obj2 = obj; //reference 

            obj2.customerName = "Vishal";

            WriteLine($"Customer name = {obj.customerName}");

            Per pers = new Per();

            //pers.ArrayPer(new int[] {10,20,30,40,50});
            int[] arr = { 10, 2, 03, 0, 6, 0, 0, 0, 0, };
            pers.ArrayPer(arr);
            WriteLine("\n"+arr[2]);

        }

    }




    internal class Per
    {
        internal string? customerName;

        internal void ArrayPer(int[] arr)
        {
            for(int i = 0;i<arr.Length;i++)
            {
                Write(arr[i] + " ");
            }

            arr[2] = 102;
        }
    }
}
