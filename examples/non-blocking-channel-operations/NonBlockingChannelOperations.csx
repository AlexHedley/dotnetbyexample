// .NET channels support non-blocking operations via TryRead/TryWrite.
using System.Threading.Channels;

var messages = Channel.CreateUnbounded<string>();
var signals = Channel.CreateUnbounded<bool>();

// Non-blocking receive: TryRead returns false if no message is available.
if (messages.Reader.TryRead(out var msg))
{
    Console.WriteLine("received message:", msg);
}
else
{
    Console.WriteLine("no message received");
}

// Non-blocking send: TryWrite returns false if the channel is full.
var sent = messages.Writer.TryWrite("hi");
Console.WriteLine("sent:", sent);

// Non-blocking receive on multiple channels.
await messages.Writer.WriteAsync("hi");
if (messages.Reader.TryRead(out msg))
{
    Console.WriteLine("received message:", msg);
}
else if (signals.Reader.TryRead(out var sig))
{
    Console.WriteLine("received signal:", sig);
}
else
{
    Console.WriteLine("no activity");
}
