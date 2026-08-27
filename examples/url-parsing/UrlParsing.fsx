// URL parsing in F# using System.Uri.

let s = "postgres://user:pass@host.com:5432/path?k=v#f"
let u = System.Uri(s)

printfn "%s" u.Scheme
printfn "%s" u.Host
printfn "%d" u.Port
printfn "%s" u.UserInfo

let userInfo = u.UserInfo.Split(':')
printfn "%s" userInfo.[0]
printfn "%s" userInfo.[1]

printfn "%s" u.AbsolutePath
printfn "%s" u.Fragment
printfn "%s" u.Query
