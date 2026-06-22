// Methods in F# are defined as member functions on types.

// Define a struct with methods using member.
type Rect(width: float, height: float) =
    member _.Width = width
    member _.Height = height
    member this.Area() = this.Width * this.Height
    member this.Perim() = 2.0 * this.Width + 2.0 * this.Height
    override this.ToString() = sprintf "{Width:%g, Height:%g}" this.Width this.Height

let r = Rect(3.0, 4.0)

printfn "area: %g" (r.Area())
printfn "perim: %g" (r.Perim())
