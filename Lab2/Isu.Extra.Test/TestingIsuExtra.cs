using Isu.Entities;
using Isu.Extra.Models;
using Isu.Extra.Services;

using Isu.Models;

using Xunit;
namespace Isu.Extra.Test;

public class TestingIsuExtra
{
    public Lesson CreateLesson(int day, int numberOfLesson, string nameOfSubject, string nameOfTeacher, int numberOfAuditory)
    {
        Lesson newLesson = Lesson.Builder
            .WithDay(day)
            .WithNumber(numberOfLesson)
            .WithSubject(nameOfSubject)
            .WithNameOfTeacher(nameOfTeacher)
            .WithNumberOfAuditory(numberOfAuditory)
            .Build();
        return newLesson;
    }

    [Fact]
    public void AddNewCourse()
    {
        var myIsu = new IsuExtra();
        Lesson newLesson = Lesson.Builder
            .WithDay(1)
            .WithNumber(1)
            .WithSubject("Math")
            .WithNameOfTeacher("Ivanova I.I")
            .WithNumberOfAuditory(1221)
            .Build();
        var newListLesson = new List<Lesson>();
        newListLesson.Add(newLesson);
        var newTimeTable = new TimeTable(newListLesson);
        ElectiveModule newCourse = myIsu.CreateNewCourse('M', "Computer engineering graphics");
        var newDivision = new Division(15, newTimeTable, newCourse);
        var newListDivision = new List<Division>();
        newListDivision.Add(newDivision);

        Assert.Contains(newCourse, myIsu.GetListOfCourses());
    }
}