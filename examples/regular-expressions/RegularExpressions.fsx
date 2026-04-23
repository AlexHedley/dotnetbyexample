// Regular expressions in F#.
open System.Text.RegularExpressions

// Test whether a pattern matches.
let matched = Regex.IsMatch("peach punch", @"p([a-z]+)ch")
printfn "%b" matched

// Find the first match.
let r = Regex(@"p([a-z]+)ch")
let m = r.Match("peach punch")
printfn "%s" m.Value
printfn "%s" m.Groups.[0].Value
printfn "%s" m.Groups.[1].Value

// Find all matches.
let matches = r.Matches("peach punch pinch")
printfn "matches: %d" matches.Count
for m in matches do
    printfn "%s" m.Value

// Replace.
printfn "%s" (r.Replace("a peach", "<fruit>"))
printfn "%s" (r.Replace("a peach", fun (m: Match) -> m.Value.ToUpper()))
