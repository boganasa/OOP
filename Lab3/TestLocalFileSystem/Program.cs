using System.IO.Compression;
using Backups.Entities;
using Backups.Models;
using Backups.Services;
using Zio;
using Zio.FileSystems;

namespace TestLocalFileSystem;

public class Program
{
    public static void Main(string[] args)
    {
        IPath pathA = new PathZio("/Stacy/ITMO/English/chart.docx");
        var pathB = new PathZio("/Stacy/ITMO/English/StuckInTheOcean");
        

        IRepository repository = new RepositoryLocal();
        IArchive archiver = new ArchiveAsZip();
        IStorageAlgorithm storageAlgorithm = new SingleStorageAlgorithm();
        var config = new Config(repository, storageAlgorithm);

        var objectA = new BackupObject(repository, pathA);
        var objectB = new BackupObject(repository, pathB);

        IPath backupTaskDirName = new PathZio("/Stacy/ITMO/English/BackupTask");
        var backupTask = new BackupTask(config, backupTaskDirName, new Backup());

        backupTask.AddBackupObject(objectA);
        backupTask.AddBackupObject(objectB);

        backupTask.Run();

    }
}