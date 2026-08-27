// In .NET, foreach and LINQ provide range-like iteration over built-in types.
// C# 13+ also supports range patterns with foreach.

// Iterate over elements of an array.
int[] nums = { 2, 3, 4 };
int sum = 0;
foreach (var num in nums)
{
    sum += num;
}
Console.WriteLine("sum:", sum);

// foreach on arrays gives you the value.
// Use the index with a for loop.
for (int i = 0; i < nums.Length; i++)
{
    if (nums[i] == 3)
    {
        Console.WriteLine("index:", i);
    }
}

// foreach over a dictionary gives key/value pairs.
var kvs = new Dictionary<string, string>
{
    ["a"] = "apple",
    ["b"] = "banana"
};
foreach (var (k, v) in kvs)
{
    Console.WriteLine($"{k} -> {v}");
}

// foreach over a string gives chars.
foreach (var (i, c) in "go".Select((c, i) => (i, c)))
{
    Console.WriteLine(i, (int)c);
}
