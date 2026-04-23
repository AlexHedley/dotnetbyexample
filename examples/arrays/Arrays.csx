// In .NET, an array is a numbered sequence of elements of a specific length.

// Here we create an array 'a' that will hold exactly 5 ints.
// The type is int[] and its length is 5.
// By default an int array is initialized to zero values.
int[] a = new int[5];
Console.Write("emp:");
foreach (var v in a) Console.Write(" " + v);
Console.WriteLine();

// We can set a value at an index using the array[index] = value syntax,
// and get a value with array[index].
a[4] = 100;
Console.WriteLine("set:", a[4]);
Console.WriteLine("get:", a[4]);

// The builtin Length returns the length of an array.
Console.WriteLine("len:", a.Length);

// Use this syntax to declare and initialize an array in one line.
int[] b = { 1, 2, 3, 4, 5 };
Console.Write("dcl:");
foreach (var v in b) Console.Write(" " + v);
Console.WriteLine();

// Array types are one-dimensional by default. Use multidimensional arrays
// for 2D arrays.
int[,] twoD = new int[2, 3];
for (int i = 0; i < 2; i++)
{
    for (int j = 0; j < 3; j++)
    {
        twoD[i, j] = i + j;
    }
}
Console.Write("2d:");
for (int i = 0; i < 2; i++)
    for (int j = 0; j < 3; j++)
        Console.Write(" " + twoD[i, j]);
Console.WriteLine();
