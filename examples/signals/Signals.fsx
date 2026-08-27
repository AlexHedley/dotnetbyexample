// Signals in F#.
open System.Threading

let cts = new CancellationTokenSource()

// Handle Ctrl+C (SIGINT).
Console.CancelKeyPress.Add(fun e ->
    e.Cancel <- true
    printfn "\nReceived SIGINT"
    cts.Cancel()
)

printfn "awaiting signal"

try
    System.Threading.Tasks.Task.Delay(-1, cts.Token).Wait()
with
| :? OperationCanceledException -> ()

printfn "exiting gracefully"
