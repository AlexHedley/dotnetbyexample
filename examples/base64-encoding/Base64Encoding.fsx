// Base64 encoding in F#.
open System.Text

let data = "abc123!?$*&()'-=@~"

// Encode.
let encoded = System.Convert.ToBase64String(Encoding.UTF8.GetBytes(data))
printfn "%s" encoded

// URL-safe.
let urlEncoded = encoded.Replace('+', '-').Replace('/', '_')
printfn "%s" urlEncoded

// Decode.
let decoded = Encoding.UTF8.GetString(System.Convert.FromBase64String(encoded))
printfn "%s" decoded
