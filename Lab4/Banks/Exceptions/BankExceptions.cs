using System.Runtime.Serialization;

namespace Banks.Exceptions;

public abstract class BankExceptions : Exception
{
    protected BankExceptions(string message)
        : base(message) { }
}

public class ResubscribeException : BankExceptions
{
    public ResubscribeException()
        : base($"Client already subscribe") { }
}

public class UnsubscribeException : BankExceptions
{
    public UnsubscribeException()
        : base($"Client is not subscribe") { }
}

public class AccountExistException : BankExceptions
{
    public AccountExistException(string name)
        : base($"there is no such type of account {name}") { }
}

public class AttackException : BankExceptions
{
    public AttackException(string name)
        : base($"Client {name} is suspicion of attacker") { }
}

public class CancelException : BankExceptions
{
    public CancelException()
        : base($"Cant cansel transaction") { }
}

public class ExistingException : BankExceptions
{
    public ExistingException(string name)
        : base($"There is no such bank {name}") { }
}