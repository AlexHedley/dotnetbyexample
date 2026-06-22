' Sorting in VB.NET.

Dim strs As New List(Of String)({"c", "a", "b"})
strs.Sort()
Console.WriteLine("Strings: " & String.Join(" ", strs))

Dim ints As New List(Of Integer)({7, 2, 4})
ints.Sort()
Console.WriteLine("Ints: " & String.Join(" ", ints))

Console.WriteLine("Sorted: " & ints.SequenceEqual(ints.OrderBy(Function(x) x)))

' Sort in descending order.
strs.Sort(Function(a, b) String.Compare(b, a, StringComparison.Ordinal))
Console.WriteLine("Reverse: " & String.Join(" ", strs))
