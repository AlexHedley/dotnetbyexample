// Number parsing in F#.

// Parse a float.
let f = float "1.234"
printfn "%f" f

// Parse an int.
let i = int "123"
printfn "%d" i

// Parse a hex string.
let d = System.Convert.ToInt64("1c8", 16)
printfn "%d" d

// TryParse for error handling.
match System.Int32.TryParse("abc") with
| true, n -> printfn "%d" n
| false, _ -> printfn "Parse error"
