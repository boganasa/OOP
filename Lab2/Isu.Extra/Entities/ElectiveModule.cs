using Isu.Extra.Exceptions;

namespace Isu.Extra;

public class ElectiveModule
{
    private char _megafaculty;
    private string _name;
    private IReadOnlyList<Division> _divisions = new List<Division>();

    public ElectiveModule(char megafaculty, string name)
    {
        const string validLetters = "DGHKLMNOPRTUVZ";
        if (!validLetters.Contains(megafaculty))
        {
            throw new NameOfMegafacultyException(megafaculty);
        }

        _megafaculty = megafaculty;
        _name = name;
    }

    public IReadOnlyList<Division> GetDivisions() => _divisions;
    public char GetMegafaculty() => _megafaculty;
    public string GetName() => _name;

    public void SetDivisions(List<Division> divisions)
    {
        _divisions = new List<Division>(divisions);
    }
}