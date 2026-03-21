// .NET provides signal handling via Console.CancelKeyPress
// and AppDomain.CurrentDomain events.

// Handle SIGINT (Ctrl+C) and SIGTERM.
var cts = new CancellationTokenSource();

// Handle Ctrl+C (SIGINT).
Console.CancelKeyPress += (_, e) => {
    e.Cancel = true; // Prevent immediate termination.
    Console.WriteLine("\nReceived SIGINT");
    cts.Cancel();
};

// Handle SIGTERM on Unix via PosixSignalRegistration.
// (Available in .NET 6+)
using var sigterm = PosixSignalRegistration.Create(PosixSignal.SIGTERM, context => {
    Console.WriteLine("Received SIGTERM");
    cts.Cancel();
});

Console.WriteLine("awaiting signal");

try
{
    // Wait until cancelled.
    await Task.Delay(Timeout.Infinite, cts.Token);
}
catch (OperationCanceledException)
{
    // Signal received.
}

Console.WriteLine("exiting gracefully");
