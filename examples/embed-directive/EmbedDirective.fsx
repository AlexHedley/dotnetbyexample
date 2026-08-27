// In F#, embedded resources are accessed via Assembly.GetManifestResourceStream.

let fileContents = "hello dotnet\n"
printfn "%s" fileContents

// In a project, access embedded resources like this:
// let assembly = System.Reflection.Assembly.GetExecutingAssembly()
// let stream = assembly.GetManifestResourceStream("Namespace.file.txt")
printfn "Embedded resource example - see project configuration for full usage."
