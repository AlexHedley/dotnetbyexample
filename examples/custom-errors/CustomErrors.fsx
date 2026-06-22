// Custom exceptions in F# extend the Exception class.

// Define custom exception types.
exception ValidationError of string * string  // field * message
exception NotFoundError of int

// Alternative: use standard .NET exceptions.
type MyValidationError(field: string, message: string) =
    inherit System.Exception(message)
    member _.Field = field

let getUser id =
    if id <= 0 then
        raise (MyValidationError("id", "must be positive"))
    elif id > 100 then
        raise (System.Exception(sprintf "Resource with id %d not found" id))
    else
        sprintf "user_%d" id

let tryGetUser id =
    try
        let user = getUser id
        printfn "Found: %s" user
    with
    | :? MyValidationError as ve ->
        printfn "Validation error on field '%s': %s" ve.Field ve.Message
    | e ->
        printfn "Error: %s" e.Message

tryGetUser 1
tryGetUser -1
tryGetUser 999
