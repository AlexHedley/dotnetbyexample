// .NET supports command-line flag parsing via System.CommandLine or manual parsing.
// Here we demonstrate a simple manual approach.

// Parse flags manually.
string word = "foo";
int numb = 42;
bool fork = false;
string svar = "bar";

for (int i = 0; i < args.Length; i++)
{
    if (args[i].StartsWith("--word="))
        word = args[i].Substring(7);
    else if (args[i].StartsWith("--numb="))
        numb = int.Parse(args[i].Substring(7));
    else if (args[i] == "--fork")
        fork = true;
    else if (args[i].StartsWith("--svar="))
        svar = args[i].Substring(7);
}

Console.WriteLine("word:", word);
Console.WriteLine("numb:", numb);
Console.WriteLine("fork:", fork);
Console.WriteLine("svar:", svar);
Console.WriteLine("tail:", string.Join(", ", args));
