' Buffered channels in VB.NET use bounded Channel.
Imports System.Threading.Channels

Dim ch As Channel(Of String) = Channel.CreateBounded(Of String)(2)

Await ch.Writer.WriteAsync("buffered")
Await ch.Writer.WriteAsync("channel")

Console.WriteLine(Await ch.Reader.ReadAsync())
Console.WriteLine(Await ch.Reader.ReadAsync())
