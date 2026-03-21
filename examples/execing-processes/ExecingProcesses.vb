' Execing processes in VB.NET.

Imports System.Diagnostics

Dim psi As New ProcessStartInfo("/bin/ls")
psi.UseShellExecute = False
psi.ArgumentList.Add("-a")
psi.ArgumentList.Add("-l")
psi.ArgumentList.Add("/tmp")

Dim proc As Process = Process.Start(psi)
proc.WaitForExit()
Environment.Exit(proc.ExitCode)
