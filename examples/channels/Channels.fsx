// In F#, channels use System.Threading.Channels.
open System.Threading.Channels

let ch = Channel.CreateUnbounded<string>()

// Send from an async workflow.
let send = async {
    do! ch.Writer.WriteAsync("ping").AsTask() |> Async.AwaitTask
}

Async.Start send

// Read the value.
let msg = ch.Reader.ReadAsync().AsTask() |> Async.AwaitTask |> Async.RunSynchronously
printfn "%s" msg
