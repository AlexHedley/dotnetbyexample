' In VB.NET, panic is equivalent to throwing an exception.

Sub Panic(msg As String)
    Throw New InvalidOperationException(msg)
End Sub

Try
    Panic("a problem")
Catch e As InvalidOperationException
    Console.WriteLine("Caught panic: " & e.Message)
End Try
