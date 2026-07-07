using EmptyProjectTesting.Background_worker;
using System.Text;

namespace EmptyProjectTesting._Mutex
{
    public class MutexSample
    {
        //What is mutex?
        //A mutex is a synchronization primitive that allows only one thread or oe process to access a shared resources at a time.
        /*
        App1.exe 
                 ====================> Write one file Abc.Txt
        App2.exe

        App1 and app2 both are trying to write file at same time. Problem is file is getting corrupted. Solution is to use mutex same level process and different process.
        */
        private readonly IWebHostEnvironment _env;
        public static string _path = string.Empty;

        public MutexSample(IWebHostEnvironment env)
        {
            _env = env;
            _path = Path.Combine(_env.ContentRootPath, "CombineWriter");
        }
        //static Mutex mu = new Mutex(); same process work but different process not work use named mutex
        static Mutex mu = new Mutex(false,"Global\\Mutex"); //first param is bool value Means initial owner nahi banna usually false hi use hota hai.

        public async Task<(string msg,bool status)> MutexExample()
        {
            //Thread th = new Thread(Worker);
            //Thread th2 = new Thread(Worker);
            Task T1 = Task.Run(()=>Worker("I am from Task 1 and I Writing File"));
            Task T2 = Task.Run(()=>Worker("I am from Task 2 and I Writing File"));
            //th.Start("I am from Thread 1 and I Writing File");
            //th2.Start("I am from Thread 2 and I Writing File");

            //Main thread wait for Threads to complete execution
            //th.Join();
            //th2.Join();
            try
            {
            await Task.WhenAll(T1, T2);
            string msg = "Main Thread Completed";
            Console.WriteLine(msg);
            return (msg,true); //tuple return
            }catch (Exception ex)
            {
                Console.WriteLine(ex?.Message.ToString());
                return ("Exception occured", false);
            }
        }
        public void Worker(object? obj)
        {
            mu.WaitOne();
            string data = "No Content found";
            if (obj is not null and string access)
            {
                data = access + " ";
            }
            try
            {
                if (!Directory.Exists(_path))
                {
                    Directory.CreateDirectory(_path);
                    Console.WriteLine("Directory Created Name: CombineWriter");
                }
                using FileStream fs = new FileStream(Path.Combine(_path, "Writer.txt"), FileMode.Append, FileAccess.Write);
                byte[] bytes = new byte[1024]; //set container length
                bytes = Encoding.UTF8.GetBytes(data); //convert string to byte array

                fs.Write(bytes, 0, bytes.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Console.WriteLine("CombineWriter SuccessFull");
                mu.ReleaseMutex();
                mu.Dispose();//mutex object clean up alternate create mutex object under the using keyword auto dispose ex using (Mutex mu = new Mutex())
            }
        }
    }
}
