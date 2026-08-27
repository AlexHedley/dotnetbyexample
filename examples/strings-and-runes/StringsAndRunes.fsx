// In F#, strings are .NET strings (UTF-16 sequences).

let s = "สวัสดี"

// Length returns the number of UTF-16 code units.
printfn "Len: %d" s.Length

// Iterate over runes using EnumerateRunes.
s.EnumerateRunes() |> Seq.iter (fun r ->
    printf "rune: %d '%c' " r.Value (char r.Value))
printfn ""

// Use StringInfo for Unicode text elements.
let si = System.Globalization.StringInfo(s)
printfn "Text element count: %d" si.LengthInTextElements
