' VB.NET supports recursive functions.

Function Fact(n As Integer) As Integer
    If n = 0 Then
        Return 1
    End If
    Return n * Fact(n - 1)
End Function

Console.WriteLine(Fact(7))

' Fibonacci using recursion.
Function Fib(n As Integer) As Integer
    If n < 2 Then
        Return n
    End If
    Return Fib(n - 1) + Fib(n - 2)
End Function

Console.WriteLine(Fib(7))
