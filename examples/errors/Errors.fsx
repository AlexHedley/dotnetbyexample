// In F#, errors can be handled with exceptions or Result types.

// Using Result<T, E> for functional error handling.
let divide a b =
    if b = 0 then Error "Cannot divide by zero"
    else Ok (a / b)

// Pattern match on Result.
match divide 10 2 with
| Ok result -> printfn "10 / 2 = %d" result
| Error msg -> printfn "Error: %s" msg

match divide 10 0 with
| Ok result -> printfn "10 / 0 = %d" result
| Error msg -> printfn "Error: %s" msg

// Exception-based approach.
let divideExn a b =
    if b = 0 then raise (System.DivideByZeroException("Cannot divide by zero"))
    a / b

try
    let result = divideExn 10 2
    printfn "10 / 2 = %d" result
with
| :? System.DivideByZeroException as e ->
    printfn "Error: %s" e.Message
