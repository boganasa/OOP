using Isu.Exceptions;
using Isu.Models
    ;

namespace Isu.Entities;

public class Group
{
    private const int NumberOfStudents = 21;
    private readonly GroupName _groupName = null!;
    private readonly List<Student> _students = null!;

    public Group() { }

    public Group(GroupName groupName)
    {
        _groupName = groupName;
        _students = new List<Student>();
    }

    public Group(GroupName groupName, List<Student> students)
    {
        if (students.Count > NumberOfStudents)
        {
            throw new NumberOfStudentsException($"The number of students is more than allowed: {NumberOfStudents} < {students.Count}");
        }

        _groupName = groupName;
        _students = students;
    }

    public void AddNewStudent(Student student)
    {
        if (_students.Count + 1 > NumberOfStudents)
        {
            throw new NumberOfStudentsException($"The number of students is more than allowed: {NumberOfStudents} < {_students.Count + 1}");
        }

        if (_students.Contains(student))
        {
            throw new StudentExistenceException($"Student {student.Name()} is already in the group {student.Group() !._groupName}");
        }

        if (student.Group() != null)
        {
            throw new StudentExistenceException(
                $"Student {student.Name()} is already in the another group {_groupName}");
        }

        _students.Add(student);
        student.SetGroup(this);
    }

    public void DeleteStudent(Student student)
    {
        if (!_students.Contains(student))
        {
            throw new StudentExistenceException($"Student {student.Name()} is not in the group {_groupName}");
        }

        _students.Remove(student);
        student.SetGroup(null);
    }

    public GroupName Name() => _groupName;
    public List<Student> ListOfStudents() => _students;
}