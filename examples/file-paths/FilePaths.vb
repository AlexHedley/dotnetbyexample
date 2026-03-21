' File paths in VB.NET.

Dim p As String = Path.Combine("dir1", "dir2", "filename")
Console.WriteLine("p: " & p)

Console.WriteLine("Dir(p): " & Path.GetDirectoryName(p))
Console.WriteLine("Base(p): " & Path.GetFileName(p))

Console.WriteLine("IsAbs: " & Path.IsPathRooted("dir/file"))
Console.WriteLine("IsAbs: " & Path.IsPathRooted("/dir/file"))

Dim filename As String = "config.json"
Console.WriteLine("Ext(filename): " & Path.GetExtension(filename))
Console.WriteLine("DelExt(filename): " & Path.GetFileNameWithoutExtension(filename))
