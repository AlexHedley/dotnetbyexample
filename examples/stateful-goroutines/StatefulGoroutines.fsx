// Stateful goroutines in F# using a mailbox processor (MailboxProcessor).

type Msg =
    | Read of string * AsyncReplyChannel<int>
    | Write of string * int * AsyncReplyChannel<unit>

let stateAgent = MailboxProcessor.Start(fun inbox ->
    let rec loop (state: Map<string, int>) = async {
        let! msg = inbox.Receive()
        match msg with
        | Read(key, reply) ->
            reply.Reply(Map.tryFind key state |> Option.defaultValue 0)
            return! loop state
        | Write(key, value, reply) ->
            reply.Reply(())
            return! loop (Map.add key value state)
    }
    loop Map.empty
)

// Perform some reads and writes.
for i in 0..99 do
    stateAgent.PostAndReply(fun reply -> Write("key", i, reply))

let result = stateAgent.PostAndReply(fun reply -> Read("key", reply))
printfn "final value: %d" result
