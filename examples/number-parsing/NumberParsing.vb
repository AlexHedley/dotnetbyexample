' Number parsing in VB.NET.

Dim f As Single = Single.Parse("1.234")
Console.WriteLine(f)

Dim i As Integer = Integer.Parse("123")
Console.WriteLine(i)

Dim d As Long = Convert.ToInt64("1c8", 16)
Console.WriteLine(d)

Console.WriteLine(Integer.Parse("1234567890"))

Dim n As Integer
If Not Integer.TryParse("abc", n) Then
    Console.WriteLine("Parse error")
End If
