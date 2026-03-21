// In .NET, iterators use IEnumerable<T> and yield return.
// This is equivalent to Go's range over function iterators.

// An iterator function that generates all natural numbers up to n.
IEnumerable<int> GenNaturals(int n)
{
    int x = 0;
    while (x < n)
    {
        yield return x;
        x++;
    }
}

// An infinite iterator (use with Take to limit).
IEnumerable<int> InfiniteNaturals()
{
    int x = 0;
    while (true)
    {
        yield return x;
        x++;
    }
}

// Iterate using foreach.
foreach (var n in GenNaturals(5))
{
    Console.Write(n + " ");
}
Console.WriteLine();

// Use LINQ Take to limit an infinite iterator.
foreach (var n in InfiniteNaturals().Take(5))
{
    Console.Write(n + " ");
}
Console.WriteLine();

// Custom iterator with pairs (like Go's key-value range).
IEnumerable<(int, int)> IndexedSquares()
{
    for (int i = 0; i < 5; i++)
    {
        yield return (i, i * i);
    }
}

foreach (var (i, sq) in IndexedSquares())
{
    Console.WriteLine($"{i}: {sq}");
}
