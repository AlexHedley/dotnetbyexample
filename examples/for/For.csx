// for is .NET's only looping construct. Here are
// some basic types of for loops.

// The most basic type, with a single condition (like a while loop).
int i = 1;
while (i <= 3)
{
    Console.WriteLine(i);
    i++;
}

// A classic initial/condition/after for loop.
for (int j = 0; j < 3; j++)
{
    Console.WriteLine(j);
}

// Another way of accomplishing the basic "do this N times" iteration
// is range over an integer in .NET using Enumerable.Range.
foreach (int k in Enumerable.Range(0, 3))
{
    Console.WriteLine("range", k);
}

// for without a condition will loop repeatedly
// until you break out of the loop or return from the function.
while (true)
{
    Console.WriteLine("loop");
    break;
}

// You can also continue to the next iteration of the loop.
for (int n = 0; n <= 5; n++)
{
    if (n % 2 == 0)
    {
        continue;
    }
    Console.WriteLine(n);
}
