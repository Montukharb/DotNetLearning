using Elastic.Transport;

namespace EmptyProjectTesting.Tasks_Prog
{
    public class TaskSample
    {
        public void TaskMain() //Main thread turant execute ho jata hai, worker thread background me execute hota hai. Task complete hone ke baad worker thread destroy nahi hota, wapas pool me chala jata hai.
        {
            Console.WriteLine("Main thread is running..");
            Task.Run(work); // Task.Run() = ThreadPool ko work submit karta hai. 
                            //Task ne work de diya hai ThreadPool ko, ThreadPool free worker thread ko task assign karega, worker thread complete karke wapas pool me chala jata hai. Execution parallel ho sakta hai. Output ka order fixed nahi hota.

            Console.WriteLine("Main Thread is completed");
        }
        void work()
        {
            Console.WriteLine("Begin Work");
            Thread.Sleep(5000); // Simulate some work being done by the worker thread.
            Console.WriteLine("End work");
            //return 100;
        }

        //why we used Task.Run() instead of normal method ?
        // Task.Run() method is used to execute a method asynchronously on a separate thread from the thread pool. It allows you to run a method in the background without blocking the main thread, which can improve the responsiveness of your application. In contrast, calling a normal method would execute it synchronously on the same thread, potentially causing delays or freezing the user interface if the method takes a long time to complete.

        //Task method and properties examples
        public string StatusExample()
        {
            Console.WriteLine("Stats Main start");
            Task task = Task.Run(() =>
            {
                Console.WriteLine("Task Worker begin");
                Thread.Sleep(2000);
                Console.WriteLine("Task worker end");
            });
            task.Wait(); //jab tak task complete nahi hota main thread wait kare ga learning phase me use kar rahe hai wait ka normally nahi hota 
            Console.WriteLine(task.Status);
            Console.WriteLine("Status Main end");
            return task.Status.ToString();
        }

        public async Task<string> IsCompletedEx()
        {
            Console.WriteLine("Main Thread Start");
            Task<string> task = Task.Run(() =>
            {
                return "Task is completed";
            });
            Console.WriteLine("Before task result await is completed status = " + task.IsCompleted);
            var res = await task;
            Console.WriteLine(res);
            Console.WriteLine("After task result await is completed status = " + task.IsCompleted);
            Console.WriteLine("Main thread end");
            return res;
        }

        /*     Task task = Task.Run(() =>
                     {
                         throw new Exception("Error");
                     });
             try
             {
                 task.Wait();
             }
             catch
             {
             }
             Console.WriteLine(task.IsFaulted);*/
        public void ContinueWithExample()
        {
            Task.Run(() =>
            {
                Console.WriteLine("Download File");
            })
   .ContinueWith(t =>
   {
       Console.WriteLine("Extract Zip");
   })
   .ContinueWith(t =>
   {
       Console.WriteLine("Read Data");
   })
   .ContinueWith(t =>
   {
       Console.WriteLine("Save Database");
   })
   .ContinueWith(t =>
   {
       Console.WriteLine("Send Email");
   })
   .Wait();
        }


    }
    /*   Modern Recommendation

   Aajkal agar sequential async code likhna ho, to ContinueWith() ki jagah await use karna zyada readable aur recommended hai. 

   await DownloadAsync();
       await ExtractAsync();
       await SaveAsync();
       await SendEmailAsync();*/
}
