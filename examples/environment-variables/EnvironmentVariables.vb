' Environment variables in VB.NET.

Environment.SetEnvironmentVariable("FOO", "1")
Console.WriteLine("FOO: " & (Environment.GetEnvironmentVariable("FOO") ?? "(not set)"))
Console.WriteLine("BAR: " & (Environment.GetEnvironmentVariable("BAR") ?? "(not set)"))

' List all env vars (first few).
Dim count As Integer = 0
For Each de As System.Collections.DictionaryEntry In Environment.GetEnvironmentVariables()
    Console.WriteLine(de.Key & ": " & de.Value)
    count += 1
    If count >= 5 Then Exit For
Next
