//using EmptyProjectTesting.Background_worker;
using EmptyProjectTesting.Monitor_Lock;
using Microsoft.JSInterop;
using System.Numerics;
using static Microsoft.Data.SqlClient.Internal.SqlClientEventSource;

namespace EmptyProjectTesting.Thread_s
{
    /*    ThreadPool is a collection of reusable worker threads managed by.NET runtime to execute background tasks efficiently.

Har choti task ke liye new Thread() banana expensive hota hai.
Isliye.NET pehle se kuch threads bana kar rakhta hai.
Jab koi task aata hai to wahi existing thread use ho jata hai.
Task complete hone ke baad thread destroy nahi hota.
Wapas pool me chala jata hai.*/

    /*
    ThreadPool.QueueUserWorkItem(_ => Console.WriteLine("A"));
    ThreadPool.QueueUserWorkItem(_ => Console.WriteLine("B"));
    ThreadPool.QueueUserWorkItem(_ => Console.WriteLine("C"));
    ✅ QueueUserWorkItem() = Task ThreadPool ko submit karta hai.
    ✅ ThreadPool free worker thread ko task assign karta hai.
    ✅ Worker thread complete karke wapas pool me chala jata hai.
    ✅ Execution parallel ho sakta hai.
    ❌ Output ka order fixed nahi hota.*/

    /*
        Thread pool important methods
            | Method                    | Use              |
    | ------------------------- | ---------------- |
    | QueueUserWorkItem()       | Task Queue karna |
    | UnsafeQueueUserWorkItem() | Fast Queue       |
    | GetAvailableThreads()     | Free Threads     |
    | GetMaxThreads()           | Maximum Threads  |
    | GetMinThreads()           | Minimum Threads  |
    | SetMinThreads()           | Minimum Change   |
    | SetMaxThreads()           | Maximum Change   |
    */
    public class ThreadPoolSample
    {
        public void ThreadPoolMain()
        {
            /*
               ✅ Direct pass karna hai → void Worker(object? state) signature fix hai
               ✅ void Worker() hai → Lambda use karo:
            */
            ThreadPool.QueueUserWorkItem(Worker);
            Console.WriteLine("Main thread is running.");

            //Console.ReadLine(); // Wait for user input to keep the application running output show karne ke liye. Otherwise, the application may exit before the worker thread has a chance to execute.
            //Anonymous method or lambda expression use karna hai → delegate or lambda use karo:

            ThreadPool.QueueUserWorkItem(delegate //anonymous method
            {
                ThreadPool.SetMaxThreads(5000, 2000);
                ThreadPool.GetMaxThreads(out int workerMaxThreads, out int ioMaxThreads);
                Console.WriteLine("Mannual set Worker MaximumThreads" + workerMaxThreads);
                Console.WriteLine("Mannual set I/O MaximumThreads" + ioMaxThreads);
            });
            ThreadPool.QueueUserWorkItem(_ => //lambda expression
            {
                ThreadPool.GetMaxThreads(out int workerMaxThreads, out int ioMaxThreads);
                Console.WriteLine("Worker MaximumThreads" + workerMaxThreads);
                Console.WriteLine("I/O MaximumThreads" + ioMaxThreads);

            });

        }
        public void Worker(object? state)
        {
            Console.WriteLine("Worker thread is running.");
            Console.WriteLine($"Worker thread Id : {Thread.CurrentThread.ManagedThreadId}");
            ThreadPool.GetAvailableThreads(out int workerThreads, out int ioThreads);
            Console.WriteLine($"Free worker threads : {workerThreads}");
            Console.WriteLine($"Free I/O threads : {ioThreads}");
            Thread.Sleep(2000); // Simulate some work being done by the worker thread.
        }
    }
}
