// .NET supports multiple return values via tuples.
// This feature is used often in idiomatic .NET to return both
// result and error values from a function.

// The (int, int) in this function signature shows that
// the function returns 2 ints (using value tuples).
(int, int) Vals()
{
    return (3, 7);
}

// If you use only a subset of the returned values,
// use the blank identifier _ with tuple deconstruction.
(string, string) MinMax(int a, int b)
{
    return (a.ToString(), b.ToString());
}

// Here we use the 2 different return values from the call with tuple deconstruction.
var (a, b) = Vals();
Console.WriteLine(a);
Console.WriteLine(b);

// Here we use only a subset of the returned values.
var (min, _) = MinMax(1, 2);
Console.WriteLine(min);
