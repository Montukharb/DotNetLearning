using Elastic.CommonSchema;
using Microsoft.Extensions.DependencyModel;
using System.ComponentModel;
using System.Diagnostics;

using System.Threading.Tasks; //Task or parallel 
namespace EmptyProjectTesting.Race_Condition
{
    /*
     Race condition created when two Thread try to access same time on resources and provide UnExpected Result.
     A race condition occurs when multiple threads concurrently read and write shared data, causing the final outcome to depend entirely on the unpredictable order and timing of their execution.
    # Solution :
    1.Locks / Synchronization: Code ke uss hissa ko lock kar dena eak time par eak hi thread use kar sake
    2.Monitor class 
     */
    public class RaceProgram
    {
        public static int _count;
        public static int _count2;
        //Create thread class obj
        public Thread T = new Thread(CreateRaceCondition.ThreadWorker1);
        public Thread T2 = new Thread(CreateRaceCondition.ThreadWorker2);

        //Task lik
        public int ThreadHandler()
        {
            Console.WriteLine("Current Processor ID: " + Thread.GetCurrentProcessorId()); //GetCurrentProcessorId() method returns the ID of the processor on which the current thread is running. This can be useful for debugging and performance analysis, as it allows you to see which processor is executing a particular thread at a given time.

            T.Start(); //First Thread now go To ready state cpu decide whether is running state.
            T2.Start();
            T.Join(); //block the calling thread until the specified thread completes executions.
            T2.Join(); //block the calling thread ----------------------------------------------.

            Console.WriteLine($"Count value = {_count}"); //Execute after both thread executed because both are sing join method.
            return _count;
            /*Race condition har baar reproduce hogi?
            Nahi.Race condition timing - dependent bug hai. Har execution me same result nahi aata.Kabhi correct output mil sakta hai, kabhi incorrect. Isliye ise detect aur debug karna mushkil hota hai.

            Core 1           Core 2

            T1               T2
            Isliye race condition kabhi hoti hai aur kabhi nahi. Scheduler aur hardware decide karte hain ki threads kaise execute honge.
        */
        }
        public int ThreadHandler_RaceCondSolver()
        {
            Thread T = new Thread(SolveRaceCondition.ThreadWorker1);
            Thread T2 = new Thread(SolveRaceCondition.ThreadWorker2);
            T.Start();
            T2.Start();

            T.Join();
            T2.Join();
            Console.WriteLine($"Count2 value = {_count2}");
            return _count2;
        }
    }

    public class CreateRaceCondition
    {
        public static void ThreadWorker1()
        {
            for (int i = 0; i <= 100000; i++) //using loop because normal increment can't determine race conditon
            {
                RaceProgram._count++;
            }
            /*Conceptually one step show but practically here created three steps 
             Step1 =  read variable 
             Step2 = add/increment
             Step3 = Write
             */
        }
        public static void ThreadWorker2()
        {
            for (int i = 0; i <= 100000; i++)
            {
                RaceProgram._count++;
            }
            /*Conceptually one step show but practically here created three steps 
             Step1 =  read variable 
             Step2 = add/increment
             Step3 = Write
             */
        }
    }
    public class SolveRaceCondition
    {
        static object _obj = new();
        public static void ThreadWorker1()
        {
            for (int i = 0; i < 100000; i++)
            {
                lock (_obj)
                {
                    RaceProgram._count2++;
                }
            }
        }
        public static void ThreadWorker2()
        {
            for (int i = 0; i < 100000; i++)
            {
                lock (_obj)
                {
                    RaceProgram._count2++;
                }
            }

        }

        /*
    problem ye hai jab normal case me os thread ko scheduler decide karta hai kon start kare ga kon close kuch Dll me require hoti hai ki same Os thread use ho. Isliye .NET me Thread.BeginThreadAffinity() aur Thread.EndThreadAffinity() methods diye gaye hain. Ye methods OS ko inform karte hain ki is thread ko same OS thread par execute karna hai.
        
         Aaj ke modern .NET (.NET Core/.NET 5+) applications me BeginThreadAffinity() aur EndThreadAffinity() bahut hi kam use hote hain. Inka main purpose legacy COM interop ya native libraries ke saath kaam karna tha jahan same OS thread ki requirement hoti thi. Normal Console App, ASP.NET Core, WinForms, WPF, ya business applications me tumhe lagbhag kabhi inki zarurat nahi padegi.
        | Project Type                   | Use `BeginThreadAffinity()`? |
| ------------------------------ | ---------------------------- |
| Console App                    | ❌ Never                      |
| ASP.NET Core Web API           | ❌ Never                      |
| MVC                            | ❌ Never                      |
| Entity Framework Core          | ❌ Never                      |
| Blazor                         | ❌ Never                      |
| MAUI                           | ❌ Never                      |
| WinForms/WPF (normal code)     | ❌ Almost never               |
| Native C++ DLL Interop         | ✅ Sometimes                  |
| COM Interop (legacy)           | ✅ Sometimes                  |
| Hardware SDKs                  | ✅ Sometimes                  |
| Game Engine / Rendering        | ✅ Rarely                     |
| OS-level / Runtime development | ✅ Yes                        |

         */
        public static void SameOsThread()
        {
            Thread.BeginThreadAffinity(); //Inform OS that this thread should run on the same OS thread.
            try
            {
                // Perform operations that require the same OS thread here.
                Console.WriteLine("Thread is running with affinity to the same OS thread.");
                for (int i = 1; i <= 5; i++)
                {
                    Console.WriteLine($"Index : {i} Processor/CPU : {Thread.GetCurrentProcessorId()} is Thread Id {Thread.CurrentThread.ManagedThreadId}");
                    Thread.Sleep(800); // Simulate some work
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: {0}", e.Message);
            }
            finally
            {
                Thread.EndThreadAffinity(); //Inform OS that this thread can now be scheduled on any OS thread.
            }
        }
        //Note: 99% of the time, you will never need to use BeginThreadAffinity() and EndThreadAffinity() in modern .NET applications. Use them only when you have a specific requirement to run code on the same OS thread, such as when dealing with legacy COM components or certain native libraries.
    }
}
