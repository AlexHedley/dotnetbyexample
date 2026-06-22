// Range over channels in F#.
open System.Threading.Channels

let queue = Channel.CreateUnbounded<string>()

queue.Writer.WriteAsync("one").AsTask().Wait()
queue.Writer.WriteAsync("two").AsTask().Wait()
queue.Writer.Complete()

// ReadAllAsync iterates until channel is closed.
let readAll = async {
    let mutable running = true
    while running do
        try
            let! elem = queue.Reader.ReadAsync().AsTask() |> Async.AwaitTask
            printfn "%s" elem
        with _ ->
            running <- false
}

Async.RunSynchronously readAll
