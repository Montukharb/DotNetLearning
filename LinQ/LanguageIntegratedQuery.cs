
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;

namespace LinQ
{
    internal class LanguageIntegratedQuery
    {
        internal LanguageIntegratedQuery()
        {

            void IEnummerableMethod()
            {
                //IEnumerable povide only iteration no add remove method
                IEnumerable ienum = new ArrayList();
                IEnumerable ienum2 = new List<int>() { 10, 20, 30, 40, 50 };

                WriteLine("IEnumerable Data");
                foreach (var i in ienum2)
                {
                    Write(i + " ");
                }

                WriteLine();

                IEnumerable<int> ienum3 = new List<int>() { 1, 2, 3, 1, 6, 4, 8, 8 };

                foreach (var i in ienum3)
                {
                    Write(i + " ");
                }
                WriteLine();
            }

            IEnummerableMethod();


            void IEnumerableCollectionMethod()
            {
                //isme cout add remove clear method use kar sakte hai 
                ICollection<string> names = new List<string>();

                names.Add("Montu");

                Console.WriteLine("Name item = " + names.Count);
            }

            IEnumerableCollectionMethod();

            void IList()
            {
                IList<int> nums = new List<int>();

                nums.Add(10);
                Console.WriteLine("Ilist item = " + nums[0]);
            }

            IList();

            void ICollectionMehtod()
            {
                //ICollection<string, string> Door = new Dictionary<string, string>() { };
            }

            ICollectionMehtod();

            /* 
             IQueryable<T> is an interface that represents a queryable data source and allows LINQ queries to be translated into another query language such as SQL.
            IQueryable eak interface hai jo linQ query ko sql query ma convert kar sakta hai or database par execute karwa sakta hai
            IQueryable tell the database Filtering tum karo pura data muje mat bhejo 

            var adults = users.Where(x => x.Age > 18); //LinQ query ye direct memory me filter nahi kare ga Sql Generate kare ga example

            SELECT * 
            FROM users
            WHERE AGE > 18


            Sirf Selected Method SQL banati hai
             Where
             Select
             OrderBy
             Skip
             Take
             Any
             Count
             GroupBy


            context.Users   //Select * from user = ye query database me chale gi after to list data ram me aye ga
           .ToList()  // After ToList() data ab Ram ma aye ga further operation ram me hoge same work as a AsEnumerable()
           .Where(x => x.Age > 18)


            .AsEnumerable()  IQueryable() ko IEnumerable() me convert kar de ga sql translation stop Ram Memory ma execute hoga


             */
            void IQueryAbleMannualy()
            {

                //create IQueryable mannually using list
                WriteLine("IQueryAble Mannual Create");
                List<int> nums = new List<int>() { 10, 20, 30, 40, 50, 60 };

                IQueryable<int> users = nums.AsQueryable<int>();  //data iQuerable me to convert ho gaya laking query generate nahi hogi kyui data already memory me hai;

                foreach (var user in users)
                {
                    Write(user + " ");
                }
                WriteLine();
            }

            IQueryAbleMannualy();

            /*
             Que = What is Defered Execution ?
             Ans = Query abhi execute nahi hoti sirf query prepare/store hoti hai Excecution bad me hota hai.

            Example = nums.where(x => x.age>18); Not execute here only prepare store isi ko defered Execution bolte hai

            Execute hogi First(), Tolist(),Count(),foreach() kisi iteration par etc;
            */

            /*
             Que = What is ImmediateExecution ? 
             Ans = Query Filtering Immediate Execute hoti hai example 
             
            var result = nums.Where(x => x > 2).ToList();
             */

            /*
            Que = What is Expression Tree? 
            Ans = Code ka Data Structure and tree representation
            Example :-
            x => x.Age > 18
            Normal lag raha hai
            
            BUT IQueryable me:
            ye directly run nahi hota
            
            Tree structure ban zata hai taki framework analyze kar sake sql generate kar sakte 
            Final Exp :- Blueprint of Code.

             */

            WriteLine("Language Integrated Query");
            FilteringData();
            ProjectionData();
            SortingData();
            Elements();
            Quantifiers();
            Aggregation();
            Conversion();
            Grouping();
            Joining();
            TakeMethod();
        }

