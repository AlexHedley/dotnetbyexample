// .NET supports methods defined on struct and class types.

// This Rect struct type has area and perim methods.
struct Rect
{
    public double Width, Height;
    
    public Rect(double width, double height)
    {
        Width = width;
        Height = height;
    }

    // Methods in C# are defined inside the type declaration.
    // This method is on a value type (struct) - structs are value types.
    public double Area()
    {
        return Width * Height;
    }

    public double Perim()
    {
        return 2 * Width + 2 * Height;
    }
    
    public override string ToString() => $"{{Width:{Width}, Height:{Height}}}";
}

var r = new Rect(3, 4);

// Call the two methods defined for our struct.
Console.WriteLine("area:", r.Area());
Console.WriteLine("perim:", r.Perim());

// .NET automatically handles method calls on both value and reference types.
// (No explicit pointer dereference needed like in Go.)
Console.WriteLine("area via ref:", r.Area());
