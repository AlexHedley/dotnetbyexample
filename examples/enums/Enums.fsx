// Enums in F# use discriminated unions or the enum type.

// Discriminated union (preferred F# way).
type ServerState =
    | Idle
    | Connected
    | Error
    | Retrying

let state = Idle
printfn "%A" state

let transition s =
    match s with
    | Idle -> Connected
    | Connected -> Idle
    | Retrying -> Connected
    | Error -> Error

let ns = transition state
printfn "transition: %A" ns

// F# also supports .NET enums.
type Permission =
    | None = 0
    | Read = 1
    | Write = 2
    | Execute = 4

let p = Permission.Read ||| Permission.Write
printfn "permissions: %A" p
