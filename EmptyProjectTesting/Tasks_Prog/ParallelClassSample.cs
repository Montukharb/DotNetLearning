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
        public async Task RealParallelEx()
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

            Parallel.For(0, 100, (i, state) =>
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
            ✔ Data Transformation*/

            List<string> source = new List<string>() { "a.jpg", "b.jpg", "c.jpg" };
            Parallel.ForEach(source, file =>
                {
                    Console.WriteLine(file);
                });

            //parallel async method Main Thread block nahi karta Or dono hi Task return karte hai
            await Parallel.ForAsync<int>(0, 10, async (iterator, cancelAtiontoken) =>
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
        public string Name { get; set; }
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
}
