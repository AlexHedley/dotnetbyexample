// Time formatting/parsing in F#.

let t = System.DateTime(2012, 11, 1, 22, 8, 41, System.DateTimeKind.Utc)

// Format using format strings.
printfn "%s" (t.ToString("o"))
printfn "%s" (t.ToString("yyyy-MM-dd HH:mm:ss"))
printfn "%s" (t.ToString("R"))

// Parse a date string.
let t1 = System.DateTime.Parse("2012-11-01 22:08:41")
printfn "%A" t1

// TryParse.
let mutable t2 = System.DateTime.MinValue
if not (System.DateTime.TryParse("invalid date", &t2)) then
    printfn "Parse error"
