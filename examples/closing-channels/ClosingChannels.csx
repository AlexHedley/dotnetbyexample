// Closing a channel in .NET means calling Writer.Complete().
// Readers can detect this by checking CanCount or catching ChannelClosedException.
using System.Threading.Channels;

const int JOBS = 5;
var jobs = Channel.CreateUnbounded<int>();
var done = Channel.CreateUnbounded<bool>();

// Worker task reads jobs until the channel is closed.
_ = Task.Run(async () => {
    await foreach (var j in jobs.Reader.ReadAllAsync())
    {
        Console.WriteLine("received job", j);
    }
    Console.WriteLine("received all jobs");
    await done.Writer.WriteAsync(true);
});

// Send jobs and close the channel.
for (int j = 1; j <= JOBS; j++)
{
    await jobs.Writer.WriteAsync(j);
    Console.WriteLine("sent job", j);
}
jobs.Writer.Complete();
Console.WriteLine("sent all jobs");

// Wait for the worker to finish.
await done.Reader.ReadAsync();

// We can also check if more values are available.
Console.WriteLine("more jobs available:", jobs.Reader.TryRead(out _));
