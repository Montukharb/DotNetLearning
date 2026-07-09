using System.Threading.Channels;

namespace EmptyProjectTesting.Channe_l
{
    public class ChannelSample
    {
        public Channel<string> channel = Channel.CreateUnbounded<string>();
        //unbounded means unlimited channel work concurrently

        public async Task ChannelExample()
        {

            await channel.Writer.WriteAsync("Email-1");

            string email = await channel.Reader.ReadAsync();

            Console.WriteLine(email);
        }

        //BackgroundService example continue 
        /*protected override async Task ExecuteAsync(
    CancellationToken stoppingToken)
        {
            await foreach (var email in channel.Reader.ReadAllAsync(stoppingToken)) 
            {
                await SendEmail(email);
            }
        }*/

        //Methods of Channel Writer object
        /*
            WriteAsync() //capacity full still wait
            TryWrite() //return true or false no wait  capacity full no wait
            WaitToWriteAsync() //return true or false whether writing possible or not
if(await channel.Writer.WaitToWriteAsync())
{
    await channel.Writer.WriteAsync(100);
}

            Complete() //
            TryComplete()        

         */

        //Methods of Channel Reader object

        /*
           ReadAsync() ------------- 
        Channel<int> channel =
Channel.CreateUnbounded<int>();

await channel.Writer.WriteAsync(10);

int x =
await channel.Reader.ReadAsync(); //read and remove item from channel queue

Console.WriteLine(x);
        ---------------------------

           TryRead() //channel is empty return false no wait
        if(channel.Reader.TryRead(out int number))
{
    Console.WriteLine(number);
}

           WaitToReadAsync() //purpose wait until data available


           ReadAllAsync()
        Kyunki ReadAllAsync() internally ye sab handle karta hai:

Queue empty ho to async wait karta hai.
Naya item aaye to automatically resume hota hai.
Complete() ke baad aur saare items consume hone par loop khud end ho jata hai.
CancellationToken bhi handle karta hai.

        Channel<int> channel =
Channel.CreateUnbounded<int>();

await channel.Writer.WriteAsync(10);

await channel.Writer.WriteAsync(20);

await channel.Writer.WriteAsync(30);

channel.Writer.Complete();

await foreach(var item
    in channel.Reader.ReadAllAsync())
{
    Console.WriteLine(item);
}
        */

    }
}

/*
 Application Start

        │

        ▼

EmailBackgroundService Created

        │

        ▼

ExecuteAsync() Called (Only Once)

        │

        ▼

ReadAllAsync()

        │

        ▼

No Data

        │

        ▼

Suspend (No Thread Block)

────────────────────────────

Controller

        │

        ▼

WriteAsync()

        │

        ▼

Channel Signals Reader

        │

        ▼

BackgroundService Resume

        │

        ▼

Read Email

        │

        ▼

Send Email

        │

        ▼

ReadAllAsync() Again

        │

        ▼

Suspend
 */