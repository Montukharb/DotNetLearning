//namespace Basics.OOPS.NameSpaceTesting;   //fileScope namespace
 
internal class NameSpaceTesting
{
  internal static string ink = "yes ink";
}

namespace Adf
{
   internal class A
    {
        private class Nested {
            internal class DoubleNested { }
        }
        protected class Nest { }


        internal A()
        {
            Nested obj = new Nested();

            Nested.DoubleNested ob = new Nested.DoubleNested();
        }

    }
}

namespace Testings
{
    class A {
       void met()
        {

        }

        void branchPerformace()
        {

        }

        public void calls()
        {
            branchPerformace();
            //met();
        }
    }

    //partial

}
