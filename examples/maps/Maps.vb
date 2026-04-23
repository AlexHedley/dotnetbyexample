' Maps in VB.NET use Dictionary(Of K, V).

' Create an empty dictionary.
Dim m As New Dictionary(Of String, Integer)()

' Set key/value pairs.
m("k1") = 7
m("k2") = 13

Console.WriteLine("map: " & m("k1") & " " & m("k2"))

' Get a value for a key.
Dim v1 As Integer = m("k1")
Console.WriteLine("v1: " & v1)

' Check if key exists.
Dim v3 As Integer
If m.TryGetValue("k3", v3) Then
    Console.WriteLine("v3: " & v3)
Else
    Console.WriteLine("v3: 0")
End If

Console.WriteLine("len: " & m.Count)

' Remove a key.
m.Remove("k2")
Console.WriteLine("map: " & m.Count)

' Check if key exists.
Dim prs As Boolean = m.ContainsKey("k2")
Console.WriteLine("prs: " & prs)

' Initialize with values.
Dim n As New Dictionary(Of String, Integer) From {{"foo", 1}, {"bar", 2}}
Console.WriteLine("map: " & n("foo") & " " & n("bar"))
