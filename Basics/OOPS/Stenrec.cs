
using System.Security.Cryptography.X509Certificates;

namespace Basics.OOPS
{
    internal class Stenrec
    {

        
        internal Stenrec()
        {
            WriteLine("StrenRecord working contructor");
            //object create here;
            var a = 10;

            var type = a.GetType();
            //WriteLine(typeof(int));
             

            dynamic aa = 23;
            aa = "vishal";

            WriteLine("type fo aa = " + aa.GetType());

            //char ch = 'c';
            WriteLine(typeof(char));
            WriteLine(sizeof(uint));

            MyStruct stob = new MyStruct("VishalSharma" , "sdf@123",23, "dfs","fw","dfe","dftr");
            WriteLine("struct type = " + stob.GetType());
            stob.display();
            //MyStruct2 visStru = new MyStruct2();
            //WriteLine(visStru.Add);
        } 
    }   //class end;

    #region struct start
     
   internal struct MyStruct
    {
        public string userName;
        public string password;
        public int age;

        public MyStruct()//no parameter provided in struct constructor must be public access specifier reqired to initialize property
        {
            userName = "vishal sharma";
            password = "vish@1324";

        }

        internal MyStruct(string userName, string password, int age = 10, params string[] arr)
        {
            this.userName = userName;
            this.password = password;
            this.age = age;
            //foreach (var item in arr)
            //{
            //    Write(item + " ");
            //}
        }

        internal void display()
        {

            WriteLine("username =  "+userName + " password  = " + password + "age = " + age);
            string a1 = "\\\\server\\fileshare\\helloworld.cs";
            WriteLine(a1);

            //C# allows verbatim strint 
            int a = 10;
            string a2 = @$"\hello \this \\double slash {a}";
            WriteLine(a2);
            string a3 = @"
            lorem ipsum
 dollar how are you vishal
";

            WriteLine(a3);
            string raw = """"The """ sequence denotes raw string literals."""";
            WriteLine(raw);

            ref string x_userRef = ref Check.GetUser();
            x_userRef = "new Value"; 
            WriteLine(Check.x_user);

            Check oCh = new Check() { Name = "Vishal Don" , Email = "vishal123@gmail.com",Address = "Rohtak ABC villa 32"};
            var (Name, Email,Address) = oCh;

            WriteLine(Name + " hello brother" + Email);
            WriteLine("this type = "+this.GetType().Name);
            string[] arr = { "a", "b", "c" };
            string result = string.Join("-", arr);

            string se = "hello";
            WriteLine(se.Equals("hello"));

        }
    }

    //second struct;
          public struct Check
             {

        public string Name { get; set; }
        public string Email { get; set; }

        public string number { get; init; }
        public string Address { get; set; }
                public static string x_user = "Old value";

        //this method return a refernce x_user variable;
          public static ref string GetUser() => ref x_user;
          public Check()
        {
            number = "261669160";
        }

        //deconstruct method automatically called by compiler when we accept after created object as as obj in nomarl variable with tuple sign
        public void Deconstruct(out string name, out string email,out string address)
        {
            name = Name;
            email = Email;
            address = Address;

            WriteLine("number = " + number);
           
        }

    }




    #endregion


}


//string methods
/* 
Length
ToUpper(), ToLower()
Trim(), TrimStart(), TrimEnd()
Contains("ell")
StartsWith("he"), EndsWith("lo"),
IndexOf('l') 
Substring(1,3)
Replace('l','x')
Split(',');
string.join("-",arr);
"hello".Equals("hello");

{
string.Compare("a","b") // -1
Return ValueMeaning0Dono strings bilkul barabar hain.Negative (< 0)s1, s2 se pehle aata hai (Alphabetical order).Positive (> 0)s1, s2 ke baad aata hai.

}
Console.WriteLine(string.IsNullOrEmpty(s)); // 
Console.WriteLine(string.IsNullOrWhiteSpace(s)); // true 
string result = string.Concat("Hello", " ", "World");


string original = "Hello";

// 1. String ko char array mein badlo
char[] charArray = original.ToCharArray();

// 2. Array ko reverse karo
Array.Reverse(charArray);

// 3. Wapas string banao
string reversed = new string(charArray);

Console.WriteLine(reversed); // Output: olleH



 */