// Rate limiting in .NET can be done with SemaphoreSlim or
// System.Threading.RateLimiting (available in .NET 7+).
using System.Threading.Channels;

var requests = Channel.CreateUnbounded<int>();
for (int i = 1; i <= 5; i++)
    await requests.Writer.WriteAsync(i);
requests.Writer.Complete();

// Basic rate limiter: limit to 1 request per 200ms.
// Using a PeriodicTimer as a rate limiter.
var limiter = Channel.CreateUnbounded<DateTime>();

// Start a ticker that produces one token every 200ms.
_ = Task.Run(async () => {
    var ticker = new PeriodicTimer(TimeSpan.FromMilliseconds(200));
    while (await ticker.WaitForNextTickAsync())
    {
        if (!limiter.Writer.TryWrite(DateTime.Now))
            break;
    }
});

// Process requests, limited by the rate limiter.
await foreach (var req in requests.Reader.ReadAllAsync())
{
    await limiter.Reader.ReadAsync();
    Console.WriteLine("request", req, DateTime.Now);
}
