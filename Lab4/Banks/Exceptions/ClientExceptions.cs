using System.Runtime.Serialization;

namespace Banks.Exceptions;

public abstract class ClientExceptions : Exception
{
    protected ClientExceptions(string message)
        : base(message) { }
}

public class PassportException : ClientExceptions
{
    public PassportException(int num)
        : base($"Not correct passport {num}") { }
}

public class AttackerException : ClientExceptions
{
    public AttackerException(string num)
        : base($"Not attacker  {num}") { }
}