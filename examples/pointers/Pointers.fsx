// F# uses ref cells for mutable references.

// Pass a value (no mutation).
let zeroval (ival: int) =
    let _ = 0  // does nothing to the original
    ()

// Pass a ref cell (mutation).
let zeroptr (iptr: int ref) =
    iptr.Value <- 0

let i = ref 1
printfn "initial: %d" i.Value

zeroval i.Value
printfn "zeroval: %d" i.Value

zeroptr i
printfn "zeroptr: %d" i.Value
