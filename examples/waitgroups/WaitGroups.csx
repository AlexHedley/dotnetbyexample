// WaitGroups in .NET are equivalent to CountdownEvent or Task.WhenAll.

// Method to use CountdownEvent (equivalent to sync.WaitGroup).
void Worker(int id, CountdownEvent wg)
{
    Console.WriteLine("Worker", id, "starting");
    Thread.Sleep(id * 1000);
    Console.WriteLine("Worker", id, "done");
    wg.Signal();
}

// Use CountdownEvent to wait for multiple workers.
int numWorkers = 5;
var wg = new CountdownEvent(numWorkers);

for (int i = 1; i <= numWorkers; i++)
{
    int id = i;
    Task.Run(() => Worker(id, wg));
}

wg.Wait();
Console.WriteLine("All workers done");

// Alternative: use Task.WhenAll
var tasks = Enumerable.Range(1, 5).Select(id =>
    Task.Run(() => {
        Console.WriteLine("Task", id, "starting");
        Thread.Sleep(id * 100);
        Console.WriteLine("Task", id, "done");
    })).ToArray();

await Task.WhenAll(tasks);
Console.WriteLine("All tasks done");
