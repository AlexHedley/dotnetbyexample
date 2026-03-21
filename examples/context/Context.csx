// In .NET, CancellationToken is used for cancellation context,
// similar to Go's context.Context for controlling goroutine lifecycles.

// A function that takes a CancellationToken.
async Task Hello(CancellationToken ct)
{
    Console.WriteLine("server: hello handler started");
    
    try
    {
        // Simulate some work.
        await Task.Delay(2000, ct);
        
        if (ct.IsCancellationRequested)
        {
            Console.WriteLine("server: context cancelled");
            return;
        }
        
        Console.WriteLine("server: hello handler done");
    }
    catch (OperationCanceledException)
    {
        Console.WriteLine("server: context cancelled (OperationCanceledException)");
    }
}

// Create a CancellationTokenSource to control cancellation.
var cts = new CancellationTokenSource();

// Start the hello task.
var helloTask = Hello(cts.Token);

// Cancel after 1 second.
await Task.Delay(1000);
cts.Cancel();

// Wait for the hello task to finish.
await helloTask;
Console.WriteLine("main done");
