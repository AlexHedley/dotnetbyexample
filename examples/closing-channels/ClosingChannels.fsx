// Closing channels in F#.
open System.Threading.Channels

let jobs = Channel.CreateUnbounded<int>()
let doneChannel = Channel.CreateUnbounded<bool>()

// Worker reads jobs until the channel is closed.
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
