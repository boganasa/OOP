using Backups.Models;
using Backups.Services;

namespace Backups.Entities;

public class RestorePoint
{
    public RestorePoint(IReadOnlyList<BackupObject> objects, IStorage storage, DateTime data)
    {
        Objects = new List<BackupObject>(objects);
        Storage = storage;
        Data = data;
    }

    public DateTime Data { get; }
    public IReadOnlyList<BackupObject> Objects { get; }

    public IStorage Storage { get; }
}