        //global list for operations
        internal List<int> list = new List<int>() { 4, 9, 8, 5, 33, 99, 45, 6, 8, 69, 456, 49, 79, 64, 6949612, 84, 84971, 66, 3, 7, 23 };
        internal ArrayList arrayList = new ArrayList() { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, "hello", "Apple", "Mango", "Banana", "orange", "papaya", "pineapple", 45.6f, 656.3f, 565.256d, 'A', 'b', 'c' };
        internal void displayList(dynamic data, string message = "There are no message")
        {
            Write(message + ": ");
            foreach (var item in data)
            {
                Write(item + " ");
            }
            WriteLine();
        }

        /*
         linq is a powerful feature in C# that allows you to query and manipulate data in a more redable and concise way. It provides a set of methods that can be used to perform various operations on collections, such as filtering, sorting, grouping, and projecting data.
         */

        //filtering data using linq methods
        internal void FilteringData()
        {
            var filteredData = list.Where(x => x < 100); //filtering numbers less than 100
                                                         //note that method not excecuted until we iterate over the filteredData Variable
            displayList(filteredData, "Filtered Data using where");

            //filtering type of data using ofType method

            var filterDataUType = arrayList.OfType<string>(); //filtering only string data from arrayList
            displayList(filterDataUType, "Filtered Data using ofType");

            //Index Method introduce in Net 9 version
            //Index method convert any data into index based data like i.index and i.item for example;


            var indexBased = list.Index<int>();

            WriteLine("Index based Method");
            foreach (var i in indexBased)
            {
                WriteLine($"Index: {i.Index} value: {i.Item}");
            }

            var skippeditem = list.Skip(10);
            Write("After Skipped 10 items remainning items = ");
            foreach (var i in skippeditem)
            {
                Write(i + ",");
            }
            WriteLine();

            //skipwhile() jab tak condition true hai skip karta rahe ga if condtion break stop skipping never restart next item
            //for eaxample list = [1,2,3,4,1,2,3,5]
            List<int> Llist = new() { 1, 2, 3, 4, 1, 2, 3, 5 };

            var skipWhileItem = Llist.SkipWhile(x => x < 3);  //output 3,4,1,2,3,5  skip only 1,2 because if condition break never restart

            Write("After SkipWhile x < 3 items remainning items = ");
            foreach (var i in skipWhileItem)
            {
                Write(i + ",");
            }
            WriteLine();

            IEnumerable<int> skipLast = Llist.SkipLast(2); //skip 2 item start from end of list
            Write("After SkipLast remainning items = " + string.Join(",", skipLast));

            Llist.Sort();
            Llist.Reverse();
            WriteLine("after reversed = " +  string.Join(",", Llist));
            //Llist.Reverse(1,4) starting index,or count ka based par reverse kare ga only

            /*
             2 Reverse method hai eak linq ka eak list ka ye collection ka ye decide hota hai compile time pa kon sa apply hoga method dono ka same hai apply karne ka 
            source ienumerable hai to linq ka apply hoga new result return kare ga collection hai to original ma change kare ga 
            Note: agar original ma change nahi karna to list.AsEnumerable().Reverse() use kar lo ye new result return kare ga
             */
        }

        //projection data change but not the original data only the output of the query is changed

        internal void ProjectionData()
        {
            var projectedData = list.Select(x => x * 2); //projecting data by multiplying each element by 2


            displayList(projectedData, "Projected Data using select");

            //chaine system
            var chainedData = list.Where(x => x > 200).Select(x => x % 2 == 0 ? x * 2 : 0);
            displayList(chainedData, "Chained Data using where and select");

            //selectMany method is used to make a nested collection into a single flat collection in output not original data

            var nestedList = new List<List<int>>()
                {
                    new List<int>(){1,2,3},
                    new List<int>(){4,5,6},
                    new List<int>(){7,8,9}
                };

            var flatList = nestedList.SelectMany(x => x); //flattening the nested list into a single list
            displayList(flatList, "Flat List using selectMany");
        }

