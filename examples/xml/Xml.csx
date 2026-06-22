// .NET provides built-in XML support via System.Xml.

using System.Xml;
using System.Xml.Serialization;

// Define a type to represent a plant.
[XmlRoot("plant")]
class Plant
{
    [XmlAttribute("id")]
    public int Id { get; set; }
    
    [XmlElement("name")]
    public string Name { get; set; } = "";
    
    [XmlElement("origin")]
    public List<string> Origin { get; set; } = new();
    
    public override string ToString() => $"Plant id={Id}, name={Name}, origin={string.Join(", ", Origin)}";
}

// Serialize to XML.
var p = new Plant { Id = 27, Name = "Fir", Origin = new List<string> { "intl", "canada" } };

var serializer = new XmlSerializer(typeof(Plant));
using var writer = new StringWriter();
serializer.Serialize(writer, p);
Console.WriteLine(writer.ToString());

// Use XmlDocument for more flexible XML manipulation.
var xml = "<plant id=\"27\"><name>Fir</name></plant>";
var doc = new XmlDocument();
doc.LoadXml(xml);
Console.WriteLine(doc.DocumentElement?.GetAttribute("id"));
Console.WriteLine(doc.DocumentElement?["name"]?.InnerText);
