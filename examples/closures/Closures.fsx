// F# supports closures naturally since functions are first-class.

// Define a function that returns a closure.
let intSeq () =
    let mutable i = 0
    fun () ->
        i <- i + 1
        i

// Call intSeq to get a closure.
let nextInt = intSeq ()

printfn "%d" (nextInt ())
printfn "%d" (nextInt ())
printfn "%d" (nextInt ())

// New closure has its own state.
let newInts = intSeq ()
printfn "%d" (newInts ())
