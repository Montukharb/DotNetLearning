using System;
using System.Collections.Generic;
using System.Text;

namespace Basics.OOPS
{
    internal class IEnumeratorClass
    {
        public List<int> list = new List<int>() { 10, 20, 30, 40 };

        //C# me IEnumerator ka use collection ko one by one iterate karne ke liya kiya jata hai
        //GetEnumerator() is a method of IEnumerator object jo collection ka enumerator return karta hai.
        //IEnumerator ka main method moveNext() ye tab tak move kare ga null value na mil zaye.

        public void EnumeratorMethod()
        {
            IEnumerator<int> en = list.GetEnumerator();

            WriteLine("Before move next default position of enumerator = " + en.Current);
            en.MoveNext();
            WriteLine("After MoveNext() = " + en.Current);
            en.MoveNext();
            WriteLine("After MoveNext() = " + en.Current);
            en.Reset(); //reset the ienumerator before first item -1 position return value 0;
            WriteLine("After reset ienumerator = " + en.Current);

            en.Dispose(); //releasing or resetting unmanaged resources.

            //using loop
            Write("Using loop MoveNext() = ");
            while(en.MoveNext())
            {
                Write(en.Current + " ");
            }
            WriteLine();
        }

    }
}

