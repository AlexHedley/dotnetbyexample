' Switch statements in VB.NET use Select Case.

Dim i As Integer = 2
Console.Write("Write " & i & " as ")
Select Case i
    Case 1
        Console.WriteLine("one")
    Case 2
        Console.WriteLine("two")
    Case 3
        Console.WriteLine("three")
End Select

' You can use commas to separate multiple values in the same case.
Select Case DateTime.Now.DayOfWeek
    Case DayOfWeek.Saturday, DayOfWeek.Sunday
        Console.WriteLine("It's the weekend")
    Case Else
        Console.WriteLine("It's a weekday")
End Select

' Select Case without an expression (using True).
Dim t As Integer = DateTime.Now.Hour
Select Case True
    Case t < 12
        Console.WriteLine("It's before noon")
    Case Else
        Console.WriteLine("It's after noon")
End Select

' Type comparison using TypeOf.
Dim whatAmI As Object = True
Dim result As String
If TypeOf whatAmI Is Boolean Then
    result = "bool: " & whatAmI.ToString()
ElseIf TypeOf whatAmI Is Integer Then
    result = "int: " & whatAmI.ToString()
ElseIf TypeOf whatAmI Is String Then
    result = "string: " & whatAmI.ToString()
Else
    result = "unknown type"
End If
Console.WriteLine(result)
