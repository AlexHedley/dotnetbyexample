// A common requirement in programs is getting the number of seconds,
// milliseconds, or nanoseconds since the Unix epoch.
// In .NET, use DateTimeOffset for Unix time operations.

// Get current Unix time in seconds.
var now = DateTimeOffset.UtcNow;
Console.WriteLine(now);
Console.WriteLine(now.ToUnixTimeSeconds());
Console.WriteLine(now.ToUnixTimeMilliseconds());

// Convert Unix time back to DateTimeOffset.
var fromUnix = DateTimeOffset.FromUnixTimeSeconds(now.ToUnixTimeSeconds());
Console.WriteLine(fromUnix);

var fromUnixMs = DateTimeOffset.FromUnixTimeMilliseconds(now.ToUnixTimeMilliseconds());
Console.WriteLine(fromUnixMs);
