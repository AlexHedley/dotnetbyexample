// Branching with if and else in .NET is straightforward.

// Here's a basic example.
int num = 9;
if (num < 0)
{
    Console.WriteLine(num + " is negative");
}
else if (num < 10)
{
    Console.WriteLine(num + " has 1 digit");
}
else
{
    Console.WriteLine(num + " has multiple digits");
}

// You can have an if statement without an else.
if (num % 2 == 0)
{
    Console.WriteLine("even");
}

// A statement can precede conditionals in .NET using a local scope.
{
    int num2 = 7;
    if (num2 % 2 == 0)
    {
        Console.WriteLine(num2 + " is even");
    }
    else
    {
        Console.WriteLine(num2 + " is odd");
    }
}

// Note that you don't need parentheses around conditions
// in .NET, but the braces are required.
Console.WriteLine("done");
