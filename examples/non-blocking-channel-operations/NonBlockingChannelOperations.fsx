// Non-blocking channel operations in F#.
open System.Threading.Channels

let messages = Channel.CreateUnbounded<string>()
let signals = Channel.CreateUnbounded<bool>()

// Non-blocking receive.
let mutable msg = ""
if messages.Reader.TryRead(&msg) then
    printfn "received message: %s" msg
else
    printfn "no message received"

// Non-blocking send.
let sent = messages.Writer.TryWrite("hi")
printfn "sent: %b" sent
