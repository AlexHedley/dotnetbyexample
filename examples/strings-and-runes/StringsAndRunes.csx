// In .NET, strings are sequences of UTF-16 characters (char).
// For Unicode code points (similar to Go's runes), use the int or char type.

// s is a string assigned a literal value.
string s = "สวัสดี";

// Since strings are sequences of chars in .NET,
// this will produce the length in chars (UTF-16 code units).
Console.WriteLine("Len:", s.Length);

// Iterating with a standard for loop will give UTF-16 chars.
for (int i = 0; i < s.Length; i++)
{
    Console.Write($"{(int)s[i]:X4} ");
}
Console.WriteLine();

// Use StringInfo to get actual Unicode text elements.
var si = new System.Globalization.StringInfo(s);
Console.WriteLine("Text element count:", si.LengthInTextElements);

// Enumerate Unicode text elements.
var enumerator = System.Globalization.StringInfo.GetTextElementEnumerator(s);
int idx = 0;
while (enumerator.MoveNext())
{
    var textElement = enumerator.GetTextElement();
    Console.WriteLine($"{idx} {string.Join(",", textElement.Select(c => (int)c)):} '{textElement}'");
    idx += textElement.Length;
}

// You can work with rune-like values using System.Text.Rune.
foreach (var rune in s.EnumerateRunes())
{
    Console.Write($"rune: {rune.Value} '{rune}' ");
}
Console.WriteLine();
