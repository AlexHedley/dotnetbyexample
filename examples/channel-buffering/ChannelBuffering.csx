// In .NET, channels can be bounded (buffered) or unbounded.
using System.Threading.Channels;

// Create a bounded channel with capacity 2.
var ch = Channel.CreateBounded<string>(2);

// Because this channel is buffered, we can send these values
// without a corresponding concurrent receive.
await ch.Writer.WriteAsync("buffered");
await ch.Writer.WriteAsync("channel");

// Read the two values from the channel.
Console.WriteLine(await ch.Reader.ReadAsync());
Console.WriteLine(await ch.Reader.ReadAsync());
