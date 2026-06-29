using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace EmptyProjectTesting.ParallelProgramming
{
    public class ParallelProgram
    {
       public void SequentialMainMethod()
        {
            Console.WriteLine("Sequential Execution Started");

            Print("Task A");
            Print("Task B");

            Console.WriteLine("Finished execution");
        }

        static void Print(string task)
        {
            for (int i = 1; i <= 5; i++)
            {
                Console.WriteLine($"{task} : {i}");
                Thread.Sleep(3000);
            }
        }

        public void ThreadOperations()
        {
            Console.WriteLine("Operation Thread id"+Thread.CurrentThread.ManagedThreadId);
        }
    }
}