        //Sorting data using linq methods orderby and orderbyDescending
        internal void SortingData()
        {
            var sortedData = list.OrderBy(x => x); //sorting data in ascending order
            displayList(sortedData, "Sorted Data using orderby");

            var sortedDataDescending = list.OrderByDescending(x => x); //sorting data in descending order
            displayList(sortedDataDescending, "Sorted Data using orderbyDescending");

            //condition based jaise kisi object par use karna to to x=> x.age etc par lage sakte hai 
            //sorting condition based
            var sortedDataConditionBased = list.OrderBy(x => x % 2); //sorting data based on even and odd numbers, then sorting each group in ascending order
            displayList(sortedDataConditionBased, "Sorted Data using condition based sorting");

            //Thenby and thenbyDescending method is used to sort data based on multiple criteria
            var sortedDataMultipleCriteria = list.OrderBy(x => x % 2).ThenBy(x => x); //sorting data first by even and odd numbers, then sorting each group in ascending order
            displayList(sortedDataMultipleCriteria, "Sorted Data using multiple criteria sorting");

            //note:Phle sortby hona chaiya tbhi second sorting chale gi thenby multiple laga sakte hai jaise thenby(x=> x.age).thenby(x=> x.name) etc

            var sortedDataMultipleCriteriaDescending = list.OrderBy(x => x % 2).ThenByDescending(x => x); //sorting data first by even and odd numbers, then sorting each group in descending order
            displayList(sortedDataMultipleCriteriaDescending, "Sorted Data using multiple criteria sorting in descending order");

            int[] arr = { 4, 9, 9, 2, 1, 30, 4, 6, 2, 3, };
            //order and orderDescending new linQ method Both are working only primitive data types not working in objects. For Example
            var sortedArr = arr.Order<int>(); //we can also set generic.
            var sortedArrDescending = arr.OrderDescending<int>(); //also set generic or not depend your requirement.
            WriteLine("sortedArr = " + string.Join(",", sortedArr));
            WriteLine("sortedArrDescending = " + string.Join(",", sortedArrDescending));
        }

        //Elements (Single value nikalna) using linq methods like first, firstordefault, single, singleordefault, last, lastordefault etc
        internal void Elements()
        {
            var firstElement = list.First(); //returns the first element of the list
            WriteLine("First Element: " + firstElement);
            var firstElementLessThan10 = list.First(x => x < 10); //returns the first element that is less than 10
            WriteLine("First Element less than 10: " + firstElementLessThan10);
            //sirf eak element hona chaiya otherwise exception throw karega agar multiple element hoga to
            var singleElement = list.Single(x => x == 99); //returns the single element that is equal to 99
            WriteLine("Single Element equal to 99: " + singleElement);
            var lastElement = list.Last(); //returns the last element of the list
            WriteLine("Last Element: " + lastElement);
            var lastElementGreaterThan100 = list.Last(x => x > 100); //returns the last element that is greater than 100
            WriteLine("Last Element greater than 100: " + lastElementGreaterThan100);

            var singleOrDefaultElement = list.SingleOrDefault(x => x == 1000); //returns the single element that is equal to 1000 or default value if not found
            WriteLine("Single Or Default Element equal to 1000: " + singleOrDefaultElement);

            var firstOrDefaultElement = list.FirstOrDefault(x => x > 1000); //returns the first element that is greater than 1000 or default value if not found
            WriteLine("First Or Default Element greater than 1000: " + firstOrDefaultElement);

            var lastOrDefaultElement = list.LastOrDefault(x => x > 1000);
            WriteLine("Last Or Default Element greater than 1000: " + lastOrDefaultElement);
        }

        //Quantifiers (true or false return karna) using linq methods like any, all, contains etc

