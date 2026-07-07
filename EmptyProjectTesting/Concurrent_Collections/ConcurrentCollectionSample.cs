using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace EmptyProjectTesting.Concurrent_Collections
{
    public class ConcurrentCollectionSample
    {
        //what is ConcurrentCollection ?
        //ConcurrentCollection is a collection that can be used concurrently by multiple threads without any additional locking object and solved the proble of race condition.

        /*        
        ConcurrentDictionary<TKey, TValue>
        ConcurrentBag<T>
        ConcurrentQueue<T>
        ConcurrentStack<T>
        BlockingCollection<T>
        */

        public bool ConcurrentDictionaryExample()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            ConcurrentDictionary<int, string> cdist = new ConcurrentDictionary<int, string>();
            try
            {
                //Attempts to add a key / value pair if the key does not already exist.

                Parallel.For(1, 10000, (iteration, loopState) =>
                {
                    cdist.TryAdd<int, string>(iteration, $"Threads Id: {Thread.CurrentThread.ManagedThreadId.ToString()}");
                });
                foreach (var i in cdist)
                {
                    Console.Write($"Key: {i.Key}, Value: {i.Value}" + "----");
                }
                sw.Stop();
                Console.WriteLine($"{Environment.NewLine}Execution Time = {sw.ElapsedMilliseconds} ms");
                if (cdist.TryGetValue(20, out string? Item_value))
                {
                    Console.WriteLine("20: " + Item_value);
                }
                string newvalue = "Water World";
                string? oldValue = Item_value;

                if (cdist.TryUpdate(20, newvalue, oldValue ?? "null")) { }

                //if key is not present add 1 , master else key 1 updated value
                cdist.AddOrUpdate(1, "Master", (key, OldValue) => $"{OldValue} newValue"); //Possible set data = Threads Id: 1 newValue , because key 1 is already present
                if (cdist.TryGetValue(1, out string? testValue))
                {
                    Console.WriteLine("Testing Value = " + testValue); //possible output = Threads Id: 1 newValue
                }

                var res = cdist.GetOrAdd(1, "Master new Value"); //Gets the existing value or adds a new one if it does not exist.
                Console.WriteLine(res);

                bool status = cdist.TryRemove(1, out string? deletedvalue); // TryRemove() attempts to remove the value with the specified key from the ConcurrentDictionary.
                //if removed successfully return true and deleted value will be stored in deletedvalue else false
               
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex?.Message?.ToString());
                return false;
            }
        }
    }
}
