' Text templates in VB.NET.

Dim name As String = "World"
Dim greeting As String = $"Hello, {name}!"
Console.WriteLine(greeting)

Function RenderTemplate(template As String, vars As Dictionary(Of String, String)) As String
    Dim result As New System.Text.StringBuilder(template)
    For Each kv As KeyValuePair(Of String, String) In vars
        result.Replace("{{" & kv.Key & "}}", kv.Value)
    Next
    Return result.ToString()
End Function

Dim vars As New Dictionary(Of String, String)() From {{"Name", "Alice"}, {"Age", "30"}}
Dim template As String = "Name: {{Name}}, Age: {{Age}}"
Console.WriteLine(RenderTemplate(template, vars))

Dim sb As New System.Text.StringBuilder()
sb.AppendLine("Items:")
Dim items() As String = {"apple", "banana", "cherry"}
For Each item As String In items
    sb.AppendLine("  - " & item)
Next
Console.Write(sb.ToString())
