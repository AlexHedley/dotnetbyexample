' Custom exceptions in VB.NET extend the Exception class.

Class ValidationError
    Inherits Exception
    
    Public ReadOnly Property Field As String
    
    Public Sub New(field As String, message As String)
        MyBase.New(message)
        Me.Field = field
    End Sub
    
    Public Overrides Function ToString() As String
        Return $"ValidationError: field={Field}, message={Message}"
    End Function
End Class

Class NotFoundError
    Inherits Exception
    
    Public ReadOnly Property Id As Integer
    
    Public Sub New(id As Integer)
        MyBase.New($"Resource with id {id} not found")
        Me.Id = id
    End Sub
End Class

Function GetUser(id As Integer) As String
    If id <= 0 Then
        Throw New ValidationError("id", "must be positive")
    End If
    If id > 100 Then
        Throw New NotFoundError(id)
    End If
    Return $"user_{id}"
End Function

Sub TryGetUser(id As Integer)
    Try
        Dim user As String = GetUser(id)
        Console.WriteLine($"Found: {user}")
    Catch ve As ValidationError
        Console.WriteLine($"Validation error on field '{ve.Field}': {ve.Message}")
    Catch nfe As NotFoundError
        Console.WriteLine($"Not found: id={nfe.Id}")
    End Try
End Sub

TryGetUser(1)
TryGetUser(-1)
TryGetUser(999)
