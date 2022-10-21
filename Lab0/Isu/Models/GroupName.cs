using Isu.Exceptions;
namespace Isu.Models;

public class GroupName
{
    private readonly char _megaFaculty;
    private char _formOfStudy;
    private CourseNumber _courseNumber;
    private string _groupNumber;
    public GroupName(string megaFaculty, string formOfStudy, CourseNumber courseNumber, string groupNumber)
    {
        const string validLetters = "DGHKLMNOPRTUVZf";
        if (megaFaculty.Length != 1 || !validLetters.Contains(megaFaculty))
        {
                throw new WrongGroupNameException($"Megafaculty {megaFaculty}  does not exist");
        }

        if (formOfStudy.Length != 1 || formOfStudy[0] != '3')
        {
            throw new WrongGroupNameException($"Student is not studying for bachelor's degree");
        }

        if (groupNumber.Length != 2 || !(groupNumber[0] - '0' >= 0 && groupNumber[0] - '0' <= 9) || !(groupNumber[1] - '0' >= 0 && groupNumber[1] - '0' <= 9))
        {
            throw new WrongGroupNameException($"Invalid group format: {groupNumber}");
        }

        _megaFaculty = megaFaculty[0];
        _formOfStudy = formOfStudy[0];
        _courseNumber = courseNumber;
        _groupNumber = groupNumber;
    }

    public string Name()
    {
        string fullGroupName;
        fullGroupName = string.Concat(_megaFaculty, _formOfStudy);
        fullGroupName = string.Concat(fullGroupName, _courseNumber.Number);
        fullGroupName = string.Concat(fullGroupName, _groupNumber);
        return fullGroupName;
    }

    public char MegaFaculty() => _megaFaculty;
    public char FormOfStudy() => _formOfStudy;
    public CourseNumber Number() => _courseNumber;
    public string GroupNumber() => _groupNumber;
}