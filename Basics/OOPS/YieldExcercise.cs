
using Adf;
using Microsoft.VisualBasic;
using System.Diagnostics.Metrics;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Basics.OOPS
{
    internal class YieldExcercise
    {
        //Ye ek value return karta hai without function ko completely khatam kiye. Function pause ho jata hai aur next iteration me wahi se continue hota hai.

        /*
                Ye normally inke sath use hota hai:
                
                IEnumerable
                IEnumerable<T>
                IEnumerator
                IEnumerator<T>
        
               Restriction of yield
               Not used with: try Catch block,method with ref,out,in, parameters,lambda expression  
         */
        //real life use large data processiong file reading line by line and infinte case etc
        public IEnumerable<int> HugeData()
        {
            for (int i = 0; i < 1000000; i++)
            {
                yield return i;
            }
        }

        //infinte sequence 
        public IEnumerable<int> Infinite()
        {
            int i = 1;

            while (true)
            {
                yield return i++;
            }
        }

        public IEnumerable<int> PauseFunction()
        {

            yield return 10;

            //operation
            yield return 20;

            yield break; //stoped the iterator immediate

            //yield return 30; //unreachable code
        }
        /* First iteration → 10
        Function pause
        Next iteration → 20
        Function end */


        public YieldExcercise()
        {
            foreach (int i in PauseFunction())
            {
                WriteLine("Yield value = " + i);
            }

            //hudge data and infinte call
            foreach(int i in HugeData())
            {
                if (i == 100) { return; }
                WriteLine("Huge data: " + i);
            }

            foreach (int i in Infinite())
            {
                if (i == 100) return;

                Write("infinte data: " + i + ", ");
            }
            WriteLine();
        }
    }
    internal class EnumeratorClass
    {
        //Enumerator is an object that moves through a collection one item at a time.

    }
}


//iEnumerator and yield