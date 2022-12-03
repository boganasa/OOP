using System.Runtime.Serialization;

namespace Banks.Exceptions;

public abstract class DepositExceptions : Exception
{
    protected DepositExceptions(string message)
        : base(message) { }
}

public class CantDoException : DepositExceptions
{
    public CantDoException(string doing)
        : base($"You can't {doing} money from deposit now") { }
}