// Reading files in F#.

// Create a temp file to read.
let tempFile = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "dat.txt")
System.IO.File.WriteAllText(tempFile, "hello\nworld\n")

// Read entire file.
let content = System.IO.File.ReadAllText(tempFile)
printfn "%s" content

// Read all lines.
let lines = System.IO.File.ReadAllLines(tempFile)
lines |> Array.iter (printfn "%s")

// Stream reading.
use sr = new System.IO.StreamReader(tempFile)
let mutable line = sr.ReadLine()
while line <> null do
    printfn "%s" line
    line <- sr.ReadLine()

// Clean up.
System.IO.File.Delete(tempFile)
