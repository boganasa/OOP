using System.Drawing;

namespace Backups.Entities;

public class Backup
{
    public Backup()
    {
        Points = new List<RestorePoint>();
    }

    public IReadOnlyList<RestorePoint> Points { get; private set; }

    public void AddRestorePoint(RestorePoint point)
    {
        var newList = new List<RestorePoint>(Points);
        newList.Add(point);
        Points = newList;
    }
}