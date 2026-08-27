// Random numbers in F#.

let rng = System.Random()

printfn "%d %d" (rng.Next()) (rng.Next())
printfn "%d" (rng.Next(0, 100))
printfn "%f" (rng.NextDouble())

// Seeded random.
let seeded = System.Random(42)
printfn "%d" (seeded.Next())
printfn "%d" (seeded.Next())
