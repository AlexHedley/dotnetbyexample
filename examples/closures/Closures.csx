// .NET supports closures (anonymous functions that close over variables
// from their enclosing scope).

// This function IntSeq returns another function, which we define
// anonymously in the body of IntSeq. The returned function closes over
// the variable i to form a closure.
Func<int> IntSeq()
{
    int i = 0;
    return () => {
        i++;
        return i;
    };
}

// We call IntSeq, assigning the result (a function) to nextInt.
// This function value captures its own i value, which will be
// updated each time we call nextInt.
var nextInt = IntSeq();

// See the effect of the closure by calling nextInt a few times.
Console.WriteLine(nextInt());
Console.WriteLine(nextInt());
Console.WriteLine(nextInt());

// To confirm that the state is unique to that particular function,
// create and test a new one.
var newInts = IntSeq();
Console.WriteLine(newInts());
