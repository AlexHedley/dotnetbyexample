' Writing files in VB.NET.

System.IO.File.WriteAllText("/tmp/dat1.txt", "hello" & Environment.NewLine & "go" & Environment.NewLine)

Using sw As New System.IO.StreamWriter("/tmp/dat2.txt")
    sw.WriteLine("some")
    sw.WriteLine("writes")
    sw.Write("buffered")
    sw.Flush()
End Using

Console.WriteLine(System.IO.File.ReadAllText("/tmp/dat1.txt"))
Console.WriteLine(System.IO.File.ReadAllText("/tmp/dat2.txt"))
