// Line filters in F# read from stdin and write to stdout.

let mutable line = stdin.ReadLine()
while line <> null do
    printfn "%s" (line.ToUpper())
    line <- stdin.ReadLine()
