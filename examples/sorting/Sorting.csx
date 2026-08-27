// .NET provides sorting for built-in types via Array.Sort and List.Sort,
// and LINQ's OrderBy for collections.

// Sorting strings.
var strs = new List<string> { "c", "a", "b" };
strs.Sort();
Console.WriteLine("Strings:", string.Join(" ", strs));

// Sorting ints.
var ints = new List<int> { 7, 2, 4 };
ints.Sort();
Console.WriteLine("Ints:", string.Join(" ", ints));

// Check if a slice is already sorted.
Console.WriteLine("Sorted:", ints.SequenceEqual(ints.OrderBy(x => x)));

// Sort in descending order.
strs.Sort((a, b) => string.Compare(b, a, StringComparison.Ordinal));
Console.WriteLine("Reverse:", string.Join(" ", strs));
