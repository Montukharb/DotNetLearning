#define DEBUG
#define COMPILE
using System.Runtime.CompilerServices;
namespace Basics.OOPS
{
    /*
    stackalloc is used to allocate memory on the stack for high-performance, 
    short-lived data, and is commonly used with Span<T> in modern C#.
    
    Rules
    1. for using memory allocation or pointer use unsafe blocks also enable unsafe in csproj file
    2. There are 3 ways to use unsafe [ 1. unsafe method , 2. class unsafe(danger), 3. block level unsafe in method
     */


    internal class NewMemoryAllocation
    {

        [SkipLocalsInit] // means clr by default assigned default value like int a = 0; etc 
        //skiplocalsinit skip this process did't set any value if we print variable any then return garbage value must be assign some value before using it
       internal unsafe void CheckMemoryAllocation()
        {
            int size = 10;
            int* memorySize = stackalloc int[size]; //memory created int type and his size is 10 indexer

            //assigned value using for loop
            for(int i = 0; i < size; i++)
            {
                Write((memorySize[i] = i * 10) + ", ");
            }
            WriteLine(sizeof(byte)); //1
            WriteLine(sizeof(bool)); //1
            WriteLine(sizeof(char)); //2
            WriteLine(sizeof(short)); //2
            int b;
            int *a = &b;
            WriteLine("address  = "+(nuint)a);
            WriteLine("Garbadge value = "+ *a);
            //#pragma warning disable 414

            //            string Message;
            //#pragma warning restore 414

            //display 
            //for(int i = 0;i< size; i++)
            //{
            //    Write(memorySize[i] + ",");
            //}
            //WriteLine();


 /*         PREPROCESSOR DIRECTIVE
            Preprocessor directives supply the compiler with additional information about
            regions of code. The most common preprocessor directives are the conditional
            directives, which provide a way to include or exclude regions of code from
            compilation:
*/
#if DEBUG
            WriteLine("Yes permission debuge");
            
#else
           WriteLine("Restricted");  
#endif


        }

    }

}
