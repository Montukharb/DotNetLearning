using Microsoft.Extensions.DependencyModel;
using System.ComponentModel;
using System.Diagnostics;

namespace EmptyProjectTesting.Race_Condition
{
    /*
     Race condition created when two Thread try to access same time on resources and provide UnExpected Result.
     A race condition occurs when multiple threads concurrently read and write shared data, causing the final outcome to depend entirely on the unpredictable order and timing of their execution.
    # Solution :
    1.Locks / Synchronization: Code ke uss hissa ko lock kar dena eak time par eak hi thread use kar sake
    2.interlocked
     */
    public class RaceProgram
    {
        public static int _count;
        public static int _count2;
        //Create thread class obj
        public Thread T = new Thread(CreateRaceCondition.ThreadWorker1);
        public Thread T2 = new Thread(CreateRaceCondition.ThreadWorker2);

        public int ThreadHandler()
        {
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
    }
}
