using Isu.Entities;
using Isu.Extra.Models;
using Isu.Extra.Services;

using Isu.Models;

using Xunit;
namespace Isu.Extra.Test;

public class TestingIsuExtra
{
    [Fact]
    public void AddNewCourse()
    {
        var myIsu = new IsuExtra();
        Lesson newLesson = myIsu.CreateLesson(1, 1, "Math", "Ivanova I. I.", 1221);
        var newListLesson = new List<Lesson>();
        newListLesson.Add(newLesson);
        var newTimeTable = new TimeTable(newListLesson);
        var newDivision = new Division(15, newTimeTable);
        var newListDivision = new List<Division>();
        newListDivision.Add(newDivision);
        ElectiveModule newCourse = myIsu.CreateNewCourse('M', "Computer engineering graphics", newListDivision);

        Assert.Contains(newCourse, myIsu.GetListOfCourses());
    }

    [Fact]
    public void StudentCanSignInCourse()
    {
        var myIsu = new IsuExtra();
        Lesson newLesson = myIsu.CreateLesson(1, 1, "Math", "Ivanova I. I.", 1221);
        var newListLesson = new List<Lesson>();
        newListLesson.Add(newLesson);
        var newTimeTable = new TimeTable(newListLesson);
        var newDivision = new Division(15, newTimeTable);
        var newListDivision = new List<Division>();
        newListDivision.Add(newDivision);
        ElectiveModule newCourse = myIsu.CreateNewCourse('M', "Computer engineering graphics", newListDivision);

        var testId = new Id();
        var testCourseNumber = new CourseNumber('2');
        var testGroupName = new GroupName("D", "3", testCourseNumber, "03");
        var testListStudents = new List<ExtraStudent>();
        var testStudent = new ExtraStudent("Ivanov Ivan", testId);
        Lesson testLesson = myIsu.CreateLesson(2, 1, "OOP", "Petrov V. V.", 2312);
        var testListLesson = new List<Lesson>();
        testListLesson.Add(testLesson);
        var testTimeTable = new TimeTable(testListLesson);
        var testGroup = new ExtraGroup(testGroupName, testListStudents, testTimeTable);

        testGroup.AddExtraStudent(testStudent);

        var listLesson = new List<Lesson>();
        listLesson.Add(newLesson);
        listLesson.Add(testLesson);
        var timeTable = new TimeTable(listLesson);
        TimeTable studentTimeTable = myIsu.SignUpForCourse(testStudent, newCourse);

        Assert.Equal(timeTable, studentTimeTable);
    }

    [Fact]
    public void StudentCanSignOutCourse()
    {
        var myIsu = new IsuExtra();
        Lesson newLesson = myIsu.CreateLesson(1, 1, "Math", "Ivanova I. I.", 1221);
        var newListLesson = new List<Lesson>();
        newListLesson.Add(newLesson);
        var newTimeTable = new TimeTable(newListLesson);
        var newDivision = new Division(15, newTimeTable);
        var newListDivision = new List<Division>();
        newListDivision.Add(newDivision);
        ElectiveModule newCourse = myIsu.CreateNewCourse('M', "Computer engineering graphics", newListDivision);

        var testId = new Id();
        var testCourseNumber = new CourseNumber('2');
        var testGroupName = new GroupName("D", "3", testCourseNumber, "03");
        var testListStudents = new List<ExtraStudent>();
        var testStudent = new ExtraStudent("Ivanov Ivan", testId);
        Lesson testLesson = myIsu.CreateLesson(2, 1, "OOP", "Petrov V. V.", 2312);
        var testListLesson = new List<Lesson>();
        testListLesson.Add(testLesson);
        var testTimeTable = new TimeTable(testListLesson);
        var testGroup = new ExtraGroup(testGroupName, testListStudents, testTimeTable);

        testGroup.AddExtraStudent(testStudent);

        var listLesson = new List<Lesson>();
        listLesson.Add(testLesson);
        var timeTable = new TimeTable(listLesson);

        TimeTable studentTimeTable = myIsu.SignUpForCourse(testStudent, newCourse);
        studentTimeTable = myIsu.DeleteReservation(testStudent, newDivision);

        Assert.Equal(timeTable, studentTimeTable);
    }

