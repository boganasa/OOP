namespace Backups.Exceptions;

public abstract class ObjectException : Exception
{
    protected ObjectException(string message)
        : base(message) { }
}

public class Existance : ObjectException
{
    public Existance(string name)
        : base($"File {name} is not exist") { }
}

public class ExistanceinBackup : ObjectException
{
    public ExistanceinBackup(string name)
        : base($"File {name} is not exist in current backup") { }
}