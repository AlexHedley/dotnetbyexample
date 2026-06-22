// In .NET, List<T> is similar to Go's slices - a dynamically-sized
// sequence. Span<T> and Memory<T> also provide slice-like views.

// Unlike arrays, List<T> is dynamic.
List<string> s = new List<string>();
Console.WriteLine("empty:", s.Count);

// Append elements with Add.
s.Add("a");
s.Add("b");
s.Add("c");
Console.WriteLine("add:", string.Join(" ", s));
Console.Write("apd:");
foreach (var v in s) Console.Write(" " + v);
Console.WriteLine();

// Lists can be sliced using GetRange.
// The first argument is the start index, the second is the count.
var l = s.GetRange(2, 1);
Console.WriteLine("sl1:", string.Join(" ", l));

var l2 = s.GetRange(0, 2);
Console.WriteLine("sl2:", string.Join(" ", l2));

var l3 = s.GetRange(0, s.Count);
Console.WriteLine("sl3:", string.Join(" ", l3));

// We can declare and initialize a variable for list in a single line too.
var t = new List<string> { "g", "h", "i" };
Console.Write("dcl:");
foreach (var v in t) Console.Write(" " + v);
Console.WriteLine();

// AddRange to append another list.
s.AddRange(t);
Console.Write("apd:");
foreach (var v in s) Console.Write(" " + v);
Console.WriteLine();

// Lists can also be formed into 2D structures.
var twoD = new List<List<int>>();
for (int i = 0; i < 3; i++)
{
    var innerLen = i + 1;
    var inner = new List<int>();
    for (int j = 0; j < innerLen; j++)
    {
        inner.Add(i + j);
    }
    twoD.Add(inner);
}
Console.Write("2d:");
foreach (var inner in twoD)
{
    Console.Write(" [" + string.Join(" ", inner) + "]");
}
Console.WriteLine();
