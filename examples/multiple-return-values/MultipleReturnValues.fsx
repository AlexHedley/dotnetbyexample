// F# supports multiple return values via tuples.

// Define a function returning a tuple.
let vals () = (3, 7)

let minMax a b = (string a, string b)

// Deconstruct the tuple.
let (a, b) = vals ()
printfn "%d" a
printfn "%d" b

// Only use the first value.
let (min, _) = minMax 1 2
printfn "%s" min
