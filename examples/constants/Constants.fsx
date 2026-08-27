// F# uses let for constants (immutable bindings).

// let creates an immutable binding.
let s = "constant"
printfn "%s" s

let n = 500000000

let d = 3e20 / float n
printfn "%g" d

printfn "%d" (int64 d)

printfn "%f" (System.Math.Sin(float n))
