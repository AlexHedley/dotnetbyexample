// In .NET, goroutines are analogous to Tasks (async/await)
// and Thread pool work items.

void F(string from)
{
    for (int i = 0; i < 3; i++)
    {
        Console.WriteLine(from + ":", i);
    }
}

// Run F synchronously.
F("direct");

// Run F asynchronously using Task.Run (like a goroutine).
var t1 = Task.Run(() => F("goroutine"));

// You can also start a task with an anonymous function.
var t2 = Task.Run(() => {
    Console.WriteLine("going");
});

// Wait for both tasks to complete.
Task.WaitAll(t1, t2);
Console.WriteLine("done");
