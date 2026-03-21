' Timers in VB.NET use Task.Delay.

Dim timer1 As Task = Task.Run(Async Function()
    Await Task.Delay(2000)
    Console.WriteLine("Timer 1 fired")
End Function)

Dim cts As New CancellationTokenSource()
Dim timer2 As Task = Task.Run(Async Function()
    Try
        Await Task.Delay(1000, cts.Token)
        Console.WriteLine("Timer 2 fired")
    Catch ex As OperationCanceledException
        ' Timer was cancelled
    End Try
End Function)

cts.Cancel()
Await timer1
Console.WriteLine("Timer 2 stopped: " & timer2.IsCanceled)
