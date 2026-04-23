// Sorting by functions in F#.

// Sort strings by length.
let fruits = ["peach"; "kiwi"; "apple"]
let byLength = fruits |> List.sortBy (fun s -> s.Length)
printfn "By length: %A" byLength

// More complex sorting.
let sortedFruits = fruits |> List.sortWith (fun a b ->
    let cmp = compare a.Length b.Length
    if cmp <> 0 then cmp else compare a b)
printfn "Sorted: %A" sortedFruits

// Custom record type.
type Person = { Name: string; Age: int }

let people = [
    { Name = "Alice"; Age = 30 }
    { Name = "Bob"; Age = 25 }
    { Name = "Charlie"; Age = 35 }
]

let sortedPeople = people |> List.sortBy (fun p -> p.Age)
sortedPeople |> List.iter (fun p -> printfn "%s: %d" p.Name p.Age)
