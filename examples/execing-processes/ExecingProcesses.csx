// In .NET, there's no direct equivalent to Go's exec.Command for replacing
// the current process. However, we can start a process and wait for it.

using System.Diagnostics;

// In Go, exec replaces the current process. In .NET, we start a new process
// and optionally exit after it completes.

var binary = "/bin/ls";

// Look up the binary path (equivalent to exec.LookPath).
var lookResult = Environment.GetEnvironmentVariable("PATH")?
    .Split(':')
    .Select(dir => Path.Combine(dir, "ls"))
    .FirstOrDefault(File.Exists);
Console.WriteLine("Found ls at:", lookResult ?? "not found");

var psi = new ProcessStartInfo(binary)
{
    UseShellExecute = false
};
psi.ArgumentList.Add("-a");
psi.ArgumentList.Add("-l");
psi.ArgumentList.Add("/tmp");

var proc = Process.Start(psi);
proc?.WaitForExit();

// Note: In .NET, we don't replace the current process, but we can exit with the same code.
Environment.Exit(proc?.ExitCode ?? 0);
