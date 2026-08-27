// .NET's Uri class provides URL parsing functionality.

// Parse a URL.
var s = "postgres://user:pass@host.com:5432/path?k=v#f";
var u = new Uri(s);

// Scheme, host, port.
Console.WriteLine(u.Scheme);
Console.WriteLine(u.Host); // host:port
Console.WriteLine(u.Port);
Console.WriteLine(u.UserInfo); // user:pass

var userInfo = u.UserInfo.Split(':');
Console.WriteLine(userInfo[0]); // user
Console.WriteLine(userInfo[1]); // password

// Path and fragment.
Console.WriteLine(u.AbsolutePath);
Console.WriteLine(u.Fragment);

// Query string parameters.
Console.WriteLine(u.Query);
var queryParams = System.Web.HttpUtility.ParseQueryString(u.Query);
Console.WriteLine(queryParams["k"]);
