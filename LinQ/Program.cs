

namespace LinQ
{
public class Program
{
        public string name { get; set; }
    public static void Main(string[] args){
        Console.WriteLine("Hello, World!");
var objTest = new Test();
            Console.WriteLine(Environment.CurrentDirectory);
            Console.WriteLine(Environment.OSVersion);
            Console.WriteLine(Environment.MachineName);
            Console.WriteLine(Environment.UserDomainName);
            int d = 10;
            Console.WriteLine(typeof(Program)?.Namespace ?? "Namespace not set");
            Console.WriteLine(typeof(int));
            var data = nameof(Program);
            Console.WriteLine(data);


            Console.WriteLine(typeof(string)); // System.String
            Console.WriteLine(typeof(Program));

            var t = typeof(Program);
            
            Console.WriteLine(nameof(t.Name)); // "String"..

            Console.WriteLine("..................");
            Console.WriteLine(typeof(Program)); // Type info
            Console.WriteLine(nameof(Program)); // "Student"

            Console.WriteLine(nameof(Program.name)); // "Name".WriteLine()

        }
    }

}

 public class PubTest{
    private PubTest() { }
}
internal class Test
{
    private class Secret
    {

    }
    static void Demo()
    {
        //PubTest obj = new PubTest();
    }

}