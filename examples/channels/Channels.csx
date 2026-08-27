// In .NET, channels are provided by System.Threading.Channels.
// They are similar to Go's channels.
#r "nuget: System.Threading.Channels"
using System.Threading.Channels;

// Create a channel.
var ch = Channel.CreateUnbounded<string>();

// Send a value into the channel (from a separate task).
var _ = Task.Run(async () => {
    await ch.Writer.WriteAsync("ping");
});

// Read a value from the channel.
var msg = await ch.Reader.ReadAsync();
Console.WriteLine(msg);
