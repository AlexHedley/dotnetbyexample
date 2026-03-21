' Signals in VB.NET.

Dim cts As New CancellationTokenSource()

' Handle Ctrl+C (SIGINT).
AddHandler Console.CancelKeyPress, Sub(sender, e)
    e.Cancel = True
    Console.WriteLine(Environment.NewLine & "Received SIGINT")
    cts.Cancel()
End Sub

Console.WriteLine("awaiting signal")

Try
    Task.Delay(Timeout.Infinite, cts.Token).Wait()
Catch ex As OperationCanceledException
    ' Signal received.
End Try

Console.WriteLine("exiting gracefully")
