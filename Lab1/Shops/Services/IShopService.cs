using Shops.Entities;
using Shops.Exceptions;
using Shops.Models;
namespace Shops.Services;

public interface IShopService
{
    Group? AddGroup(GroupName name);
    Student AddStudent(Group group, string name);

    Student GetStudent(int id);
    Student? FindStudent(int id);
    List<Student> FindStudents(GroupName groupName);
    List<Student> FindStudents(CourseNumber courseNumber);

    Group? FindGroup(GroupName groupName);
    List<Group> FindGroups(CourseNumber courseNumber);

    void ChangeStudentGroup(Student student, Group newGroup);
}