' In .NET VB.NET, variables are explicitly declared and used by
' the compiler to check type-correctness of function calls etc.

' Dim declares a variable with a specific type.
Dim a As String = "initial"
Console.WriteLine(a)

' You can declare multiple variables at once.
Dim b As Integer = 1
Dim c As Integer = 2
Console.WriteLine(b & " " & c)

' VB.NET can infer the type with Option Infer.
Dim d = True
Console.WriteLine(d)

' Variables declared without initialization are zero-valued.
Dim e As Integer
Console.WriteLine(e)

' Explicit type declaration.
Dim f As String = "apple"
Console.WriteLine(f)
