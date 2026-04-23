' HTTP Server in VB.NET using HttpListener.

Imports System.Net

Console.WriteLine("HTTP Server example")
Console.WriteLine("A simple server responds to /hello and /headers endpoints.")

' In a real application:
' Dim listener As New HttpListener()
' listener.Prefixes.Add("http://localhost:8090/")
' listener.Start()
' Console.WriteLine("Listening on port 8090...")
' Dim context As HttpListenerContext = Await listener.GetContextAsync()
' ' Handle context...
