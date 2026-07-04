namespace EmptyProjectTesting.Tasks_Prog
{
    public class ParallelClassSample
    {
        public void Compress()
        {

        }
        public void Resize() { }
        public void Encrpyt() { }
        public void RealParallelEx()
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
                if(i==50)
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
        }
    }
}
