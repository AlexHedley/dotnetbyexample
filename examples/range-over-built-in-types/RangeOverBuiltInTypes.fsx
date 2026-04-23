// F# uses for loops and Seq functions for iteration.

// Iterate over an array.
let nums = [| 2; 3; 4 |]
let sum = Array.sum nums
printfn "sum: %d" sum

// Iterate with index.
nums |> Array.iteri (fun i v ->
    if v = 3 then printfn "index: %d" i)

// Iterate over a map.
let kvs = Map.ofList [("a", "apple"); ("b", "banana")]
kvs |> Map.iter (fun k v -> printfn "%s -> %s" k v)

// Iterate over a string's characters.
"go" |> Seq.iteri (fun i c ->
    printfn "%d %d" i (int c))
