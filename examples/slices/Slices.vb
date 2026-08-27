' In VB.NET, List(Of T) is similar to Go's slices.

' Unlike arrays, List(Of T) is dynamic.
Dim s As New List(Of String)()
Console.WriteLine("empty: " & s.Count)

' Add elements.
s.Add("a")
s.Add("b")
s.Add("c")
Console.Write("add:")
For Each v As String In s
    Console.Write(" " & v)
Next
Console.WriteLine()

' GetRange works like slicing.
Dim l1 As List(Of String) = s.GetRange(2, 1)
Console.WriteLine("sl1: " & String.Join(" ", l1))

Dim l2 As List(Of String) = s.GetRange(0, 2)
Console.WriteLine("sl2: " & String.Join(" ", l2))

' Declare and initialize in one line.
Dim t As New List(Of String)({"g", "h", "i"})
Console.Write("dcl:")
For Each v As String In t
    Console.Write(" " & v)
Next
Console.WriteLine()

' AddRange to append.
s.AddRange(t)
Console.Write("apd:")
For Each v As String In s
    Console.Write(" " & v)
Next
Console.WriteLine()
