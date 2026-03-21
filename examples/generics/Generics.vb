' .NET has had generics since VB.NET 2005. Here are some examples.

Function MapSlice(Of T, U)(lst As List(Of T), f As Func(Of T, U)) As List(Of U)
    Dim result As New List(Of U)()
    For Each item As T In lst
        result.Add(f(item))
    Next
    Return result
End Function

Class Stack(Of T)
    Private items As New List(Of T)()
    
    Public Sub Push(item As T)
        items.Add(item)
    End Sub
    
    Public Function Pop() As T
        Dim item As T = items(items.Count - 1)
        items.RemoveAt(items.Count - 1)
        Return item
    End Function
    
    Public Function IsEmpty() As Boolean
        Return items.Count = 0
    End Function
End Class

Dim s As List(Of Integer) = MapSlice(New List(Of Integer)({1, 2, 3}), Function(x As Integer) x * x)
Console.WriteLine("squares: " & String.Join(" ", s))

Dim strs As List(Of String) = MapSlice(New List(Of String)({"hello", "world"}), Function(x As String) x.ToUpper())
Console.WriteLine("upper: " & String.Join(" ", strs))

Dim st As New Stack(Of Integer)()
Console.WriteLine("isEmpty: " & st.IsEmpty())
st.Push(1)
st.Push(2)
st.Push(3)
Console.WriteLine("pop: " & st.Pop())
Console.WriteLine("isEmpty: " & st.IsEmpty())
