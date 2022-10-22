using Isu.Entities;
using Isu.Extra.Exceptions;
using Isu.Extra.Models;
using Isu.Models;

namespace Isu.Extra;

public class ExtraStudent
{
    private Student _student;
    private ExtraGroup _group;
    private TimeTable _timetable;
    private IReadOnlyList<Division> _divisions = new List<Division>();
    private int _allowCount;

    public ExtraStudent(string name, Id id, ExtraGroup group, int allowCount)
    {
        _student = new Student(name, id);
        _group = group;
        _timetable = group.GetTimeTable();
        _allowCount = allowCount;
    }

    public ExtraGroup GetGroup() => _group;

    public TimeTable GetTimeTable() => _timetable;
    public IReadOnlyList<Division> GetDivisions() => _divisions;
    public Student GetStudent() => _student;
    public int GetAllowCount() => _allowCount;

    public void SetDivisions(Division division)
    {
        List<Division> divisions = new List<Division>(_divisions);
        divisions.Add(division);
        if (divisions.Count > _allowCount)
        {
            throw new ListOfCoursesIsFullException(this);
        }

        _divisions = new List<Division>(divisions);
    }

    public void SetTimeTable(TimeTable timeTable)
    {
        _timetable = timeTable;
    }

    public bool IsStudentInCourse()
    {
        foreach (Division division in _divisions)
        {
            if (division is not null)
            {
                return true;
            }
        }

        return false;
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