// .NET's HttpClient makes HTTP requests.

using System.Net.Http;

// Issue an HTTP GET request to a server.
var client = new HttpClient();
var resp = await client.GetAsync("https://gobyexample.com");

// Print the HTTP response status.
Console.WriteLine("Response status:", resp.StatusCode, (int)resp.StatusCode);

// Print the first 5 lines of the response body.
var body = await resp.Content.ReadAsStringAsync();
var lines = body.Split('\n').Take(5);
foreach (var line in lines)
    Console.WriteLine(line);

// POST with JSON body.
// var content = new StringContent("{\"key\":\"value\"}", Encoding.UTF8, "application/json");
// var postResp = await client.PostAsync("https://httpbin.org/post", content);
