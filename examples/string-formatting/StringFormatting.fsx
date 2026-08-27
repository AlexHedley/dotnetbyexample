// String formatting in F# uses printf-style formatting and interpolation.

printfn "%-10s" "golang"
printfn "%.2f" 3.14159
printfn "|%-10s|" "left"
printfn "|%10s|" "right"
printfn "%b" true
printfn "%X" 255
printfn "%02x" 255
printfn "%05d" 12

let name = "World"
printfn "Hello, %s!" name

// sprintf for creating formatted strings.
let s = sprintf "struct: {Name: %s, Value: %d}" "test" 42
printfn "%s" s
