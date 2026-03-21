// Rate limiting in F#.
open System.Threading.Channels

let requests = Channel.CreateUnbounded<int>()
for i in 1..5 do
    requests.Writer.WriteAsync(i).AsTask().Wait()
requests.Writer.Complete()

let limiter = Channel.CreateUnbounded<System.DateTime>()

// Produce tokens every 200ms.
let tokenProducer () = async {
    let ticker = new System.Threading.PeriodicTimer(System.TimeSpan.FromMilliseconds(200))
    let mutable running = true
    while running do
        let! next = ticker.WaitForNextTickAsync().AsTask() |> Async.AwaitTask
        if next then
            if not (limiter.Writer.TryWrite(System.DateTime.Now)) then
                running <- false
        else
            running <- false
}

Async.Start (tokenProducer ())

// Process requests with rate limiting.
let mutable reqRunning = true
while reqRunning do
    let mutable req = 0
    if requests.Reader.TryRead(&req) then
        limiter.Reader.ReadAsync().AsTask().Wait()
        printfn "request %d %A" req System.DateTime.Now
    else
        reqRunning <- false
