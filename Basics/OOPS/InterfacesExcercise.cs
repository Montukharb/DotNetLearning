
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
    internal class InterfacesExcercise:Writer
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
}
