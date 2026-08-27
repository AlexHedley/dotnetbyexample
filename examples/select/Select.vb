' Select equivalent in VB.NET uses Task.WhenAny.
Imports System.Threading.Channels

Dim c1 As Channel(Of String) = Channel.CreateUnbounded(Of String)()
Dim c2 As Channel(Of String) = Channel.CreateUnbounded(Of String)()

Task.Run(Async Function()
    Await Task.Delay(1000)
    Await c1.Writer.WriteAsync("one")
End Function)

Task.Run(Async Function()
    Await Task.Delay(2000)
    Await c2.Writer.WriteAsync("two")
End Function)

For i As Integer = 0 To 1
    Dim t1 As Task(Of String) = c1.Reader.ReadAsync().AsTask()
    Dim t2 As Task(Of String) = c2.Reader.ReadAsync().AsTask()
    
    Dim completed As Task = Await Task.WhenAny(t1, t2)
    
    If completed Is t1 Then
        Console.WriteLine("Received " & t1.Result)
    Else
        Console.WriteLine("Received " & t2.Result)
    End If
Next
