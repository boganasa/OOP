using Backups.Entities;
using Backups.Models;
using Backups.Services;
using Xunit;
using Zio;
using Zio.FileSystems;

namespace Backups.Test
{
    public class TestBackups
    {
        [Fact]
        public void AddBackupObjects_ExecuteBackupTask()
        {
            IFileSystem fileSystem = new MemoryFileSystem();
            var first = new PathZio("/directory1/A.txt");
            var second = new PathZio("/directory2/B.txt");
            fileSystem.CreateDirectory(first.FullPath().TooString());
            fileSystem.CreateFile(first.TooString()).Close();
            fileSystem.CreateDirectory(second.FullPath().TooString());
            fileSystem.CreateFile(second.TooString()).Close();

            IRepository repository = new RepositoryZio(fileSystem);
            IStorageAlgorithm storageAlgorithm = new SplitStorageAlgorithm();
            var config = new Config(repository, storageAlgorithm);

            var objectA = new BackupObject(repository, first);
            var objectB = new BackupObject(repository, second);

            var directory = new PathZio("~/BackupTask");
            var backupTask = new BackupTask(config, directory, new Backup());

            backupTask.AddBackupObject(objectA);
            backupTask.AddBackupObject(objectB);

            backupTask.Run();

            Assert.Equal(2, backupTask.Objects.Count);
        }
    }
}