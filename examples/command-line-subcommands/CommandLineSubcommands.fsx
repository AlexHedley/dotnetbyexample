// Command-line subcommands in F#.

let args = fsi.CommandLineArgs |> Array.skip 1

if args.Length = 0 then
    printfn "expected 'foo' or 'bar' subcommand"
    exit 1

let subCmd = args.[0]
let subArgs = args |> Array.skip 1

match subCmd with
| "foo" ->
    let enable = subArgs |> Array.contains "--enable"
    let name = subArgs |> Array.tryFind (fun a -> a.StartsWith("--name="))
                       |> Option.map (fun a -> a.[7..])
                       |> Option.defaultValue "default"
    printfn "subcommand 'foo'"
    printfn "  enable: %b" enable
    printfn "  name: %s" name
| "bar" ->
    let level = subArgs |> Array.tryFind (fun a -> a.StartsWith("--level="))
                        |> Option.map (fun a -> int a.[8..])
                        |> Option.defaultValue 0
    printfn "subcommand 'bar'"
    printfn "  level: %d" level
| _ ->
    eprintfn "unknown subcommand: %s" subCmd
    exit 1
