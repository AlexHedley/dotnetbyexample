' Channel synchronization in VB.NET.
Imports System.Threading.Channels

Dim done As Channel(Of Boolean) = Channel.CreateUnbounded(Of Boolean)()

Async Function Worker() As Task
    Console.WriteLine("working...")
    Await Task.Delay(1000)
    Console.WriteLine("done")
    Await done.Writer.WriteAsync(True)
End Function

Task.Run(AddressOf Worker)

Await done.Reader.ReadAsync()
