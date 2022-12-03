using System.Runtime.Serialization;

namespace Banks.Exceptions;

public abstract class CreditExceptions : Exception
{
    protected CreditExceptions(string message)
        : base(message) { }
}

public class OverMoreLimitExceptions : CreditExceptions
{
    public OverMoreLimitExceptions(decimal sum)
        : base($"You too go to negative {sum}") { }
}