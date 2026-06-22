// String functions in F#.

printfn "Contains:  %b" ("test".Contains("es"))
printfn "HasPrefix: %b" ("test".StartsWith("te"))
printfn "HasSuffix: %b" ("test".EndsWith("st"))
printfn "Index:     %d" ("test".IndexOf("e"))
printfn "Join:      %s" (System.String.Join("-", [|"a"; "b"|]))
printfn "Repeat:    %s" (System.String.Concat(Array.replicate 5 "a"))
printfn "Replace:   %s" ("foo".Replace("o", "0"))
printfn "Split:     %A" ("a-b-c-d-e".Split('-'))
printfn "ToLower:   %s" ("TEST".ToLower())
printfn "ToUpper:   %s" ("test".ToUpper())
printfn "Trim:      %s" ("  test  ".Trim())
