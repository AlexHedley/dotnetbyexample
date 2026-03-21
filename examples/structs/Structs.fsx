// Structs in F# are value types; records are immutable reference types.

// Define a struct using [<Struct>] attribute or a record.
type Person = { Name: string; Age: int }

// Constructor function.
let newPerson name = { Name = name; Age = 42 }

// Create structs.
printfn "%A" { Name = "Bob"; Age = 20 }
printfn "%A" { Name = "Alice"; Age = 30 }
printfn "%A" { Name = "Fred"; Age = 0 }
printfn "%A" (newPerson "Jon")

// Access fields.
let s = { Name = "Sean"; Age = 50 }
printfn "%s" s.Name

// Records use 'with' for non-destructive updates (immutable).
let sp = { s with Name = "Sean" }
printfn "%s" sp.Name

// F# records are reference types but immutable.
let dog1 = { Name = "Rex"; Age = 5 }
let dog2 = { dog1 with Name = "Max" }
printfn "%s" dog1.Name
printfn "%s" dog2.Name
