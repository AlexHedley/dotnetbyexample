// Switch statements express conditionals across many
// branches.

int i = 2;
Console.Write("Write " + i + " as ");
switch (i)
{
    case 1:
        Console.WriteLine("one");
        break;
    case 2:
        Console.WriteLine("two");
        break;
    case 3:
        Console.WriteLine("three");
        break;
}

// You can use commas to separate multiple expressions in the same case.
// We use the optional default case in this example as well.
switch (DateTime.Now.DayOfWeek)
{
    case DayOfWeek.Saturday:
    case DayOfWeek.Sunday:
        Console.WriteLine("It's the weekend");
        break;
    default:
        Console.WriteLine("It's a weekday");
        break;
}

// switch without an expression is an alternate way to express if/else logic.
int t = DateTime.Now.Hour;
switch
{
    case t < 12:
        Console.WriteLine("It's before noon");
        break;
    default:
        Console.WriteLine("It's after noon");
        break;
}

// Type switch compares types rather than values.
// In C# we use pattern matching with switch expressions.
object whatAmI = true;
string result = whatAmI switch
{
    bool b => $"bool: {b}",
    int n => $"int: {n}",
    string s => $"string: {s}",
    _ => "unknown type"
};
Console.WriteLine(result);
