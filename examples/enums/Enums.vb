' Enums in VB.NET use the Enum keyword.

Enum ServerState
    Idle
    Connected
    [Error]
    Retrying
End Enum

Dim state As ServerState = ServerState.Idle
Console.WriteLine(state)

Function Transition(s As ServerState) As ServerState
    Select Case s
        Case ServerState.Idle
            Return ServerState.Connected
        Case ServerState.Connected
            Return ServerState.Idle
        Case ServerState.Retrying
            Return ServerState.Connected
        Case ServerState.Error
            Return ServerState.Error
        Case Else
            Throw New Exception("Unknown state: " & s.ToString())
    End Select
End Function

Dim ns As ServerState = Transition(state)
Console.WriteLine("transition: " & ns.ToString())

' Flags enum.
<Flags>
Enum Permission
    None = 0
    Read = 1
    Write = 2
    Execute = 4
End Enum

Dim p As Permission = Permission.Read Or Permission.Write
Console.WriteLine("permissions: " & p.ToString())
Console.WriteLine("has read: " & p.HasFlag(Permission.Read))
Console.WriteLine("has execute: " & p.HasFlag(Permission.Execute))
