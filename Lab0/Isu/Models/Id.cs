namespace Isu.Models;

public class Id
{
    private static int _idNumberCur = 100000;
    private int _id;

    public Id()
    {
        _id = _idNumberCur;
        _idNumberCur++;
    }

    public Id(int a)
    {
        _id = 0;
    }

    public int NumberId() => _id;
}