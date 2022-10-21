namespace Isu.Extra.Exceptions;

public abstract class StudentsExceptions : Exception
{
    protected StudentsExceptions(string message)
        : base(message) { }
}

public class ListOfCoursesIsFullException : StudentsExceptions
{
    public ListOfCoursesIsFullException(ExtraStudent student)
        : base($"Student {student} has 2 courses") { }
}

public class MegafacultyException : StudentsExceptions
{
    public MegafacultyException(ExtraStudent student, char megafaculty, ElectiveModule course)
        : base($"Student {student}'s group has the same megafaculty {megafaculty} as course {course.GetName()}") { }
}