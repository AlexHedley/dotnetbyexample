// Closing channels in F#.
open System.Threading.Channels

let jobs = Channel.CreateUnbounded<int>()
let doneChannel = Channel.CreateUnbounded<bool>()

let worker () = async {
    let! items = jobs.Reader.ReadAllAsync() |> AsyncSeq.ofAsyncEnum
    // Use regular async enumerable processing.
    let mutable cont = true
    while cont do
        try
            let! j = jobs.Reader.ReadAsync().AsTask() |> Async.AwaitTask
            printfn "received job %d" j
        with _ ->
            cont <- false
    printfn "received all jobs"
    do! doneChannel.Writer.WriteAsync(true).AsTask() |> Async.AwaitTask
}

// Simple approach using Task.
let workerTask = System.Threading.Tasks.Task.Run(fun () ->
    let reader = jobs.Reader
    let mutable running = true
    while running do
        try
            let j = reader.ReadAsync().AsTask().Result
            printfn "received job %d" j
        with _ ->
            running <- false
    printfn "received all jobs"
    doneChannel.Writer.WriteAsync(true).AsTask().Wait()
)

for j in 1..5 do
    jobs.Writer.WriteAsync(j).AsTask().Wait()
    printfn "sent job %d" j

jobs.Writer.Complete()
printfn "sent all jobs"

doneChannel.Reader.ReadAsync().AsTask().Wait()
