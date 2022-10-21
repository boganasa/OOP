namespace Isu.Extra.Exceptions;

public class TimetableException : Exception
{
    protected TimetableException(string message)
        : base(message) { }
}

public class EmptyTimetable : TimetableException
{
    public EmptyTimetable(Division division)
        : base($"{division.GetCourse().GetName()} has empty timetable") { }
}

public class WrongDayException : TimetableException
{
    public WrongDayException(int day)
        : base($"{day} can not be associated with day of the week") { }
}

public class WrongNumberOfLessonException : TimetableException
{
    public WrongNumberOfLessonException(int number)
        : base($"You can not have {number} lesson") { }
}

public class WrongNumberOfAuditoryException : TimetableException
{
    public WrongNumberOfAuditoryException(int number)
        : base($"You can not have {number} of auditory. Only four-digit numbers") { }
}

public class IsNotTimetableException : TimetableException
{
    public IsNotTimetableException(object? obj)
        : base($"{obj} in not timetable") { }
}

public class IsNotSuitableCoursesException : TimetableException
{
    public IsNotSuitableCoursesException(ExtraStudent student, ElectiveModule course)
        : base($"Student {student.GetStudent().Name()} cant find division in course {course.GetName()}") { }
}

public class IsNotstudentDivisionException : TimetableException
{
    public IsNotstudentDivisionException(ExtraStudent student)
        : base($"Student {student.GetStudent().Name()} hasn't got division") { }
}