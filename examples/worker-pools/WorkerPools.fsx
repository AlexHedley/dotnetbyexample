// Worker pools in F# using channels.
open System.Threading.Channels

let worker id (jobs: ChannelReader<int>) (results: ChannelWriter<int>) = async {
    let mutable running = true
    while running do
        try
            let! j = jobs.ReadAsync().AsTask() |> Async.AwaitTask
            printfn "worker %d started job %d" id j
            do! Async.Sleep (j * 1000)
            printfn "worker %d finished job %d" id j
            do! results.WriteAsync(j * 2).AsTask() |> Async.AwaitTask
        with _ ->
            running <- false
}

let numJobs = 5
let jobs = Channel.CreateUnbounded<int>()
let results = Channel.CreateUnbounded<int>()

// Start 3 workers.
let workers = [| for id in 1..3 -> Async.StartAsTask (worker id jobs.Reader results.Writer) |]

// Send jobs.
for j in 1..numJobs do
    jobs.Writer.WriteAsync(j).AsTask().Wait()

jobs.Writer.Complete()

System.Threading.Tasks.Task.WaitAll(workers)
results.Writer.Complete()

// Collect results.
let mutable reading = true
while reading do
    let mutable r = 0
    if results.Reader.TryRead(&r) then
        printfn "result: %d" r
    else
        reading <- false
