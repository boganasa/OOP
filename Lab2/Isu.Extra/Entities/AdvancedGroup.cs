using Isu.Entities;
using Isu.Extra.Models;
using Isu.Models;

namespace Isu.Extra;

public class ExtraGroup : Group
{
    private TimeTable _timetable;
    private Group _group;
    private List<ExtraStudent> _students = new List<ExtraStudent>();

    public ExtraGroup(GroupName groupName, TimeTable timetable)
    {
        _group = new Group(groupName);
        _timetable = timetable;
    }

    public ExtraGroup(GroupName groupName, List<ExtraStudent> students, TimeTable timetable)
    {
        _group = new Group(groupName);
        foreach (ExtraStudent student in students)
        {
            student.SetTimeTable(timetable);
            _students.Add(student);
            _group.AddNewStudent(student.GetStudent());
        }

        _timetable = timetable;
    }

    public ExtraGroup(ExtraGroup group)
    {
        _group = group.GetGroup();
        _students = group.GetStudents();
        _timetable = group.GetTimeTable();
    }

    public void AddExtraStudent(ExtraStudent student)
    {
        _group.AddNewStudent(student.GetStudent());
        student.SetTimeTable(_timetable);
        _students.Add(student);
    }

    public TimeTable GetTimeTable() => _timetable;

    public List<ExtraStudent> GetStudents()
    {
        var list = new List<ExtraStudent>(_students);
        return list;
    }

    public Group GetGroup()
    {
        return _group;
    }
}