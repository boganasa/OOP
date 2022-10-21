using System.Runtime.CompilerServices;
using Isu.Entities;
using Isu.Extra.Exceptions;
using Isu.Extra.Models;
using Isu.Models;
using Isu.Services;

namespace Isu.Extra.Services;

public class IsuExtra : ClassInterface, IIsuExtra
{
    private List<ElectiveModule> _listOsCourses = new List<ElectiveModule>();

    public IReadOnlyList<ElectiveModule> GetListOfCourses() => _listOsCourses;

    public ElectiveModule CreateNewCourse(char megafaculty, string name, List<Division> divisions)
    {
        var newCourse = new ElectiveModule(megafaculty, name, divisions);
        _listOsCourses.Add(newCourse);
        return newCourse;
    }

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

    public IReadOnlyList<Division> GetListOfDivision(ElectiveModule course)
    {
        if (!_listOsCourses.Contains(course))
        {
            throw new CourseDoesNotExist(course);
        }

        IReadOnlyList<Division> listOfDivision = course.GetDivisions();
        return listOfDivision;
    }

    public IReadOnlyList<ExtraStudent> GetListOfStudentsInDivision(Division division)
    {
        IReadOnlyList<ExtraStudent> listOfStudentsInDivision = new List<ExtraStudent>(division.GetStudents());
        return listOfStudentsInDivision;
    }

    public IReadOnlyList<ExtraStudent> GetListOfStudentsIsNotInCourse(ExtraGroup group)
    {
        var studentsOutOfCourses = new List<ExtraStudent>();
        foreach (ExtraStudent student in group.GetStudents())
        {
            if (student.GetFirstDivision() is null && student.GetSecondDivision() is null)
            {
                studentsOutOfCourses.Add(student);
            }
        }

        return studentsOutOfCourses;
    }

    public bool Intersect(Lesson firstLesson, Lesson secondLesson)
    {
        return firstLesson.GetDay() == secondLesson.GetDay() &&
               firstLesson.GetNumberOfLesson == secondLesson.GetNumberOfLesson;
    }

    public TimeTable SignUpForCourse(ExtraStudent student, ElectiveModule course)
    {
        if (student.GetFirstDivision() is not null && student.GetSecondDivision() is not null)
        {
            throw new ListOfCoursesIsFullException(student);
        }

        if (student.GetStudent().Group() !.Name().MegaFaculty() == course.GetMegafaculty())
        {
            throw new MegafacultyException(student, course.GetMegafaculty(), course);
        }

        var curTimeTable = new List<Lesson>(student.GetTimeTable().GetLessons());
        bool flag = false;
        foreach (Division division in course.GetDivisions())
        {
            var moduleTimeTable = new List<Lesson>(division.GetTimeTable().GetLessons());
            flag = !(from firstLesson in curTimeTable from secondLesson in moduleTimeTable where Intersect(firstLesson, secondLesson) select firstLesson).Any();

            if (flag)
            {
                if (student.GetFirstDivision() is null)
                {
                    student.SetFirstDivision(division);
                }
                else
                {
                    student.SetSecondDivision(division);
                }

                var newListOfLessons = new List<Lesson>(curTimeTable);
                newListOfLessons.AddRange(division.GetTimeTable().GetLessons());
                var newTimeTable = new TimeTable(newListOfLessons);
                student.SetTimeTable(newTimeTable);
                return newTimeTable;
            }
        }

        if (!flag)
        {
            throw new IsNotSuitableCoursesException(student, course);
        }

        return null!;
    }

    public TimeTable DeleteReservation(ExtraStudent student, Division division)
    {
        if (student.GetFirstDivision() == division)
        {
            TimeTable newTimeTable = student.DeleteTimeTable(division.GetTimeTable());
            student.SetTimeTable(newTimeTable);
            student.SetFirstDivision(null!);
            return newTimeTable;
        }
        else if (student.GetSecondDivision() == division)
        {
            TimeTable newTimeTable = student.DeleteTimeTable(division.GetTimeTable());
            student.SetTimeTable(newTimeTable);
            student.SetSecondDivision(null!);
            return newTimeTable;
        }
        else
        {
            throw new IsNotstudentDivisionException(student);
        }
    }
}