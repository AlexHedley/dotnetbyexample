// Sometimes our .NET programs need to spawn other, non-.NET processes.

using System.Diagnostics;

// Run a simple command.
var dateOut = Process.Start(new ProcessStartInfo("date")
{
    RedirectStandardOutput = true,
    UseShellExecute = false
});
dateOut?.WaitForExit();
Console.WriteLine(dateOut?.StandardOutput.ReadToEnd());

// Run a command with arguments.
var grepInfo = new ProcessStartInfo("grep")
{
    RedirectStandardInput = true,
    RedirectStandardOutput = true,
    UseShellExecute = false
};
grepInfo.ArgumentList.Add("hello");

var grepProc = Process.Start(grepInfo);
grepProc?.StandardInput.WriteLine("hello grep");
grepProc?.StandardInput.WriteLine("other line");
grepProc?.StandardInput.Close();
grepProc?.WaitForExit();
Console.WriteLine(grepProc?.StandardOutput.ReadToEnd());
