' VB.NET supports closures via lambda expressions.

Function IntSeq() As Func(Of Integer)
    Dim i As Integer = 0
    Return Function()
               i += 1
               Return i
           End Function
End Function

Dim nextInt As Func(Of Integer) = IntSeq()

Console.WriteLine(nextInt())
Console.WriteLine(nextInt())
Console.WriteLine(nextInt())

Dim newInts As Func(Of Integer) = IntSeq()
Console.WriteLine(newInts())
