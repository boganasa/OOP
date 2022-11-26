using Backups.Models;

namespace Backups.Services;

public interface IArchive
{
    IStorage Archive(IRepository repository, IReadOnlyList<BackupObject> backupObjects, IPath path, string name);
}