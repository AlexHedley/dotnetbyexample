' Temporary files and directories in VB.NET.

Dim f As String = Path.GetTempFileName()
Console.WriteLine("Temp file: " & f)

File.WriteAllBytes(f, New Byte() {1, 2, 3, 4})

Dim dname As String = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName())
Directory.CreateDirectory(dname)
Console.WriteLine("Temp dir: " & dname)

File.Delete(f)
Directory.Delete(dname, recursive:=True)
