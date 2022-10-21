using Isu.Exceptions;
using Isu.Models
    ;

namespace Isu.Entities;

public class Group
{
    private const int NumberOfStudents = 21;
    private GroupName groupName = null!;
    private List<Student> _students = new List<Student>();

    public Group() { }

    public Group(GroupName groupName)
    {
        this.groupName = groupName;
        _students = new List<Student>();
    }

    public Group(GroupName groupName, List<Student> students)
    {
        if (students.Count > NumberOfStudents && students is not null)
        {
            throw new NumberOfStudentsException($"The number of students is more than allowed: {NumberOfStudents} < {students.Count}");
        }

        this.groupName = groupName;
        _students = students!;
    }

    public void AddNewStudent(Student student)
    {
        if (_students.Count + 1 > NumberOfStudents && _students is not null)
        {
            throw new NumberOfStudentsException($"The number of students is more than allowed: {NumberOfStudents} < {_students!.Count + 1}");
        }

        if (_students != null && _students.Contains(student))
        {
            throw new StudentExistenceException($"Student {student.Name()} is already in the group {student.Group() !.groupName}");
        }

        if (student.Group() is not null)
        {
            throw new StudentExistenceException(
                $"Student {student.Name()} is already in the another group {groupName}");
        }

        _students?.Add(student);
        student.SetGroup(this);
    }

    public void DeleteStudent(Student student)
    {
        if (!_students.Contains(student))
        {
            throw new StudentExistenceException($"Student {student.Name()} is not in the group {groupName}");
        }

        _students.Remove(student);
        student.SetGroup(null);
    }

    public GroupName Name() => groupName;
    public List<Student> ListOfStudents() => _students;
}