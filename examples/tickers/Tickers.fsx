// Tickers in F# use PeriodicTimer.

let ticker = new System.Threading.PeriodicTimer(System.TimeSpan.FromMilliseconds(500))
let cts = new System.Threading.CancellationTokenSource()

let tickerTask = async {
    let mutable running = true
    while running do
        let! next = ticker.WaitForNextTickAsync(cts.Token).AsTask() |> Async.AwaitTask
        if next then
            printfn "tick at %A" System.DateTime.Now
        else
            running <- false
}

Async.Start tickerTask

System.Threading.Thread.Sleep(1500)
cts.Cancel()
System.Threading.Thread.Sleep(100)
printfn "Ticker stopped"
