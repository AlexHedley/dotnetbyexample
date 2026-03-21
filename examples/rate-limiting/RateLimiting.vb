' Rate limiting in VB.NET.
Imports System.Threading.Channels

Dim requests As Channel(Of Integer) = Channel.CreateUnbounded(Of Integer)()
For i As Integer = 1 To 5
    Await requests.Writer.WriteAsync(i)
Next
requests.Writer.Complete()

Dim limiter As Channel(Of DateTime) = Channel.CreateUnbounded(Of DateTime)()

Task.Run(Async Function()
    Dim ticker As New PeriodicTimer(TimeSpan.FromMilliseconds(200))
    While Await ticker.WaitForNextTickAsync()
        If Not limiter.Writer.TryWrite(DateTime.Now) Then
            Exit While
        End If
    End While
End Function)

Await For Each req As Integer In requests.Reader.ReadAllAsync()
    Await limiter.Reader.ReadAsync()
    Console.WriteLine("request " & req & " " & DateTime.Now)
Next
