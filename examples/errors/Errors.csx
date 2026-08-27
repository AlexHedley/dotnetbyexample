// In .NET, errors are typically represented by exceptions.
// However, for functional error handling, you can use result types.

// A simple error-returning function using exceptions.
int Divide(int a, int b)
{
    if (b == 0)
        throw new DivideByZeroException("Cannot divide by zero");
    return a / b;
}

// Use try/catch to handle errors.
try
{
    int result = Divide(10, 2);
    Console.WriteLine("10 / 2 =", result);
}
catch (DivideByZeroException e)
{
    Console.WriteLine("Error:", e.Message);
}

try
{
    int result = Divide(10, 0);
    Console.WriteLine("10 / 0 =", result);
}
catch (DivideByZeroException e)
{
    Console.WriteLine("Error:", e.Message);
}

// Sentinel errors (predefined exception types) are used to check for specific conditions.
bool IsNotFound(Exception e) => e is FileNotFoundException;
Console.WriteLine(IsNotFound(new FileNotFoundException("test.txt not found")));
Console.WriteLine(IsNotFound(new Exception("other error")));
