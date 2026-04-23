// Timers in .NET are provided by System.Timers.Timer and Task.Delay.

// Fire once after 2 seconds using Task.Delay.
var timer1 = Task.Run(async () => {
    await Task.Delay(2000);
    Console.WriteLine("Timer 1 fired");
});

// We can also create a Cancellable timer.
var cts = new CancellationTokenSource();
var timer2 = Task.Run(async () => {
    await Task.Delay(1000, cts.Token);
    Console.WriteLine("Timer 2 fired");
}, cts.Token);

// Cancel timer2 before it fires.
cts.Cancel();

// Wait for timer1 to fire.
await timer1;

// timer2 was cancelled, show it was stopped.
Console.WriteLine("Timer 2 stopped:", timer2.IsCanceled);
