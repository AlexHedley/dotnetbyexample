' Mutexes in VB.NET use the lock keyword (SyncLock).

Class SafeCounter
    Private _mutex As New Object()
    Private _state As New Dictionary(Of String, Integer)()
    
    Public Sub Inc(key As String)
        SyncLock _mutex
            If _state.ContainsKey(key) Then
                _state(key) += 1
            Else
                _state(key) = 1
            End If
        End SyncLock
    End Sub
    
    Public Function Value(key As String) As Integer
        SyncLock _mutex
            Return If(_state.ContainsKey(key), _state(key), 0)
        End SyncLock
    End Function
End Class

Dim c As New SafeCounter()
Dim tasks As New List(Of Task)()

For i As Integer = 0 To 999
    tasks.Add(Task.Run(Sub() c.Inc("somekey")))
Next

Await Task.WhenAll(tasks)
Console.WriteLine(c.Value("somekey"))
