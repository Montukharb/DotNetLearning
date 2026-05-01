
using System.Formats.Tar;

namespace Basics.OOPS
{

    /*
     Rules 
    definition = In C#, an interface is a reference type that defines a contract for what a class or 
    struct must do. It specifies a set of related functionalities—such as methods, properties, events, 
    or indexers—without providing the actual implementation
    interface by default namespace ka ander internal hota hai class ka ander private default case me    
    interface me only property and method hote hai no field
    or property ma value set nahi kar sakte,
    all property and method by default abstract hote hai
    normal method bhi bana sakte hai with body lakin we abstrace nahi hote
    interface ma private public protected method create kar sakte hai new update after version 8.0 C#
    we can inherit another interface in interface
     */

    internal interface Writer
    {

        //public string? writerName { get; set; } = "writer"; //error
        public string writerName { get; set; }

        public void PathFinder()
        {
            WriteLine("interface normal method with body accepted");
            HelperMethod(); //this is helper method access only here
        }

        private void HelperMethod()
        {
            WriteLine("This is helper method called by with in interface scope any mehtod");
        }
        public void CreateJob(string jobType);
    }
    internal class InterfacesExcercise : Writer
    {
        public string writerName { get; set; } = "Bhagat Singh";
        public void CreateJob(string? jobType = "Software Developer")
        {
            WriteLine("Job Created = " + jobType);
            WriteLine("writer Name " + writerName);
            //interface body method accessed  Object ko Writer interface mein cast karein
            ((Writer)this).PathFinder();
            ((Writer)this).writerName = "Writer Name set Gandhi";
            WriteLine(((Writer)this).writerName);
        }
    }


    public class GBase
    {
        public GBase()
        {
            WriteLine("Gbase constructor run");
        }
        public GBase(string name) : this()
        {

            WriteLine("Gbase parameter constructor run " + name);
        }
    }
    public class GChild : GBase
    {
        public GChild() : base("Montu kharb")
        {
            WriteLine("Gchild constructor run");
        }
        public GChild(string name) : this()
        {

            WriteLine("Gchild parameter constructor run " + name);
        }

    }

    //--------------------------------------------------//
    interface SimpleNoteBOOk
    {
        protected string Note { get; set; }  //abstrace property override by only child
    }


    public class TesterDEV
    {
        public TesterDEV()
        {
            var obj = (SimpleNoteBOOk)this;
            //obj.Note = "hello";  //can't access due to security level protected
        }
    }




    interface Spiral : SimpleNoteBOOk
    {
        //property metod by default are public;

        void PageQuality(string Gsm); //by default public and abstract;
        void Stock()  //normal method public without abstract afeter c# v 8.0
        {
            WriteLine("there are only 500 stocks");
            SetInstruction();
        }

        //after C# v 8.0 We can set method access specifier internal,private,protected
        private void SetInstruction() //we can't access this method outside the interface 
        {
            WriteLine("Private method set basic instruction");
        }

        //can't declare fields
        //int ads = 45;
        public string ads { get; set; }

    }

    internal class Books : Spiral
    {

        public string Note { get; set; } = "A4 NoteBOok";
        public void PageQuality(string Gsm)
        {
            WriteLine("Page quality = " + Gsm);
            WriteLine(Note);
            var obj = (Spiral)this;

            obj.Stock();
            ((Spiral)this).Stock();
            WriteLine(ads);
            //WriteLine(obj.ads = "Double positive");
        }


        public string ads { get; set; } = "Positive";
    }


    //---------------expilicite interface 
    //isme problem ye hai ki next child class ayee to stone se undo method access
    //and override nahi kar sakti kyu ki ye explicitely defined or na hi virtual method hai undo
    internal interface Iundoable
    {
        public void undo();

        public void accessAbleMethod();
    }


    internal class Stone : Iundoable
    {
        void Iundoable.undo()
        {
            WriteLine("i undoable interface mehthod implemented using explicit style");
            //explicit method can't access using stone class object 
            //access only casting method;
        }


        //interface method ko normal(implicit) tarike se implement karte ho,
        //to wo method default me sealed (override nahi ho sakta) hota hai isliya virtual bana deta hai
        //implementation wali class me
        public virtual void accessAbleMethod()
        {
            WriteLine("Virtual method declare in interface implemented in stone base class");
        }
    }
    /*
    🔥 Reimplementation kya hota hai?

Child class dobara interface implement kare:

public class RichTextBox : TextBox, IUndoable
    {
        void IUndoable.Undo()
        {
            Console.WriteLine("RichTextBox Undo");
        }
    }

👉 Ye override nahi hai
👉 Ye new implementation hai(reimplementation)   */

    internal class DevRun : Stone
    {
        internal DevRun()
        {
            Stone s1 = new Stone();
            // s1.undo();// can't access using object a explicite implementation interface method
            //using this

            //accessed 
            ((Iundoable)s1).undo();  //same work
                                     //s1.accessAbleMethod(); //run virtual method


        }
        public override void accessAbleMethod()
        {
            WriteLine("virtual method override by child class");
        }
    }
}
