// .NET supports constants of character, string, boolean,
// and numeric values via the const keyword.

// const declares a constant value.
const string s = "constant";

Console.WriteLine(s);

// A const statement can appear anywhere a var statement can.
const int n = 500000000;

// Constant expressions perform arithmetic with arbitrary precision.
const double d = 3e20 / n;
Console.WriteLine(d);

// A numeric constant has no type until it's given one,
// such as by an explicit conversion.
Console.WriteLine((long)d);

// A number can be given a type by using it in a context that requires one,
// such as a variable assignment or function call.
Console.WriteLine(Math.Sin(n));
