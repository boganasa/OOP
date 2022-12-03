namespace Isu.Extra;
using Isu.Extra.Models;
public class Division
{
    private ElectiveModule _course;
    private int _maxNumberOfStudents;
    private IReadOnlyList<ExtraStudent> _students = new List<ExtraStudent>();
    private TimeTable _timetable;

    public Division(int maxNumberOfStudents, TimeTable timetable, ElectiveModule course)
    {
        _course = course;
        _maxNumberOfStudents = maxNumberOfStudents;
        _timetable = timetable;
    }

    public void SetElectiveModule(ElectiveModule newCourse)
    {
        _course = newCourse;
    }

    public IReadOnlyList<ExtraStudent> GetStudents()
    {
        IReadOnlyList<ExtraStudent> students = new List<ExtraStudent>(_students);
        return students;
    }

    public TimeTable GetTimeTable() => _timetable;
    public ElectiveModule GetCourse() => _course;
}