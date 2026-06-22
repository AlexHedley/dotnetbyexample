// .NET has several useful functions for working with directories.

// Create a new subdirectory in the current directory.
Directory.CreateDirectory("subdir");

// Create a file in the new directory.
File.WriteAllText(Path.Combine("subdir", "file1"), "");

// Create a hierarchy of directories.
Directory.CreateDirectory(Path.Combine("subdir", "parent", "child"));
File.WriteAllText(Path.Combine("subdir", "parent", "file2"), "");
File.WriteAllText(Path.Combine("subdir", "parent", "child", "file3"), "");

// List directory contents.
void Visit(string path)
{
    foreach (var entry in Directory.GetFileSystemEntries(path))
    {
        Console.WriteLine(entry);
    }
}
Visit("subdir");

// Visit every file recursively.
foreach (var file in Directory.GetFiles("subdir", "*", SearchOption.AllDirectories))
{
    Console.WriteLine(file);
}

// Change directory.
var curr = Directory.GetCurrentDirectory();
Console.WriteLine("Current:", curr);

// Clean up.
Directory.Delete("subdir", recursive: true);
