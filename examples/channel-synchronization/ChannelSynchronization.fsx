// Channel synchronization in F#.
open System.Threading.Channels

let done = Channel.CreateUnbounded<bool>()

let worker () = async {
    printfn "working..."
    do! Async.Sleep 1000
    printfn "done"
    do! done.Writer.WriteAsync(true).AsTask() |> Async.AwaitTask
}

Async.Start (worker ())

done.Reader.ReadAsync().AsTask() |> Async.AwaitTask |> Async.RunSynchronously |> ignore
