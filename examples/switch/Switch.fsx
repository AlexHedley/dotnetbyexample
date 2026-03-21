// Switch statements in F# use match expressions.

let i = 2
printf "Write %d as " i
match i with
| 1 -> printfn "one"
| 2 -> printfn "two"
| 3 -> printfn "three"
| _ -> printfn "other"

// Match on day of week.
let day = System.DateTime.Now.DayOfWeek
match day with
| System.DayOfWeek.Saturday
| System.DayOfWeek.Sunday -> printfn "It's the weekend"
| _ -> printfn "It's a weekday"

// Match with guards (equivalent to switch without expression).
let t = System.DateTime.Now.Hour
match t with
| h when h < 12 -> printfn "It's before noon"
| _ -> printfn "It's after noon"

// Type matching with discriminated unions or :? pattern.
let whatAmI: obj = box true
match whatAmI with
| :? bool as b -> printfn "bool: %b" b
| :? int as n -> printfn "int: %d" n
| :? string as s -> printfn "string: %s" s
| _ -> printfn "unknown type"
