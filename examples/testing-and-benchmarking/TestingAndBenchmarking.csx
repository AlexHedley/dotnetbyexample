// Unit testing in .NET is done with frameworks like xUnit, NUnit, or MSTest.
// Benchmarking can be done with BenchmarkDotNet.

// Here's a simple example of a function and its test.

// The function to test.
int IntMin(int a, int b) => a < b ? a : b;

// Manual test (in a real project, use xUnit/NUnit/MSTest).
void TestIntMin()
{
    int ans = IntMin(2, -2);
    if (ans != -2)
        throw new Exception($"IntMin(2, -2) = {ans}; want -2");
    Console.WriteLine("TestIntMin passed");
}

// Table-driven tests.
void TestIntMinTableDriven()
{
    var tests = new[]
    {
        (a: 0, b: 1, expected: 0),
        (a: 1, b: 0, expected: 0),
        (a: 2, b: -2, expected: -2),
        (a: 0, b: -1, expected: -1),
        (a: -1, b: 0, expected: -1),
    };

    foreach (var tt in tests)
    {
        int ans = IntMin(tt.a, tt.b);
        if (ans != tt.expected)
            throw new Exception($"IntMin({tt.a}, {tt.b}) = {ans}; want {tt.expected}");
    }
    Console.WriteLine("TestIntMinTableDriven passed");
}

TestIntMin();
TestIntMinTableDriven();
Console.WriteLine("All tests passed!");
