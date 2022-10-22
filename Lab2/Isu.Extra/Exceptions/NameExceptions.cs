namespace Isu.Extra.Exceptions;

public abstract class NameException : Exception
{
    protected NameException(string message)
        : base(message) { }
}

public class NameOfMegafacultyException : NameException
{
    public NameOfMegafacultyException(char ch)
        : base($"{ch} is out of valid letters") { }
}