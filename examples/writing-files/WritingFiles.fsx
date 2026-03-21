// Writing files in F#.

System.IO.File.WriteAllText("/tmp/dat1.txt", "hello\ngo\n")

use sw = new System.IO.StreamWriter("/tmp/dat2.txt")
sw.WriteLine("some")
sw.WriteLine("writes")
sw.Write("buffered")
sw.Flush()

printfn "%s" (System.IO.File.ReadAllText("/tmp/dat1.txt"))
printfn "%s" (System.IO.File.ReadAllText("/tmp/dat2.txt"))
