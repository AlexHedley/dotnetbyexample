// .NET channels expose ChannelReader<T> and ChannelWriter<T>
// for directional communication, similar to Go's channel directions.
using System.Threading.Channels;

// Ping only sends to the channel (uses ChannelWriter).
async Task Ping(ChannelWriter<string> pings, string msg)
{
    await pings.WriteAsync(msg);
}

// Pong receives from one channel and sends to another.
async Task Pong(ChannelReader<string> pings, ChannelWriter<string> pongs)
{
    var msg = await pings.ReadAsync();
    await pongs.WriteAsync(msg);
}

var pings = Channel.CreateUnbounded<string>();
var pongs = Channel.CreateUnbounded<string>();

await Ping(pings.Writer, "passed message");
await Pong(pings.Reader, pongs.Writer);

Console.WriteLine(await pongs.Reader.ReadAsync());
