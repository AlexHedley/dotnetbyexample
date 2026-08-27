// Maps in F# use Map (immutable) or Dictionary (mutable).

// Create an empty map using Map.
let m = Map.empty<string, int>

// Add key/value pairs.
let m1 = m |> Map.add "k1" 7 |> Map.add "k2" 13

// Get values.
printfn "map: %d %d" m1.["k1"] m1.["k2"]
printfn "v1: %d" m1.["k1"]

// TryFind to safely get a value.
match m1 |> Map.tryFind "k3" with
| Some v3 -> printfn "v3: %d" v3
| None -> printfn "v3: 0"

printfn "len: %d" m1.Count

// Remove a key.
let m2 = m1 |> Map.remove "k2"
printfn "map: %d" m2.Count

// Check if key exists.
let prs = m2 |> Map.containsKey "k2"
printfn "prs: %b" prs

// Initialize with values.
let n = Map.ofList [("foo", 1); ("bar", 2)]
printfn "map: %d %d" n.["foo"] n.["bar"]
