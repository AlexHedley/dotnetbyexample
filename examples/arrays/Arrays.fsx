// In F#, arrays are fixed-size sequences.

// Create an array of 5 ints (default value 0).
let a = Array.zeroCreate<int> 5
printf "emp:"
a |> Array.iter (fun v -> printf " %d" v)
printfn ""

// Set and get values by index.
a.[4] <- 100
printfn "set: %d" a.[4]
printfn "get: %d" a.[4]

// Length property.
printfn "len: %d" a.Length

// Declare and initialize an array in one line.
let b = [| 1; 2; 3; 4; 5 |]
printf "dcl:"
b |> Array.iter (fun v -> printf " %d" v)
printfn ""

// 2D array.
let twoD = Array2D.init 2 3 (fun i j -> i + j)
printf "2d:"
for i = 0 to 1 do
    for j = 0 to 2 do
        printf " %d" twoD.[i, j]
printfn ""
