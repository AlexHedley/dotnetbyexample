// .NET allows creating custom error types by extending Exception.

// A custom error type.
class ValidationError : Exception
{
    public string Field { get; }
    
    public ValidationError(string field, string message) : base(message)
    {
        Field = field;
    }
    
    public override string ToString() => $"ValidationError: field={Field}, message={Message}";
}

// Another custom error.
class NotFoundError : Exception
{
    public int Id { get; }
    
    public NotFoundError(int id) : base($"Resource with id {id} not found")
    {
        Id = id;
    }
}

// A function that can throw a custom error.
string GetUser(int id)
{
    if (id <= 0)
        throw new ValidationError("id", "must be positive");
    if (id > 100)
        throw new NotFoundError(id);
    return $"user_{id}";
}

// Handle different error types.
void TryGetUser(int id)
{
    try
    {
        var user = GetUser(id);
        Console.WriteLine($"Found: {user}");
    }
    catch (ValidationError ve)
    {
        Console.WriteLine($"Validation error on field '{ve.Field}': {ve.Message}");
    }
    catch (NotFoundError nfe)
    {
        Console.WriteLine($"Not found: id={nfe.Id}");
    }
}

TryGetUser(1);
TryGetUser(-1);
TryGetUser(999);
