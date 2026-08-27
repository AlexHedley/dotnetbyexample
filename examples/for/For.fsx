// F# uses recursion or sequence expressions for loops.
// For imperative loops, F# has for and while.

// A while loop.
let mutable i = 1
while i <= 3 do
    printfn "%d" i
    i <- i + 1

// A classic for loop (for i = 0 to 2).
for j = 0 to 2 do
    printfn "%d" j

// Using Seq.iter for a range.
{0..2} |> Seq.iter (fun k -> printfn "range %d" k)

// An infinite loop with a break (using exception or mutable).
let mutable running = true
while running do
    printfn "loop"
    running <- false

// Continue equivalent using if.
for n = 0 to 5 do
    if n % 2 <> 0 then
        printfn "%d" n
