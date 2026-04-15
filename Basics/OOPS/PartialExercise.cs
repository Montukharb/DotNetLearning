
using System.Linq.Expressions;

namespace Basics.OOPS
{
    internal class Test
    {
        public event Action? OnRun;
        public void Run() {  OnRun?.Invoke(); }
    }


    internal class Ecommerce
    {
        internal string? applicationName { get; set; }
        internal int? OpeningYear { get; set; }
        internal string? Address { get; set; }


       
        internal void AddItem(Ecommerce? action)
        {
            WriteLine("item added = " + action?.applicationName);
        }

        internal void DisplayData(Ecommerce? action)
        {
            WriteLine($"Application name = {action?.applicationName} - Opening year = {action?.OpeningYear} - Address = {action?.Address}");
        }

    }

    internal partial class PartialExercise
    {

        internal void LabmdaMethodOP()
        {
            Ecommerce obj = new(); //create object normal
            
            Action<Ecommerce?>? actionDelegate = null; //create inbuild delegate function non returnable value only execute
            
            
            actionDelegate += obj.AddItem; //subscribe method
            actionDelegate += obj.DisplayData; //----------

            //invoke method = There are two types to invoke method normal or using Invoke method;
            //1st 
            actionDelegate(new Ecommerce { applicationName = "Vishal Enterprises", OpeningYear = 2026, Address = "Narela Deli 40 ABC BLOCK"});

            //second method using Invoke method
            actionDelegate?.Invoke(new Ecommerce(){ applicationName = "Vishal Washing Station", OpeningYear = 2027, Address = "Old polic station new delhi 40 ABC BLOCK" });
       
            
            var lambda = (int x) =>
        {
            WriteLine("Lambda function running = " + x * x);
        };

            var lambda2 = (int y) => { WriteLine("lambda function 2 run = " + y * 3); };
            Action<int>? trigger = null;
            trigger += lambda;
            trigger+= lambda2;

            //trigger(10); same work
            trigger?.Invoke(12);

        }

    
       internal void Car()
        {
            WriteLine("Car is running ");

        }
        internal partial void Car2();

    }
}
