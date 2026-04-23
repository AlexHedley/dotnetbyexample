' In VB.NET, errors are represented by exceptions.

Function Divide(a As Integer, b As Integer) As Integer
    If b = 0 Then
        Throw New DivideByZeroException("Cannot divide by zero")
    End If
    Return a \ b
End Function

Try
    Dim result As Integer = Divide(10, 2)
    Console.WriteLine("10 / 2 = " & result)
Catch e As DivideByZeroException
    Console.WriteLine("Error: " & e.Message)
End Try

Try
    Dim result As Integer = Divide(10, 0)
    Console.WriteLine("10 / 0 = " & result)
Catch e As DivideByZeroException
    Console.WriteLine("Error: " & e.Message)
End Try
