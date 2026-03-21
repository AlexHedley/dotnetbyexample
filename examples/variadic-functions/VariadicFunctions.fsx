// Variadic functions in F# accept a sequence or use params.

// Define a function taking a list of integers.
let sum (nums: int list) =
    List.sum nums

// Call with various number of elements.
printfn "%d" (sum [1; 2])
printfn "%d" (sum [1; 2; 3])

// Pass an existing list.
let nums = [1; 2; 3; 4]
printfn "%d" (sum nums)
