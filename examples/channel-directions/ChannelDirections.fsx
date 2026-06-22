// Channel directions in F#.
open System.Threading.Channels

let ping (pings: ChannelWriter<string>) msg = async {
    do! pings.WriteAsync(msg).AsTask() |> Async.AwaitTask
}

let pong (pings: ChannelReader<string>) (pongs: ChannelWriter<string>) = async {
    let! msg = pings.ReadAsync().AsTask() |> Async.AwaitTask
    do! pongs.WriteAsync(msg).AsTask() |> Async.AwaitTask
}

let pings = Channel.CreateUnbounded<string>()
let pongs = Channel.CreateUnbounded<string>()

ping pings.Writer "passed message" |> Async.RunSynchronously
pong pings.Reader pongs.Writer |> Async.RunSynchronously

let msg = pongs.Reader.ReadAsync().AsTask() |> Async.AwaitTask |> Async.RunSynchronously
printfn "%s" msg
