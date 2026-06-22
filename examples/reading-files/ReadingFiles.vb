' Reading files in VB.NET.

' First create a temp file for reading.
Dim tempFile As String = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "dat.txt")
System.IO.File.WriteAllText(tempFile, "hello" & Environment.NewLine & "world" & Environment.NewLine)

' Read entire file as string.
Dim content As String = System.IO.File.ReadAllText(tempFile)
Console.WriteLine(content)

' Read all lines.
Dim lines() As String = System.IO.File.ReadAllLines(tempFile)
For Each line As String In lines
    Console.WriteLine(line)
Next

' Stream reading.
Using sr As New System.IO.StreamReader(tempFile)
    Dim line As String
    line = sr.ReadLine()
    While line IsNot Nothing
        Console.WriteLine(line)
        line = sr.ReadLine()
    End While
End Using

' Clean up.
System.IO.File.Delete(tempFile)
