' .NET has several built-in value types.
' Here are a few basic examples.

' Strings, which can be added together with +.
Console.WriteLine("go" & "lang")

' Integers and floats.
Console.WriteLine("1+1 = " & (1+1).ToString())
Console.WriteLine("7.0/3.0 = " & (7.0/3.0).ToString())

' Booleans, with boolean operators as you'd expect.
Console.WriteLine(True AndAlso False)
Console.WriteLine(True OrElse False)
Console.WriteLine(Not True)
