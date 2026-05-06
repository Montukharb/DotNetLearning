
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace LinQ
{
    internal class LanguageIntegratedQuery
    {
        internal LanguageIntegratedQuery()
        {
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

        //partitioning data ko partition karna using linq method skip, take, skipwhile, takewhile etc
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

}