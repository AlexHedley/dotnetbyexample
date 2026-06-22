// .NET implements several hash functions in System.Security.Cryptography.

using System.Security.Cryptography;
using System.Text;

var s = "sha256 this string";

// Compute SHA256 hash.
var bytes = Encoding.UTF8.GetBytes(s);
var hash = SHA256.HashData(bytes);

// Print the hash as hex.
Console.WriteLine(Convert.ToHexString(hash).ToLower());

// Verify by comparing with expected.
var expected = "1af1dfa857bf1d8814fe1af8983c18080019922e557f15a8a0d3db739d77aacb";
Console.WriteLine(Convert.ToHexString(hash).ToLower() == expected);
