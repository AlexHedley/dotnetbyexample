' Command-line flags in VB.NET.

Dim word As String = "foo"
Dim numb As Integer = 42
Dim fork As Boolean = False

Dim cmdArgs() As String = Environment.GetCommandLineArgs().Skip(1).ToArray()
For Each arg As String In cmdArgs
    If arg.StartsWith("--word=") Then
        word = arg.Substring(7)
    ElseIf arg.StartsWith("--numb=") Then
        numb = Integer.Parse(arg.Substring(7))
    ElseIf arg = "--fork" Then
        fork = True
    End If
Next

Console.WriteLine("word: " & word)
Console.WriteLine("numb: " & numb)
Console.WriteLine("fork: " & fork)
