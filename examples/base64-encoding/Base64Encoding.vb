' Base64 encoding in VB.NET.

Imports System.Text

Dim data As String = "abc123!?$*&()'-=@~"

Dim encoded As String = Convert.ToBase64String(Encoding.UTF8.GetBytes(data))
Console.WriteLine(encoded)

Dim decoded As String = Encoding.UTF8.GetString(Convert.FromBase64String(encoded))
Console.WriteLine(decoded)
