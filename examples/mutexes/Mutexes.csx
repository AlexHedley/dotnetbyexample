// In .NET, the lock keyword and SemaphoreSlim provide mutex functionality.

class SafeCounter
{
    private readonly object _mutex = new object();
    private Dictionary<string, int> _state = new Dictionary<string, int>();

    public void Inc(string key)
    {
        lock (_mutex)
        {
            _state[key] = _state.GetValueOrDefault(key, 0) + 1;
        }
    }

    public int Value(string key)
    {
        lock (_mutex)
        {
            return _state.GetValueOrDefault(key, 0);
        }
    }
}

var c = new SafeCounter();
var tasks = new List<Task>();

for (int i = 0; i < 1000; i++)
{
    tasks.Add(Task.Run(() => c.Inc("somekey")));
}

await Task.WhenAll(tasks);
Console.WriteLine(c.Value("somekey"));
