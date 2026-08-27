// .NET's equivalent to Go's recover is try/catch.
// Recover means catching and handling exceptions gracefully.

void SafeDivide(int a, int b)
{
    try
    {
        Console.WriteLine(a / b);
    }
    catch (DivideByZeroException e)
    {
        // Recovered from panic/exception.
        Console.WriteLine("Recovered:", e.Message);
    }
}

// A function that might "panic" (throw).
void MayPanic()
{
    throw new InvalidOperationException("a problem");
}

// Recover (catch) the exception.
void SafeDiv()
{
    try
    {
        MayPanic();
    }
    catch (Exception e)
    {
        Console.WriteLine("Recovered. Error:", e.Message);
    }
    Console.WriteLine("After SafeDiv");
}

SafeDivide(10, 2);
SafeDivide(10, 0);
SafeDiv();