        internal void Quantifiers()
        {
            var anyElementGreaterThan100 = list.Any(x => x > 100); //returns true if any element is greater than 100
            WriteLine("Any Element greater than 100: " + anyElementGreaterThan100);
            var allElementsGreaterThan0 = list.All(x => x > 0); //returns true if all elements are greater than 0
            WriteLine("All Elements greater than 0: " + allElementsGreaterThan0);
            var containsElement99 = list.Contains(99); //returns true if the list contains the element 99
            WriteLine("List contains element 99: " + containsElement99);

        }

        //Aggregation (single value nikalna) using linq methods like count, sum, average, min, max etc
        internal void Aggregation()
        {
            var count = list.Count(); //returns the number of elements in the list
            WriteLine("Count: " + count);
            var sum = list.Sum(); //returns the sum of all elements in the list
            WriteLine("Sum: " + sum);
            var average = list.Average(); //returns the average of all elements in the list
            WriteLine("Average: " + average);
            var min = list.Min(); //returns the minimum element in the list
            WriteLine("Min: " + min);
            var max = list.Max(); //returns the maximum element in the list
            WriteLine("Max: " + max);

            //aggregate previous result or next item par apply hota hai jaise 1*2*3*4 = 24 etc
            var aggregate = list.Aggregate((x, y) => x + y); //returns the aggregate value of all elements in the list by applying the specified function
            WriteLine("Aggregate: " + aggregate);
        }

        //Conversion tolist, toarray, todictionary, tohashset etc
        internal void Conversion()
        {
            var toList = list.ToList(); //converts the list to a new list
            displayList(toList, "Converted to List");
            var toArray = list.ToArray(); //converts the list to an array
            displayList(toArray, "Converted to Array");

            //key create karte time duplicate key exception throw karege to isliya ya to phele list.Distinct() karna hoga duplicate remove par isse data loss hoga ya fir key ko unique banana hoga jaise yaha par guid use kar rahe hai to unique key milegi.

            var toDictionary = list.ToDictionary(x => System.Guid.NewGuid(), x => x * 2); //converts the list to a dictionary with key as element and value as element multiplied by 2
            WriteLine("Converted to Dictionary: ");
            foreach (var item in toDictionary)
            {
                WriteLine("Key: " + item.Key + ", Value: " + item.Value);
            }
            var toHashSet = list.ToHashSet(); //converts the list to a hashset
            displayList(toHashSet, "Converted to HashSet");
        }

        //Grouping data ko group karna using linq method groupby example group by even and odd numbers
        internal void Grouping()
        {
            var groupedData = list.GroupBy(x => x % 2 == 0 ? "Even" : "Odd"); //grouping data by even and odd numbers
            WriteLine("Grouped Data: ");
            foreach (var group in groupedData)
            {
                WriteLine("Group: " + group.Key);
                foreach (var item in group)
                {
                    Write(item + " ");
                }
                WriteLine();
            }
        }

