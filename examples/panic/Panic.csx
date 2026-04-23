// In .NET, Go's panic is equivalent to throwing an exception.
// A panic means something went unexpectedly wrong.
// We use it for errors that shouldn't occur during normal operation,
// or that we aren't prepared to handle gracefully.

// We'll use throw throughout this site to panic.
// Here's an example of panicking if we get an unexpected error.
void Panic(string msg)
{
    throw new InvalidOperationException(msg);
}

// A common use of panic is to abort if a function returns an error value
// that we don't know how to (or want to) handle.
// This example panics if we get an unexpected error.
try
{
    // A program that panics.
    Panic("a problem");
}
catch (InvalidOperationException e)
{
    Console.WriteLine("Caught panic:", e.Message);
}

// Note that unlike some languages which use exceptions for handling
// of many errors, in .NET it's idiomatic to handle errors via
// exception handling or result types.
