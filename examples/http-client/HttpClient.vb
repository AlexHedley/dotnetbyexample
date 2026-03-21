' HTTP Client in VB.NET.

Dim client As New System.Net.Http.HttpClient()
Dim resp As System.Net.Http.HttpResponseMessage = Await client.GetAsync("https://gobyexample.com")

Console.WriteLine("Response status: " & resp.StatusCode & " " & CInt(resp.StatusCode))

Dim body As String = Await resp.Content.ReadAsStringAsync()
Dim lines = body.Split(vbLf).Take(5)
For Each line As String In lines
    Console.WriteLine(line)
Next
