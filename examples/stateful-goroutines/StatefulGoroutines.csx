// In .NET, stateful goroutines can be implemented using channels
// or the actor pattern with a dedicated task owning state.
using System.Threading.Channels;

record ReadOp(string Key, TaskCompletionSource<int> Resp);
record WriteOp(string Key, int Val, TaskCompletionSource<bool> Resp);

var reads = Channel.CreateUnbounded<ReadOp>();
var writes = Channel.CreateUnbounded<WriteOp>();

// The state owner: a single task that owns the state and serves reads/writes.
var stateTask = Task.Run(async () => {
    var state = new Dictionary<string, int>();
    while (true)
    {
        // Try to handle a read or write, whichever comes first.
        var readTask = reads.Reader.ReadAsync().AsTask();
        var writeTask = writes.Reader.ReadAsync().AsTask();
        
        var completed = await Task.WhenAny(readTask, writeTask);
        
        if (completed == readTask)
        {
            var read = await readTask;
            read.Resp.SetResult(state.GetValueOrDefault(read.Key, 0));
        }
        else
        {
            var write = await writeTask;
            state[write.Key] = write.Val;
            write.Resp.SetResult(true);
        }
    }
});

// Run some reads and writes.
var tasks = new List<Task>();
for (int i = 0; i < 100; i++)
{
    var resp = new TaskCompletionSource<int>();
    await reads.Writer.WriteAsync(new ReadOp("key", resp));
    tasks.Add(resp.Task);
}
for (int i = 0; i < 100; i++)
{
    var resp = new TaskCompletionSource<bool>();
    await writes.Writer.WriteAsync(new WriteOp("key", i, resp));
    tasks.Add(resp.Task);
}

await Task.WhenAll(tasks);
Console.WriteLine("reads:", tasks.Count(t => t.IsCompleted));
