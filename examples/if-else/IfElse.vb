' Branching with If/Else in VB.NET is straightforward.

' Here's a basic example.
Dim num As Integer = 9
If num < 0 Then
    Console.WriteLine(num & " is negative")
ElseIf num < 10 Then
    Console.WriteLine(num & " has 1 digit")
Else
    Console.WriteLine(num & " has multiple digits")
End If

' You can have an If statement without an Else.
If num Mod 2 = 0 Then
    Console.WriteLine("even")
End If

' A statement can precede conditionals.
Dim num2 As Integer = 7
If num2 Mod 2 = 0 Then
    Console.WriteLine(num2 & " is even")
Else
    Console.WriteLine(num2 & " is odd")
End If

Console.WriteLine("done")
