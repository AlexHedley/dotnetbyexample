' Methods in VB.NET are defined inside type declarations.

Structure Rect
    Public Width As Double
    Public Height As Double
    
    Public Sub New(width As Double, height As Double)
        Me.Width = width
        Me.Height = height
    End Sub
    
    Public Function Area() As Double
        Return Width * Height
    End Function
    
    Public Function Perim() As Double
        Return 2 * Width + 2 * Height
    End Function
    
    Public Overrides Function ToString() As String
        Return $"{{Width:{Width}, Height:{Height}}}"
    End Function
End Structure

Dim r As New Rect(3, 4)

Console.WriteLine("area: " & r.Area())
Console.WriteLine("perim: " & r.Perim())
