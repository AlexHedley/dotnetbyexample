' URL parsing in VB.NET.

Dim s As String = "postgres://user:pass@host.com:5432/path?k=v#f"
Dim u As New Uri(s)

Console.WriteLine(u.Scheme)
Console.WriteLine(u.Host)
Console.WriteLine(u.Port)
Console.WriteLine(u.UserInfo)

Dim userInfo() As String = u.UserInfo.Split(":"c)
Console.WriteLine(userInfo(0))
Console.WriteLine(userInfo(1))

Console.WriteLine(u.AbsolutePath)
Console.WriteLine(u.Fragment)
Console.WriteLine(u.Query)
