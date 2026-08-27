// Worker pools in .NET can be implemented using channels and tasks.
using System.Threading.Channels;

// Worker function that processes jobs.
async Task Worker(int id, ChannelReader<int> jobs, ChannelWriter<int> results)
{
    await foreach (var j in jobs.ReadAllAsync())
    {
        Console.WriteLine($"worker {id} started job {j}");
        await Task.Delay(j * 1000); // Simulate work
        Console.WriteLine($"worker {id} finished job {j}");
        await results.WriteAsync(j * 2);
    }
}

const int NUM_JOBS = 5;
var jobs = Channel.CreateUnbounded<int>();
var results = Channel.CreateUnbounded<int>();

// Start 3 workers.
var workers = new List<Task>();
for (int w = 1; w <= 3; w++)
{
    var id = w;
    workers.Add(Worker(id, jobs.Reader, results.Writer));
}

// Send 5 jobs.
for (int j = 1; j <= NUM_JOBS; j++)
{
    await jobs.Writer.WriteAsync(j);
}
jobs.Writer.Complete();

// Wait for all workers to complete.
await Task.WhenAll(workers);
results.Writer.Complete();

// Collect results.
await foreach (var a in results.Reader.ReadAllAsync())
{
    Console.WriteLine("result:", a);
}
