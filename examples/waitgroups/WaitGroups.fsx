// WaitGroups in F# use CountdownEvent or Task.WhenAll.
open System.Threading

let worker id (wg: CountdownEvent) =
    printfn "Worker %d starting" id
    Thread.Sleep(id * 1000)
    printfn "Worker %d done" id
    wg.Signal() |> ignore

let numWorkers = 5
let wg = new CountdownEvent(numWorkers)

for i in 1..numWorkers do
    let id = i
    System.Threading.Tasks.Task.Run(fun () -> worker id wg) |> ignore

wg.Wait()
printfn "All workers done"
