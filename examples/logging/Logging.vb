' Logging in VB.NET.

' Basic console logging.
Console.WriteLine("standard logger")

' Using Console.Error for stderr logging.
Console.Error.WriteLine("error log")

' Formatted log message.
Dim logMessage As String = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] INFO: log message"
Console.WriteLine(logMessage)
