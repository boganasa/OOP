using Isu.Entities;
using Isu.Extra.Exceptions;
using Isu.Extra.Models;
using Isu.Models;
using Isu.Services;

namespace Isu.Extra.Services;

public class IsuExtra : IIsuService, IIsuExtra
{
    private List<ElectiveModule> _listOsCourses = new List<ElectiveModule>();
    private List<ExtraGroup> _listOfGroup = new List<ExtraGroup>();
    private IIsuService _isuService = new ClassInterface();

    public IReadOnlyList<ElectiveModule> GetListOfCourses() => _listOsCourses;

    public ElectiveModule CreateNewCourse(char megafaculty, string name)
    {
        var newCourse = new ElectiveModule(megafaculty, name);
        _listOsCourses.Add(newCourse);
        return newCourse;
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
            if (!student.IsStudentInCourse())
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
        if (student.GetDivisions().Count == student.GetAllowCount())
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
                student.SetDivisions(division);
                var newListOfLessons = new List<Lesson>(curTimeTable);
                newListOfLessons.AddRange(division.GetTimeTable().GetLessons());
                var newTimeTable = new TimeTable(newListOfLessons);
                student.SetTimeTable(newTimeTable);
                return newTimeTable;
            }
        }

        throw new IsNotSuitableCoursesException(student, course);
    }

    public TimeTable DeleteReservation(ExtraStudent student, Division division)
    {
        bool flag = false;
        foreach (Division curdivision in student.GetDivisions())
        {
            if (curdivision == division)
            {
                flag = true;
                TimeTable newTimeTable = student.DeleteTimeTable(division.GetTimeTable());
                student.SetTimeTable(newTimeTable);
                student.SetDivisions(division);
                return newTimeTable;
            }
        }

        if (!flag)
        {
            throw new IsNotstudentDivisionException(student);
        }

        return student.GetTimeTable();
    }

    public Group? AddGroup(GroupName name)
    {
        Group? group = _isuService.AddGroup(name);
        if (group is null)
        {
            throw new InvalidOperationException();
        }

        var extraGroup = new ExtraGroup(group.Name(), new TimeTable(new List<Lesson>()));
        _listOfGroup.Add(extraGroup);
        return group;
    }

    public Student AddStudent(Group group, string name)
    {
        Student student = _isuService.AddStudent(group, name);
        var extraGroup = new ExtraGroup(group.Name(), null!);
        var extraStudent = new ExtraStudent(student.Name(), student.IdNumber(), extraGroup, 2);
        return student;
    }

    public Student GetStudent(int id)
    {
        Student student = _isuService.GetStudent(id);
        var extraGroup = new ExtraGroup(student.Group() !.Name(), null!);
        var extraStudent = new ExtraStudent(student.Name(), student.IdNumber(), extraGroup, 2);
        foreach (ExtraGroup group in _listOfGroup)
        {
            foreach (ExtraStudent curstudent in group.GetStudents())
            {
                if (curstudent.GetStudent() == student)
                {
                    extraStudent = curstudent;
                    break;
                }
            }
        }

        return student;
    }

    public Student? FindStudent(int id)
    {
        Student? student = _isuService.FindStudent(id);
        return student;
    }

    public List<Student> FindStudents(GroupName groupName)
    {
        List<Student> students = _isuService.FindStudents(groupName);
        return students;
    }

    public List<Student> FindStudents(CourseNumber courseNumber)
    {
        List<Student> students = _isuService.FindStudents(courseNumber);
        return students;
    }

    public Group? FindGroup(GroupName groupName)
    {
        Group? group = _isuService.FindGroup(groupName);
        return group;
    }

    public List<Group> FindGroups(CourseNumber courseNumber)
    {
        List<Group> groups = _isuService.FindGroups(courseNumber);
        return groups;
    }

    public void ChangeStudentGroup(Student student, Group newGroup)
    {
        _isuService.ChangeStudentGroup(student, newGroup);
    }
}