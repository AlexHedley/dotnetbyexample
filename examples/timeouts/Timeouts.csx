// Timeouts are important for programs that connect to external resources
// or that otherwise need to bound execution time.
using System.Threading.Channels;

var c1 = Channel.CreateUnbounded<string>();
var c2 = Channel.CreateUnbounded<string>();

// Simulate a slow operation.
_ = Task.Run(async () => {
    await Task.Delay(2000);
    await c1.Writer.WriteAsync("result 1");
});

// Use CancellationToken for timeout.
using var cts1 = new CancellationTokenSource(TimeSpan.FromSeconds(1));
try
{
    var result = await c1.Reader.ReadAsync(cts1.Token);
    Console.WriteLine(result);
}
catch (OperationCanceledException)
{
    Console.WriteLine("timeout 1");
}

// A faster operation.
_ = Task.Run(async () => {
    await Task.Delay(200);
    await c2.Writer.WriteAsync("result 2");
});

using var cts2 = new CancellationTokenSource(TimeSpan.FromSeconds(3));
try
{
    var result = await c2.Reader.ReadAsync(cts2.Token);
    Console.WriteLine(result);
}
catch (OperationCanceledException)
{
    Console.WriteLine("timeout 2");
}
