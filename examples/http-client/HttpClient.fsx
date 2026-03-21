// HTTP Client in F#.
open System.Net.Http

let client = new HttpClient()

let main = async {
    let! resp = client.GetAsync("https://gobyexample.com") |> Async.AwaitTask
    printfn "Response status: %A %d" resp.StatusCode (int resp.StatusCode)
    
    let! body = resp.Content.ReadAsStringAsync() |> Async.AwaitTask
    body.Split('\n') |> Array.take (min 5 (body.Split('\n').Length)) |> Array.iter (printfn "%s")
}

Async.RunSynchronously main
