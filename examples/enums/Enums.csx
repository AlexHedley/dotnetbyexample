// Enums in .NET are a distinct value type constrained to a range of named constants.

// Here's an enum type ServerState with the possible values.
enum ServerState
{
    Idle,
    Connected,
    Error,
    Retrying
}

// By implementing the ToString method (or using a custom approach),
// we can print the name of an enum value.
ServerState state = ServerState.Idle;
Console.WriteLine(state);

// Transition function demonstrates using enums in a switch expression.
ServerState Transition(ServerState s)
{
    return s switch
    {
        ServerState.Idle => ServerState.Connected,
        ServerState.Connected => ServerState.Idle,
        ServerState.Retrying => ServerState.Connected,
        ServerState.Error => ServerState.Error,
        _ => throw new Exception($"Unknown state: {s}")
    };
}

var ns = Transition(state);
Console.WriteLine("transition:", ns);

// Flags enum for bit masking.
[Flags]
enum Permission
{
    None = 0,
    Read = 1,
    Write = 2,
    Execute = 4
}

var p = Permission.Read | Permission.Write;
Console.WriteLine("permissions:", p);
Console.WriteLine("has read:", p.HasFlag(Permission.Read));
Console.WriteLine("has execute:", p.HasFlag(Permission.Execute));
