using System.Globalization;
using Backups.Models;
using Backups.Services;

namespace Backups.Entities;

public class BackupTask
{
    public BackupTask(Config config, IPath path, Backup backup)
    {
        Objects = new List<BackupObject>();
        Config = config;
        Backup = backup;
        Path = path;
    }

    public IReadOnlyList<BackupObject> Objects { get; private set; }
    public Config Config { get; }
    public Backup Backup { get; }
    public IPath Path { get; }

    public void RemoveBackupObject(BackupObject newObject)
    {
        var list = new List<BackupObject>(Objects);
        list.Remove(newObject);
        Objects = new List<BackupObject>(list);
    }

    public void AddBackupObject(BackupObject newObject)
    {
        var list = new List<BackupObject>(Objects);
        list.Add(newObject);
        Objects = new List<BackupObject>(list);
    }

    public void Run()
    {
        IArchive archive = new ArchiveAsZip();
        DateTime data = DateTime.Now;
        string dataToString = data.ToString(CultureInfo.CurrentCulture).Replace(":", "_");
        IPath fullPath = Path.Concat(dataToString);
        IStorage newStorage = Config.Algorithm.RunStorageAlgorithm(archive, Config.Repository, Objects, fullPath, dataToString);
        var newPoint = new RestorePoint(Objects, newStorage, data);
        Backup.AddRestorePoint(newPoint);
    }
}