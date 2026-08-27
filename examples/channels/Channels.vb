' In VB.NET, channels use System.Threading.Channels.
' Run with: dotnet script Channels.vb
Imports System.Threading.Channels

Dim ch As Channel(Of String) = Channel.CreateUnbounded(Of String)()

' Send a value from a separate task.
Task.Run(Async Function()
    Await ch.Writer.WriteAsync("ping")
End Function)

' Read the value.
Dim msg As String = Await ch.Reader.ReadAsync()
Console.WriteLine(msg)
