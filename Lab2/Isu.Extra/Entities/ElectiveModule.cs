using Isu.Entities;
using Isu.Extra.Exceptions;

namespace Isu.Extra;

public class ElectiveModule
{
    private char _megafaculty;
    private string _name;
    private IReadOnlyList<Division> _divisions;

    public ElectiveModule(char megafaculty, string name, List<Division> divisions)
    {
        const string validLetters = "DGHKLMNOPRTUVZ";
        if (!validLetters.Contains(megafaculty))
        {
            throw new NameOfMegafacultyException(megafaculty);
        }

        _megafaculty = megafaculty;
        _name = name;
        foreach (Division division in divisions)
        {
            division.SetElectiveModule(this);
        }

        _divisions = new List<Division>(divisions);
    }

    public IReadOnlyList<Division> GetDivisions() => _divisions;
    public char GetMegafaculty() => _megafaculty;
    public string GetName() => _name;
}