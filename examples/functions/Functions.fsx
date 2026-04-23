// Functions in F# are first-class values.

// Define a function using let.
let plus a b = a + b

// F# functions curry by default.
let plusPlus a b c = a + b + c

// Call functions.
let res = plus 1 2
printfn "1+2 = %d" res

let res2 = plusPlus 1 2 3
printfn "1+2+3 = %d" res2