        // Joining data from two collections using linq method join example joining two lists based on common elements
        // Do collections ko ek common field(key) ke basis par combine karta hai
        // Do list ko jodta hai jaha unki key match hoti hai
        internal void Joining()
        {
            var list1 = new List<int>() { 1, 2, 3, 4, 5, 6, 7 };
            var list2 = new List<int>() { 4, 5, 6, 7, 8 };
            var list3 = new List<int>() { 4, 5, 8, 9, 10, 11 };
            var joinedData = list1.Join(list2, x => x, y => y, (x, y) => new { x, y }).Join(list3, xy => xy.y, c => c, (xy, c) => new { c }); //joining two lists based on common elements
            displayList(joinedData, "Joined Data using join");
            string s = "hello world";
            //spilt in character array linke h e l l o   w o r l d
            //var res = s.Split(" ");
            var res = s.ToArray();
            foreach (var item in res)
            {
                WriteLine(item);
            }
            /*
            var result = students.Join(
            departments,          // 2nd collection
            s => s.DeptId,        // student key
            d => d.Id,            // department key
            (s, d) => new         // result
            {
                s.Name,
                d.DeptName
            }
            );
            class Student
                {
                    public int Id;
                    public string Name;
                    public int DeptId;
                }
                
                class Department
                {
                    public int Id;
                    public string DeptName;
                }
                            var students = new List<Student>
                {
                    new Student { Id = 1, Name = "A", DeptId = 1 },
                    new Student { Id = 2, Name = "B", DeptId = 2 }
                };
                
                var departments = new List<Department>
                {
                    new Department { Id = 1, DeptName = "IT" },
                    new Department { Id = 2, DeptName = "HR" }
                };
            */

            var students = new List<Student1>
                {
                    new Student1 { Id = 1, Name = "Montu", DeptId = 1 },
                    new Student1 { Id = 2, Name = "Vishal", DeptId = 2 }
                };

            var departments = new List<Department1>
                {
                    new Department1 { Id = 1, DeptName = "IT" },
                    new Department1 { Id = 2, DeptName = "HR" }
                };
            var libraryData = new List<Library>() {
                    new Library { Id = 1, BookName = "C# in Depth", Author = "Jon Skeet", Year = 2019 },
                    new Library { Id = 2, BookName = "Clean Code", Author = "Robert C. Martin", Year = 2008 },

            };

            var bankofindia = new List<BankOfIndia>()
            {
                new BankOfIndia{ BankId = 1, Year = 2001, AccHolderName = "Sachin",Balance = 985200},
                new BankOfIndia{ BankId = 2, Year = 2002, AccHolderName = "Ashish",Balance = 20360},
                new BankOfIndia{ BankId = 3, Year = 2003, AccHolderName = "Vishal",Balance = 20660},
                new BankOfIndia{ BankId = 4, Year = 2003, AccHolderName = "Deepesh",Balance = 20740},
                new BankOfIndia{ BankId = 5, Year = 2002, AccHolderName = "Montu",Balance = 1200},
                new BankOfIndia{ BankId = 6, Year = 2002, AccHolderName = "Kharb",Balance = 2200},
                
            };

            var grouped = bankofindia.GroupBy<BankOfIndia,int>(x=> x.Year);

            foreach(var group in grouped)
            {
                WriteLine("Grouped Name = " + group.Key);
                
                foreach(var item in group)
                {
                    Write($"BankId: {item.BankId}, AccountHolderName: {item.AccHolderName}, OpenedData: {item.Year}, Balance: {item.Balance}");
                }
                WriteLine();
            }

            var combineResult = from stu in students
                                join dep in departments on stu.DeptId equals dep.Id
                                join lib in libraryData on stu.Id equals lib.Id
                                select new
                                {
                                    Id = stu.Id,
                                    Name = stu.Name,
                                    DeptName = dep.DeptName,
                                    BookName = lib.BookName,
                                    Author = lib.Author,
                                    year = lib.Year
,
                                };

            foreach (var item in combineResult)
            {
                WriteLine("Id: " + item.Id + ", Name: " + item.Name + ", DeptName: " + item.DeptName + ", BookName: " + item.BookName + ", Author: " + item.Author + ", Year: " + item.year);
            }
        }


        internal void TakeMethod()
        {
            //take = choose first n item pick means starting ke kitna item lene hai
            List<string> list = new List<string>() { "Toko", "Ponki", "Men", "Donkey", "Men", "Women", "Other" };

            var item = list.Take(3);
            WriteLine("Take item = " + string.Join(",", item));

            WriteLine("TakeWhile item = " + string.Join(",", list.TakeWhile(x => x != "Men")));
            WriteLine("TakeLast item = " + string.Join(",", list.TakeLast(2)));

        }

    }

    class Student1
    {
        public int Id;
        public string? Name;
        public int DeptId;
    }

    class Department1
    {
        public int Id;
        public string? DeptName;
    };
    class Library
    {
        public int Id;
        public string? BookName;
        public string? Author;
        public int Year;
    }

    class BankOfIndia
    {
        public int BankId;
        public string? AccHolderName;
        public int Year;
        public long Balance;

    }

}