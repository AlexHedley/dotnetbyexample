// .NET's equivalent to Go's select is Task.WhenAny which lets
// you wait on multiple channel operations simultaneously.
using System.Threading.Channels;

var c1 = Channel.CreateUnbounded<string>();
var c2 = Channel.CreateUnbounded<string>();

// Each value is sent from a separate task.
_ = Task.Run(async () => {
    await Task.Delay(1000);
    await c1.Writer.WriteAsync("one");
});
_ = Task.Run(async () => {
    await Task.Delay(2000);
    await c2.Writer.WriteAsync("two");
});

// Wait on both channels for 2 iterations.
for (int i = 0; i < 2; i++)
{
    var t1 = c1.Reader.ReadAsync().AsTask();
    var t2 = c2.Reader.ReadAsync().AsTask();
    
    var completed = await Task.WhenAny(t1, t2);
    
    if (completed == t1)
        Console.WriteLine("Received", t1.Result);
    else
        Console.WriteLine("Received", t2.Result);
}
