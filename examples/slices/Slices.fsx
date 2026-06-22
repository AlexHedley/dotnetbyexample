// In F#, lists and arrays are immutable; ResizeArray is the mutable List.

// A list in F# is immutable and uses :: (cons) for prepending.
let s = ["a"; "b"; "c"]
printfn "dcl: %A" s

// Accessing elements.
printfn "get: %s" s.[2]
printfn "len: %d" s.Length

// Slicing with List.skip and List.take.
let sl1 = s |> List.skip 2 |> List.take 1
printfn "sl1: %A" sl1

let sl2 = s |> List.take 2
printfn "sl2: %A" sl2

// Concatenate lists with @.
let t = ["g"; "h"; "i"]
let apd = s @ t
printfn "apd: %A" apd

// 2D lists.
let twoD = [ for i in 0..2 -> [ for j in 0..i -> i + j ] ]
printfn "2d: %A" twoD
