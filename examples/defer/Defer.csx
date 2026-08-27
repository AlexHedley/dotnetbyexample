// .NET provides defer-like functionality via using statements,
// try/finally blocks, and IDisposable.

// Defer equivalent using try/finally.
void CreateFile(string path)
{
    Console.WriteLine("creating");
    var f = File.Create(path);
    try
    {
        Console.WriteLine("writing");
        File.WriteAllText(path, "data");
        // ... more file operations ...
    }
    finally
    {
        // This always runs, similar to defer in Go.
        Console.WriteLine("closing");
        f.Close();
    }
}

// The idiomatic C# way: using statement (auto-disposes).
void CreateFileUsing(string path)
{
    Console.WriteLine("creating");
    using var f = File.Create(path);
    Console.WriteLine("writing");
    // f.Dispose() is called automatically when the scope ends.
    Console.WriteLine("closing (via using)");
}

// Example showing defer ordering (last-in, first-out in Go).
// In C#, we can achieve this with a stack of actions.
void DeferLIFO()
{
    var deferred = new Stack<Action>();
    
    deferred.Push(() => Console.WriteLine("deferred 1"));
    deferred.Push(() => Console.WriteLine("deferred 2"));
    deferred.Push(() => Console.WriteLine("deferred 3"));
    
    Console.WriteLine("before deferred");
    
    // Execute in LIFO order (like Go's defer).
    while (deferred.TryPop(out var action))
        action();
}

DeferLIFO();
