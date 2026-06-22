// Directories in F#.

System.IO.Directory.CreateDirectory("subdir_fs") |> ignore
System.IO.File.WriteAllText(System.IO.Path.Combine("subdir_fs", "file1.txt"), "")

System.IO.Directory.GetFileSystemEntries("subdir_fs")
|> Array.iter (printfn "%s")

printfn "Current: %s" (System.IO.Directory.GetCurrentDirectory())

System.IO.Directory.Delete("subdir_fs", true)
