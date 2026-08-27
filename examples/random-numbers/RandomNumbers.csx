// .NET's Random class provides pseudorandom number generation.

// Create a Random instance.
var rng = new Random();

// Random int (0 to int.MaxValue).
Console.WriteLine(rng.Next(), rng.Next());

// Random int in a range [0, 100).
Console.WriteLine(rng.Next(0, 100));

// Random float [0.0, 1.0).
Console.WriteLine(rng.NextDouble());
Console.WriteLine(rng.NextDouble());

// Seeded random for reproducible results.
var seeded = new Random(42);
Console.WriteLine(seeded.Next());
Console.WriteLine(seeded.Next());

// Random bytes.
byte[] bytes = new byte[4];
rng.NextBytes(bytes);
Console.WriteLine(BitConverter.ToString(bytes));

// Use Random.Shared for thread-safe access.
Console.WriteLine(Random.Shared.Next(0, 100));
