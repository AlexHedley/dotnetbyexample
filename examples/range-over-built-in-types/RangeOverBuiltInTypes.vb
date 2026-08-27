' VB.NET uses For Each for iteration over built-in types.

' Iterate over elements of an array.
Dim nums() As Integer = {2, 3, 4}
Dim sum As Integer = 0
For Each num As Integer In nums
    sum += num
Next
Console.WriteLine("sum: " & sum)

' For loop with index.
For i As Integer = 0 To nums.Length - 1
    If nums(i) = 3 Then
        Console.WriteLine("index: " & i)
    End If
Next

' For Each over a dictionary.
Dim kvs As New Dictionary(Of String, String) From {{"a", "apple"}, {"b", "banana"}}
For Each kv As KeyValuePair(Of String, String) In kvs
    Console.WriteLine(kv.Key & " -> " & kv.Value)
Next

' Iterate over a string.
Dim str As String = "go"
For i As Integer = 0 To str.Length - 1
    Console.WriteLine(i & " " & AscW(str(i)))
Next
