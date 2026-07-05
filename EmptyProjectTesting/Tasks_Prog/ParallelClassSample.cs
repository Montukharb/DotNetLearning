using Microsoft.AspNetCore.SignalR;
using System.Collections;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EmptyProjectTesting.Tasks_Prog
{
    public class ParallelClassSample
    {
        public void Compress()
        {

        }
        public void Resize() { }
        public void Encrpyt() { }
        public async Task RealParallelEx(CancellationToken cancellationToken)
        {
            //What is Parallel Class ?
            // The parallel class provides methods for parallel execution of cpu-bound operations.
            //Invoke method syncronous hai sab ka execute hone ka wait karta hai no return value jaise Task.WaitAll()
            //Mehtod 1
            Parallel.Invoke(
                Compress,
                Resize,
                Encrpyt
                );

            //Mehtod 2
            Parallel.Invoke(
                () => { },
                () => { },
                () => { }
                );
            //Maximum parallel execution setting
            ParallelOptions options = new ParallelOptions() { MaxDegreeOfParallelism = 2 };
            Parallel.Invoke(
                options
                //Method1,
                //Method2,
                //Method3
                );
            // start index 0, endindex 20, i is iterator 1 by 1 auto update not altering, state is object to controll loop [break,stop,IsStopped,LowestBreakIteration,ShouldExitCurrentIteration]

            ParallelOptions options2 = options;
            options2.CancellationToken = cancellationToken;

            Parallel.For(0, 100, options2, (i, state) =>
            {
                if (i == 50)
                {
                    state.Break(); // Index 50 ke baad wale naye iterations start nahi honge
                }
                // Index 0 se 49 tak sabhi zaroor chalenge.
                // Index 51 aur uske aage wale cancel ho jayenge.
                Console.WriteLine($"Processing: {i}");

            });
            Parallel.For(0, 100, (i, state) =>
            {
                if (i == 50)
                {
                    state.Stop(); // Saare threads ko turant rukne ka signal dega
                }

                // Agar 'i' se chote (lower) index wale iterations start nahi hue the, toh wo kabhi run nahi honge.
                Console.WriteLine($"Processing: {i}");
            });

            /*
            Kab Parallel.Use Kare?
            ✔ Image Processing
            ✔ Compression
            ✔ AI
            ✔ Scientific Calculation
            ✔ Matrix Multiplication
            ✔ Huge Collections
            ✔ Data Tran
            
            
            
            sformation*/

            List<string> source = new List<string>() { "a.jpg", "b.jpg", "c.jpg" };
            Parallel.ForEach(source, file =>
                {
                    Console.WriteLine(file);
                });

            //parallel async method Main Thread block nahi karta Or dono hi Task return karte hai
            await Parallel.ForAsync<int>(0, 10, options, async (iterator, cancelAtiontoken) =>
            {
                Console.WriteLine(iterator);
                await Task.Delay(2000);

            });

            await Parallel.ForEachAsync(source, async (iterator, cancellationToken) =>
            {
                Console.WriteLine(iterator);
            });
        }
    }

    class EmployeeT
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }

    class Program
    {
        static async Task Main()
        {
            List<EmployeeT> employees = new()
        {
            new EmployeeT{ Id=1, Name="Ram"},
            new EmployeeT{ Id=2, Name="Shyam"},
            new EmployeeT{ Id=3, Name="Mohan"}
        };

            await Parallel.ForEachAsync<EmployeeT>(employees, async (emp, token) =>
                {
                    Console.WriteLine($"Start {emp.Name}");

                    await Task.Delay(1000);

                    Console.WriteLine($"End {emp.Name}");
                });

            Console.WriteLine("Done");
        }
    }
    /*
        Jab hum Parallel.For ya Parallel.ForEach ka use karte hain, toh multiple threads ek saath kaam karte hain. Agar aapko un sabhi threads se ek final result calculate karna ho (jaise ki sum nikalna ya maximum value dhundhna), toh ek normal lock ka use karne se aapka program bahut slow ho jata hai kyunki sabhi threads ek hi lock ke liye wait karte hain.

    Is problem ko solve karne ke liye hum localInit, body, aur localFinally pattern ka use karte hain.Isme har thread ko apna ek private (local) variable mil jata hai, aur locking sirf end mein karni padti hai.*/
    public class ParallelThreadRace
    {

        //int random = Random.Shared.Next(minValue:1, maxValue:1000);
        //double floor = Math.Floor(10.2);


        public async static void ParallelRaceSolution()
        {
            int[] numbers = Enumerable.Range(1, 10000).ToArray();
            long totalSum = 0;

            //Note:localInit,body,localFinally for or foreach ki overload hai async method ka nahi
            ParallelLoopResult PllelLooprslt = Parallel.ForEach<int, long>(
                source: numbers,

                // 1. localInit: Har thread ke private sum (localSum) ko 0 se initialize and Type
                localInit: () => 0L,

                // 2. body: Current number ko thread ke localSum mein add karo
                body: (number, loopState, localSum) =>
                {
                    //localSum means long sum = 0 every thread has own local state variable
                    //if(number == 17)
                    //{
                    //    loopState.Break();
                    //or
                    //loopState.Stop();
                    //}
                    localSum += number;
                    return localSum; // Updated sum return karo next round ke liye
                },

                // 3. localFinally: Thread ka kaam khatam hone par uske localSum ko main totalSum mein add karo
                localFinally: (localSum) =>
                {
                    // Interlocked ka use karke bina 'lock' block ke safely addition kiya jaa raha hai
                    Interlocked.Add(ref totalSum, localSum);
                }
            );

            Console.WriteLine("Parallel ForEach loop completed status: " + PllelLooprslt.IsCompleted); ;
            Console.WriteLine($"The total sum is: {totalSum}");
        }
    }
}

