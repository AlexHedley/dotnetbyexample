// Temporary files are useful for programs that need to
// create data that isn't needed after the program exits.

// Create a temporary file.
var f = Path.GetTempFileName();
Console.WriteLine("Temp file:", f);

// Write some data to the file.
File.WriteAllBytes(f, new byte[] { 1, 2, 3, 4 });
Console.WriteLine("Wrote to:", f);

// Create a temporary directory.
var dname = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
Directory.CreateDirectory(dname);
Console.WriteLine("Temp dir:", dname);

// Create a file inside the temp dir.
var fname = Path.Combine(dname, "file1");
File.WriteAllBytes(fname, new byte[] { 1, 2 });
Console.WriteLine("Created:", fname);

// Clean up.
File.Delete(f);
Directory.Delete(dname, recursive: true);
