
using Basics.OOPS;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
              5.SortedList -> hashtable ka sorted version as a key sorting
              6.BitArray
              7.HashCode
              

             */
            OldStyleCollections();
            //Generic Collections
            GenericCollections();
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

            //arrayList.Insert(2,"vote Chor");
            arrayList.Add(1000);
            //arrayList.InsertRange(2, new string[] { "Rahul Gandhi", "Putin", "Xi-jimping", "vote Chor" }); //index 2 se start hoke array ke sare items insert karega
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
                    WriteLine("Peek item of stack = " + stack.Peek());
                    WriteLine("Deleted item = " + stack.Pop());
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
                Queue que = new Queue(new int[] { 103, 2049, 250, 030, 89, 54 });
                que.Enqueue(2);
                var quearr = que.ToArray();
                //quearr.Sort();
                //quearr.Reverse();
                WriteLine("Queue Collection");
                foreach (var queitem in que)
                {
                    Write(queitem + " ");
                }
                WriteLine();
                if (que.Count > 0)
                {
                    WriteLine("Peep item of que = " + que.Peek());
                    WriteLine("Enqueue item of que = " + que.Dequeue());
                }
                //copy queue
                Queue copyQue = (Queue)que.Clone();
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
                SortedList sortedlist = new SortedList() { { "Name", "Montu" }, { "name", "montu" }, { "age", 23 } };
                sortedlist.Add("city", "Sonipat");

                WriteLine("sorted list collection");
                //sortedlist.Add("email", "Montukharb868@gmail.com");
                foreach (DictionaryEntry item in sortedlist)
                {
                    Write(item.Key + ":" + item.Value);
                }

                //all operation same hai hashtable ke jaise but sorted order maintain karta hai or index find for example
                WriteLine(Environment.NewLine + "Index of name = " + sortedlist.IndexOfKey("name"));
                WriteLine(sortedlist.GetByIndex(2));
            }
            SortedListColl();
             
            /*
             Bit array eak collection hai jo bits (0 aur 1) ko store karta hai. Ye memory efficient hota hai jab aapko large number of boolean values store karni hoti hain.
             */
            void BitArrayCollection()
            {
                //there are three way to create bit array
                //size se
                BitArray bitArray = new BitArray(5); //5 means size of bitarray default all set 0
                //output false false false false false

                //2.bool array se
                bool[] bollarr = new bool[] { false, true, false };
                BitArray bitArray1 = new BitArray(bollarr);
                //output same copy ho zata hai bool array se

                //3.int array se ye size ko bits ma convert karta hai jainse normal represet karte hai 5 = 101;
                BitArray bitarray3 = new BitArray(new int[] { 5 });
                //output 101; bit array banega true,false,true;

                //index use set and get
                //WriteLine(bitarray3[0]);//true
                //WriteLine(bitarray3[1]);//false
                //WriteLine(bitarray3[2]);//true

                BitArray b1 = new BitArray(new bool[] { true, false });
                BitArray b2 = new BitArray(new bool[] { true, true });

                WriteLine(b1.And(b2));
                foreach (var bit in b1)
                {
                    WriteLine(bit + " ");
                }
                WriteLine(b1.Or(b2));

                WriteLine(b1.Xor(b2)); //different bit ko true karta hai same bit ko false karta hai

                WriteLine(b1.Not()); //true ko false false ko true karta hai
                WriteLine(b1.Length); //size of bit array
                b1[0] = true;

            }
            BitArrayCollection();

            //har object ka hash code hota hai jo uske data ke basis par generate hota hai,
            WriteLine(arrayList.GetHashCode()); //hash code generate karta hai arraylist ke liye but ye unique nahi hota hai same data ke liye same hash code generate hota hai isliye hash code ko unique identifier ke roop me use nahi karna chahiye

            //HashCode hashcode = new HashCode();
            int a = 10;
            string b = "Montu";
            WriteLine("string B hashcode = " + b.GetHashCode());
            WriteLine("Integer A hashcode = " + a.GetHashCode());
            //hashcode.Add(a); hashcode.Add(b);
            //WriteLine("hashcode = " + hashcode.ToHashCode());

        }

        //Modern way
        internal void GenericCollections()
        {

            //NameSpace = System.Collections.Generic
            /*
             1.List
             2.Dictionary
             3.Queue
             4.Stack
             5.HashSet
             6.LinkedList
             7.SortedSet
             8.SortedDictionary
             9.SortedList

            Ye direct use nahi hote, but base hote hain:
            IEnumerable<T>
            ICollection<T>
            IList<T>
            IDictionary<TKey, TValue>

             */

            void ListCollection()
            {
                /*
                 list eak generic collection hai jo dynamically resizable array ki tarah kaam karta hai. Ye type safe hota hai, matlab aap sirf ek specific type ke data ko store kar sakte hain. List me aapko size define karne ki zarurat nahi hoti, ye automatically apne size ko adjust kar leta hai jab aap data add ya remove karte hain.
                 */
                List<int> numList = new List<int>() { 1970, 230, 350, 21, 430, 36, 871, 645, 952, 20, 36 };

                numList.Add(698); //add single item
                if (numList.Count > 0)
                {
                    numList.Remove(20); //remove specific item
                    numList.RemoveAt(2); //remove item at specific index
                }


                foreach (int i in numList)
                {
                    Write(i + " ");
                }
                WriteLine();
                numList.Sort(); //sort list in ascending order
                if (numList.Count > 0)
                {
                    var countRemovedItems = numList.RemoveAll(x => x < 100); //remove all items less than 100 and return count of removed items
                    WriteLine("Removed items count = " + countRemovedItems);
                }

                if (numList.Count > 0)
                {
                    numList.RemoveRange(3, 4); //range of index se delete karta hai 3 se start hoke 4 item delete karega
                }
                foreach (int i in numList)
                {
                    Write(i + " ");
                }
                WriteLine(numList.Contains(36) ? "List contains 36" : "List does not contain 36");
                WriteLine("Index of 36 = " + numList.IndexOf(36)); //index of specific item
                numList.Insert(2, 999); //insert item at specific index
                numList.InsertRange(4, new int[] { 111, 222, 333 }); //insert range of items at specific index,list ke index 4 se start hoke 111,222,333 insert karega.
                numList.Reverse(); //reverse list order.

                var item = numList.Find(x => x > 250); //find first item that matches the condition
                WriteLine("First item greater than 250 = " + item);

                var allItems = numList.FindAll(x => x > 350); //find all items that matches the condition
                WriteLine("All items greater than 350 = " + string.Join(", ", allItems));

                //object list
                List<StudentList> listObj = new List<StudentList>()
                {
                  new StudentList(){ Id = 1, Name = "Montu", Age = 23 },
                  new StudentList(){ Id = 2, Name = "Sachin", Age = 21 },
                  new StudentList(){ Id = 3, Name = "vishal", Age = 23 },
                      new StudentList(){ Id = 4, Name = "Konki", Age = 22 }

                }
                    ;

                listObj.Add(new StudentList { Id = 5, Name = "Tonki", Age = 23 });

                //print listObj
                foreach (StudentList sl in listObj)
                {
                    WriteLine("id:" + sl.Id + " name:" + sl.Name + " age:" + sl.Age);
                }

                WriteLine("Total students = " + listObj.Count());
                listObj.Clear();

                //Note: Size pata ho to capacity set karo avoid unnecessary resizing of list.

            }
            ListCollection();

            void DictionaryCollection()
            {
                /*
                 Dictionary eak generic collection hai jo key-value pairs ko store karta hai. Ye type safe hota hai, matlab aap sirf ek specific type ke key aur value ko store kar sakte hain. Dictionary me aapko size define karne ki zarurat nahi hoti, ye automatically apne size ko adjust kar leta hai jab aap data add ya remove karte hain.
                 */
                Dictionary<int, string> dict = new Dictionary<int, string>() { { 1, "Montu" }, { 2, "Sachin" }, { 3, "Vishal" } };
                dict.Add(4, "Konki");
                dict[5] = "Tonki"; //another way to add item in dictionary
                if (dict.ContainsKey(2))
                {
                    WriteLine("Key 2 exists in dictionary with value = " + dict[2]);
                }
                else
                {
                    WriteLine("Key 2 does not exist in dictionary");
                }
                foreach (var kvp in dict)
                {
                    WriteLine("Key: " + kvp.Key + " Value: " + kvp.Value);
                }
                dict.Remove(3); //remove item by key
                WriteLine("After removing key 3");
                foreach (var kvp in dict)
                {
                    WriteLine("Key: " + kvp.Key + " Value: " + kvp.Value);
                }
                WriteLine(dict[1]); // access value by key
                WriteLine("Total items in dictionary = " + dict.Count);

                //safeaccess
                if (dict.ContainsKey(2))
                {
                    WriteLine("item present in index 2 = " + dict[2]);
                }
                //try get value method eak safe way hai value access karne ka without throwing exception if key not found
                if (dict.TryGetValue(3, out var value))
                {
                    WriteLine("item present in index 3 = " + value);
                }
                else
                {
                    WriteLine("Key 3 not found in dictionary");
                }
                //update value using key
                if (dict.ContainsKey(4))
                {
                    dict[4] = "Updated Konki";
                    WriteLine("Updated value for key 4 = " + dict[4]);
                }

                //Dictionary with object;
                Dictionary<int, StudentList> data = new Dictionary<int, StudentList>()
                {
                    {1, new StudentList { Id = 1, Name = "Montu" }},
                    {2, new StudentList { Id = 2, Name = "Rahul" }}
                };

                //display dictionary with object
                foreach (var kvp in data)
                {
                    WriteLine("Key: " + kvp.Key + " Name = " + kvp.Value.Name);
                }

            }
            DictionaryCollection();

            void QueueCollection()
            {
                /*
                 Queue eak generic collection hai jo first-in-first-out (FIFO) principle par kaam karta hai. Isme aap data ko enqueue karte hain (add karte hain) rear se, aur dequeue karte hain (remove karte hain) front se. Queue me aapko size define karne ki zarurat nahi hoti, ye automatically apne size ko adjust kar leta hai jab aap data add ya remove karte hain.
                 */
                Queue<string> queue = new Queue<string>(new string[] { "Montu", "Sachin", "Vishal" });
                queue.Enqueue("Konki");
                queue.Enqueue("Tonki");
                queue.Enqueue("Rahul");

                WriteLine("Queue collection");

                foreach (var item in queue)
                {
                    WriteLine(item);
                }
                if (queue.Count > 0)
                {
                    WriteLine("Dequeue item = " + queue.Dequeue()); //remove front item
                    WriteLine("Peek item = " + queue.Peek()); //view front item without removing
                }
                if (queue.Count > 0)
                {
                    queue.Dequeue(); //remove front item safely without throwing exception
                }

                //queue with object
                Queue<StudentList> studentQueue = new Queue<StudentList>();
                studentQueue.Enqueue(new StudentList { Id = 1, Name = "Montu", Age = 23 });
                studentQueue.Enqueue(new StudentList { Id = 2, Name = "Sachin", Age = 21 });

                //display queue with object
                foreach (var student in studentQueue)
                {
                    WriteLine("Id: " + student.Id + " Name: " + student.Name + " Age: " + student.Age);
                }
                //dequeue with object
                if (studentQueue.Count > 0)
                {
                    var dequeuedStudent = studentQueue.Dequeue();
                    WriteLine("Dequeued Student - Id: " + dequeuedStudent.Id + " Name: " + dequeuedStudent.Name + " Age: " + dequeuedStudent.Age);
                }
                bool containsRahul = queue.Contains("Rahul"); //check if item exists in queue
                WriteLine("Queue contains 'Rahul' = " + containsRahul);
                StudentList sq = new StudentList()
                {
                    Id = 3,
                    Name = "Vishal",
                    Age = 22
                };
                studentQueue.Enqueue(sq);
                sq.Name = "Updated Vishal"; //reference type ke case me queue me update ho jata hai kyuki reference type ke case me memory me same location point karta hai
                foreach (var student in studentQueue)
                {
                    WriteLine("Id: " + student.Id + " Name: " + student.Name + " Age: " + student.Age);
                }
                if (queue.TryDequeue(out var result)) //safe way to dequeue without throwing exception if queue is empty
                {
                    WriteLine("Dequeued item = " + result);
                }
                else
                {
                    WriteLine("Queue is empty, cannot dequeue");
                }


                queue.Clear(); //clear all items from queue.

            }
            QueueCollection();

            void stackCollection()
            {
                /*
                 Stack eak generic collection hai jo last-in-first-out (LIFO) principle par kaam karta hai. Isme aap data ko push karte hain (add karte hain) top par, aur pop karte hain (remove karte hain) top se. Stack me aapko size define karne ki zarurat nahi hoti, ye automatically apne size ko adjust kar leta hai jab aap data add ya remove karte hain.
                 */
                Stack<string> stack = new Stack<string>(new string[] { "Montu", "Sachin", "Vishal" });
                stack.Push("Konki");
                stack.Push("Tonki");
                stack.Push("Rahul");
                WriteLine("Stack collection");
                foreach (var item in stack)
                {
                    WriteLine(item);
                }
                if (stack.Count > 0)
                {
                    WriteLine("Pop item = " + stack.Pop()); //remove top item
                    WriteLine("Peek item = " + stack.Peek()); //view top item without removing
                }
                if (stack.Count > 0)
                {
                    stack.Pop(); //remove top item safely without throwing exception
                }
                //stack with object
                Stack<StudentList> studentStack = new Stack<StudentList>();
                studentStack.Push(new StudentList { Id = 1, Name = "Montu", Age = 23 });
                studentStack.Push(new StudentList { Id = 2, Name = "Sachin", Age = 21 });
                //display stack with object
                foreach (var student in studentStack)
                {
                    WriteLine("Id: " + student.Id + " Name: " + student.Name + " Age: " + student.Age);
                }
                //pop with object
                if (studentStack.Count > 0)
                {
                    var poppedStudent = studentStack.Pop();
                    WriteLine("Popped Student - Id: " + poppedStudent.Id + " Name: " + poppedStudent.Name + " Age: " + poppedStudent.Age);
                }
                bool containsRahul = stack.Contains("Rahul"); //check if item exists in stack
                WriteLine("Stack contains 'Rahul' = " + containsRahul);
                var arr = stack.ToArray();
                foreach (var item in arr)
                {
                    Write(item + " ");
                }
                WriteLine();

                if (stack.TryPop(out var poppedItem))
                {
                    WriteLine("poped item = " + poppedItem);
                }
                if (stack.TryPeek(out var value))
                {
                    Console.WriteLine("Peek stack item = " + value);
                }

                stack.TrimExcess(); // stack ke ander jo extra reserved capacity hai ausko hata/release kar deta hai.




                stack.Clear(); //clear all items from stack.
            }
            stackCollection();
            void HashSetCollection()
            {
                /*
          Hash set eak collection hai jo unique elements ko store karta hai. Ye unordered hota hai aur fast lookup provide karta hai. HashSet me duplicate values allowed nahi hoti hain.
         uses - jab unique data store karna ho, jaise user IDs, email addresses, etc. HashSet me order ki tension nahi hoti, isliye ye fast hota hai jab aapko data ko search karna ho ya check karna ho ki koi term exist karta hai ya nahi.
         Example:- aadhar number, pan card number, voter id number, etc. in real life unique data store karne ke liye use hota hai.
          */
                HashSet<int> hashset = new HashSet<int>() { 10, 20, 30, 40, 40 };
                //can't add 40 in second time because its duplicate of 40 and not throw error return bool statement true unique false duplicate
                bool res = hashset.Add(50);
                WriteLine("Result unique and duplicate = " + res);
                res = hashset.Add(50);
                WriteLine("Result unique and duplicate = " + res);

                //diplay all set
                foreach (int i in hashset)
                {
                    Write(i + " ");
                }
                WriteLine();
                WriteLine("Contains check 20 = " + hashset.Contains<int>(20));

                if (hashset.Contains<int>(30))
                {
                    WriteLine("Removed item = " + hashset.Remove(30));
                    //return bool True/False.

                }
                WriteLine("Total set = " + hashset.Count());

                HashSet<int> set2 = new HashSet<int>() { 50, 20, 60, 80, 100 };

                //Dono sets ko combine kar do (duplicate hata ke)
                hashset.UnionWith(set2); //unionWith original hashset me merge kare ga
                                         //union, unionBy = linQ ka method hai

                foreach (var i in hashset)
                {
                    Write(i + " ");
                }
                WriteLine();

                //Sirf common elements rakho jo dono me match ho
                WriteLine("Intersection set");
                hashset.IntersectWith(set2);
                foreach (var i in hashset)
                {
                    Write(i + " ");
                }
                WriteLine();

                //set1 me se wo elements hata do jo set2 me ha for ex set1 = 1,2,3,4 set2 = 3,4 result = 1,2
                WriteLine("After Except with");
                hashset.ExceptWith(new HashSet<int>() { 1, 2, 3, 4, 100 });
                foreach (var i2 in hashset)
                {
                    if (hashset.Count == 0)
                    {
                        WriteLine("Empty hashset");
                        return;
                    }
                    Write(i2 + " ");
                }
                WriteLine();
                var set1 = new HashSet<int> { 1, 2, 3 };
                var set12 = new HashSet<int> { 3, 4, 5 };

                //dono me jo common nahi hai sirf wo rakho.
                set1.SymmetricExceptWith(set12);
                foreach (var i in set1)
                {
                    Write(i + " ");

                }
                //issubsetof kya seta ka sabhi elements setb me hai
                HashSet<int> seta = new HashSet<int> { 1, 2 };
                HashSet<int> setb = new HashSet<int> { 1, 2, 3 };

                WriteLine(seta.IsSubsetOf(setb)); //true or false.


                //IsSupersetOf()
                //Kya setaa me setbb ke sab elements hai?

                HashSet<int> setaa = new HashSet<int> { 1, 2, 3 };
                HashSet<int> setbb = new HashSet<int> { 1, 2 };

                WriteLine("IsSupersetOf = " + setaa.IsSupersetOf(setbb));

                //overlaps
                //Kya dono me at least ek element common hai ?
                WriteLine("Overlaps = " + setaa.Overlaps(setbb));


                HashSet<int> seta1 = new HashSet<int> { 1, 2, 3 };
                HashSet<int> seta2 = new HashSet<int> { 3, 2, 1 };

                WriteLine("SetEquals = " + seta1.SetEquals(seta2));

            }
            HashSetCollection();

            void LinkedListCollection()
            {
                /*
                 👉 LinkedList ek collection hai jisme:

                  ✔ Data nodes me store hota hai
                  ✔ Har node ke paas:
                  
                  Value
                  Next pointer
                  Previous pointer
                  
                  👉 Ye Doubly Linked List hoti hai (C# me)
                 */
                LinkedList<int> linkedList = new LinkedList<int>(new int[] { 10, 20, 30, 40, 50 });
                linkedList.AddFirst(1);
                linkedList.AddLast(100);

                WriteLine("Linked list");
                void disp(LinkedList<int> list)
                {
                    foreach (int i in list)
                    {
                        Write(i + " ");
                    }
                    WriteLine();
                }
                disp(linkedList);
                var res = linkedList.Find(40);
                //find method node return karta hai ausme value next previous pointer hota hai
                if (res?.Previous != null)
                {
                    var s = linkedList.AddBefore(res, 400);
                    WriteLine("Value added before 40 = " + s.Value);
                }
                if (res?.Next != null)
                {
                    linkedList.AddAfter(res, 500);
                }
                disp(linkedList);

                /*
                thre are three way to remove linked list element 
                1. using value return bool
                2. node void return
                3. first and last return void
                */
                WriteLine("Remove using value return bool only = " + linkedList.Remove(1));
                linkedList.RemoveFirst();
                linkedList.RemoveLast();
                disp(linkedList);
                WriteLine("Total node = " + linkedList.Count());
                WriteLine("First node = " + linkedList.First());
                WriteLine("Last node = " + linkedList.Last());


            }
            LinkedListCollection();

            void SortedSetCollection()
            {
                /*
                 HashSet ka same hai only auto sorting deta hai baki same.
                 */
                SortedSet<int> sortedSet = new SortedSet<int>() { 1, 112, 3, 0, 0, -1, -2, -2, 2, 6, 4, 5, 630, 4, 50, 64, 78, 9 };

                void disp(SortedSet<int> set)
                {
                    foreach (int i in set)
                    {
                        Write(i + " ");
                    }
                    WriteLine();
                }
                disp(sortedSet);
            }
            SortedSetCollection();
            //sortedDictionary bhi dictionary jaise hai bas key sorted hoti hai auto-matic
            //sortedList me dat index based or sorted by key and key value pair small data access karne ke liya index based access    

        }
        public class StudentList
        {
            public int Id { get; set; }
            public string? Name { get; set; }
            public int Age { get; set; }
        }
    }

}