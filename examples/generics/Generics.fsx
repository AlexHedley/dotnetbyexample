// F# supports generics (called type parameters) naturally.

// A generic map function (List.map is built-in, but here's a custom one).
let mapSlice f lst = List.map f lst

// A generic stack using discriminated union.
type Stack<'T> =
    | Empty
    | Push of 'T * Stack<'T>

let push item stack = Push(item, stack)
let pop stack =
    match stack with
    | Push(item, rest) -> item, rest
    | Empty -> failwith "Stack is empty"
let isEmpty stack = stack = Empty

// Invoke with type inference.
let squares = mapSlice (fun x -> x * x) [1; 2; 3]
printfn "squares: %A" squares

let uppers = mapSlice (fun (x: string) -> x.ToUpper()) ["hello"; "world"]
printfn "upper: %A" uppers

let mutable st: Stack<int> = Empty
printfn "isEmpty: %b" (isEmpty st)

st <- push 1 st
st <- push 2 st
st <- push 3 st

let (v, st2) = pop st
printfn "pop: %d" v
printfn "isEmpty: %b" (isEmpty st2)
