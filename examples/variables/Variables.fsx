// In F#, values are immutable by default and types are inferred.

// let binds a name to a value (immutable by default).
let a = "initial"
printfn "%s" a

// Multiple bindings.
let b = 1
let c = 2
printfn "%d %d" b c

// F# infers the type.
let d = true
printfn "%b" d

// Explicit type annotation.
let e: int = 0
printfn "%d" e

// Mutable variables use the mutable keyword.
let mutable f = "apple"
printfn "%s" f
