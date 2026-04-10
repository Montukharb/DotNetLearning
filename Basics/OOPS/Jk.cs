
namespace Basics.OOPS
{
    public class Jk
    {
        public Jk() : this("Vishal")
        {
            WriteLine("Base const.");
        }
        public Jk(string name)
        {
            WriteLine($"second const.{name}");
        }
        public Jk(int age, string fullname) : this()
        {
            WriteLine($"third const.{age}, {fullname}");
        }
    }
    public class Jk2 : Jk
    {
        public Jk2() : base(23, "Vishal sharma")
        {
            WriteLine("Derived const");
        }

    }
}
