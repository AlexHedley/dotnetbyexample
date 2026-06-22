// Timeouts in F# use CancellationToken.
open System.Threading.Channels
open System.Threading

let c1 = Channel.CreateUnbounded<string>()
let c2 = Channel.CreateUnbounded<string>()

let slowOp () = async {
    do! Async.Sleep 2000
    do! c1.Writer.WriteAsync("result 1").AsTask() |> Async.AwaitTask
}

let fastOp () = async {
    do! Async.Sleep 200
    do! c2.Writer.WriteAsync("result 2").AsTask() |> Async.AwaitTask
}

Async.Start (slowOp ())

// Timeout using Async.StartWithContinuations with a CancellationToken.
let cts1 = new CancellationTokenSource(1000)
try
    let result = c1.Reader.ReadAsync(cts1.Token).AsTask() |> Async.AwaitTask |> Async.RunSynchronously
    printfn "%s" result
with
| :? OperationCanceledException -> printfn "timeout 1"

Async.Start (fastOp ())

let cts2 = new CancellationTokenSource(3000)
try
    let result = c2.Reader.ReadAsync(cts2.Token).AsTask() |> Async.AwaitTask |> Async.RunSynchronously
    printfn "%s" result
with
| :? OperationCanceledException -> printfn "timeout 2"
