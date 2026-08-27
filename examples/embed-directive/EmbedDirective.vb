' In VB.NET, embedded resources are marked in the project file.
' Here we simulate embedding a string directly.

Dim fileContents As String = "hello dotnet" & Environment.NewLine
Console.WriteLine(fileContents)

' In a project, use:
' Dim assembly As Assembly = Assembly.GetExecutingAssembly()
' Dim stream As Stream = assembly.GetManifestResourceStream("Namespace.file.txt")
Console.WriteLine("Embedded resource example - see project configuration for full usage.")
