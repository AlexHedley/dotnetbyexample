// HTTP Server in F# using HttpListener.
open System.Net

printfn "HTTP Server example"
printfn "A simple server responds to /hello and /headers endpoints."

// In a real application:
// let listener = new HttpListener()
// listener.Prefixes.Add("http://localhost:8090/")
// listener.Start()
// printfn "Listening on port 8090..."
// let context = listener.GetContextAsync() |> Async.AwaitTask |> Async.RunSynchronously
// let response = context.Response
// let bytes = System.Text.Encoding.UTF8.GetBytes("hello\n")
// response.OutputStream.Write(bytes, 0, bytes.Length)
// response.Close()
