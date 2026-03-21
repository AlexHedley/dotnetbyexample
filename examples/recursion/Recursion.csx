// .NET supports recursive functions. Here's a classic example.

// This Fact function calls itself until it reaches the base case of Fact(0).
int Fact(int n)
{
    if (n == 0)
        return 1;
    return n * Fact(n - 1);
}

Console.WriteLine(Fact(7));

// Closures can also be recursive, but this requires explicitly
// declaring a variable to hold the closure.
Func<int, int> fib = null!;
fib = (int n) => {
    if (n < 2)
        return n;
    return fib(n - 1) + fib(n - 2);
};

Console.WriteLine(fib(7));
