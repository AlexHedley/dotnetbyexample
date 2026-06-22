// .NET provides number parsing via Parse and TryParse methods.

// Parse a float.
var f = float.Parse("1.234");
Console.WriteLine(f);

// Parse an int.
var i = int.Parse("123");
Console.WriteLine(i);

// Parse a hex string.
var d = Convert.ToInt64("0x1c8", 16);
Console.WriteLine(d);

// Parse a binary string.
var b = Convert.ToInt32("111100000100101001", 2);
Console.WriteLine(b);

// Parse an int from a string (base 10 by default).
Console.WriteLine(int.Parse("1234567890"));

// TryParse for error handling.
if (int.TryParse("abc", out var n))
    Console.WriteLine(n);
else
    Console.WriteLine("Parse error");
