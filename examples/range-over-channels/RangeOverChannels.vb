' Range over channels in VB.NET.
Imports System.Threading.Channels

Dim queue As Channel(Of String) = Channel.CreateUnbounded(Of String)()

Await queue.Writer.WriteAsync("one")
Await queue.Writer.WriteAsync("two")
queue.Writer.Complete()

Await For Each elem As String In queue.Reader.ReadAllAsync()
    Console.WriteLine(elem)
Next
