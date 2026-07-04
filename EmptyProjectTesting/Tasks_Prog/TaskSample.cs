using Elastic.Transport;
using Microsoft.SqlServer.Server;
using System.ComponentModel;
using System.Timers;

/*| 
Method                    | 
| ----------------------- | 1-1 wait | All wait | Thread block |
| `t1.Wait(); t2.Wait();` | ✅ Haan | ❌ Nahi | ✅ Haan |
| `await t1; await t2;`   | ✅ Haan | ❌ Nahi | ❌ Nahi |
| `Task.WaitAll()`        | ❌ Nahi | ✅ Haan | ✅ Haan |
| `await Task.WhenAll()`  | ❌ Nahi | ✅ Haan | ❌ Nahi |
*/


namespace EmptyProjectTesting.Tasks_Prog
{
    /*
     Task object properties/methods
    1.task.IsCompleted
    2.IsFaulted
    3.IsCancelled
    4.Status
    5.Wait & wait(sec)
    6.IsCompletedSuccessfully
    7.Result
    
     */
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
            task.Wait(); //jab tak task complete nahi hota main thread wait kare ga learning phase me use kar rahe hai wait ka normally nahi hota Thread block karta hai
            Console.WriteLine(task.Status);
            Console.WriteLine("Status Main end");
            return task.Status.ToString();
        }
        /* WaitAsync() ka asli use tab hai jab tum timeout ya cancellation add karna chahte ho, jaise:
 await task.WaitAsync(TimeSpan.FromSeconds(5));
 Isliye real projects me bina parameter ke WaitAsync() bahut kam dikhega; zyada tar timeout ya cancellation ke sath use hota hai*/

        public async Task<string> IsCompletedEx()
        {
            Console.WriteLine("Main Thread Start");
            Task<string> task = Task.Run(() =>
            {
                return "Task is completed";
            });
            Console.WriteLine("Before task result await is completed status = " + task.IsCompleted);
            var res = await task; //auto unwrapped 
            //var data = task.Result;
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
                return 1;
            })
   .ContinueWith(t =>
   {
       Console.WriteLine("Extract Zip");
       return 2;

   })
   .ContinueWith(t =>
   {
       Console.WriteLine("Read Data");
       return 3;

   })
   .ContinueWith(t =>
   {
       Console.WriteLine("Save Database");
       return 4;

   })
   .ContinueWith(t =>
   {
       Console.WriteLine("Send Email" + t.Result);
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
    public class TaskSample2
    {
        //public async void TaskEx()
        public async Task<int> TaskEx()
        //Modern.NET me normal cases ke liye Task.Run() prefer kiya jata hai.StartNew() tab use hota hai jab custom scheduler ya advanced options ki zarurat ho.
        {
            Task<Task<int>> task = Task.Factory.StartNew(async () =>
            {
                Console.WriteLine("Task Factory startNew");
                await Task.Delay(1000); //Thread.Sleep(1000) se better hai Worker Thread block nahi karta cpu ka sath hi release kar deta hai
                Console.WriteLine("Task Factory EndNew");
                return 5000;
            });
            await task; //single exception always here
            Console.WriteLine("Task Main Thread End");
            return await task.Result;
        }

        //waitAll Example waitall main thread blocked karta hai
        public async Task<string> WaitAllEx()
        {
            Task t1 = Task.Run(() =>
            {
                Thread.Sleep(2000);
                Console.WriteLine("Task 1 Finished");
            });

            Task t2 = Task.Run(() =>
            {
                Thread.Sleep(3000);
                Console.WriteLine("Task 2 Finished");
            });

            Task t3 = Task.Run(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("Task 3 Finished");
            });

            Console.WriteLine("Waiting...");

            Task.WaitAll(t1, t2, t3);

            return "All Tasks Completed";

        }

        //WhenAll Main Thread block nahi karta async wait karwata hai new task return karta hai

        public async Task WhenAllEx()
        {
            Task t1 = Task.Run(async () =>
            {
                await Task.Delay(2000);
                Console.WriteLine("Task 1");
            });

            Task t2 = Task.Run(async () =>
            {
                await Task.Delay(3000);
                Console.WriteLine("Task 2");
            });

            Console.WriteLine("Waiting...");

            await Task.WhenAll(t1, t2); //Multiple task run in parallel, can't block thread, jab sabhi complete ho jaye tabhi agge bhade ga 
                                        //Note: Task parallel chal rahe hai is liya yaha 5 sec wait nahi 3 sec wait hoga
            /*
            Sequential(await ek ke baad ek) → Time = sum
            Parallel(Task.WhenAll) → Time = Maximum.
            Yahi Task.WhenAll() ka sabse bada benefit hai.
            */

            Console.WriteLine("All Completed");

            Task<int> t11 = Task.Run(() => 10);
            Task<int> t21 = Task.Run(() => 20);

            //Task<T> return karne wale task result ko new task array deta hai
            int[] result = await Task.WhenAll(t11, t21);

            foreach (int i in result)
            {
                Console.WriteLine("whenAll2 result = " + i);
            }
        }

        public async Task ExceptionExample()
        {
            Task t1 = Task.Run(async () =>
            {
                await Task.Delay(2000);
                throw new Exception("Task1 Error");
            });

            Task t2 = Task.Run(async () =>
            {
                await Task.Delay(3000);
                Console.WriteLine("Task 2");
            });
            try
            {
                await Task.WhenAll(t1, t2); //exception whenall ka around hi ayegi 
            }
            //Lekin await karne par normally ek exception rethrow hoti hai(baaki exceptions Task.Exception me AggregateException ke andar available rehti hain).
            catch
            {
                foreach (var ex in t1.Exception?.InnerExceptions ?? [])
                {
                    Console.WriteLine($"{ex?.Message}");
                }
                foreach (var ex in t2.Exception?.InnerExceptions ?? [])
                {
                    Console.WriteLine($"{ex?.Message}");
                }

                /* Single Task →  Exception await task par aati hai.
 Task.WhenAll() → Exception await Task.WhenAll(...) par aati hai.
 Isliye try-catch await Task.WhenAll() ke around lagaya jata hai.*/
            }


            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex?.Message);
            //}


            Console.WriteLine("All Completed");
        }

        public async Task Wait_WhenAnyEx()
        {
            //Task.WhenAny() syncronous hai Thread blocked
            //await Task.Whenany() asyncronous No Thread Block
            //wait until any one of the supplied tasks completes
            Task t1 = Task.Run(() =>
            {
                Thread.Sleep(5000);
                Console.WriteLine("Task1");
            });

            Task t2 = Task.Run(() =>
            {
                Thread.Sleep(2000);
                Console.WriteLine("Task2");
            });

            Task t3 = Task.Run(() =>
            {
                Thread.Sleep(4000);
                Console.WriteLine("Task3");
            });

            int index = Task.WaitAny(t1, t2, t3); //Block until any one of the supplied tasks completed

            Console.WriteLine($"First Completed Task Index = {index}");
            Task.WaitAll();

            //asyncronous whenany with index get
            Task completedTask = await Task.WhenAny(t1, t2, t3);

            int Index = Array.IndexOf(new[] { t1, t2, t3 }, completedTask);

            Console.WriteLine($"First Completed Task Index = {Index}");

        }
    }

    class Program420
    {
        static void Main()
        {
            // 3 Tasks ka ek array banaya
            /*            Task[] tasks = new Task[3];

                        // Har task ko assign kiya aur start kiya
                        tasks[0] = Task.Run(() => DoWork(1, 2000));
                        tasks[1] = Task.Run(() => DoWork(2, 1000));
                        tasks[2] = Task.Run(() => DoWork(3, 3000));*/
            Task[] tasks =
            [
                // Har task ko assign kiya aur start kiya
                Task.Run(() => DoWork(1, 2000)),
                Task.Run(() => DoWork(2, 1000)),
                Task.Run(() => DoWork(3, 3000)),
            ];
            Console.WriteLine("Tasks start ho gaye hain. Wait kar rahe hain...");

            // Task.WaitAll: Yeh main thread ko tab tak rokega jab tak saare Tasks khatam nahi ho jate
            Task.WaitAll(tasks);

            Console.WriteLine("Saare Tasks successfully complete ho gaye!");
        }
        public async Task ListTaskEx()
        {
            List<Task> taskList = new List<Task>();

            for (int i = 1; i <= 5; i++)
            {
                int currentId = i;
                // List mein naya task add kar rahe hain
                taskList.Add(Task.Run(() => ProcessData(currentId)));
            }

            // List ko Array mein convert karke await kar sakte hain
            await Task.WhenAll(taskList);
        }
        static async Task ProcessData(int currentId)
        {
            Console.WriteLine($"Task {currentId} start hua...");
            await Task.Delay(1000);
            Console.WriteLine($"Task {currentId} khatam hua.");
        }

        static void DoWork(int taskId, int delay)
        {
            Console.WriteLine($"Task {taskId} start hua...");
            Task.Delay(delay).Wait(); // Dummy work (Delay)
            Console.WriteLine($"Task {taskId} khatam hua.");
        }
    }
}
/*
"Andar Task.Run + await hai to caller ko bhi Task return karna chahiye?"

Nahi.Agar method async void hai, to compiler us Task ko consume kar leta hai. Caller tak sirf void hi jata hai.

Isliye async void ko avoid kiya jata hai. Kyunki caller:

❌ await nahi kar sakta.
❌ Completion track nahi kar sakta.
❌ Exceptions ko properly handle nahi kar sakta.

Isi wajah se ASP.NET Core controllers aur almost sab async methods me Task ya Task<T> return kiya jata hai, async void nahi.*/

/*
❌ AggregateException normally rethrow nahi hoti.
✅ Original exception rethrow hoti hai.

Lekin saari exceptions Task ke andar hoti hain.

Task allTasks = Task.WhenAll(t1, t2);

try
{
    await allTasks;
}
catch
{
    foreach (var ex in allTasks.Exception!.InnerExceptions)
    {
        Console.WriteLine(ex.Message);
    }
}

Yahan allTasks.Exception ek AggregateException hoti hai.

Case 2: Task.WaitAll() ya.Wait()
try
{
    Task.WaitAll(t1, t2);
}
catch (AggregateException ex)
{
    Console.WriteLine(ex.InnerExceptions.Count);
}

Yahan:
AggregateException hi throw hoti hai.*/