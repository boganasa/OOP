using Backups.Models;
using Backups.Services;

namespace Backups.Entities;

public class SplitStorageAlgorithm : IStorageAlgorithm
{
    public IStorage RunStorageAlgorithm(IArchive archive, IRepository repository, IReadOnlyList<BackupObject> objects, IPath path, string name)
    {
        var storages = new List<IStorage>();
        foreach (BackupObject backupObject in objects)
        {
            name = backupObject.Path.TooString();
            storages.Add(archive.Archive(repository, new List<BackupObject> { backupObject }, path, name));
        }

        storages[0] = storages[0].Adapt(storages);
        return storages[0];
    }
}