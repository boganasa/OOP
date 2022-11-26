using System.Drawing;
using Backups.Services;

namespace Backups.Entities;

public class Backup : IBackup
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