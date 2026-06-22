// Interfaces in F# use abstract members or object expressions.

// Define an interface.
type IGeometry =
    abstract member Area: unit -> float
    abstract member Perim: unit -> float

// Implement the interface using object expressions.
let makeRect width height =
    { new IGeometry with
        member _.Area() = width * height
        member _.Perim() = 2.0 * width + 2.0 * height }

let makeCircle radius =
    { new IGeometry with
        member _.Area() = System.Math.PI * radius * radius
        member _.Perim() = 2.0 * System.Math.PI * radius }

let measure (g: IGeometry) =
    printfn "%g" (g.Area())
    printfn "%g" (g.Perim())

let r = makeRect 3.0 4.0
let c = makeCircle 5.0

measure r
measure c
