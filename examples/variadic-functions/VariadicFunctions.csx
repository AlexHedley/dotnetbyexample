// Variadic functions can be called with any number of trailing arguments.
// For example, Console.WriteLine is a common variadic function.

// Here's a function that will take an arbitrary number of ints as arguments.
int Sum(params int[] nums)
{
    int total = 0;
    foreach (var num in nums)
    {
        total += num;
    }
    return total;
}

// Variadic functions can be called in the usual way with individual arguments.
Console.WriteLine(Sum(1, 2));
Console.WriteLine(Sum(1, 2, 3));

// If you already have multiple args in an array,
// apply them to a variadic function using spread syntax (not available in older C#)
// or pass the array directly.
int[] nums = { 1, 2, 3, 4 };
Console.WriteLine(Sum(nums));
