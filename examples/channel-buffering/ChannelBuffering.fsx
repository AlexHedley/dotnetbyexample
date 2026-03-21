// Buffered channels in F# use bounded Channel.
open System.Threading.Channels

let ch = Channel.CreateBounded<string>(2)

ch.Writer.WriteAsync("buffered").AsTask() |> Async.AwaitTask |> Async.RunSynchronously
ch.Writer.WriteAsync("channel").AsTask() |> Async.AwaitTask |> Async.RunSynchronously

let msg1 = ch.Reader.ReadAsync().AsTask() |> Async.AwaitTask |> Async.RunSynchronously
let msg2 = ch.Reader.ReadAsync().AsTask() |> Async.AwaitTask |> Async.RunSynchronously
printfn "%s" msg1
printfn "%s" msg2
