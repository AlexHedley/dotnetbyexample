' String functions in VB.NET.

Console.WriteLine("Contains:  " & "test".Contains("es"))
Console.WriteLine("HasPrefix: " & "test".StartsWith("te"))
Console.WriteLine("HasSuffix: " & "test".EndsWith("st"))
Console.WriteLine("Index:     " & "test".IndexOf("e"))
Console.WriteLine("Join:      " & String.Join("-", {"a", "b"}))
Console.WriteLine("Replace:   " & "foo".Replace("o", "0"))
Console.WriteLine("Split:     " & String.Join(" ", "a-b-c-d-e".Split("-"c)))
Console.WriteLine("ToLower:   " & "TEST".ToLower())
Console.WriteLine("ToUpper:   " & "test".ToUpper())
Console.WriteLine("Trim:      " & "  test  ".Trim())
