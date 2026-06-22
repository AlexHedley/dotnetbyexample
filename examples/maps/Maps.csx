// Maps are .NET's builtin associative data type.
// Dictionary<K,V> is equivalent to Go's maps.

// To create an empty map, use Dictionary<K,V>.
var m = new Dictionary<string, int>();

// Set key/value pairs using the typical name[key] = val syntax.
m["k1"] = 7;
m["k2"] = 13;

// Printing a map with e.g. Console.WriteLine will show all of its key/value pairs.
Console.WriteLine("map:", m["k1"], m["k2"]);

// Get a value for a key with name[key].
int v1 = m["k1"];
Console.WriteLine("v1:", v1);

// If the key doesn't exist, you can check with TryGetValue.
if (m.TryGetValue("k3", out int v3))
    Console.WriteLine("v3:", v3);
else
    Console.WriteLine("v3: 0");

// The Count property returns the number of key/value pairs.
Console.WriteLine("len:", m.Count);

// The Remove method removes key/value pairs from a map.
m.Remove("k2");
Console.WriteLine("map:", m.Count);

// To check if a key is in the map, use ContainsKey.
bool prs = m.ContainsKey("k2");
Console.WriteLine("prs:", prs);

// You can also declare and initialize a new map in the same line with this syntax.
var n = new Dictionary<string, int>
{
    ["foo"] = 1,
    ["bar"] = 2
};
Console.WriteLine("map:", n["foo"], n["bar"]);
