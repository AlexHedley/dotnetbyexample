' Tickers in VB.NET use PeriodicTimer.

Dim ticker As New PeriodicTimer(TimeSpan.FromMilliseconds(500))
Dim done As New CancellationTokenSource()

Dim tickerTask As Task = Task.Run(Async Function()
    While Await ticker.WaitForNextTickAsync(done.Token)
        Console.WriteLine("tick at " & DateTime.Now)
    End While
End Function)

Await Task.Delay(1500)
done.Cancel()

Try
    Await tickerTask
Catch
End Try
Console.WriteLine("Ticker stopped")
