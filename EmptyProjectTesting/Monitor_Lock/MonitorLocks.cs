using EmptyProjectTesting.Background_worker;

namespace EmptyProjectTesting.Monitor_Lock
{
    public class MonitorLocks
    {
        /*
        Monitor provides synchronization for threads by allowing only one thread to enter a critical section at a time.

        Monitor ek locking mechanism hai.
        Iska kaam hai
        Ek time par sirf ek thread ko shared resource access karne dena.
        
        Monitor kisi method par lock nahi lagata.
        Ye kisi object par lock lagata hai.

        Monitor.Enter()
        Monitor.Exit()
        Monitor.TryEnter()
        Monitor.Wait()
        Monitor.Pulse()
        Monitor.PulseAll()
         */
        public static void MonitorLockExample()
        {
            object lockObject = new object();
            // Thread 1
            //Note: lock internally uses Monitor.Enter and Monitor.Exit to acquire and release the lock.
            Thread thread1 = new Thread(() =>
            {
                Monitor.Enter(lockObject);
                try
                {
                    Console.WriteLine("Thread 1: Entered critical section.");
                    Thread.Sleep(2000); // Simulate some work
                    Console.WriteLine("Thread 1: Exiting critical section.");
                }
                finally
                {
                    Monitor.Exit(lockObject);
                }
            });
            // Thread 2
            Thread thread2 = new Thread(() =>
            {
                try
                {
                    Monitor.Enter(lockObject);

                    Console.WriteLine("Thread 2: Entered critical section.");
                    Thread.Sleep(1000); // Simulate some work
                    Console.WriteLine("Thread 2: Exiting critical section.");
                }
                finally
                {
                    Monitor.Exit(lockObject);
                }
            });

            thread1.Start();
            thread2.Start();
            thread1.Join();
            thread2.Join();

        }
        public static void Worker()
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} is working.");
        }
        public static void MonitorLockSafe()
        {
            //Safe method
            bool lockTaken = false;
            Thread Th = new Thread(Worker);
            try
            {
                Monitor.Enter(Th, ref lockTaken);
                // Critical section code here

                //Note Wait,Pulse,PulseAll only call under the lock. Otherwise, it will throw SynchronizationLockException.
                Monitor.Wait(Th, 3000); // Wait for a signal or timeout) current thread ko sleep mode me dal deta ha or lock se release kar deta hai aur dusre thread ko lock lene ka mauka deta hai. overload ma do parameter hai. ek object aur dusra time out ka. agar time out ho jata hai to thread wapas lock lene ki koshish karega.
                Monitor.Pulse(Th); // waiting queue me intezar kar rahe thread ko signal bhejta hai ki ab wo lock le sakte hai. ye current thread ko lock se release nahi karta. ye sirf waiting queue me intezaar kar rahe thread ko signal bhejta hai ki ab wo lock le sakte hai.
                Monitor.PulseAll(Th); // waiting queue me intezar kar rahe sabhi thread ko signal bhejta hai ki ab wo lock le sakte hai. ye current thread ko lock se release nahi karta. ye sirf waiting queue me intezaar kar rahe thread ko signal bhejta hai ki ab wo lock le sakte hai.
            }
            finally
            {
                if (lockTaken)
                    Monitor.Exit(Th);
            }
            Th.Start();
            Th.Join();

        }
        /*    Definition
    Lock lene ki koshish karta hai.
    Agar lock mil gaya
    True
    warna
    False
    Wait nahi karega.
    */
        public static void MonitorTryEnter()
        {
            object locker = new object();
            if (Monitor.TryEnter(locker, 5000))
            {
                try
                {
                    Console.WriteLine("Lock Mil Gaya");
                }
                finally
                {
                    Monitor.Exit(locker);
                }
            }
            else
            {
                Console.WriteLine("Busy");

            }
        }
    }
    class Bank
    {
        static object locker = new object();
        static int balance = 1000;
        public static void Withdraw(int amount)
        { 
            if (Monitor.TryEnter(locker /*5000 optional*/))
            {
                try
                {
                    if (balance >= amount)
                    {
                        balance -= amount;
                        Console.WriteLine($"Withdraw: {amount}");
                        Console.WriteLine($"Balance: {balance}");
                    }
                    else
                    {
                        Console.WriteLine("Insufficient Balance");
                    }
                }
                finally
                {
                    Monitor.Exit(locker);
                }
            }
            else
            {
                Console.WriteLine("Bank Account Busy");
            }
        }
    }


    class MonitorProgram
    {
        private static readonly object _lockObj = new object();
        private static bool _hasItem = false; 

        static void MainMethod()
        {
            Thread producer = new Thread(Produce);
            Thread consumer = new Thread(Consume);

            consumer.Start();
            producer.Start();

            producer.Join();
            consumer.Join();

            Console.WriteLine("All Works are completed");
        }

        static void Produce()
        {
            for (int i = 1; i <= 3; i++)
            {
                lock (_lockObj)
                {
                    //box me phele se item hai wait karo
                    while (_hasItem)
                    {
                        Console.WriteLine($"[Producer] Box is full thread is waiting...");
                        Monitor.Wait(_lockObj); //current thread ko sleep mode me dal deta ha or lock se release kar deta hai aur dusre thread ko lock lene ka mauka deta hai. overload ma do parameter hai. ek object aur dusra time out ka. agar time out ho jata hai to thread wapas lock lene ki koshish karega.
                    }

                    // item entered in box
                    Console.WriteLine($"[Producer] item {i} in box");
                    _hasItem = true;

                    // Consumer Wakeup item is ready in box
                    Monitor.Pulse(_lockObj);
                }
                Thread.Sleep(1000); //For understanding purpose, give some gap
            }
        }

        static void Consume()
        {
            for (int i = 1; i <= 3; i++)
            {
                lock (_lockObj)
                {
                    // if box is empty wait for producer to produce item
                    while (!_hasItem)
                    {
                        Console.WriteLine($"[Consumer] Box is empty thread is waiting...");
                        Monitor.Wait(_lockObj);
                    }

                    // item taken from box
                    Console.WriteLine($"[Consumer] item {i} taken from box.");
                    _hasItem = false;

                    // Producer wake up box is empty now produce item
                    Monitor.Pulse(_lockObj);
                }
                Thread.Sleep(1500); //For understanding purpose, give some gap 
            }
        }
    }
}
