' In VB.NET, goroutines are analogous to Tasks.

Sub F(from As String)
    For i As Integer = 0 To 2
        Console.WriteLine(from & ": " & i)
    Next
End Sub

' Run F synchronously.
F("direct")

' Run F asynchronously using Task.Run.
Dim t1 As Task = Task.Run(Sub() F("goroutine"))

' Anonymous task.
Dim t2 As Task = Task.Run(Sub() Console.WriteLine("going"))

' Wait for both tasks.
Task.WaitAll(t1, t2)
Console.WriteLine("done")
