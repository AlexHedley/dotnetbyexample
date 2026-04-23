// Environment variables are a universal mechanism for conveying
// configuration information to programs.

// Use Environment.SetEnvironmentVariable to set environment variables.
Environment.SetEnvironmentVariable("FOO", "1");
Console.WriteLine("FOO:", Environment.GetEnvironmentVariable("FOO"));
Console.WriteLine("BAR:", Environment.GetEnvironmentVariable("BAR") ?? "(not set)");

// Use Environment.GetEnvironmentVariables to list all env vars.
foreach (System.Collections.DictionaryEntry de in Environment.GetEnvironmentVariables())
{
    Console.WriteLine($"{de.Key}: {de.Value}");
}
