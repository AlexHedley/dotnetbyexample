' Directories in VB.NET.

Directory.CreateDirectory("subdir_vb")
System.IO.File.WriteAllText(Path.Combine("subdir_vb", "file1.txt"), "")

For Each entry As String In Directory.GetFileSystemEntries("subdir_vb")
    Console.WriteLine(entry)
Next

Console.WriteLine("Current: " & Directory.GetCurrentDirectory())

Directory.Delete("subdir_vb", recursive:=True)
