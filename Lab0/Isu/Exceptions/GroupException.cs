using System.Runtime.Serialization;

namespace Isu.Exceptions;

public class NumberOfStudentsException : Exception
{
    public NumberOfStudentsException() { }

    public NumberOfStudentsException(string message)
        : base(message) { }

    public NumberOfStudentsException(string message, Exception inner)
        : base(message, inner) { }

    protected NumberOfStudentsException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}

public class StudentExistenceException : Exception
{
    public StudentExistenceException() { }

    public StudentExistenceException(string message)
        : base(message) { }

    public StudentExistenceException(string message, Exception inner)
        : base(message, inner) { }

    protected StudentExistenceException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}

public class GroupExistenceException : Exception
{
    public GroupExistenceException() { }

    public GroupExistenceException(string message)
        : base(message) { }

    public GroupExistenceException(string message, Exception inner)
        : base(message, inner) { }

    protected GroupExistenceException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}