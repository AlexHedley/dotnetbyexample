' Spawning processes in VB.NET.

Imports System.Diagnostics

Dim psi As New ProcessStartInfo("echo")
psi.ArgumentList.Add("hello")
psi.RedirectStandardOutput = True
psi.UseShellExecute = False

Dim proc As Process = Process.Start(psi)
proc.WaitForExit()
Console.WriteLine(proc.StandardOutput.ReadToEnd())
