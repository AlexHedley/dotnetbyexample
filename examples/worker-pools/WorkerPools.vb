' Worker pools in VB.NET.
Imports System.Threading.Channels

Async Function Worker(id As Integer, jobs As ChannelReader(Of Integer), results As ChannelWriter(Of Integer)) As Task
    Await For Each j As Integer In jobs.ReadAllAsync()
        Console.WriteLine($"worker {id} started job {j}")
        Await Task.Delay(j * 1000)
        Console.WriteLine($"worker {id} finished job {j}")
        Await results.WriteAsync(j * 2)
    Next
End Function

Const NUM_JOBS As Integer = 5
Dim jobs As Channel(Of Integer) = Channel.CreateUnbounded(Of Integer)()
Dim results As Channel(Of Integer) = Channel.CreateUnbounded(Of Integer)()

Dim workers As New List(Of Task)()
For w As Integer = 1 To 3
    workers.Add(Worker(w, jobs.Reader, results.Writer))
Next

For j As Integer = 1 To NUM_JOBS
    Await jobs.Writer.WriteAsync(j)
Next
jobs.Writer.Complete()

Await Task.WhenAll(workers)
results.Writer.Complete()

Await For Each a As Integer In results.Reader.ReadAllAsync()
    Console.WriteLine("result: " & a)
Next
