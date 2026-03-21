// Atomic counters in F# use Interlocked class.
open System.Threading

let mutable ops = 0

let tasks = [| for _ in 1..50 ->
    System.Threading.Tasks.Task.Run(fun () ->
        for _ in 1..1000 do
            Interlocked.Increment(&ops) |> ignore
    )
|]

System.Threading.Tasks.Task.WaitAll(tasks)
printfn "ops: %d" ops
