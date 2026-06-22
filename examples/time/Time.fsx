// Time in F# uses System.DateTime and TimeSpan.

let now = System.DateTime.Now
printfn "%A" now

let t1 = System.DateTime(2009, 11, 17, 20, 34, 58, System.DateTimeKind.Utc)
printfn "%A" t1

printfn "%d %d %d" t1.Year t1.Month t1.Day
printfn "%d %d %d" t1.Hour t1.Minute t1.Second
printfn "%A" t1.DayOfWeek

let t2 = t1.AddHours(2.0)
printfn "%d" (System.DateTime.Compare(t1, t2))

let diff = t2 - t1
printfn "%A" diff
printfn "%.1f hours" diff.TotalHours
printfn "%A" (t1.Add(diff))
printfn "%A" (t1.Subtract(diff))
