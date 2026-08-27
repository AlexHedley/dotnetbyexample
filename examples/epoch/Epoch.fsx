// Epoch in F# uses System.DateTimeOffset.

let now = System.DateTimeOffset.UtcNow
printfn "%A" now
printfn "%d" (now.ToUnixTimeSeconds())
printfn "%d" (now.ToUnixTimeMilliseconds())

let fromUnix = System.DateTimeOffset.FromUnixTimeSeconds(now.ToUnixTimeSeconds())
printfn "%A" fromUnix
