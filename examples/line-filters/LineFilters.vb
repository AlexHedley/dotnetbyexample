' Line filters in VB.NET read from stdin and write to stdout.

Dim line As String = Console.ReadLine()
While line IsNot Nothing
    Console.WriteLine(line.ToUpper())
    line = Console.ReadLine()
End While
