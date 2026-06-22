// XML in F# using System.Xml.
open System.Xml

// Create XML using XmlDocument.
let doc = XmlDocument()
let root = doc.CreateElement("plant")
root.SetAttribute("id", "27")
let nameElem = doc.CreateElement("name")
nameElem.InnerText <- "Fir"
root.AppendChild(nameElem) |> ignore
doc.AppendChild(root) |> ignore

let sw = new System.IO.StringWriter()
doc.Save(sw)
printfn "%s" (sw.ToString())

// Parse XML.
let xml = "<plant id=\"27\"><name>Fir</name><origin>intl</origin></plant>"
let doc2 = XmlDocument()
doc2.LoadXml(xml)
printfn "%s" (doc2.DocumentElement.GetAttribute("id"))
printfn "%s" (doc2.DocumentElement.["name"].InnerText)
