// F# achieves composition through type inheritance and object expressions.

// A base type.
type Base(num: int) =
    member _.Num = num
    member _.Describe() = sprintf "base with num=%d" num

// A Container type that includes a Base.
type Container(num: int, str: string) =
    inherit Base(num)
    member _.Str = str

let co = Container(1, "some name")
printfn "co={Num: %d, Str: %s}" co.Num co.Str
printfn "describe: %s" (co.Describe())

// Interface.
type IDescriber =
    abstract member Describe: unit -> string

// Container implements IDescriber through inheritance from Base.
let d: IDescriber =
    { new IDescriber with
        member _.Describe() = co.Describe() }
printfn "describer: %s" (d.Describe())
