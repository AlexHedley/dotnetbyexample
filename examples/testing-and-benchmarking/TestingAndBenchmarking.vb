' Testing in VB.NET uses xUnit, NUnit, or MSTest.
' Here's a simple manual test example.

Function IntMin(a As Integer, b As Integer) As Integer
    Return If(a < b, a, b)
End Function

Sub TestIntMin()
    Dim ans As Integer = IntMin(2, -2)
    If ans <> -2 Then
        Throw New Exception($"IntMin(2, -2) = {ans}; want -2")
    End If
    Console.WriteLine("TestIntMin passed")
End Sub

TestIntMin()
Console.WriteLine("All tests passed!")
