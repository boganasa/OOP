using Isu.Exceptions;
namespace Isu.Models;

public class CourseNumber
{
    private char _courseNumber;

    public CourseNumber(char course)
    {
        if (course - '0' > 4 || course - '0' < 1)
        {
            throw new WrongCourseNumberException($"Course: {course}  does not exist");
        }

        _courseNumber = course;
    }

    public char Number { get; }
}