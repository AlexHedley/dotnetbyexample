' Variadic functions in VB.NET use ParamArray.

Function Sum(ParamArray nums() As Integer) As Integer
    Dim total As Integer = 0
    For Each num As Integer In nums
        total += num
    Next
    Return total
End Function

Console.WriteLine(Sum(1, 2))
Console.WriteLine(Sum(1, 2, 3))

' Pass an array directly.
Dim nums() As Integer = {1, 2, 3, 4}
Console.WriteLine(Sum(nums))
