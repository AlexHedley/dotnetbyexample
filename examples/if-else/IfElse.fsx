// Branching with if/elif/else in F# is an expression.

// Here's a basic example.
let num = 9
if num < 0 then
    printfn "%d is negative" num
elif num < 10 then
    printfn "%d has 1 digit" num
else
    printfn "%d has multiple digits" num

// You can have an if without an else (returns unit).
if num % 2 = 0 then
    printfn "even"

// if/else as an expression.
let num2 = 7
let result =
    if num2 % 2 = 0 then
        sprintf "%d is even" num2
    else
        sprintf "%d is odd" num2
printfn "%s" result

printfn "done"
