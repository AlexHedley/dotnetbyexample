' Stateful goroutines in VB.NET use channels or actor pattern.
Imports System.Threading.Channels

' Simple example: owned state via a dedicated task.
Dim reads As Channel(Of (String, TaskCompletionSource(Of Integer))) = Channel.CreateUnbounded(Of (String, TaskCompletionSource(Of Integer)))()
Dim writes As Channel(Of (String, Integer, TaskCompletionSource(Of Boolean))) = Channel.CreateUnbounded(Of (String, Integer, TaskCompletionSource(Of Boolean)))()

Dim state As New Dictionary(Of String, Integer)()
Dim stateTask As Task = Task.Run(Async Function()
    While True
        Dim readTask = reads.Reader.ReadAsync().AsTask()
        Dim writeTask = writes.Reader.ReadAsync().AsTask()
        Dim completed As Task = Await Task.WhenAny(readTask, writeTask)
        
        If completed Is readTask Then
            Dim read = Await readTask
            read.Item2.SetResult(If(state.ContainsKey(read.Item1), state(read.Item1), 0))
        Else
            Dim write = Await writeTask
            state(write.Item1) = write.Item2
            write.Item3.SetResult(True)
        End If
    End While
End Function)

Console.WriteLine("Stateful goroutine pattern implemented")
