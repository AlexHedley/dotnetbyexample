' Epoch in VB.NET uses DateTimeOffset.

Dim now As DateTimeOffset = DateTimeOffset.UtcNow
Console.WriteLine(now)
Console.WriteLine(now.ToUnixTimeSeconds())
Console.WriteLine(now.ToUnixTimeMilliseconds())

Dim fromUnix As DateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(now.ToUnixTimeSeconds())
Console.WriteLine(fromUnix)
