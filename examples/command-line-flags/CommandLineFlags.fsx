// Command-line flags in F#.

let args = fsi.CommandLineArgs |> Array.skip 1

let mutable word = "foo"
let mutable numb = 42
let mutable fork = false

args |> Array.iter (fun arg ->
    if arg.StartsWith("--word=") then word <- arg.[7..]
    elif arg.StartsWith("--numb=") then numb <- int arg.[7..]
    elif arg = "--fork" then fork <- true
)

printfn "word: %s" word
printfn "numb: %d" numb
printfn "fork: %b" fork
