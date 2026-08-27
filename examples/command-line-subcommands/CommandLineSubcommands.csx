// .NET supports subcommands via System.CommandLine or manual parsing.
// Here we show a manual approach to handling subcommands.

if (args.Length == 0)
{
    Console.WriteLine("expected 'foo' or 'bar' subcommand");
    Environment.Exit(1);
}

string subCmd = args[0];
string[] subArgs = args.Skip(1).ToArray();

switch (subCmd)
{
    case "foo":
        // Parse foo's flags.
        bool enable = subArgs.Contains("--enable");
        string name = "default";
        foreach (var a in subArgs)
            if (a.StartsWith("--name=")) name = a[7..];
        Console.WriteLine("subcommand 'foo'");
        Console.WriteLine("  enable:", enable);
        Console.WriteLine("  name:", name);
        break;

    case "bar":
        // Parse bar's flags.
        int level = 0;
        foreach (var a in subArgs)
            if (a.StartsWith("--level=")) level = int.Parse(a[8..]);
        Console.WriteLine("subcommand 'bar'");
        Console.WriteLine("  level:", level);
        break;

    default:
        Console.Error.WriteLine($"unknown subcommand: {subCmd}");
        Environment.Exit(1);
        break;
}
