// The Path class provides functions to parse and construct file paths
// in a way that is portable between operating systems.

// Combine paths using Path.Combine.
var p = Path.Combine("dir1", "dir2", "filename");
Console.WriteLine("p:", p);

// Use Path.GetDirectoryName to get the parent directory.
Console.WriteLine("Dir(p):", Path.GetDirectoryName(p));

// Use Path.GetFileName to get the filename.
Console.WriteLine("Base(p):", Path.GetFileName(p));

// Check if a path is absolute.
Console.WriteLine("IsAbs:", Path.IsPathRooted("dir/file"));
Console.WriteLine("IsAbs:", Path.IsPathRooted("/dir/file"));

// Get file extension.
var filename = "config.json";
Console.WriteLine("Ext(filename):", Path.GetExtension(filename));
Console.WriteLine("DelExt(filename):", Path.GetFileNameWithoutExtension(filename));

// Find the relative path.
var rel = Path.GetRelativePath("a/b", "a/b/t/file");
Console.WriteLine("Rel:", rel);
