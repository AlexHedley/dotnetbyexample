// .NET provides excellent JSON support via System.Text.Json.

using System.Text.Json;
using System.Text.Json.Serialization;

// Define types for marshalling/unmarshalling.
record Response(
    [property: JsonPropertyName("page")] int Page,
    [property: JsonPropertyName("fruits")] List<string> Fruits
);

// Encoding (serialization) to JSON.
var map = new Dictionary<string, object>
{
    { "page", 1 },
    { "fruits", new[] { "apple", "peach", "pear" } }
};

Console.WriteLine(JsonSerializer.Serialize(map));

// Serialize a record.
var response = new Response(1, new List<string> { "apple", "peach", "pear" });
Console.WriteLine(JsonSerializer.Serialize(response));

// Decode JSON strings.
var json = "{\"num\":6.13,\"strs\":[\"a\",\"b\"]}";
var doc = JsonDocument.Parse(json);
Console.WriteLine(doc.RootElement.GetProperty("num").GetDouble());
Console.WriteLine(doc.RootElement.GetProperty("strs")[0].GetString());

// Unmarshal into a type.
var r = JsonSerializer.Deserialize<Response>("{\"page\":2,\"fruits\":[\"mango\"]}");
Console.WriteLine(r?.Page);
Console.WriteLine(r?.Fruits?[0]);
