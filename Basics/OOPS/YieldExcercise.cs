
namespace Basics.OOPS
{
    internal class YieldExcercise
    {

        internal IEnumerable<int> PauseFunction()
        {
            yield return 10;

        //operation
            yield return 20;
        }


        internal YieldExcercise()
        {
            foreach (int i in PauseFunction())
            {
                WriteLine("Yield value = " + i);
            }
        }
    }
}
