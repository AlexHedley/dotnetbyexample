// In F#, goroutines are analogous to async workflows and tasks.

let f (from: string) =
    for i in 0..2 do
        printfn "%s: %d" from i

// Run synchronously.
f "direct"

// Run asynchronously using async workflows.
let t1 = async { f "goroutine" }
let t2 = async { printfn "going" }

// Start both and wait.
let tasks = [| Async.StartAsTask t1; Async.StartAsTask t2 |]
System.Threading.Tasks.Task.WaitAll(tasks)
printfn "done"
