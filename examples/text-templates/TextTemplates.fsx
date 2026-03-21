// Text templates in F#.

let name = "World"
printfn "Hello, %s!" name

// Simple template substitution.
let renderTemplate (template: string) (vars: Map<string, string>) =
    vars |> Map.fold (fun (acc: string) key value -> acc.Replace(sprintf "{{%s}}" key, value)) template

let vars = Map.ofList [("Name", "Alice"); ("Age", "30")]
let template = "Name: {{Name}}, Age: {{Age}}"
printfn "%s" (renderTemplate template vars)

// Using string builder.
let items = ["apple"; "banana"; "cherry"]
let sb = System.Text.StringBuilder()
sb.AppendLine("Items:") |> ignore
items |> List.iter (fun item -> sb.AppendLine(sprintf "  - %s" item) |> ignore)
printf "%s" (sb.ToString())
