
using Basics.OOPS;
using System.Collections;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.Json;
using System.Dynamic;
using System.Text;

namespace Basics.OOPS
{


    internal class CollectionsExercise
    {
        internal CollectionsExercise()
        {
            //old style collections non Generic
            /*
              ye wo collections hai jo .NET ke purane versions me use hoti thi, aur ye type safe nahi hoti.
              ye collections object type ke data ko store karti hai, isliye aap kisi bhi type ka data store kar sakte hain, lekin jab aap data ko retrieve karte hain, to aapko usse correct type me cast karna padta hai, jo ki error prone ho sakta hai.
              Ye System.Collections namespace me milte hain
              1.ArrayList
              2.Hashtable
              3.Stack
              4.Queue
              5.SortedList
              6.BitArray
              7.HashSet
              8.LinkedList
              

             */
            OldStyleCollections();
        }

        internal void OldStyleCollections()
        {
            ArrayList arrayList = new ArrayList();

            arrayList.Add(10);
            arrayList.Add("Montu kharb");
            arrayList.Add(3.14);

            foreach (var item in arrayList)
            {
                WriteLine(item);
            }

            //-------------------------------
            arrayList.Remove(10);
            WriteLine("After Operations");
            arrayList.Add(100);
            //arrayList.Sort(); // This will throw an exception because the list contains different types of data
            int i = arrayList.IndexOf("Montu kharb");  //if not found return -1;
            WriteLine("indexof = " + i);

            //arrayList.Insert(2,new string[] {"Rahul Gandhi","Putin" ,"Xi-jimping","vote Chor"});
            arrayList.Add(1000);
            arrayList.Add(100);
            int size = arrayList.Count; //size of arraylist

            arrayList.RemoveAt(size - 1);  //remove last item or removeAt index base delete karta hai
            bool contains = arrayList.Contains(3.14);
            WriteLine("Contains 3.14 = " + contains);

            foreach (var item in arrayList)
            {
                WriteLine(item);
            }

            //unboxing and type casting

            bool isInt = int.TryParse(arrayList[arrayList.Count - 1]?.ToString(), out int data);
            if (isInt)
            {
                WriteLine("Data is integer = " + data);

            }
            else
            {
                WriteLine("Data is not integer");

            }

            arrayList.Clear(); //clear all items from arraylist

            //combine two array list
            ArrayList one = new ArrayList() { 1, 2, 3, 4, "one", "Mango", "orange" };
            ArrayList two = new ArrayList() { 45.6f, 87, "Yellow", "Pink" };

            ArrayList combine = new ArrayList() { "user", "emp", 420 };
            combine.AddRange(one);
            combine.AddRange(two);
            WriteLine("Big Array List");

            foreach (var item in combine)
            {
                Write(item + " ");
            }
            WriteLine();

            /*
             Hash table eak non generic collection hai jo key-value pairs ko store karta hai. Isme aap kisi bhi type ke key aur value ko store kar sakte hain, lekin jab aap data ko retrieve karte hain, to aapko usse correct type me cast karna padta hai, jo ki error prone ho sakta hai.
             */

            void HashTableCollection()
            {
                //Hashtable hashTable = new Hashtable() { {"Name","Montu"},{"age",23 }};
                Hashtable hashTable = new Hashtable();
                hashTable.Add("Name", "Montu");
                hashTable.Add("age", 23);
                hashTable.Add("city", "Sonipat");

                hashTable.Add("email", "montukharb68@gmail.com");

                bool containKey = hashTable.Contains(key: "Name");
                WriteLine("Checking key using only contain = " + containKey);

                WriteLine("ContainsValue montukharb68@gmai.com = " + hashTable.ContainsValue("montukharb68@gmail.com"));

                //accessing Alldata from hashtable

                //bool containKey = ha
                //
                //shTable.Contains(key: "Name");




                foreach (DictionaryEntry entry in hashTable)
                {
                    WriteLine($"Key = {entry.Key} , Value = {entry.Value}");
                }
                WriteLine("Direct Value");
                foreach (var entry in hashTable) // direct value 
                {
                    WriteLine(entry);
                }

                //unboxing single data
                string? name = hashTable.ContainsKey("Name") ? hashTable["Name"]?.ToString() : "key not found";
                WriteLine("Name = " + name);



                //hashTable Arraylist ke jaise addRange nahi hoti isko indirect loop se add karte hai for example

                Hashtable hashTable2 = new Hashtable() { { "skills", new string[] { "html", "css", "etc" } }, { "phoneNumber", "617920010" } };

                //combine two hashtable using loop
                foreach (DictionaryEntry entry in hashTable2)
                {
                    hashTable.Add(entry.Key, entry.Value);
                }

                WriteLine("After combine hashtable");
                foreach (DictionaryEntry entry in hashTable)
                {
                    WriteLine($"Key = {entry.Key} , Value = {entry.Value}");

                }
                hashTable.Clear();//clear hash table;
            }
            HashTableCollection();

            /*
             stack eak linear collection Which is worked lifo concept last in first out
             stack empty still use pop and peek method throw error 
             */
            void StackCollection()
            {
                Stack stack = new Stack(new int[] { 10, 20, 30, 40, 50 });
                stack.Push("Filling");

                stack.Push("Station");
                if (stack.Count > 0)
                {
                    WriteLine("Deleted item = " + stack.Pop());
                    WriteLine("Peek item of stack = " + stack.Peek());

                }

                foreach (var item in stack)
                {
                    WriteLine(item + " ");
                }

                //delete all stack
                stack.Clear();
            }
            StackCollection();

            /*
             queue eak linear data structure hai rear se enter front se exit 
            enqueue or dequeue work on FiFo Principle.

             */

            void queueCollection()
            {
                Queue que = new Queue(new int[] {103,2049,250,030,89,54});
                que.Enqueue(2);
                var quearr = que.ToArray();
                //quearr.Sort();
                //quearr.Reverse();
                WriteLine("Queue Collection");
                foreach(var queitem in que)
                {
                    Write(queitem + " ");
                }
                WriteLine();
                if(que.Count >0)
                {
                    WriteLine("Peep item of que = " + que.Peek());
                    WriteLine("Enqueue item of que = " + que.Dequeue());
                }

                que.Clear(); //complete que deleted;
            }
            queueCollection();

            /*

            🔷 1. SortedList kya hota hai?

               👉 SortedList ek key-value collection hai jo automatically keys ko sorted order me store karta hai

               👉 Matlab:

               Hashtable jaisa (key-value) ✔️
               Lekin sorted order maintain karta hai ✔️
               index search allow karta hai internally array use karta hai inserting sligthly slow reason array shifting karta hai 
             */
            void SortedListColl()
            {
                SortedList sortedlist = new SortedList() { {"Name","Montu" },{"name","montu"},{"age",23 }};

                WriteLine("sorted list collection");
                //sortedlist.Add("email", "Montukharb868@gmail.com");
                foreach(DictionaryEntry item in sortedlist)
                {
                    Write(item.Key + ":" + item.Value);
                }


            }
            SortedListColl();
        }
    }
}




