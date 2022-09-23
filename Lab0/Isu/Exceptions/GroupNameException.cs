using System.Runtime.Serialization;

namespace Isu.Exceptions;

public class WrongGroupNameException : Exception
{
    public WrongGroupNameException() { }

    public WrongGroupNameException(string message)
        : base(message) { }

    public WrongGroupNameException(string message, Exception inner)
        : base(message, inner) { }

    protected WrongGroupNameException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}