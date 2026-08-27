' XML in VB.NET.

Imports System.Xml
Imports System.Xml.Serialization

<XmlRoot("plant")>
Class Plant
    <XmlAttribute("id")>
    Public Property Id As Integer
    
    <XmlElement("name")>
    Public Property Name As String = ""
    
    Public Overrides Function ToString() As String
        Return $"Plant id={Id}, name={Name}"
    End Function
End Class

Dim p As New Plant With {.Id = 27, .Name = "Fir"}
Dim serializer As New XmlSerializer(GetType(Plant))
Dim sw As New StringWriter()
serializer.Serialize(sw, p)
Console.WriteLine(sw.ToString())

Dim xml As String = "<plant id=""27""><name>Fir</name></plant>"
Dim doc As New XmlDocument()
doc.LoadXml(xml)
Console.WriteLine(doc.DocumentElement?.GetAttribute("id"))
