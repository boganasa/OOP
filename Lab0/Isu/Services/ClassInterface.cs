namespace Isu.Services;
using Entities;
using Models;
using Exceptions;

public class ClassInterface : IIsuService
{
    private List<Group> _groupList;

    public ClassInterface()
    {
        _groupList = new List<Group>();
    }

    public ClassInterface(List<Group> groupList)
    {
        _groupList = groupList;
    }

    public Group? AddGroup(GroupName name)
    {
        Group groupNullCheck = _groupList.FirstOrDefault(group => group.Name() == name) !;
        if (groupNullCheck != null)
        {
            throw new GroupExistenceException($"Group {name} is already in the list");
        }

        return groupNullCheck;
    }

    public Student AddStudent(Group group, string name)
    {
        if (!_groupList.Contains(group))
        {
            throw new GroupExistenceException($"Group {group} does not exist");
        }

        var newId = new Id();
        var newStudent = new Student(name, newId, group);
        group.AddNewStudent(newStudent);
        return newStudent;
    }

    public Student GetStudent(int id)
    {
        var myStudent = new Student();
        foreach (Group group in _groupList)
        {
            foreach (Student student in group.ListOfStudents().Where(student => student.IdNumber().NumberId() == id))
            {
                myStudent = student;
                break;
            }
        }

        if (myStudent.IdNumber().NumberId() == 0)
        {
            throw new StudentExistenceException($"Student with id {id} is not found");
        }

        return myStudent;
    }

    public Student? FindStudent(int id)
    {
        var myStudent = new Student();
        foreach (Group group in _groupList)
        {
            foreach (Student student in group.ListOfStudents().Where(student => student.IdNumber().NumberId() == id))
            {
                myStudent = student;
                break;
            }
        }

        return myStudent.IdNumber().NumberId() == 0 ? null : myStudent;
    }

    public List<Student> FindStudents(GroupName groupName)
    {
        var myGroup = new Group();
        foreach (Group group in _groupList.Where(group => group.Name().Name() == groupName.Name()))
        {
            myGroup = group;
            break;
        }

        if (myGroup.ListOfStudents().Count == 0)
        {
            throw new GroupExistenceException($"Group {groupName} does not exist");
        }

        return myGroup.ListOfStudents();
    }

    public List<Student> FindStudents(CourseNumber courseNumber)
    {
        var myGroup = new Group();
        foreach (Group group in _groupList.Where(group => group.Name().Number().Number == courseNumber.Number))
        {
            myGroup = group;
            break;
        }

        if (myGroup.ListOfStudents().Count == 0)
        {
            throw new GroupExistenceException($"Course {courseNumber} is empty");
        }

        return myGroup.ListOfStudents();
    }

    public Group FindGroup(GroupName groupName)
    {
        var myGroup = new Group();
        foreach (Group group in _groupList.Where(group => group.Name() == groupName))
        {
            myGroup = group;
            break;
        }

        return myGroup;
    }

    public List<Group> FindGroups(CourseNumber courseNumber)
    {
        var myGroupList = new List<Group>();
        foreach (Group group in _groupList.Where(group => group.Name().Number().Number == courseNumber.Number))
        {
            myGroupList.Add(group);
        }

        return myGroupList;
    }

    public void ChangeStudentGroup(Student student, Group newGroup)
    {
        var myStudent = new Student();
        foreach (Group group in _groupList)
        {
            foreach (Student studentI in group.ListOfStudents().Where(studentI => studentI == student))
            {
                myStudent = studentI;
                break;
            }
        }

        if (myStudent.IdNumber().NumberId() == 0)
        {
            throw new StudentExistenceException($"Student {student.Name()} is not found");
        }

        Group findGroup = FindGroup(myStudent.Group()?.Name() !);
        findGroup.DeleteStudent(student);
        newGroup.AddNewStudent(student);
    }
}