// Temporary files and directories in F#.

let f = System.IO.Path.GetTempFileName()
printfn "Temp file: %s" f

System.IO.File.WriteAllBytes(f, [|1uy; 2uy; 3uy; 4uy|])

let dname = System.IO.Path.Combine(System.IO.Path.GetTempPath(), System.IO.Path.GetRandomFileName())
System.IO.Directory.CreateDirectory(dname) |> ignore
printfn "Temp dir: %s" dname

System.IO.File.Delete(f)
System.IO.Directory.Delete(dname, true)
