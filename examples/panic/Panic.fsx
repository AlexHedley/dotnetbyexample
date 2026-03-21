// In F#, panic is equivalent to using failwith or raise.

// failwith throws an exception with a message.
let panic msg = failwith msg

// reraise propagates an exception.
try
    panic "a problem"
with
| ex -> printfn "Caught panic: %s" ex.Message
