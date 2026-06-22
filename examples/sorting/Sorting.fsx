// Sorting in F# using List.sort and Array.sort.

// Sorting strings.
let strs = ["c"; "a"; "b"] |> List.sort
printfn "Strings: %A" strs

// Sorting ints.
let ints = [7; 2; 4] |> List.sort
printfn "Ints: %A" ints

// Check if sorted (a sorted list equals itself).
let isSorted = ints = List.sort ints
printfn "Sorted: %b" isSorted

// Sort in descending order.
let strsDesc = ["c"; "a"; "b"] |> List.sortDescending
printfn "Reverse: %A" strsDesc
