using Elastic.CommonSchema;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Threading;

namespace EmptyProjectTesting.ParallelProgramming
{
    public class ParallelProgram
    {
        private readonly ILogger<ParallelProgram> _logger;
        public ParallelProgram(ILogger<ParallelProgram> logger)
        {
            _logger = logger;
        }
        public void SequentialMainMethod()
        {
            Console.WriteLine("Sequential Execution Started");

            Print("Task A");
            Print("Task B");

            Console.WriteLine("Finished execution");
        }

        static void Print(string task)
        {
            for (int i = 1; i <= 2; i++)
            {
                Console.WriteLine($"{task} : {i}");
                Thread.Sleep(2000);
            }
        }
        /*
        Thread represents an operating system thread.
        Complete List
        Properties
           1.Name ✅ t.name = "Download Thread" Note: Thread name can be set only once. If you try to set it again, it will throw an InvalidOperationException.
           2.IsAlive ✅ t.IsAlive result true if thread is running or false if thread is not running.
           3.IsBackground ✅ t.IsBackground = true; If a thread is marked as a background thread. program will not wait for it to finish before exiting. 
            Priority
            ManagedThreadId
            ThreadState
            CurrentCulture
            CurrentUICulture

        Instance Methods
            Start() ✅
            Join() ✅
            Interrupt()
            Abort() ❌ Obsolete
            Suspend() ❌ Obsolete
            Resume() ❌ Obsolete
            
        Static Methods
            Sleep() ✅
            Yield() ✅
            SpinWait() ✅
            MemoryBarrier() ✅
            GetCurrentProcessorId() ✅
            BeginThreadAffinity() ✅
            EndThreadAffinity() ✅
      */
        public void ThreadOperations()
        {
            Console.WriteLine("Main Thread Operations id {id} = " + Thread.CurrentThread.ManagedThreadId);
            /*
            A thread is main unit to execute any process.
            Thread lifecycle = New -> Runnable -> waiting -> Running -> TerminatedId. 
            There are two types of thread constructor.
            1.ThreadStart // A passing method no parameter no return
            2. ParameterizedThreadStart 
             */
            Thread tspin = new Thread(WorkThread3);
            tspin.Start();
            Thread t = new Thread(WorkThread);//ThreadStart
            //string s = Convert.ToString( t.IsAlive);
            //string ss = t.IsAlive.ToString();
            Thread t2 = new Thread(WorkThread2);
            _logger.LogInformation("Alive Status = {status}", t.IsAlive);
            t.Start(); //start threading
            t2.Start();
            _logger.LogInformation("Alive Status = {status}", t.IsAlive);
            //t.Join(); // Block the calling thread until the specified thread completes execution.
            t.Join(TimeSpan.FromSeconds(2)); //Block the calling thread only 2 second after that execute.
                                             //TimeSpan ts = new TimeSpan();

            _logger.LogInformation("Main Thread Executed");

        }

        public void WorkThread()
        {
            Thread.CurrentThread.Name = "WorkThread Op";
            for (int i = 1; i <= 2; i++)
            {
                _logger.LogInformation("Work Thread index: {index} ", i);
                _logger.LogInformation("Work Thread Name: {Name} ", Thread.CurrentThread.Name);
                _logger.LogInformation("Work Thread id: {id} ", Thread.CurrentThread.ManagedThreadId.ToString());

                // Thread.Sleep() current thread ko Sleeping state me daal deta hai.Is dauran thread CPU use nahi karta. CPU wait nahi karta; scheduler turant Ready queue me se kisi suitable thread ko execute kar deta hai. Sleep complete hone ke baad thread pehle Ready state me aata hai, aur phir scheduler jab CPU allocate karta hai tab Running state me jata hai.
                Thread.Sleep(2000); //Stoped current thread in 2 sec does't effect another thread. After sleeping ready state then cpu scheduling running 

                Thread.Yield(); // Yield Thread cpu release kar deta hai or Sirf same process ke ready thread ko chance dene ki request karta hai. or Current Thread ready state me hi rehta hai. ye force nahi karta only request ye possible hai ki abhi koi or thread ready state ma ho ya na ho to apne thread ko continue kar de ga
                                //ex Thrad a,b,c b{Thread.yield()} execution A,B stoped released cpu scheduler decide which one is ready and running thread after complted only 1 thread then comeback own thread A and Ready -> Running -> end.
                /* Note: Memory Trick

                     Thread.Sleep() → "Main kuch time ke liye available hi nahi hoon."
                     Thread.Yield() → "Main available hoon, lekin agar koi aur Ready thread hai to usko chance de do."

                     Isliye Yield() ke baad ye guarantee nahi hoti ki current thread ko hi turant CPU wapas milega, aur na hi ye guarantee hoti hai ki wo Ready queue me sabse aage hoga.Scheduler hi final decision leta hai. */
            }
        }
        public void WorkThread2()
        {
            _logger.LogInformation("WorkThread2");
        }
        public void WorkThread3() //spinWait Example Thread
        {
            /* 
             Cpu nahi chhodta
             Running state me hi rehta hai
             Empty loop me ghoomta rehta hai
             */
            _logger.LogInformation("Start WorkThread3");
            Thread.SpinWait(3000);
            _logger.LogInformation("End WorkThread3");
            WorkThread4();
        }

