// .NET has had generics since C# 2.0. Here are some examples.

// As an example of a generic function, MapSlice takes a list of any type
// and a function and applies that function to each element.
List<U> MapSlice<T, U>(List<T> lst, Func<T, U> f)
{
    var result = new List<U>();
    foreach (var item in lst)
    {
        result.Add(f(item));
    }
    return result;
}

// As an example of a generic type, Stack[T] is a generic stack.
class Stack<T>
{
    private List<T> items = new List<T>();
    
    public void Push(T item)
    {
        items.Add(item);
    }
    
    public T Pop()
    {
        var item = items[items.Count - 1];
        items.RemoveAt(items.Count - 1);
        return item;
    }
    
    public bool IsEmpty() => items.Count == 0;
}

// When invoking generic functions, we can often rely on type inference.
var s = MapSlice(new List<int> { 1, 2, 3 }, (int x) => x * x);
Console.WriteLine("squares:", string.Join(" ", s));

var strs = MapSlice(new List<string> { "hello", "world" }, (string x) => x.ToUpper());
Console.WriteLine("upper:", string.Join(" ", strs));

// For a generic Stack, the type must be provided explicitly when constructing.
var st = new Stack<int>();
Console.WriteLine("isEmpty:", st.IsEmpty());

st.Push(1);
st.Push(2);
st.Push(3);
Console.WriteLine("pop:", st.Pop());
Console.WriteLine("isEmpty:", st.IsEmpty());
