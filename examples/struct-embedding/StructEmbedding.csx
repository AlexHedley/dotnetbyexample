// .NET supports struct embedding via composition and inheritance.
// While Go has struct embedding, C# achieves similar results through
// class inheritance and composition.

// A base type.
class Base
{
    public int Num { get; set; }
    
    public string Describe()
    {
        return $"base with num={Num}";
    }
}

// A Container that embeds (inherits from) Base.
class Container : Base
{
    public string Str { get; set; } = "";
}

// When using instances of Container, methods from Base are accessible.
var co = new Container { Num = 1, Str = "some name" };

// We can access the base's fields directly on co.
Console.WriteLine("co={Num:", co.Num, ", Str:", co.Str, "}");

// We can also access the full interface of the embedded type on co.
Console.WriteLine("describe:", co.Describe());
Console.WriteLine("descr:", co.Describe());

// Interfaces can also be embedded/implemented.
interface IDescriber
{
    string Describe();
}

// Container implicitly implements IDescriber through inheritance.
IDescriber d = co;
Console.WriteLine("describer:", d.Describe());
