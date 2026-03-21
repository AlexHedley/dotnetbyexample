// JSON in F# using System.Text.Json.
open System.Text.Json

// Serialize a dictionary.
let data = dict [("page", box 1); ("fruits", box [|"apple"; "peach"; "pear"|])]
printfn "%s" (JsonSerializer.Serialize(data))

// Parse JSON.
let json = "{\"num\":6.13,\"strs\":[\"a\",\"b\"]}"
let doc = JsonDocument.Parse(json)
printfn "%g" (doc.RootElement.GetProperty("num").GetDouble())
printfn "%s" (doc.RootElement.GetProperty("strs").[0].GetString())
