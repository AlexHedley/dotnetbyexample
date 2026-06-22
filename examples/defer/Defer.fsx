// Defer in F# uses use bindings or try/finally.

// The 'use' binding automatically disposes at end of scope.
let createFile path =
    printfn "creating"
    let f = System.IO.File.Create(path)
    try
        printfn "writing"
        System.IO.File.WriteAllText(path, "data")
    finally
        printfn "closing"
        f.Close()

// LIFO defer using a list.
let deferLIFO () =
    let deferred = System.Collections.Generic.Stack<unit -> unit>()
    deferred.Push(fun () -> printfn "deferred 1")
    deferred.Push(fun () -> printfn "deferred 2")
    deferred.Push(fun () -> printfn "deferred 3")
    
    printfn "before deferred"
    
    while deferred.Count > 0 do
        deferred.Pop()()

deferLIFO ()
