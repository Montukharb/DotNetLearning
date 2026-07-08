using Elastic.CommonSchema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Timers;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        public bool BlockingCollectionExample()
        {
            //What is BlockingCollection ?
            /*            
            BlockingCollection<T> is a thread - safe collection class that provides the following features:
            An implementation of the Producer - Consumer pattern.
            Concurrent adding and taking of items from multiple threads.
            Optional maximum capacity.
            Insertion and removal operations that block when collection is empty or full.
            Insertion and removal "try" operations that do not block or that block up to a specified period of time.
            Encapsulates any collection type that implements IProducerConsumerCollection<T>
            Cancellation with cancellation tokens.

            */

            BlockingCollection<int> queue = new BlockingCollection<int>(1000); //1000 is maximum capacity
            //BlockingCollection internally uses ConcurrentQueue
            //important methods
            /*
            Add() //
            TryAdd() //if bounded queue capacity is full return false no wait
            Take() //remove an item and retur it if not in queue still wait till item is added
            TryTake() //no wait non-blocking
            CompleteAdding() //producer says no more data will be added iske baad queue.Add error throw
            IsCompleted //return true if queue is completed and no more items will be added or empty queue
            Count //return queue item Total count
            GetConsumingEnumerable()  //get one by one queue data until queue is empty if emtpy still wait to new queue data
            
             */

            //Add() producer add data to the queue
            queue.Add(10);
            queue.Add(20);
            queue.Add(30);
            queue.Add(40);
            queue.Add(50);
            queue.CompleteAdding(); //can be used to indicate that no more items will be added to the BlockingCollection.
            int value = queue.Take(); //Removes an item. If queue is empty, waits automatically.
            Console.WriteLine(value);

            //TryTake() if queue is empty no exception will be thrown
            if (queue.TryTake(out int Rvalue))
            {
                Console.WriteLine(Rvalue);
            }
            //GetConsumingEnumerable() interanlly take take take karega queue data empty wait karega
            /*
              Real world use case

                        Email Queue
                        SMS Queue
                        PDF Generation
                        Image Processing
                        Log Processing
                        Video Encoding
                        Notification Queue

                        Note: user response send before queue processing complete because after queue we will send the task to another thread which is called background processing.
            */

            foreach (int item in queue.GetConsumingEnumerable()) //consumer hmm background service ma get kare ge.
            {
                Console.WriteLine($"Consumed {item}");

                Thread.Sleep(1500);
            }
            //Is example me GetConsumingEnumerable() synchronous(blocking) hai.Isliye modern ASP.NET Core applications me Channel<T> ko prefer kiya jata hai, kyunki usme ReadAsync() aur WriteAsync() milte hain aur background service async tareeke se kaam karti hai.


            return true;
        }
    }
}




/*
 Architecture:

Controller (Producer)

        │

        ▼

BlockingCollection<EmailRequest>

        │

        ▼

BackgroundService (Consumer)

        │

        ▼

EmailService.SendEmailAsync()
Step 1 : Email Model
public class EmailRequest
{
    public string To { get; set; } = "";

    public string Subject { get; set; } = "";

    public string Body { get; set; } = "";
}
Step 2 : Queue Service
using System.Collections.Concurrent;

public class EmailQueue
{
    private readonly BlockingCollection<EmailRequest> _queue =
        new BlockingCollection<EmailRequest>();

    // Producer
    public void Enqueue(EmailRequest request)
    {
        _queue.Add(request);
    }

    // Consumer
    public IEnumerable<EmailRequest> GetEmails()
    {
        return _queue.GetConsumingEnumerable();
    }

    public void Stop()
    {
        _queue.CompleteAdding();
    }
}

Yahan

Enqueue()

↓

Controller use karega


GetEmails()

↓

BackgroundService use karegi
Step 3 : Register Service
builder.Services.AddSingleton<EmailQueue>();

Queue application me sirf ek hi instance rahegi.

Step 4 : Controller (Producer)
[ApiController]
[Route("api/[controller]")]
public class EmailController : ControllerBase
{
    private readonly EmailQueue _queue;

    public EmailController(EmailQueue queue)
    {
        _queue = queue;
    }

    [HttpPost]
    public IActionResult SendEmail()
    {
        _queue.Enqueue(new EmailRequest
        {
            To = "abc@gmail.com",
            Subject = "Welcome",
            Body = "Hello User"
        });

        return Ok("Email Added To Queue");
    }
}

Flow

HTTP Request

↓

Enqueue()

↓

Return OK

User wait nahi karega.

Step 5 : Background Service (Consumer)
using Microsoft.Extensions.Hosting;

public class EmailBackgroundService : BackgroundService
{
    private readonly EmailQueue _queue;

    public EmailBackgroundService(EmailQueue queue)
    {
        _queue = queue;
    }

    protected override Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        return Task.Run(() =>
        {
            foreach (EmailRequest email in _queue.GetEmails())
            {
                Console.WriteLine($"Sending Email To : {email.To}");

                Thread.Sleep(3000);

                Console.WriteLine("Email Sent");
            }
        }, stoppingToken);
    }
}
Step 6 : Register Background Service
builder.Services.AddHostedService<EmailBackgroundService>();
*/

//=================================================================================//
/*
 Controller
public async Task<IActionResult> Register(RegisterDto dto)
{
    await _userService.CreateUser(dto);

    _emailQueue.Enqueue(new EmailRequest
    {
        To = dto.Email,
        Subject = "Welcome",
        Body = "Welcome to our website"
    });

    return Ok("Registration Successful");
}

Controller email send nahi karta, sirf queue me request daalta hai.

BackgroundService
public class EmailBackgroundService : BackgroundService
{
    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            EmailRequest email =
                await _emailQueue.DequeueAsync(stoppingToken);

            await _emailService.SendEmailAsync(email);
        }
    }
}

Ye service application start hote hi background me continuously chalti rehti hai.

Agar exception aa jaye?
try
{
    await _emailService.SendEmailAsync(email);
}
catch (Exception ex)
{
    _logger.LogError(ex, "Email sending failed");

    // Retry ya Dead Letter Queue
}

User ko koi error nahi dikhega, kyunki user ko response pehle hi mil chuka hai.
 */