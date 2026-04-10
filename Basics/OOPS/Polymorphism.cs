
namespace Basics.OOPS
{

    internal class Asset
    {
        internal string? name { get; set; }
    }

    //child class 1
    internal class child1:Asset
    {
        internal int stoc { get; set; }
    }

    //child class 2

    internal class child2:Asset
    {
      internal int employe { get; set; }
    }


    //polymorphism runtime
    internal class Base {
        internal virtual void displayHONK()
        {
            WriteLine("Base class method run tut tut");
        }
    }

    internal class Derived:Base
    {
        internal override void displayHONK()
        {
            WriteLine("Child class method run override by derived class");
        }
    }
   
   
     
    internal abstract class Abs
     {

        //internal abstract int aa; //we can't create abstract field in abstract class

        //abstract property
        internal abstract int Fuel { get; set; } //we can declare abstract property but can't set value here;

        //abstract method
        internal abstract void SignalerAbs(int a,int b,out int sum); //we can't create abstract method body;
        
        //normal method
        internal void Show()
        {
            WriteLine("Normal method created in abstract class called by child class");
        }

        //virtual method
        internal virtual void Dell() //there are no error if we not overriding virtual method which is created in abstract class
        {
            WriteLine("Abstract class virtual method");
        }


     }
    
    internal class AbsDerived : Abs
    {
        internal override int Fuel { get; set; } = 85; //set default value we can also set in constructor or object creation time in right side curly braces

        internal override void SignalerAbs(int a, int b, out int sum)
        {
            sum = a + b; //set out value and get where it sended;
        }
        //no error 
        internal override void Dell()
        {
            WriteLine("Fuel value = " + Fuel);
            base.Dell();  //run abstract class virtual method we used base keyword when create child class object and store his refernce its own Scope;
        }
    }

    //sealed class 
    //rule we can't inherid in child class its loacked;
    internal sealed class Jet { 
        
       internal void disp()
        {
            WriteLine("sealed display function can't inherited in another child class");
        }

    }

    //internal class JetDerived : Jet //can't derived sealed class in child throw err
    //{

    //}

    //sealed method = secure but less ass compare in sealed class for example;

    internal class RBI
    {
        internal virtual void BankRequirements()
        {
            WriteLine("Basic document and fund etc");
        }
    }

    internal class GovtBody:RBI
    {

        //another child class can't override further after this override but here is a one loop hole child class override by new keyword
        internal sealed override void BankRequirements() 
        {
            WriteLine("Basic document and fund override by GovtBody Officers");
        }
    }

    internal class LocalBank():GovtBody
    {

    //new keyword hide the method in run time only compiled access through downcasting safe method
        internal new void BankRequirements()
        {
            WriteLine("Basic document and fund override by localBank Managers and Ceo");
        }
    }


    internal static class Polymorphism
    {
        
        internal static void Display(Asset holdObj)
        {
            WriteLine(holdObj.name);
        }

        internal static void EnteryPoint()
        {
            //object child 1;
            child1 ch1 = new child1();
            ch1.name = "My Name is Child 1";
            ch1.stoc = 50;

            //display method call;
            Display(ch1);

            //second child object
            child2 ch2 = new child2();
            ch2.name = "My name is Child 2";
            ch2.employe = 150;

            Display(ch2);

            //child object store in parent reference;
            
            Asset as1 = ch1;
            WriteLine(as1.name);

            //downCasting notsafe
            child1 ch12 = (child1)as1;
            WriteLine(ch12.stoc);

            if (as1 is child1 ownChild)
            {
                WriteLine(ownChild.stoc);
            }
            else
            {
                WriteLine("As1 not child of chil1 failed safe casting");
            }

            //run time polymorphism;
            Base derobj = new Derived();

            derobj.displayHONK(); //output derived class method run predicted
            AbsDerived absObj = new AbsDerived();
            absObj.SignalerAbs(10, 20, out int sum);
            WriteLine("sum of two numbers = " + sum);

            absObj.Show();
            absObj.Dell();


            //sealed methods
            RBI rbi = new LocalBank();
            rbi.BankRequirements();
            
            if(rbi is LocalBank access)
            {
                access.BankRequirements(); //changed success
            }

        }

    }
}
