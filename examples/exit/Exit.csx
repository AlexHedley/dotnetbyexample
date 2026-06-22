// Use Environment.Exit to immediately exit with a given status.

// defer (try/finally) will NOT be run when using Environment.Exit.
try
{
    // This will not be executed after Environment.Exit.
}
finally
{
    // Note: This will NOT be called if Environment.Exit is used directly.
    // Console.WriteLine("this won't run");
}

Console.WriteLine("exiting");

// Exit with status 3.
Environment.Exit(3);
