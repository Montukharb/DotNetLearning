

using System.Text;
using System.Text.RegularExpressions;
namespace Basics.OOPS
{
   
    

    //Important Methods of string builder:
    //Append() → add text
    //AppendLine() → add line
    //Insert() → beech me add
    //Remove() → delete part
    //Replace() → replace text

    internal class StringBuilderAndSystemTextNamespace
    {
        //string builder are mutable string class
        internal string st = new string("hello world");
        internal void Display()
        {
            WriteLine($"Normal string = {st}");
            //st = "new value"; every time create new object and copy value;

            //Use string builder insted of strings
            //create object;

            StringBuilder sb = new StringBuilder("Default string builder string");
            WriteLine(sb);
            sb.Append("new String update in end");
            WriteLine(sb);

            sb.AppendLine("Lorem ipsum dollar add new line"); //new line add
            sb.Insert(50, "Jat Smaz galat bat ka sathi nahi hai");   //add text in positioned index
            sb.Replace("Default", "I love my india");
            WriteLine(sb);

            //convert text to byte
            byte[] bytedata = Encoding.UTF8.GetBytes(sb.ToString());

            //convert byte to text
            sb.Append(Encoding.UTF8.GetString(bytedata));
            WriteLine(sb);

        }
    }
}
