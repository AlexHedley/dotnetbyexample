// In .NET, we can iterate over a channel using ReadAllAsync.
using System.Threading.Channels;

var queue = Channel.CreateUnbounded<string>();

await queue.Writer.WriteAsync("one");
await queue.Writer.WriteAsync("two");
queue.Writer.Complete();

// ReadAllAsync iterates over all values until the channel is closed.
await foreach (var elem in queue.Reader.ReadAllAsync())
{
    Console.WriteLine(elem);
}
