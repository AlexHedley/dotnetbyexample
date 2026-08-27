' .NET VB.NET supports constants via the Const keyword.

' Const declares a constant value.
Const s As String = "constant"

Console.WriteLine(s)

' A Const statement can appear anywhere a Dim statement can.
Const n As Integer = 500000000

' Constant expressions.
Dim d As Double = 3.0E+20 / n
Console.WriteLine(d)

Console.WriteLine(CLng(d))

Console.WriteLine(Math.Sin(n))
