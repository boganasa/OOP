namespace Isu.Entities;

public class CounterOfStudents
{
    private int counter = 0;

    private CounterOfStudents() { }

    private int GetId()
    {
        counter += 1;
        return counter;
    }
}