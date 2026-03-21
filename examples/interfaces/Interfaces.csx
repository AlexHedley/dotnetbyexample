// Interfaces are named collections of method signatures in .NET.

// Here's a basic interface for geometric shapes.
interface IGeometry
{
    double Area();
    double Perim();
}

// For our example we'll implement this interface on Rect and Circle types.
class Rect : IGeometry
{
    public double Width, Height;
    
    public Rect(double width, double height)
    {
        Width = width;
        Height = height;
    }
    
    public double Area() => Width * Height;
    public double Perim() => 2 * Width + 2 * Height;
}

class Circle : IGeometry
{
    public double Radius;
    
    public Circle(double radius)
    {
        Radius = radius;
    }
    
    public double Area() => Math.PI * Radius * Radius;
    public double Perim() => 2 * Math.PI * Radius;
}

// If a variable has an interface type, then we can call methods
// that are in the named interface.
void Measure(IGeometry g)
{
    Console.WriteLine(g);
    Console.WriteLine(g.Area());
    Console.WriteLine(g.Perim());
}

var r = new Rect(3, 4);
var c = new Circle(5);

// The Circle and Rect struct types both implement the IGeometry interface
// so we can use instances of these structs as arguments to Measure.
Measure(r);
Measure(c);
