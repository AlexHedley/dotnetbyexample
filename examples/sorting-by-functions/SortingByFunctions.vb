' Sorting by functions in VB.NET.

Dim fruits As New List(Of String)({"peach", "kiwi", "apple"})
fruits.Sort(Function(a, b) a.Length.CompareTo(b.Length))
Console.WriteLine("By length: " & String.Join(" ", fruits))

' Using LINQ OrderBy.
Dim sortedFruits = fruits.OrderBy(Function(f) f.Length).ThenBy(Function(f) f)
Console.WriteLine("OrderBy: " & String.Join(" ", sortedFruits))

' Custom struct.
Structure Person
    Public Name As String
    Public Age As Integer
    Public Sub New(name As String, age As Integer)
        Me.Name = name
        Me.Age = age
    End Sub
End Structure

Dim people As New List(Of Person)({New Person("Alice", 30), New Person("Bob", 25), New Person("Charlie", 35)})
people.Sort(Function(a, b) a.Age.CompareTo(b.Age))
For Each p As Person In people
    Console.WriteLine(p.Name & ": " & p.Age)
Next
