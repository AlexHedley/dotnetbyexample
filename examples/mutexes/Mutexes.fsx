// Mutexes in F# use lock or Monitor.
open System.Collections.Generic

type SafeCounter() =
    let mutex = obj()
    let state = Dictionary<string, int>()
    
    member _.Inc(key: string) =
        lock mutex (fun () ->
            if state.ContainsKey(key) then
                state.[key] <- state.[key] + 1
            else
                state.[key] <- 1
        )
    
    member _.Value(key: string) =
        lock mutex (fun () ->
            if state.ContainsKey(key) then state.[key] else 0
        )

let c = SafeCounter()
let tasks = [| for _ in 1..1000 ->
    System.Threading.Tasks.Task.Run(fun () -> c.Inc("somekey"))
|]

System.Threading.Tasks.Task.WaitAll(tasks)
printfn "%d" (c.Value("somekey"))
