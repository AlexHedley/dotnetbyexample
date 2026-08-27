' Channel directions in VB.NET use ChannelReader/ChannelWriter.
Imports System.Threading.Channels

Async Function Ping(pings As ChannelWriter(Of String), msg As String) As Task
    Await pings.WriteAsync(msg)
End Function

Async Function Pong(pings As ChannelReader(Of String), pongs As ChannelWriter(Of String)) As Task
    Dim msg As String = Await pings.ReadAsync()
    Await pongs.WriteAsync(msg)
End Function

Dim pings As Channel(Of String) = Channel.CreateUnbounded(Of String)()
Dim pongs As Channel(Of String) = Channel.CreateUnbounded(Of String)()

Await Ping(pings.Writer, "passed message")
Await Pong(pings.Reader, pongs.Writer)

Console.WriteLine(Await pongs.Reader.ReadAsync())
