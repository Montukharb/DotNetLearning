

using System.Collections;

namespace Basics.OOPS
{
    //Generics allow you to write type-safe reusable code without specifying exact data types.

    internal class Gen<T>
    {
        internal T? Name { get; set; }   //T means Data type get from object

        internal T SetName(T value)
        {
            //Name = "Montu kharb"; //Generic can't allowed hardcode get value from parameter
            
            return Name = value;
        }        
        //internal void addItem(T item)
        //{
            //list.Add(item);
            //list.Add("fd");
        //}

        internal static T Greet(T msg)
        {
            return msg;
        }

    }


    //class level generics
    public class DataStore<T,T2>
    {
        public T[] arr = new T[10];
        
       
        public int count = 0;
        public void AddData(T data)
        {
            if(count != arr.Length)
            {
                arr[count++] = data;
            }
            else
            {
                WriteLine("Memory full");
            }
        }

        public void DisplayData()
        {
            foreach(T data in arr)
            {
                
                if(!(data != null && data.Equals(0)))   
                {
                Write($"{data}, ");
                }
            }
            WriteLine();
        }

    }

    // Advanced generics with constraints

    public class Technology<T> where T : class   //means T only class delegates array interface
    {
        public T? item { get; set; }

        public Technology(T value)
        {
           

            if (value != null)
            {
                item = value;
                WriteLine("data = "+item);
            }
        }
    }

    //new keyword constraints
    //requirement generic type passing class must have one parameterless constructor
    public class Factory<T> where T : InterfaceSomeone , new() //new() always last
    {
    
      public T CreateInstance()
        {
            var obj = new T();
            if(obj is School access)
            { 
            
                access.Display("Building Repair");
                
            }
            //its accessible becasue interface take gurantee this method always in School class
            obj.RestrictedArea("From Factory class CreateInstance method");
            return new T(); //return new object every time
        }
      

    }

    public interface InterfaceSomeone
    {
        void RestrictedArea(string callbyClassName);
    }

    public class School:InterfaceSomeone
    {
        public School() { } //constructor

        public void Display(string tender)
        {
            WriteLine($"current Target assigned = {tender}");
        }

        public void RestrictedArea(string callbyClassName)
        {
            WriteLine("This is restricted Area method declare in interface body in school call by = " + callbyClassName);
        }
    }

    internal class GenericsExcercise
    {
        internal GenericsExcercise()
        {
            Gen<string> genobj = new();
            genobj.SetName("Welcome to India Mr. Montu kharb");
            var res = Gen<string>.Greet("Hey morning");
            WriteLine(res);

            DataStore<int,string> dobj = new();
            dobj.AddData(10);
            dobj.AddData(20);
            dobj.AddData(30);
            dobj.AddData(40);
            dobj.AddData(50);
            dobj.AddData(60);
            dobj.DisplayData();

            //advance type generics class objects
            Technology<string> tecobj = new("Advance level item Generics");

            //factory object
            Factory<School> facOBj = new Factory<School>();

            var obj = facOBj.CreateInstance();
            WriteLine(obj.GetType().FullName);


        
        }

    }
}
