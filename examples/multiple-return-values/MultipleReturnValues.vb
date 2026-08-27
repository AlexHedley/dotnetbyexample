' VB.NET supports multiple return values via Tuple or ValueTuple.

Function Vals() As (Integer, Integer)
    Return (3, 7)
End Function

Function MinMax(a As Integer, b As Integer) As (String, String)
    Return (a.ToString(), b.ToString())
End Function

' Deconstruct the tuple.
Dim result = Vals()
Console.WriteLine(result.Item1)
Console.WriteLine(result.Item2)

' Only use the first value.
Dim mm = MinMax(1, 2)
Console.WriteLine(mm.Item1)
