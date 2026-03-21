// In .NET, embedded resources can be accessed via
// Assembly.GetManifestResourceStream or the [EmbeddedResource] build action.
// In .csx scripts, we can embed data directly or use relative file paths.

using System.Reflection;

// Embed a string directly (equivalent to Go's //go:embed for text).
var fileContents = "hello dotnet\n";
Console.WriteLine(fileContents);

// In a full project, you would mark a file as EmbeddedResource in the .csproj:
// <EmbeddedResource Include="file.txt" />
// Then access it like this:
// var assembly = Assembly.GetExecutingAssembly();
// using var stream = assembly.GetManifestResourceStream("Namespace.file.txt");
// using var reader = new StreamReader(stream!);
// Console.WriteLine(reader.ReadToEnd());

// In .NET 6+, you can use [EmbeddedFile] attribute in source generators.
// The standard approach is to use Embedded Resources in the project file.
Console.WriteLine("Embedded resource example - see project configuration for full usage.");
