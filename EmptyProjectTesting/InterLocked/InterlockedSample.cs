namespace EmptyProjectTesting.InterLocked
{
    public class InterlockedSample
    {
        static long count = 10;
        //Example of all Interlocked class methods
        public static void InterLockedClassMethods()
        {
            Interlocked.Increment(ref count); //Increment the value of count
            Console.WriteLine(count);

            Interlocked.Decrement(ref count); //Decrement the value of count
            Console.WriteLine(count);

            Interlocked.Exchange(ref count, 100L); //Exchange the old value of count with 100L new value and return the old value
            Console.WriteLine(count);

            Interlocked.CompareExchange(ref count, 100L, 10L); //Compare the old value of count with 100L new value compare with 10L
            Console.WriteLine(count);

            long value = Interlocked.Read(ref count); //Read the value of count safely automic read
            Console.WriteLine(value);

            Interlocked.Add(ref count, 1200L);//Add the value of count with new value

        }
    }
}
