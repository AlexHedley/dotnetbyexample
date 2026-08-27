' For is VB.NET's primary looping construct. 
' Here are some basic types of loops.

' The most basic type, with a single condition (like a While loop).
Dim i As Integer = 1
While i <= 3
    Console.WriteLine(i)
    i += 1
End While

' A classic For loop with initializer, condition, and step.
For j As Integer = 0 To 2
    Console.WriteLine(j)
Next

' Using For Each with a range.
For Each k As Integer In Enumerable.Range(0, 3)
    Console.WriteLine("range " & k.ToString())
Next

' An infinite loop with an exit condition.
While True
    Console.WriteLine("loop")
    Exit While
End While

' Continue to the next iteration.
For n As Integer = 0 To 5
    If n Mod 2 = 0 Then
        Continue For
    End If
    Console.WriteLine(n)
Next
