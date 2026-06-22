' Non-blocking channel operations in VB.NET.
Imports System.Threading.Channels

Dim messages As Channel(Of String) = Channel.CreateUnbounded(Of String)()
Dim signals As Channel(Of Boolean) = Channel.CreateUnbounded(Of Boolean)()

Dim msg As String = Nothing
If messages.Reader.TryRead(msg) Then
    Console.WriteLine("received message: " & msg)
Else
    Console.WriteLine("no message received")
End If

Dim sent As Boolean = messages.Writer.TryWrite("hi")
Console.WriteLine("sent: " & sent)
