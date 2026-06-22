// Execing processes in F#.
open System.Diagnostics

let psi = ProcessStartInfo("/bin/ls")
psi.UseShellExecute <- false
psi.ArgumentList.Add("-a")
psi.ArgumentList.Add("-l")
psi.ArgumentList.Add("/tmp")

let proc = Process.Start(psi)
proc.WaitForExit()
exit proc.ExitCode
