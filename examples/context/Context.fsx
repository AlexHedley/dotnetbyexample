// Context (CancellationToken) in F#.
open System.Threading

let hello (ct: CancellationToken) = async {
    printfn "server: hello handler started"
    try
        do! Async.Sleep 2000
        if ct.IsCancellationRequested then
            printfn "server: context cancelled"
        else
            printfn "server: hello handler done"
    with
    | :? OperationCanceledException ->
        printfn "server: context cancelled"
}

let cts = new CancellationTokenSource()

// Start and cancel after 1 second.
Async.Start(hello cts.Token, cts.Token)
System.Threading.Thread.Sleep(1000)
cts.Cancel()
System.Threading.Thread.Sleep(100)
printfn "main done"
