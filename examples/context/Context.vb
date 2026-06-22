' Context (CancellationToken) in VB.NET.

Async Function Hello(ct As CancellationToken) As Task
    Console.WriteLine("server: hello handler started")
    Try
        Await Task.Delay(2000, ct)
        Console.WriteLine("server: hello handler done")
    Catch ex As OperationCanceledException
        Console.WriteLine("server: context cancelled")
    End Try
End Function

Dim cts As New CancellationTokenSource()
Dim helloTask As Task = Hello(cts.Token)

Await Task.Delay(1000)
cts.Cancel()

Await helloTask
Console.WriteLine("main done")
