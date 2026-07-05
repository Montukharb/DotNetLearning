namespace EmptyProjectTesting.P_LINQ
{
    public static class PlinqSample
    {
        //What is Plinq ?
        //PLINQ (Parallel LINQ) enables LINQ queries to execute in parallel across multiple CPU cores.
        public static void PLINQMethods()
        {
            var numbers = Enumerable.Range(1, 200);
            var result =
                numbers
                .AsParallel()
                .Select(x =>
                {
                    Console.WriteLine(

                        $"Number={x} Thread={Thread.CurrentThread.ManagedThreadId}"
                    );

                    return x * x;
                });
            foreach (var item in result)
            {
                Console.WriteLine(item);
            }
            AsSequentialDemo();
            AsOrderedDemo();
            AsUnOrderedDemo();
            WithDegreeOfParallelismDemo();
            ForAllDemo();

        }


        //======================================================
        // 1. AsSequential()
        //======================================================

        static void AsSequentialDemo()
        {
            Console.WriteLine("Main Thread = " +
                Thread.CurrentThread.ManagedThreadId);

            var result = Enumerable.Range(1, 10)

                .AsParallel()

                .Where(x =>
                {
                    Console.WriteLine($"Where : {x}  Thread : {Thread.CurrentThread.ManagedThreadId}");
                    return x % 2 == 0;
                })

                .AsSequential()

                .Select(x =>
                {
                    Console.WriteLine($"Select : {x}  Thread : {Thread.CurrentThread.ManagedThreadId}");
                    return x;
                });

            foreach (var item in result)
            {
                Console.WriteLine($"Result = {item}");
            }
        }

        //======================================================
        // 2. AsOrdered()
        //======================================================

        static void AsOrderedDemo()
        {
            var result = Enumerable.Range(1, 20)

                .AsParallel()

                .AsOrdered()

                .Select(x =>
                {
                    Thread.Sleep(200);

                    Console.WriteLine($"Processing {x}  Thread : {Thread.CurrentThread.ManagedThreadId}");

                    return x;
                });

            Console.WriteLine();

            foreach (var item in result)
            {
                Console.Write(item + " ");
            }

            Console.WriteLine();
        }

        //======================================================
        // 3. AsUnOrdered()
        //======================================================

        static void AsUnOrderedDemo()
        {
            var result = Enumerable.Range(1, 20)

                .AsParallel()

                .AsOrdered()

                .Where(x => x > 0)

                .AsUnordered()

                .Select(x =>
                {
                    Thread.Sleep(200);

                    Console.WriteLine($"Processing {x} Thread : {Thread.CurrentThread.ManagedThreadId}");

                    return x;
                });

            Console.WriteLine();

            foreach (var item in result)
            {
                Console.Write(item + " ");
            }

            Console.WriteLine();
        }

        //======================================================
        // 4. WithDegreeOfParallelism()
        //======================================================

        static void WithDegreeOfParallelismDemo()
        {
            Enumerable.Range(1, 20)

                .AsParallel()

                .WithDegreeOfParallelism(2)

                .ForAll(x =>
                {
                    Console.WriteLine(
                        $"Number = {x} Thread = {Thread.CurrentThread.ManagedThreadId}");

                    Thread.Sleep(500);
                });
        }

        //======================================================
        // 5. ForAll()
        //======================================================

        static void ForAllDemo()
        {
            Enumerable.Range(1, 20)

                .AsParallel()

                .Select(x =>
                {
                    Console.WriteLine(
                        $"Select {x} Thread = {Thread.CurrentThread.ManagedThreadId}");

                    return x * x;
                })

                .ForAll(result =>
                {
                    Console.WriteLine(
                        $"Square = {result} Thread = {Thread.CurrentThread.ManagedThreadId}");
                });
        }

    }

}
