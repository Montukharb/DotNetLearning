using EmptyProjectTesting.Background_worker;

namespace EmptyProjectTesting.Thread_s
{
    public class SampleThread
    {
        /* Normal Thread example in Race condition program
           Parameterized Thread example here
                     ⬇️
        */

        /*
        public SampleThread(string name) { }
        string userName = "John Doe";
        public Thread Th1 = new Thread();
       
        A field initializer cannot reference the non-static field, method, or property 'SampleThread.Worker()'
*/
        private record ObjectPasser(int id, string name);
        public SampleThread()
        {
            Thread Th1 = new Thread(Worker);
            Th1.Start("John Doe"); //here we can send any type of data no problem but can't send multiple data at a time because thread constructor accept only one parameter. If we want to send multiple data then we can use class object and send that object as parameter. it is lengthy process but we can do that.
            Th1.Join(); // Wait for the thread to finish worker then current thread will execute next line of code.
            Console.WriteLine("MainThread");
            Thread Th2 = new Thread(Worker2);

            Th2.Start(new ObjectPasser(2, "Jane Doe")); //class object passer is length process use instead of that lambda expression is better way to send multiple data as parameter in thread.
            //last option is use gloabal variable assigned return value and use that variable in thread but it is not good practice because it can create race condition iski jagah use kare ga Task<TResult>
            //int a = b = c = 10; wrong in c# but work in java & c++ 
            int a, b, c = 10;
            a = b = c;
            Thread Th3 = new Thread(() =>
            {
                Console.WriteLine("Worker3 thread is running.");

                Console.WriteLine(a + b + c);
            });
        }
        void Worker(object? paramData)
        {
            Console.WriteLine(paramData);
            //before any operation we can check if paramData is null or not Data type.
            if (paramData is not null and string paramDataStr)
            {
                Console.WriteLine("User name is: " + paramDataStr);
            }
            else
            {
                Console.WriteLine("Invalid parameter data.");
            }
        }
        void Worker2(object? obj)
        {
            if (obj is ObjectPasser passer)
            {
                Console.WriteLine($"Worker2 thread is running for user: {passer.name} with ID: {passer.id}");
            }
            else
            {
                Console.WriteLine("Invalid parameter data for Worker2.");
            }
        }
    }
}
