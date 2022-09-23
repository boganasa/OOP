using Isu.Models;

namespace Isu.Entities;

public class Student
{
    private readonly string _studentName;
    private readonly Id _id;
    private Group? _group;

    public Student()
    {
        _studentName = string.Empty;
        _id = new Id(0);
        var defaultNumber = new CourseNumber('1');
        _group = new Group();
    }

    public Student(string name, Id id)
    {
        _studentName = name;
        _id = id;
        _group = null;
    }

    public Student(string name, Id id, Group group)
    {
        _studentName = name;
        _id = id;
        _group = group;
        group.AddNewStudent(this);
    }

    public void SetGroup(Group? name) => _group = name;
    public string Name() => _studentName;
    public Id IdNumber() => _id;
    public Group? Group() => _group;
}