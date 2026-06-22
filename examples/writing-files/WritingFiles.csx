// Writing files in .NET follows similar patterns to reading.

// The simplest way to write a file is File.WriteAllText.
File.WriteAllText("/tmp/dat1", "hello\ngo\n");

// For more granular writes, use a StreamWriter.
using (var f = new StreamWriter("/tmp/dat2"))
{
    var b1 = Encoding.UTF8.GetBytes("some\n");
    await f.BaseStream.WriteAsync(b1);
    
    var b2 = Encoding.UTF8.GetBytes("writes\n");
    await f.BaseStream.WriteAsync(b2);
    
    await f.WriteAsync("buffered\n");
    
    // Issue a Flush to make sure the writer flushes the write buffer.
    await f.FlushAsync();
}

// Verify the files.
Console.WriteLine(File.ReadAllText("/tmp/dat1"));
Console.WriteLine(File.ReadAllText("/tmp/dat2"));
