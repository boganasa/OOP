using System.Globalization;
using Backups.Exceptions;
using Backups.Models;
using Backups.Services;

namespace Backups.Entities;

public class BackupTask
{
    public BackupTask(Config config, IPath path, IBackup backup)
    {
        Objects = new List<BackupObject>();
        Config = config;
        Backup = backup;
        Path = path;
    }

    public IReadOnlyList<BackupObject> Objects { get; private set; }
    public Config Config { get; }
    public IBackup Backup { get; }
    public IPath Path { get; }

    public void RemoveBackupObject(BackupObject newObject)
    {
        var list = new List<BackupObject>(Objects);
        if (!list.Contains(newObject))
        {
            throw new ExistanceinBackup(newObject.ToString() !);
        }

        list.Remove(newObject);
        Objects = new List<BackupObject>(list);
    }

    public void AddBackupObject(BackupObject newObject)
    {
        var list = new List<BackupObject>(Objects);
        if (list.Contains(newObject))
        {
            throw new ExistanceinBackup(newObject.ToString() !);
        }

        list.Add(newObject);
        Objects = new List<BackupObject>(list);
    }

    public void Run()
    {
        DateTime data = DateTime.Now;
        string dataToString = data.ToString("MM/dd/yyyy_HH_mm_ss");
        IPath fullPath = Path.Concat(dataToString);
        IStorage newStorage = Config.Algorithm.RunStorageAlgorithm(Config.Archiver, Config.Repository, Objects, fullPath, dataToString);
        var newPoint = new RestorePoint(Objects, newStorage, data);
        Backup.AddRestorePoint(newPoint);
    }
}