// Sometimes we'll want to sort a collection by something other
// than its natural order.

// For example, suppose we wanted to sort strings by their length
// instead of alphabetically.
var fruits = new List<string> { "peach", "kiwi", "apple" };

// Sort by string length using a comparison function.
fruits.Sort((a, b) => a.Length.CompareTo(b.Length));
Console.WriteLine("By length:", string.Join(" ", fruits));

// We can also use LINQ's OrderBy for more complex sorting.
var sortedFruits = fruits.OrderBy(f => f.Length).ThenBy(f => f);
Console.WriteLine("OrderBy:", string.Join(" ", sortedFruits));

// Custom struct with comparison.
record Person(string Name, int Age);
var people = new List<Person>
{
    new("Alice", 30),
    new("Bob", 25),
    new("Charlie", 35)
};

people.Sort((a, b) => a.Age.CompareTo(b.Age));
foreach (var p in people)
    Console.WriteLine($"{p.Name}: {p.Age}");
