' Command-line arguments in VB.NET.

Dim argsAll() As String = Environment.GetCommandLineArgs()
Console.WriteLine("argsAll: " & String.Join(", ", argsAll))

Dim args() As String = Environment.GetCommandLineArgs().Skip(1).ToArray()
Console.WriteLine("args: " & String.Join(", ", args))

If args.Length > 0 Then
    Console.WriteLine("arg1: " & args(0))
End If
