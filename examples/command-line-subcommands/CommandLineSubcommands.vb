' Command-line subcommands in VB.NET.

Dim cmdArgs() As String = Environment.GetCommandLineArgs().Skip(1).ToArray()
If cmdArgs.Length = 0 Then
    Console.WriteLine("expected 'foo' or 'bar' subcommand")
    Environment.Exit(1)
End If

Select Case cmdArgs(0)
    Case "foo"
        Console.WriteLine("subcommand 'foo'")
    Case "bar"
        Console.WriteLine("subcommand 'bar'")
    Case Else
        Console.Error.WriteLine("unknown subcommand: " & cmdArgs(0))
        Environment.Exit(1)
End Select
