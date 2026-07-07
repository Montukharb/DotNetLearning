namespace EmptyProjectTesting.Reader_Writer_LockSlim
{
    //What is ReaderWriterLockSlim ?
    // ReaderWriterLockSlim allows multiple threads to read simultaneously while ensuring that only one thread can write at a time.

    public class ReaderWriterLockSlimSample
    {
        Dictionary<int, string> dict = new Dictionary<int, string>() { { 1, "one" }, { 2, "two" }, { 3, "three" }, { 4, "four" }, { 5, "five" }, { 6, "six" }, { 7, "seven" }, { 8, "eight" }, { 9, "nine" }, { 10, "ten" } };
        public ReaderWriterLockSlim rwls = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
        public async Task<string> ReadWriteLockSlimExample()
        {
            Console.WriteLine("ReadWriter Main Thread Start..");
            Task task1 = Task.Run(() => ReadWorker());
            Task task2 = Task.Run(() => ReadWorker());
            Task task3 = Task.Run(() => ReadWorker());
            Task task4 = Task.Run(() => WriteWorker());

            await Task.WhenAll(task1, task2, task3, task4);

            Console.WriteLine("ReadWriter Main Thread End..");
            rwls.Dispose();
            return "Read Write lock Sample Successfully Executed";
        }

        public void ReadWorker()
        {
            rwls.EnterReadLock();
            try
            {
                foreach (var item in dict)
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine("Reader completed from Thread id " + Thread.CurrentThread.ManagedThreadId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex?.Message?.ToString());
            }
            finally
            {
                rwls.ExitReadLock();
            }
        }

        public void WriteWorker()
        {
            rwls.EnterWriteLock();
            try
            {
                dict.TryAdd(11, "Eleven");
                dict.TryAdd(12, "twelve");
                Console.WriteLine("writer completed from Thread id " + Thread.CurrentThread.ManagedThreadId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex?.Message?.ToString()}");
            }
            finally
            {
                rwls.ExitWriteLock();
            }
        }
    }
}
