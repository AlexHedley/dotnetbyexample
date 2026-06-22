' VB.NET achieves struct embedding through class inheritance.

Class Base
    Public Property Num As Integer
    
    Public Function Describe() As String
        Return $"base with num={Num}"
    End Function
End Class

Class Container
    Inherits Base
    
    Public Property Str As String = ""
End Class

Dim co As New Container With {.Num = 1, .Str = "some name"}

Console.WriteLine("co={{Num: " & co.Num & ", Str: " & co.Str & "}}")
Console.WriteLine("describe: " & co.Describe())

Interface IDescriber
    Function Describe() As String
End Interface

' Note: Container inherits Describe from Base.
' To use as IDescriber, create a wrapper or ensure Base implements it.
