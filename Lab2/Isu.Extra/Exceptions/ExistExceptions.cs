namespace Isu.Extra.Exceptions;

public abstract class ExistException : Exception
{
    protected ExistException(string message)
        : base(message) { }
}

public class LessonDoesNotExist : ExistException
{
    public LessonDoesNotExist(Lesson lesson)
        : base($"{lesson.GetNameOfSubject()} does not find") { }
}

public class CourseDoesNotExist : ExistException
{
    public CourseDoesNotExist(ElectiveModule course)
        : base($"{course.GetName()} does not exist") { }
}