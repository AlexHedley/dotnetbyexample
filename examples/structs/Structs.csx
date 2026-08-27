// .NET's structs are typed collections of fields.
// They're useful for grouping data together to form records.

// This person struct type has name and age fields.
record struct Person(string Name, int Age);

// NewPerson constructs a new person struct with the given name.
Person NewPerson(string name)
{
    // You can safely return a struct by value.
    return new Person(name, 42);
}

// This syntax creates a new struct.
Console.WriteLine(new Person("Bob", 20));

// You can name the fields when initializing a struct.
Console.WriteLine(new Person(Name: "Alice", Age: 30));

// Omitted fields will be zero-valued.
Console.WriteLine(new Person("Fred", 0));

// Use an & prefix to yield a pointer to the struct.
// In C#, use ref or create a reference type (class) instead.
var ann = new Person("Ann", 40);
Console.WriteLine(ann);

// It's idiomatic to encapsulate new struct creation in constructor functions.
Console.WriteLine(NewPerson("Jon"));

// Access struct fields with a dot.
var s = new Person("Sean", 50);
Console.WriteLine(s.Name);

// Structs are mutable (value types are copied on assignment by default).
var sp = s with { };
sp = sp with { Name = "Sean" };
Console.WriteLine(sp.Name);

// Structs are value types in .NET - they are copied when assigned.
var dog1 = new Person("Rex", 5);
var dog2 = dog1;
dog2 = dog2 with { Name = "Max" };
Console.WriteLine(dog1.Name);
Console.WriteLine(dog2.Name);
