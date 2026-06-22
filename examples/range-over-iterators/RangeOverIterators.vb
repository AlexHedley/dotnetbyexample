' VB.NET uses IEnumerable(Of T) and Yield for iterators.

Iterator Function GenNaturals(n As Integer) As IEnumerable(Of Integer)
    Dim x As Integer = 0
    While x < n
        Yield x
        x += 1
    End While
End Function

For Each n As Integer In GenNaturals(5)
    Console.Write(n & " ")
Next
Console.WriteLine()

' LINQ-based.
Dim infiniteNaturals = Enumerable.Range(0, Integer.MaxValue)
For Each n As Integer In infiniteNaturals.Take(5)
    Console.Write(n & " ")
Next
Console.WriteLine()
