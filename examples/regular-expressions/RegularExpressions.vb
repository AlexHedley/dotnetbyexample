' Regular expressions in VB.NET.

Imports System.Text.RegularExpressions

Dim matched As Boolean = Regex.IsMatch("peach punch", "p([a-z]+)ch")
Console.WriteLine(matched)

Dim r As New Regex("p([a-z]+)ch")
Console.WriteLine(r.Match("peach punch").Value)

Dim m As Match = r.Match("peach punch")
Console.WriteLine(m.Groups(0).Value)
Console.WriteLine(m.Groups(1).Value)

Dim matches As MatchCollection = r.Matches("peach punch pinch")
Console.WriteLine("matches: " & matches.Count)
For Each match As Match In matches
    Console.WriteLine(match.Value)
Next

Console.WriteLine(r.Replace("a peach", "<fruit>"))
Console.WriteLine(r.Replace("a peach", Function(mch) mch.Value.ToUpper()))
