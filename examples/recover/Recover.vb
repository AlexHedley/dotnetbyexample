' Recover in VB.NET uses Try/Catch.

Sub SafeDivide(a As Integer, b As Integer)
    Try
        Console.WriteLine(a \ b)
    Catch e As DivideByZeroException
        Console.WriteLine("Recovered: " & e.Message)
    End Try
End Sub

Sub MayPanic()
    Throw New InvalidOperationException("a problem")
End Sub

Sub SafeDiv()
    Try
        MayPanic()
    Catch e As Exception
        Console.WriteLine("Recovered. Error: " & e.Message)
    End Try
    Console.WriteLine("After SafeDiv")
End Sub

SafeDivide(10, 2)
SafeDivide(10, 0)
SafeDiv()
