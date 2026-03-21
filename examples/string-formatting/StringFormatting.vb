' String formatting in VB.NET.

Console.WriteLine(String.Format("{0,-10}", "golang"))
Console.WriteLine(String.Format("{0:F2}", 3.14159))
Console.WriteLine(String.Format("|{0,-10}|", "left"))
Console.WriteLine(String.Format("|{0,10}|", "right"))
Console.WriteLine(String.Format("{0}", True))
Console.WriteLine(String.Format("{0:X}", 255))
Console.WriteLine(String.Format("{0:x2}", 255))
Console.WriteLine(String.Format("{0:D5}", 12))

Dim name As String = "World"
Console.WriteLine($"Hello, {name}!")
