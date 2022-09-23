using Isu.Entities;
using Isu.Exceptions;
using Isu.Models;
using Xunit;

namespace Isu.Test;
public class IsuService
{
    [Fact]
    public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
    {
        var testId = new Id();
        var testCourseNumber = new CourseNumber('2');
        var testGroupName = new GroupName("M", "3", testCourseNumber, "03");
        var testListStudents = new List<Student>();
        var testStudent = new Student("Ivanov Ivan", testId);
        var testGroup = new Group(testGroupName, testListStudents);

        testGroup.AddNewStudent(testStudent);

        Assert.Equal(testStudent.Group(), testGroup);

        Assert.Contains(testStudent, testGroup.ListOfStudents());
    }

    [Fact]
    public void ReachMaxStudentPerGroup_ThrowException()
    {
        var testCourseNumber = new CourseNumber('2');
        var testGroupName = new GroupName("M", "3", testCourseNumber, "03");
        var testListStudents = new List<Student>();

        for (int i = 0; i < 22; i++)
        {
            var testId = new Id();
            var testStudent = new Student($"Ivanov Ivan{i}", testId);
            testListStudents.Add(testStudent);
        }

        Assert.Throws<NumberOfStudentsException>(
            () =>
            {
                var group = new Group(testGroupName, testListStudents);
            });
    }

    [Fact]
    public void CreateGroupWithInvalidName_ThrowException()
    {
        const string testMegaFaculty = "A";
        const string testFormOfStudy = "3";
        var testCourseNumber = new CourseNumber('2');
        const string testGroupNumber = "03";

        Assert.Throws<WrongGroupNameException>(
            () =>
            {
                var group = new Group(new GroupName(testMegaFaculty, testFormOfStudy, testCourseNumber, testGroupNumber));
            });
    }

    [Fact]
    public void TransferStudentToAnotherGroup_GroupChanged()
    {
        var testId = new Id();
        var testCourseNumber1 = new CourseNumber('2');
        var testGroupName1 = new GroupName("M", "3", testCourseNumber1, "03");
        var testListStudents1 = new List<Student>();
        var testStudent = new Student("Ivanov Ivan", testId);
        testListStudents1.Add(testStudent);
        var testGroup1 = new Group(testGroupName1, testListStudents1);
        var testCourseNumber2 = new CourseNumber('2');
        var testGroupName2 = new GroupName("M", "3", testCourseNumber2, "13");
        var testGroup2 = new Group(testGroupName2);

        testGroup1.DeleteStudent(testStudent);
        testGroup2.AddNewStudent(testStudent);

        Assert.Equal(testGroup2.Name(), testStudent.Group() !.Name());
    }
}