        public void WorkThread4() //MemoryBarrier
        {
            /*
             # Thread.MemoryBarrier() current thread ko na Sleep me bhejta hai, na Ready me, na Waiting me. Ye sirf CPU aur compiler ko instruction deta hai ki memory read/write operations ko is barrier ke across reorder na karein, taaki doosre threads ko data consistent order me dikhe.
             */
            int a = 10;
            int b = 20;
            int c = 30;

            Thread.MemoryBarrier();  //MemoryBarrier work as wall isse upper ki momory assigned alag hogi aur niche ki alag compiler cpu apne acording instruction order shifting nahi kar sakta. 

            int d = 40;
            int e = 50;
            int f = 60;

            //lock
            /*
             Eak time par eak thread ko critical section ma enter karne deta hai ye race condition ko prevent karta hai 
            int count = 0;

        Thread 1
            count++;

        Thread 2
            count++;

         Output Expected 2
            Actual output 1 possible hi jab eak time par dono thread eak sath chal zaye normal case me 2 hi aye ga
             */
            int count = 0;
            count++;
            count++;
            Console.WriteLine("Count = " + count); // result 2
        }
    }

    class ThreadPropertyExample
    {
        static void Work()
        {
            while (true)
            {
                Console.WriteLine("Running...");
                Thread.Sleep(1000);
            }
        }

        public static void Ex1()
        {
            Thread t = new Thread(Work);

            t.IsBackground = true;

            t.Start();

            Console.WriteLine("Main End");
        }
        public static void Ex2()
        {
            //OS Scheduler ko hint deta hai ki thread kitni importance rakhta hai.
            //Lowest , BelowNormal , Normal, AboveNormal, Highest
            Thread t = new Thread(Work);
            t.Priority = ThreadPriority.AboveNormal; //Thread ki priority set karna

            /*  Priority guarantee nahi karti ki wahi pehle chalega.
                Ye sirf scheduler ko suggestion deti hai.
                OS final decision leta hai.
            */

            //thread State Current thread kis state me hai uski information deta hai.
            Console.WriteLine(t.ThreadState);
        }
        public static void CultureInfoExample()
        {
            Console.WriteLine("Current Culture" + Thread.CurrentThread.CurrentCulture.Name);
            Console.WriteLine("Current Culture DateTime" + Thread.CurrentThread.CurrentCulture.DateTimeFormat);
            Console.WriteLine(DateTime.Now.ToString("F"));
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Console.WriteLine("After en-US Culture set");
            Console.WriteLine("Current Culture" + Thread.CurrentThread.CurrentCulture.Name);
            Console.WriteLine("Current Culture DateTime" + Thread.CurrentThread.CurrentCulture.DateTimeFormat);
            Console.WriteLine(DateTime.Now.ToString("F"));
            Console.WriteLine(6546.56.ToString("C"));
            /*
             Multi-country Applications
             Banking
             Accounting
             Localization
             */

            /*
             CurrentCulture :- formating
             CurrentUICulture :- resource file
             */
            Thread.CurrentThread.CurrentUICulture =
    new CultureInfo("hi-IN"); //Resource file ke liye culture set karna Ab application Hindi resource file load karegi (agar available ho).
            Console.WriteLine("Current UI Culture" + Thread.CurrentThread.CurrentUICulture.Name);
        }

        public static void ThreadType()
        {
            /*
             Thread 2 Type ka hote hai 
             1. Foreground Thread :- Main thread ke complete hone ke baad bhi ye thread run karte hai. Program terminate nahi hota jab tak ye thread complete nahi ho jata.
            "Jab tak ek bhi Foreground thread chal raha hai, main application band nahi karunga."

            Matlab agar Main() method khatam bhi ho jaye, lekin foreground thread chal raha ho, to program wait karega.
            
             */
            //Example Foreground Thread
            Thread t = new Thread(Work2);
            // Default = Foreground Thread
            t.Start();
            Console.WriteLine("Main Finished");

            /*
             Background thread important nahi mana jata.
             CLR bolta hai:
             "Main thread khatam? Aur koi foreground thread nahi? Theek hai, application band karo."
             Chahe background thread ka kaam adhoora ho.  
             kabhi kabhi single ouput show kar sakta hai depending on thread scheduling.
             */
            //Example Background Thread
            Thread ts = new Thread(Work2);
            ts.Start();
            ts.IsBackground = true; //Background Thread
            Console.WriteLine("Background Thread Started and Ended without waiting for it to complete");

            //Foreground threads application ko alive rakhte hain, jabki Background threads application terminate hote hi automatically stop (terminate) ho jaate hain.
        }
        static void Work2()
        {
            for (int i = 1; i <= 10; i++)
            {
                Console.WriteLine(i);
                Thread.Sleep(1000);
            }
        }
    }
}

/*
---------- THREAD LIFE CYCLE ----------
                Thread Created
                      │
                      ▼
                 Unstarted
                      │
                Thread.Start()
                      │
                      ▼
                   Ready
      (CPU ka wait kar raha hai)
                      │
             Scheduler choose karega
                      │
                      ▼
                  Running
                      │
      ┌───────────────┼────────────────┐
      │               │                │
      ▼               ▼                ▼
 Thread.Sleep()   Monitor.Wait()   Thread.Yield()
      │               │                │
      ▼               ▼                ▼
 Sleeping         Waiting         Ready
      │               │                │
      │               │                │
Sleep Time Over   Pulse/PulseAll   Scheduler
      │               │                │
      └───────► Ready ◄───────────────┘
                      │
                      ▼
                  Running
                      │
          Method Complete / Return
                      │
                      ▼
                Terminated (Dead)

 */