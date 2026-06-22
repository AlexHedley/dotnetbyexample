' Random numbers in VB.NET.

Dim rng As New Random()

Console.WriteLine(rng.Next() & " " & rng.Next())
Console.WriteLine(rng.Next(0, 100))
Console.WriteLine(rng.NextDouble())

Dim seeded As New Random(42)
Console.WriteLine(seeded.Next())
Console.WriteLine(seeded.Next())