    [Fact]
    public void GetDivisionByCourse()
    {
        var myIsu = new IsuExtra();
        Lesson newLesson1 = myIsu.CreateLesson(1, 1, "Math", "Ivanova I. I.", 1221);
        Lesson newLesson2 = myIsu.CreateLesson(2, 2, "Math", "Ivanova I. I.", 1221);
        Lesson newLesson3 = myIsu.CreateLesson(3, 3, "Math", "Ivanova I. I.", 1221);
        var newListLesson1 = new List<Lesson>();
        newListLesson1.Add(newLesson1);
        var newTimeTable1 = new TimeTable(newListLesson1);
        var newDivision1 = new Division(15, newTimeTable1);
        var newListLesson2 = new List<Lesson>();
        newListLesson2.Add(newLesson2);
        var newTimeTable2 = new TimeTable(newListLesson2);
        var newDivision2 = new Division(15, newTimeTable2);
        var newListLesson3 = new List<Lesson>();
        newListLesson3.Add(newLesson3);
        var newTimeTable3 = new TimeTable(newListLesson3);
        var newDivision3 = new Division(15, newTimeTable3);
        var newListDivision = new List<Division>();
        newListDivision.Add(newDivision1);
        newListDivision.Add(newDivision2);
        newListDivision.Add(newDivision3);
        ElectiveModule newCourse = myIsu.CreateNewCourse('M', "Computer engineering graphics", newListDivision);

        var rightList = new List<Division>();
        rightList.Add(newDivision1);
        rightList.Add(newDivision2);
        rightList.Add(newDivision3);

        Assert.Equal(rightList, newCourse.GetDivisions());
    }

    [Fact]
    public void GetStudentsOutOfCourses()
    {
        var myIsu = new IsuExtra();
        Lesson newLesson = myIsu.CreateLesson(1, 1, "Math", "Ivanova I. I.", 1221);
        var newListLesson = new List<Lesson>();
        newListLesson.Add(newLesson);
        var newTimeTable = new TimeTable(newListLesson);
        var newDivision = new Division(15, newTimeTable);
        var newListDivision = new List<Division>();
        newListDivision.Add(newDivision);
        ElectiveModule newCourse = myIsu.CreateNewCourse('M', "Computer engineering graphics", newListDivision);

        var testCourseNumber = new CourseNumber('2');
        var testGroupName = new GroupName("D", "3", testCourseNumber, "03");
        var testListStudents = new List<ExtraStudent>();
        Lesson testLesson = myIsu.CreateLesson(2, 1, "OOP", "Petrov V. V.", 2312);
        var testListLesson = new List<Lesson>();
        testListLesson.Add(testLesson);
        var testTimeTable = new TimeTable(testListLesson);
        var testGroup = new ExtraGroup(testGroupName, testListStudents, testTimeTable);

        var testId1 = new Id();
        var testStudent1 = new ExtraStudent("Ivanov Ivan", testId1);
        testGroup.AddExtraStudent(testStudent1);

        var testId2 = new Id();
        var testStudent2 = new ExtraStudent("Ivanov Ivan", testId2);
        testGroup.AddExtraStudent(testStudent2);

        var testId3 = new Id();
        var testStudent3 = new ExtraStudent("Ivanov Ivan", testId3);
        testGroup.AddExtraStudent(testStudent3);

        var listLesson = new List<Lesson>();
        listLesson.Add(newLesson);
        listLesson.Add(testLesson);
        var timeTable = new TimeTable(listLesson);
        TimeTable studentTimeTable1 = myIsu.SignUpForCourse(testStudent1, newCourse);
        TimeTable studentTimeTable3 = myIsu.SignUpForCourse(testStudent3, newCourse);

        var studentOutCourses = new List<ExtraStudent>();
        studentOutCourses.Add(testStudent2);

        Assert.Equal(1, myIsu.GetListOfStudentsIsNotInCourse(testGroup).Count);
    }
}