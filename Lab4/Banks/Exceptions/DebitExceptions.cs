using System.Runtime.Serialization;

namespace Banks.Exceptions;

public abstract class DebitExceptions : Exception
{
    protected DebitExceptions(string message)
        : base(message) { }
}

public class GoToNegativeExceptions : DebitExceptions
{
    public GoToNegativeExceptions(decimal sum)
        : base($"You can't go to negative {sum}") { }
}