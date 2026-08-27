// The standard library's string class provides many useful string-related functions.
// Here are some examples to give you a sense of the package.

// We give aliases to string functions to save typing.
Func<string, string, bool> Contains = string.Contains;
Console.WriteLine("Contains:  ", "test".Contains("es"));
Console.WriteLine("Count:     ", "test".Count(c => c == 't'));
Console.WriteLine("HasPrefix: ", "test".StartsWith("te"));
Console.WriteLine("HasSuffix: ", "test".EndsWith("st"));
Console.WriteLine("Index:     ", "test".IndexOf("e"));
Console.WriteLine("Join:      ", string.Join("-", new[] { "a", "b" }));
Console.WriteLine("Repeat:    ", string.Concat(Enumerable.Repeat("a", 5)));
Console.WriteLine("Replace:   ", "foo".Replace("o", "0"));
Console.WriteLine("Replace:   ", "foo".Replace("o", "0"));
Console.WriteLine("Split:     ", string.Join(" ", "a-b-c-d-e".Split('-')));
Console.WriteLine("ToLower:   ", "TEST".ToLower());
Console.WriteLine("ToUpper:   ", "test".ToUpper());
Console.WriteLine("Trim:      ", "  test  ".Trim());
