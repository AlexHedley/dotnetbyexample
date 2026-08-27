// .NET provides base64 encoding/decoding via Convert.ToBase64String.

using System.Text;

var data = "abc123!?$*&()'-=@~";

// Encode to base64.
var encoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(data));
Console.WriteLine(encoded);

// URL-safe base64 (replace + with -, / with _).
var urlEncoded = encoded.Replace('+', '-').Replace('/', '_');
Console.WriteLine(urlEncoded);

// Decode from base64.
var decoded = Encoding.UTF8.GetString(Convert.FromBase64String(encoded));
Console.WriteLine(decoded);

// Decode URL-safe base64.
var restored = urlEncoded.Replace('-', '+').Replace('_', '/');
var decodedUrl = Encoding.UTF8.GetString(Convert.FromBase64String(restored));
Console.WriteLine(decodedUrl);
