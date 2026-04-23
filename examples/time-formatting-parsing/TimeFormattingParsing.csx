// .NET uses format strings with DateTime.ToString and DateTime.Parse.

// Time formatting.
var t = new DateTime(2012, 11, 1, 22, 8, 41, DateTimeKind.Utc);

// ISO 8601 format.
Console.WriteLine(t.ToString("o"));

// Custom format strings.
Console.WriteLine(t.ToString("yyyy-MM-dd HH:mm:ss"));
Console.WriteLine(t.ToString("ddd MMM  d HH:mm:ss yyyy"));

// Formatting with time zones.
Console.WriteLine(t.ToString("R")); // RFC 1123 format

// Parsing a formatted time string.
var t1 = DateTime.Parse("2012-11-01 22:08:41");
Console.WriteLine(t1);

// Parse with a specific format.
var t2 = DateTime.ParseExact("8 41 PM", "h mm tt", null);
Console.WriteLine(t2.Hour, t2.Minute);

// TryParse for error handling.
if (DateTime.TryParse("invalid date", out var t3))
    Console.WriteLine(t3);
else
    Console.WriteLine("Parse error");
