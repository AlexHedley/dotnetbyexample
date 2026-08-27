// .NET provides HTTP server functionality via HttpListener or ASP.NET Core.
// Here we use HttpListener for a simple example.

using System.Net;

// Create a simple HTTP server.
void Hello(HttpListenerContext context)
{
    context.Response.ContentType = "text/plain";
    var msg = System.Text.Encoding.UTF8.GetBytes("hello\n");
    context.Response.OutputStream.Write(msg);
    context.Response.Close();
}

void Headers(HttpListenerContext context)
{
    foreach (var key in context.Request.Headers.AllKeys)
    {
        var buffer = System.Text.Encoding.UTF8.GetBytes($"{key}: {context.Request.Headers[key]}\n");
        context.Response.OutputStream.Write(buffer);
    }
    context.Response.Close();
}

var listener = new HttpListener();
listener.Prefixes.Add("http://localhost:8090/hello/");
listener.Prefixes.Add("http://localhost:8090/headers/");

Console.WriteLine("Starting server on http://localhost:8090");
Console.WriteLine("Press Ctrl+C to stop.");

// Note: In a .csx script, this would block and run a server.
// listener.Start();
// var context = await listener.GetContextAsync();
// if (context.Request.Url?.AbsolutePath == "/hello/") Hello(context);
// else if (context.Request.Url?.AbsolutePath == "/headers/") Headers(context);

Console.WriteLine("HTTP server example - see comment for full implementation.");
