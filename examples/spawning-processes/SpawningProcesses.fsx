// Spawning processes in F#.
open System.Diagnostics

let psi = ProcessStartInfo("echo")
psi.ArgumentList.Add("hello")
psi.RedirectStandardOutput <- true
psi.UseShellExecute <- false

let proc = Process.Start(psi)
proc.WaitForExit()
printfn "%s" (proc.StandardOutput.ReadToEnd())
