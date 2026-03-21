' Defer in VB.NET uses Try/Finally or Using.

Sub CreateFile(path As String)
    Console.WriteLine("creating")
    Dim f As System.IO.FileStream = System.IO.File.Create(path)
    Try
        Console.WriteLine("writing")
        System.IO.File.WriteAllText(path, "data")
    Finally
        Console.WriteLine("closing")
        f.Close()
    End Try
End Sub

Sub CreateFileUsing(path As String)
    Console.WriteLine("creating")
    Using f As System.IO.FileStream = System.IO.File.Create(path)
        Console.WriteLine("writing")
        Console.WriteLine("closing (via Using)")
    End Using
End Sub

Sub DeferLIFO()
    Dim deferred As New Stack(Of Action)()
    deferred.Push(Sub() Console.WriteLine("deferred 1"))
    deferred.Push(Sub() Console.WriteLine("deferred 2"))
    deferred.Push(Sub() Console.WriteLine("deferred 3"))
    
    Console.WriteLine("before deferred")
    
    While deferred.Count > 0
        deferred.Pop().Invoke()
    End While
End Sub

DeferLIFO()
