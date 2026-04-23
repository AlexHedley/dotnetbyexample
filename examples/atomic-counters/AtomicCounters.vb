' Atomic counters in VB.NET use Interlocked class.

Dim ops As Integer = 0
Dim tasks As New List(Of Task)()

For i As Integer = 0 To 49
    tasks.Add(Task.Run(Sub()
        For c As Integer = 0 To 999
            Interlocked.Increment(ops)
        Next
    End Sub))
Next

Await Task.WhenAll(tasks)
Console.WriteLine("ops: " & ops)