/*

🧱 1.Collections in .NET(Data store karne ke tools)

👉 Collections ka matlab:

Multiple data items ko store + manage karna

Jaise array hota hai, but collections zyada powerful aur flexible hote hain.

📦 Main Collection Types
1. System.Collections (Non-Generic ❌)

👉 Old style collections

ArrayList
Hashtable

❌ Problem:

Type safety nahi (object store karta hai)
Performance slow

👉 Example:

ArrayList list = new ArrayList();
list.Add(10);
list.Add("Hello"); // allowed (problem!)
2.System.Collections.Generic(Best ✔️)

👉 MOST IMPORTANT(Exam + Interview ⭐)

List<T>
Dictionary<TKey, TValue>
Queue<T>
Stack < T >

✔️ Advantages:

Type safe
Fast
Modern

👉 Example:

List<int> numbers = new List<int>();
numbers.Add(10);
numbers.Add(20);

3.System.Collections.Concurrent(Thread - safe 🔒)

👉 Multi - threading me use hota hai

ConcurrentDictionary
ConcurrentQueue

✔️ Jab multiple threads ek hi data access kare

4. System.Collections.Immutable (Read-only 🔐)

👉 Data change nahi hota (safe)

ImmutableList

✔️ Functional programming me useful

5. System.Collections.Frozen (High Performance ⚡)

👉 Read-only + super fast

✔️ Jab data change nahi hota but speed chahiye

6. System.Collections.ObjectModel

👉 Custom collection banane ke liye

7. System.Collections.Specialized

👉 Specialized collections

StringCollection
NameValueCollection
    */