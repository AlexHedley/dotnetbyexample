// In .NET C#, variables are explicitly declared and used by
// the compiler to check type-correctness of function calls etc.

// var declares a variable and infers the type from the initializer.
var a = "initial";
Console.WriteLine(a);

// You can declare multiple variables at once.
var b = 1;
var c = 2;
Console.WriteLine(b, c);

// .NET will infer the type of initialized variables.
var d = true;
Console.WriteLine(d);

// Variables declared without a corresponding initialization
// are zero-valued. For example, the zero value for an int is 0.
int e = 0;
Console.WriteLine(e);

// If you use an explicit type, the compiler will check assignments.
string f = "apple";
Console.WriteLine(f);
