// Timers in F# use Async.Sleep and CancellationToken.

let timer1 = async {
    do! Async.Sleep 2000
    printfn "Timer 1 fired"
}

let cts = new System.Threading.CancellationTokenSource()

let timer2 = async {
    try
        do! Async.Sleep 1000
        printfn "Timer 2 fired"
    with
    | :? System.OperationCanceledException -> ()
}

// Cancel timer2 before it fires.
Async.Start(timer2, cts.Token)
cts.Cancel()

// Wait for timer1.
Async.RunSynchronously timer1
printfn "Timer 2 stopped"
