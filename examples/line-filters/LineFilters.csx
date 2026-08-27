// A line filter is a common type of program that reads input on stdin,
// processes it, and then prints some derived result to stdout.
// Here's an example of a line filter in .NET that writes a
// capitalized version of all input text.

string? line;
// Read lines from stdin.
while ((line = Console.ReadLine()) != null)
{
    // Uppercase each line.
    Console.WriteLine(line.ToUpper());
}
