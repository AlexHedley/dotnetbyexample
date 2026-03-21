// F# uses seq { } computation expressions for lazy sequences.

// A sequence generator using seq { }.
let genNaturals n = seq { for i in 0 .. n-1 -> i }

// An infinite sequence using Seq.initInfinite.
let infiniteNaturals = Seq.initInfinite id

// Iterate over the finite sequence.
genNaturals 5 |> Seq.iter (fun n -> printf "%d " n)
printfn ""

// Take first 5 elements of infinite sequence.
infiniteNaturals |> Seq.take 5 |> Seq.iter (fun n -> printf "%d " n)
printfn ""

// Indexed squares.
let indexedSquares = seq { for i in 0 .. 4 -> (i, i * i) }
indexedSquares |> Seq.iter (fun (i, sq) -> printfn "%d: %d" i sq)
