' SHA256 hashes in VB.NET.

Imports System.Security.Cryptography
Imports System.Text

Dim s As String = "sha256 this string"
Dim bytes() As Byte = Encoding.UTF8.GetBytes(s)
Dim hash() As Byte = SHA256.HashData(bytes)

Console.WriteLine(Convert.ToHexString(hash).ToLower())
