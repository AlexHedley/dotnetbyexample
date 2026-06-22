// Command-line arguments in F#.

// fsi.CommandLineArgs contains arguments.
let argsAll = fsi.CommandLineArgs
printfn "argsAll: %A" argsAll

// Skip the script name.
let args = argsAll |> Array.skip 1
printfn "args: %A" args

if args.Length > 0 then
    printfn "arg1: %s" args.[0]

// Print all args.
args |> Array.iteri (fun i arg -> printfn "args[%d]: %s" i arg)
