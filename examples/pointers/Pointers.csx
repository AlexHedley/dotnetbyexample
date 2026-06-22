// .NET supports pointers in unsafe code. For normal C# code,
// ref and out parameters are used to pass references.

// We'll show how ref parameters work in .NET.
// This is equivalent to Go's pointer parameters.
void Zeroval(int ival)
{
    ival = 0;
}

void Zeroptr(ref int iptr)
{
    iptr = 0;
}

int i = 1;
Console.WriteLine("initial:", i);

// Zeroval doesn't change i (passed by value).
Zeroval(i);
Console.WriteLine("zeroval:", i);

// Zeroptr changes i (passed by ref).
Zeroptr(ref i);
Console.WriteLine("zeroptr:", i);

// In unsafe C#, you can work with actual memory addresses.
// Using unsafe context:
unsafe
{
    int j = 42;
    int* p = &j;
    Console.WriteLine($"pointer: {(long)p:X}");
    Console.WriteLine("value via pointer:", *p);
}
