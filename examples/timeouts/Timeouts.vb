' Timeouts in VB.NET use CancellationToken.
Imports System.Threading.Channels

Dim c1 As Channel(Of String) = Channel.CreateUnbounded(Of String)()
Dim c2 As Channel(Of String) = Channel.CreateUnbounded(Of String)()

Task.Run(Async Function()
    Await Task.Delay(2000)
    Await c1.Writer.WriteAsync("result 1")
End Function)

Dim cts1 As New CancellationTokenSource(TimeSpan.FromSeconds(1))
Try
    Dim result As String = Await c1.Reader.ReadAsync(cts1.Token)
    Console.WriteLine(result)
Catch ex As OperationCanceledException
    Console.WriteLine("timeout 1")
End Try

Task.Run(Async Function()
    Await Task.Delay(200)
    Await c2.Writer.WriteAsync("result 2")
End Function)

Dim cts2 As New CancellationTokenSource(TimeSpan.FromSeconds(3))
Try
    Dim result As String = Await c2.Reader.ReadAsync(cts2.Token)
    Console.WriteLine(result)
Catch ex As OperationCanceledException
    Console.WriteLine("timeout 2")
End Try
