' JSON in VB.NET.

Imports System.Text.Json

Dim map As New Dictionary(Of String, Object)() From {
    {"page", 1},
    {"fruits", New String() {"apple", "peach", "pear"}}
}

Console.WriteLine(JsonSerializer.Serialize(map))

Dim json As String = "{""num"":6.13,""strs"":[""a"",""b""]}"
Dim doc As JsonDocument = JsonDocument.Parse(json)
Console.WriteLine(doc.RootElement.GetProperty("num").GetDouble())
Console.WriteLine(doc.RootElement.GetProperty("strs")(0).GetString())
