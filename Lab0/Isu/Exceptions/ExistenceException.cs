using System.Runtime.Serialization;

namespace Isu.Exceptions;

public class ContactingWithGroupException : Exception
{
    public ContactingWithGroupException() { }

    public ContactingWithGroupException(string message)
        : base(message) { }

    public ContactingWithGroupException(string message, Exception inner)
        : base(message, inner) { }

    protected ContactingWithGroupException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}