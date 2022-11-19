namespace Backups.Exceptions;

public abstract class ObjectException : Exception
{
    protected ObjectException(string message)
        : base(message) { }
}

public class ExistanceOfBackup : ObjectException
{
    public ExistanceOfBackup(string name)
        : base($"File {name} is not exist") { }
}