' Functions in VB.NET are called Functions (return value) or Subs (no return).

' A Function that takes two integers and returns their sum.
Function Plus(a As Integer, b As Integer) As Integer
    Return a + b
End Function

Function PlusPlus(a As Integer, b As Integer, c As Integer) As Integer
    Return a + b + c
End Function

' Call functions.
Dim res As Integer = Plus(1, 2)
Console.WriteLine("1+2 = " & res)

Dim res2 As Integer = PlusPlus(1, 2, 3)
Console.WriteLine("1+2+3 = " & res2)
