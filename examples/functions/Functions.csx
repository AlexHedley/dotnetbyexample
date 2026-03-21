// Functions are central in .NET. We'll learn about functions
// with a few different examples.

// Here's a function that takes two ints and returns their sum.
int Plus(int a, int b)
{
    return a + b;
}

// .NET requires explicit returns in methods that return a value.
// (Unlike Go, there are no "naked" returns.)

// When you have multiple consecutive parameters of the same type,
// you don't need to repeat the type for each in C# - but you can.
int PlusPlus(int a, int b, int c)
{
    return a + b + c;
}

// Call a function just as you'd expect, with name(args).
var res = Plus(1, 2);
Console.WriteLine("1+2 =", res);

var res2 = PlusPlus(1, 2, 3);
Console.WriteLine("1+2+3 =", res2);
