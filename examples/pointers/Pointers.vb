' VB.NET uses ByRef for reference parameters, similar to Go pointers.

Sub Zeroval(ival As Integer)
    ival = 0
End Sub

Sub Zeroptr(ByRef iptr As Integer)
    iptr = 0
End Sub

Dim i As Integer = 1
Console.WriteLine("initial: " & i)

' Zeroval doesn't change i.
Zeroval(i)
Console.WriteLine("zeroval: " & i)

' Zeroptr changes i via ByRef.
Zeroptr(i)
Console.WriteLine("zeroptr: " & i)
