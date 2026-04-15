

namespace Basics.OOPS
{



    internal class IndexerAndEvents
    {
       public int[] arr = new int[3];
        public int this[int i] {
            get { return  arr[i]; }
            set { arr[i] = value; }

                 }

            
    }

}
