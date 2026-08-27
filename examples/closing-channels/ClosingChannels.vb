' Closing channels in VB.NET.
Imports System.Threading.Channels

Const JOBS As Integer = 5
Dim jobs As Channel(Of Integer) = Channel.CreateUnbounded(Of Integer)()
Dim done As Channel(Of Boolean) = Channel.CreateUnbounded(Of Boolean)()

Task.Run(Async Function()
    Await For Each j As Integer In jobs.Reader.ReadAllAsync()
        Console.WriteLine("received job " & j)
    Next
    Console.WriteLine("received all jobs")
    Await done.Writer.WriteAsync(True)
End Function)

For j As Integer = 1 To JOBS
    Await jobs.Writer.WriteAsync(j)
    Console.WriteLine("sent job " & j)
Next
jobs.Writer.Complete()
Console.WriteLine("sent all jobs")

Await done.Reader.ReadAsync()
