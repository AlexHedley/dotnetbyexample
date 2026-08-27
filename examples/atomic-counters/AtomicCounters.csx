// Atomic operations in .NET are provided by the Interlocked class.

int ops = 0;
var tasks = new List<Task>();

// Start 50 tasks that each increment the counter.
for (int i = 0; i < 50; i++)
{
    tasks.Add(Task.Run(() => {
        for (int c = 0; c < 1000; c++)
        {
            Interlocked.Increment(ref ops);
        }
    }));
}

await Task.WhenAll(tasks);

// It's safe to read because Interlocked.Increment is atomic.
Console.WriteLine("ops:", ops);
