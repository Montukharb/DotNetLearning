namespace EmptyProjectTesting.S_emaphore
{
    public class Semaphore_And_SemaphoreSlim
    {
        //What is semaphore ?
        //Semaphore is a concurrency control mechanism that allows a fixed number of threads to access a shared resource at a time.
        /*
         semaphore = Maximum N Threads
         means Developer decides how many threads can access a shared resource at a time
         */
        Semaphore semaphore = new Semaphore(3, 3);
        public void Semaphore() //Not Recommended to use because it is not thread safe
        {
            for (int i = 1; i <= 10; i++)
            {
                int requestId = i;

                Thread thread = new Thread(() =>
                {
                    Console.WriteLine($"Request {requestId} Trying to Enter...");

                    semaphore.WaitOne();

                    try
                    {
                        Console.WriteLine($"Request {requestId} Started");

                        Thread.Sleep(5000);

                        Console.WriteLine($"Request {requestId} Finished");
                    }
                    finally
                    {
                        semaphore.Release();
                        semaphore.Dispose();
                    }

                });

                thread.Start();
            }

        }

        SemaphoreSlim semaphoreSlim = new SemaphoreSlim(4, 4);
        //semaphore and semaphoreslim both are used to handle at a time how many threads can access a resource heavy logic or controller par apply hota hai
        public async Task SemaphoreSlim()
        {
            await semaphoreSlim.WaitAsync(); //Recommended use it because it is thread safe

            try
            {
                //do something heavy logic code like image processing downloading third party api etc
            }
            finally
            {
                semaphoreSlim.Release();
                semaphore.Dispose();
            }
        }

        //What is spinLock ?
        //SpinLock is a synchronization primitive that repeatedly checks for a lock insted of putting the thread to sleep.
        //Lock,Monito,Mutex me lock busy ho to thread sleep/wait state me chala jaata hai.
        //But in SpinLock Thread never goes to sleep/wait state. Because it checks for the lock multiple times.
        //SpinLock is used only micro and milliseconds level. Traditional programming never use it Generally library building me use hota hai

        SpinLock spinLock = new SpinLock();
        bool lockTaken = false;
        public void SpinLock()
        {
            new Thread(() =>
            {
                Console.WriteLine("Thread 1 Trying to Enter...");
                spinLock.TryEnter(ref lockTaken);
                try
                {
                    if (lockTaken)
                    {

                        //write critical section code
                        Console.WriteLine("Thread 1 Started");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex?.Message}");
                    return;
                }
                finally
                {
                    if (lockTaken)
                    {
                        spinLock.Exit();
                        lockTaken = false;
                    }
                }
            }).Start();
        }
    }
}
