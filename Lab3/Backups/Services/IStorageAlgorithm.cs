using Backups.Models;

namespace Backups.Services;

public interface IStorageAlgorithm
{
    public IStorage RunStorageAlgorithm(IArchive archive, IRepository repository, IReadOnlyList<BackupObject> objects, IPath path, string name);
}