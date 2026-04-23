// F# supports recursive functions using the rec keyword.

// Define a recursive factorial function.
let rec fact n =
    if n = 0 then 1
    else n * fact (n - 1)

printfn "%d" (fact 7)

// Recursive Fibonacci.
let rec fib n =
    if n < 2 then n
    else fib (n - 1) + fib (n - 2)

printfn "%d" (fib 7)
