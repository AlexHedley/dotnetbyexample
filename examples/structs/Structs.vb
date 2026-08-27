' Structs in VB.NET group related fields together.

Structure Person
    Public Name As String
    Public Age As Integer
    
    Public Sub New(name As String, age As Integer)
        Me.Name = name
        Me.Age = age
    End Sub
    
    Public Overrides Function ToString() As String
        Return $"{{Name:{Name} Age:{Age}}}"
    End Function
End Structure

Function NewPerson(name As String) As Person
    Return New Person(name, 42)
End Function

Console.WriteLine(New Person("Bob", 20))
Console.WriteLine(New Person("Alice", 30))
Console.WriteLine(New Person("Fred", 0))
Console.WriteLine(NewPerson("Jon"))

Dim s As New Person("Sean", 50)
Console.WriteLine(s.Name)

' Structs are value types - copied on assignment.
Dim dog1 As New Person("Rex", 5)
Dim dog2 As Person = dog1
dog2.Name = "Max"
Console.WriteLine(dog1.Name)
Console.WriteLine(dog2.Name)
