// File paths in F#.

let p = System.IO.Path.Combine("dir1", "dir2", "filename")
printfn "p: %s" p

printfn "Dir(p): %s" (System.IO.Path.GetDirectoryName(p))
printfn "Base(p): %s" (System.IO.Path.GetFileName(p))

printfn "IsAbs: %b" (System.IO.Path.IsPathRooted("dir/file"))
printfn "IsAbs: %b" (System.IO.Path.IsPathRooted("/dir/file"))

let filename = "config.json"
printfn "Ext(filename): %s" (System.IO.Path.GetExtension(filename))
printfn "DelExt(filename): %s" (System.IO.Path.GetFileNameWithoutExtension(filename))
