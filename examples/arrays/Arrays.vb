' In VB.NET, an array is a numbered sequence of elements.

' Create an array that will hold 5 integers (index 0-4).
Dim a(4) As Integer
Console.Write("emp:")
For Each v As Integer In a
    Console.Write(" " & v)
Next
Console.WriteLine()

' Set and get values by index.
a(4) = 100
Console.WriteLine("set: " & a(4))
Console.WriteLine("get: " & a(4))

' Length property returns the length of an array.
Console.WriteLine("len: " & a.Length)

' Declare and initialize an array in one line.
Dim b() As Integer = {1, 2, 3, 4, 5}
Console.Write("dcl:")
For Each v As Integer In b
    Console.Write(" " & v)
Next
Console.WriteLine()

' Multidimensional arrays.
Dim twoD(1, 2) As Integer
For i As Integer = 0 To 1
    For j As Integer = 0 To 2
        twoD(i, j) = i + j
    Next
Next
Console.Write("2d:")
For i As Integer = 0 To 1
    For j As Integer = 0 To 2
        Console.Write(" " & twoD(i, j))
    Next
Next
Console.WriteLine()
