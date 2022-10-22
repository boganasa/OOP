using Isu.Entities;
using Isu.Extra.Models;
using Isu.Models;

namespace Isu.Extra;

public class ExtraGroup
{
    private TimeTable _timetable;
    private Group _group;
    private List<ExtraStudent> _students = new List<ExtraStudent>();

    public ExtraGroup(GroupName groupName, TimeTable timetable)
    {
        _group = new Group(groupName);
        _timetable = timetable;
    }

    public ExtraGroup(ExtraGroup group)
    {
        _group = group.GetGroup();
        _students = (List<ExtraStudent>)group.GetStudents();
        _timetable = group.GetTimeTable();
    }

    public void AddExtraStudent(ExtraStudent student)
    {
        _group.AddNewStudent(student.GetStudent());
        student.SetTimeTable(_timetable);
        _students.Add(student);
    }

    public TimeTable GetTimeTable() => _timetable;

    public IReadOnlyCollection<ExtraStudent> GetStudents() => _students;

    public Group GetGroup() => _group;
}