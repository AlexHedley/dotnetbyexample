// .NET offers built-in support for regular expressions via System.Text.RegularExpressions.

using System.Text.RegularExpressions;

// Test whether a pattern matches a string.
var matched = Regex.IsMatch("peach punch", @"p([a-z]+)ch");
Console.WriteLine(matched);

// Find the first match.
var r = new Regex(@"p([a-z]+)ch");
Console.WriteLine(r.Match("peach punch").Value);

// For other match info, use Match class.
var m = r.Match("peach punch");
Console.WriteLine(m.Groups[0].Value); // Whole match.
Console.WriteLine(m.Groups[1].Value); // Submatch (captured group).

// Find all matches.
var matches = r.Matches("peach punch pinch");
Console.WriteLine("matches:", matches.Count);
foreach (Match match in matches)
    Console.WriteLine(match.Value);

// Replace using regex.
Console.WriteLine(r.Replace("a peach", "<fruit>"));

// Replace with a custom function.
Console.WriteLine(r.Replace("a peach", m => m.Value.ToUpper()));
