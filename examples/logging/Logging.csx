// The standard library provides logging via Microsoft.Extensions.Logging.
// Here we use the simple Console-based logging.

using Microsoft.Extensions.Logging;

// Basic logging with Console.
Console.WriteLine("standard logger");

// Using ILogger via Microsoft.Extensions.Logging.
var loggerFactory = LoggerFactory.Create(builder => {
    builder.AddConsole();
    builder.SetMinimumLevel(LogLevel.Debug);
});

var logger = loggerFactory.CreateLogger("example");
logger.LogInformation("log message");
logger.LogInformation("log with fields: {Key}={Value}", "key", "val");

// Different log levels.
logger.LogDebug("debug message");
logger.LogWarning("warning message");
logger.LogError("error message");

// Structured logging with timestamps is built-in.
logger.LogInformation("{Time}: my log message", DateTime.Now);
