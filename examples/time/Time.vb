' Time in VB.NET uses DateTime and TimeSpan.

Dim now As DateTime = DateTime.Now
Console.WriteLine(now)

Dim t1 As New DateTime(2009, 11, 17, 20, 34, 58, DateTimeKind.Utc)
Console.WriteLine(t1)

Console.WriteLine(t1.Year & " " & t1.Month & " " & t1.Day)
Console.WriteLine(t1.Hour & " " & t1.Minute & " " & t1.Second)
Console.WriteLine(t1.DayOfWeek)

Dim t2 As DateTime = t1.AddHours(2)
Console.WriteLine(DateTime.Compare(t1, t2))

Dim diff As TimeSpan = t2 - t1
Console.WriteLine(diff)
Console.WriteLine(diff.TotalHours)
Console.WriteLine(t1.Add(diff))
Console.WriteLine(t1.Subtract(diff))
