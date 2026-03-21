// Select equivalent in F# uses Async.Choice or Task.WhenAny.
open System.Threading.Channels

let c1 = Channel.CreateUnbounded<string>()
let c2 = Channel.CreateUnbounded<string>()

let send1 () = async {
    do! Async.Sleep 1000
    do! c1.Writer.WriteAsync("one").AsTask() |> Async.AwaitTask
}

let send2 () = async {
    do! Async.Sleep 2000
    do! c2.Writer.WriteAsync("two").AsTask() |> Async.AwaitTask
}

Async.Start (send1 ())
Async.Start (send2 ())

for _ in 1..2 do
    let t1 = c1.Reader.ReadAsync().AsTask()
    let t2 = c2.Reader.ReadAsync().AsTask()
    
    let completed = System.Threading.Tasks.Task.WhenAny(t1, t2) |> Async.AwaitTask |> Async.RunSynchronously
    
    if completed = (t1 :> System.Threading.Tasks.Task) then
        printfn "Received %s" t1.Result
    else
        printfn "Received %s" t2.Result
