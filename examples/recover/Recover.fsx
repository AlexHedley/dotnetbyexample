// Recover in F# uses try/with (exception handling).

let safeDivide a b =
    try
        printfn "%d" (a / b)
    with
    | :? System.DivideByZeroException as e ->
        printfn "Recovered: %s" e.Message

let mayPanic () =
    failwith "a problem"

let safeDiv () =
    try
        mayPanic ()
    with
    | ex ->
        printfn "Recovered. Error: %s" ex.Message
    printfn "After safeDiv"

safeDivide 10 2
safeDivide 10 0
safeDiv ()
