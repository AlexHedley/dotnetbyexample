// Command-line arguments are accessed via the args variable
// in .csx scripts, or Environment.GetCommandLineArgs() in full programs.

// The args variable contains the command-line arguments.
// args[0] is the first argument (not the program name).
Console.WriteLine(args);

// In a full program, use Environment.GetCommandLineArgs().
var argsAll = Environment.GetCommandLineArgs();
Console.WriteLine("argsAll:", string.Join(", ", argsAll));

// You can access individual args.
if (args.Length > 0)
    Console.WriteLine("arg1:", args[0]);

// Print all args with their indices.
for (int i = 0; i < args.Length; i++)
    Console.WriteLine($"args[{i}]: {args[i]}");
