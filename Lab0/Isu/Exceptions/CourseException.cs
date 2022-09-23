using System.Runtime.Serialization;

namespace Isu.Exceptions;

public class WrongCourseNumberException : Exception
{
    public WrongCourseNumberException() { }

    public WrongCourseNumberException(string message)
        : base(message) { }

    public WrongCourseNumberException(string message, Exception inner)
        : base(message, inner) { }

    protected WrongCourseNumberException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}