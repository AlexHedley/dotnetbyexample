' In VB.NET, strings are sequences of UTF-16 Char values.

Dim s As String = "สวัสดี"

' Length returns the number of UTF-16 code units.
Console.WriteLine("Len: " & s.Length)

' Iterate over chars.
For i As Integer = 0 To s.Length - 1
    Console.Write(AscW(s(i)).ToString("X4") & " ")
Next
Console.WriteLine()

' Use StringInfo for Unicode text elements.
Dim si As New System.Globalization.StringInfo(s)
Console.WriteLine("Text element count: " & si.LengthInTextElements)

' Enumerate text elements.
Dim enumerator = System.Globalization.StringInfo.GetTextElementEnumerator(s)
Dim idx As Integer = 0
While enumerator.MoveNext()
    Dim textElement As String = enumerator.GetTextElement()
    Console.WriteLine(idx & " '" & textElement & "'")
    idx += textElement.Length
End While
