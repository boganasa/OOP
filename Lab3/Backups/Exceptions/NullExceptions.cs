namespace Backups.Exceptions;

public abstract class NullException : Exception
{
    protected NullException(string message)
        : base(message) { }
}

public class NullPath : NullException
{
    public NullPath()
        : base($"String for path is empty") { }
}
