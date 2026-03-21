// Environment variables in F#.

System.Environment.SetEnvironmentVariable("FOO", "1")
printfn "FOO: %s" (System.Environment.GetEnvironmentVariable("FOO") |> Option.ofObj |> Option.defaultValue "(not set)")
printfn "BAR: %s" (System.Environment.GetEnvironmentVariable("BAR") |> Option.ofObj |> Option.defaultValue "(not set)")

// List first few env vars.
System.Environment.GetEnvironmentVariables()
|> Seq.cast<System.Collections.DictionaryEntry>
|> Seq.take 5
|> Seq.iter (fun de -> printfn "%s: %s" (string de.Key) (string de.Value))
