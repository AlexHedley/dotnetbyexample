// Testing in F# uses xUnit or Expecto.
// Here's a simple manual test example.

let intMin a b = if a < b then a else b

let testIntMin () =
    let ans = intMin 2 -2
    if ans <> -2 then
        failwithf "intMin(2, -2) = %d; want -2" ans
    printfn "testIntMin passed"

// Table-driven tests.
let testIntMinTableDriven () =
    let tests = [
        (0, 1, 0)
        (1, 0, 0)
        (2, -2, -2)
        (0, -1, -1)
        (-1, 0, -1)
    ]
    tests |> List.iter (fun (a, b, expected) ->
        let ans = intMin a b
        if ans <> expected then
            failwithf "intMin(%d, %d) = %d; want %d" a b ans expected
    )
    printfn "testIntMinTableDriven passed"

testIntMin ()
testIntMinTableDriven ()
printfn "All tests passed!"
