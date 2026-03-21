' Interfaces in VB.NET are named collections of method signatures.

Interface IGeometry
    Function Area() As Double
    Function Perim() As Double
End Interface

Class Rect
    Implements IGeometry
    
    Public Width As Double
    Public Height As Double
    
    Public Sub New(width As Double, height As Double)
        Me.Width = width
        Me.Height = height
    End Sub
    
    Public Function Area() As Double Implements IGeometry.Area
        Return Width * Height
    End Function
    
    Public Function Perim() As Double Implements IGeometry.Perim
        Return 2 * Width + 2 * Height
    End Function
End Class

Class Circle
    Implements IGeometry
    
    Public Radius As Double
    
    Public Sub New(radius As Double)
        Me.Radius = radius
    End Sub
    
    Public Function Area() As Double Implements IGeometry.Area
        Return Math.PI * Radius * Radius
    End Function
    
    Public Function Perim() As Double Implements IGeometry.Perim
        Return 2 * Math.PI * Radius
    End Function
End Class

Sub Measure(g As IGeometry)
    Console.WriteLine(g)
    Console.WriteLine(g.Area())
    Console.WriteLine(g.Perim())
End Sub

Dim r As New Rect(3, 4)
Dim c As New Circle(5)

Measure(r)
Measure(c)
