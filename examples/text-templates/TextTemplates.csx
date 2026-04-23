// .NET provides text templating via various options.
// Here we'll use simple string interpolation and StringBuilder.

using System.Text;

// Simple template using string interpolation.
string name = "World";
string greeting = $"Hello, {name}!";
Console.WriteLine(greeting);

// Template with multiple values using a custom render function.
string RenderTemplate(string template, Dictionary<string, string> vars)
{
    var result = new StringBuilder(template);
    foreach (var (key, value) in vars)
    {
        result.Replace("{{" + key + "}}", value);
    }
    return result.ToString();
}

var vars = new Dictionary<string, string>
{
    { "Name", "Alice" },
    { "Age", "30" }
};

string template = "Name: {{Name}}, Age: {{Age}}";
Console.WriteLine(RenderTemplate(template, vars));

// Using StringBuilder for complex templates.
var sb = new StringBuilder();
sb.AppendLine("Items:");
var items = new[] { "apple", "banana", "cherry" };
foreach (var item in items)
{
    sb.AppendLine($"  - {item}");
}
Console.Write(sb.ToString());
