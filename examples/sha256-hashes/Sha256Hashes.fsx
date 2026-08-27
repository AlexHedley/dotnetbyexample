// SHA256 hashes in F#.
open System.Security.Cryptography
open System.Text

let s = "sha256 this string"
let bytes = Encoding.UTF8.GetBytes(s)
let hash = SHA256.HashData(bytes)

printfn "%s" (System.Convert.ToHexString(hash).ToLower())
