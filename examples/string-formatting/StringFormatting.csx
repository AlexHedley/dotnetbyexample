// .NET offers several ways to format strings.
// String interpolation ($""), string.Format, Console.WriteLine, etc.

// Basic string formatting.
Console.WriteLine($"{"golang":10}");

// Width and precision.
Console.WriteLine($"{3.14159:F2}");

// Padding.
Console.WriteLine($"|{"left",-10}|");
Console.WriteLine($"|{"right",10}|");
Console.WriteLine($"|{12345,10}|");
Console.WriteLine($"|{12345,-10}|");

// Boolean formatting.
Console.WriteLine($"{true}");

// Hex formatting.
Console.WriteLine($"{255:X}");
Console.WriteLine($"{255:x2}");

// Using string.Format.
Console.WriteLine(string.Format("{0:D5}", 12));

// Using string interpolation with expressions.
var name = "World";
Console.WriteLine($"Hello, {name}!");

// Verbose format (shows object structure).
var p = new { Name = "test", Value = 42 };
Console.WriteLine($"struct: {{Name: {p.Name}, Value: {p.Value}}}");
