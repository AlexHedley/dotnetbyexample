' WaitGroups in VB.NET use CountdownEvent or Task.WhenAll.

Sub Worker(id As Integer, wg As CountdownEvent)
    Console.WriteLine("Worker " & id & " starting")
    Thread.Sleep(id * 1000)
    Console.WriteLine("Worker " & id & " done")
    wg.Signal()
End Sub

Dim numWorkers As Integer = 5
Dim wg As New CountdownEvent(numWorkers)

For i As Integer = 1 To numWorkers
    Dim id As Integer = i
    Task.Run(Sub() Worker(id, wg))
Next

wg.Wait()
Console.WriteLine("All workers done")
