// Reading and writing files in .NET are basic tasks.
// Here are the first examples of reading files.

// Read the entire contents of a file.
// File.ReadAllText is the most basic file reading task.
var dat = File.ReadAllText("/tmp/dat");
Console.Write(dat);

// Use File.ReadAllBytes to read bytes.
var bytes = File.ReadAllBytes("/tmp/dat");
Console.WriteLine("bytes:", bytes.Length);
Console.WriteLine("first 5 bytes:", BitConverter.ToString(bytes[..5]));

// Streaming with StreamReader for more control.
using var f = new StreamReader("/tmp/dat");
var b1 = new char[5];
var n1 = await f.ReadAsync(b1, 0, 5);
Console.WriteLine($"{n1} chars: {new string(b1, 0, n1)}");

// Seek to a known location and read.
f.BaseStream.Seek(6, SeekOrigin.Begin);
f.DiscardBufferedData();
var b2 = new char[2];
var n2 = await f.ReadAsync(b2, 0, 2);
Console.WriteLine($"{n2} chars @ 6: {new string(b2, 0, n2)}");

// Read line by line.
var allLines = File.ReadAllLines("/tmp/dat");
foreach (var line in allLines)
    Console.WriteLine(line);
