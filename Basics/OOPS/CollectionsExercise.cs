
using Basics.OOPS;
using System.Collections;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.Json;

namespace Basics.OOPS
{


    internal class CollectionsExercise
    {
        //public void json()
        //{

        //    var person = new
        //    {
        //        Name  = "username",
        //        age = 45,
        //        mob = 65464,
        //    };
        //}
    }
}




/*

🧱 1.Collections in .NET(Data store karne ke tools)

👉 Collections ka matlab:

Multiple data items ko store + manage karna

Jaise array hota hai, but collections zyada powerful aur flexible hote hain.

📦 Main Collection Types
1. System.Collections (Non-Generic ❌)

👉 Old style collections

ArrayList
Hashtable

❌ Problem:

Type safety nahi (object store karta hai)
Performance slow

👉 Example:

ArrayList list = new ArrayList();
list.Add(10);
list.Add("Hello"); // allowed (problem!)
2.System.Collections.Generic(Best ✔️)

👉 MOST IMPORTANT(Exam + Interview ⭐)

List<T>
Dictionary<TKey, TValue>
Queue<T>
Stack < T >

✔️ Advantages:

Type safe
Fast
Modern

👉 Example:

List<int> numbers = new List<int>();
numbers.Add(10);
numbers.Add(20);

3.System.Collections.Concurrent(Thread - safe 🔒)

👉 Multi - threading me use hota hai

ConcurrentDictionary
ConcurrentQueue

✔️ Jab multiple threads ek hi data access kare

4. System.Collections.Immutable (Read-only 🔐)

👉 Data change nahi hota (safe)

ImmutableList

✔️ Functional programming me useful

5. System.Collections.Frozen (High Performance ⚡)

👉 Read-only + super fast

✔️ Jab data change nahi hota but speed chahiye

6. System.Collections.ObjectModel

👉 Custom collection banane ke liye

7. System.Collections.Specialized

👉 Specialized collections

StringCollection
NameValueCollection
    */