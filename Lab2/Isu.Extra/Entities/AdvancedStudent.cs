using Isu.Entities;
using Isu.Extra.Exceptions;
using Isu.Extra.Models;
using Isu.Models;

namespace Isu.Extra;

public class ExtraStudent : Student
{
    private Student _student;
    private ExtraGroup _group = null!;
    private TimeTable _timetable = null!;
    private Division _firstDivision = null!;
    private Division _secondDivision = null!;

    public ExtraStudent()
    {
        _student = new Student();
        _timetable = null!;
    }

    public ExtraStudent(string name, Id id)
    {
        _student = new Student(name, id);
    }

    public ExtraStudent(string name, Id id, ExtraGroup group)
    {
        _student = new Student(name, id, group);
        _timetable = group.GetTimeTable();
        _group = group;
    }

    public ExtraGroup GetGroup()
    {
        var group = new ExtraGroup(_group);
        return group;
    }

    public TimeTable GetTimeTable() => _timetable;
    public Division GetFirstDivision() => _firstDivision;
    public Division GetSecondDivision() => _secondDivision;
    public Student GetStudent() => _student;

    public void SetFirstDivision(Division division)
    {
       _firstDivision = division;
    }

    public void SetSecondDivision(Division division)
    {
       _secondDivision = division;
    }

    public void SetTimeTable(TimeTable timeTable)
    {
        _timetable = timeTable;
    }

    public bool IsStudentInCourse()
    {
        return _firstDivision is not null || _secondDivision is not null;
    }

    public TimeTable DeleteTimeTable(TimeTable timeTable)
    {
        var lessons = new List<Lesson>(this.GetTimeTable().GetLessons());
        foreach (Lesson lesson in timeTable.GetLessons())
        {
            if (!lessons.Contains(lesson))
            {
                throw new LessonDoesNotExist(lesson);
            }

            lessons.Remove(lesson);
        }

        var newTimeTable = new TimeTable(lessons);
        return newTimeTable;
    }
}