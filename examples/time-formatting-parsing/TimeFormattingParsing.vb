' Time formatting/parsing in VB.NET.

Dim t As New DateTime(2012, 11, 1, 22, 8, 41, DateTimeKind.Utc)

Console.WriteLine(t.ToString("o"))
Console.WriteLine(t.ToString("yyyy-MM-dd HH:mm:ss"))
Console.WriteLine(t.ToString("R"))

Dim t1 As DateTime = DateTime.Parse("2012-11-01 22:08:41")
Console.WriteLine(t1)

Dim t3 As DateTime
If Not DateTime.TryParse("invalid date", t3) Then
    Console.WriteLine("Parse error")
End If
