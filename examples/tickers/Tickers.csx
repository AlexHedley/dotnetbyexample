// Tickers in .NET are provided by PeriodicTimer.

// Create a ticker that fires every 500ms.
var ticker = new PeriodicTimer(TimeSpan.FromMilliseconds(500));
var done = new CancellationTokenSource();

// Start a task that prints each tick.
var tickerTask = Task.Run(async () => {
    while (await ticker.WaitForNextTickAsync(done.Token))
    {
        Console.WriteLine("tick at", DateTime.Now);
    }
});

// Let it tick for 1.5 seconds, then stop.
await Task.Delay(1500);
done.Cancel();

// Wait for ticker task to complete.
try { await tickerTask; } catch { }
Console.WriteLine("Ticker stopped");
