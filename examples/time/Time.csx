// .NET offers extensive support for times and durations via DateTime and TimeSpan.

// Get the current time.
var now = DateTime.Now;
Console.WriteLine(now);

// Build a DateTime from components.
var t1 = new DateTime(2009, 11, 17, 20, 34, 58, 651, DateTimeKind.Utc);
Console.WriteLine(t1);

// You can extract the various components of a DateTime.
Console.WriteLine(t1.Year, t1.Month, t1.Day);
Console.WriteLine(t1.Hour, t1.Minute, t1.Second);
Console.WriteLine(t1.Nanosecond()); // Extension not built-in; use Ticks
Console.WriteLine(t1.DayOfWeek);

// Compare times using standard comparison operators.
var t2 = t1.AddHours(2);
Console.WriteLine(t1.CompareTo(t2));
Console.WriteLine(t2.CompareTo(t1));

// The Sub method returns a TimeSpan.
var diff = t2 - t1;
Console.WriteLine(diff);
Console.WriteLine(diff.TotalHours);
Console.WriteLine(diff.Minutes);

// Use Add to advance a time by a given duration.
Console.WriteLine(t1.Add(diff));
Console.WriteLine(t1.Subtract(diff));
