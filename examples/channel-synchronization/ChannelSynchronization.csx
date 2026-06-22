// We can use channels to synchronize execution across tasks.
using System.Threading.Channels;

var done = Channel.CreateUnbounded<bool>();

async Task Worker()
{
    Console.WriteLine("working...");
    await Task.Delay(1000);
    Console.WriteLine("done");
    
    // Notify the main task that work is done.
    await done.Writer.WriteAsync(true);
}

// Start a worker task.
_ = Task.Run(Worker);

// Block until we receive a notification from the worker task.
await done.Reader.ReadAsync();
