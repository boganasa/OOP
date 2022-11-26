using System.Globalization;
using Backups.Models;
using Backups.Services;

namespace Backups.Entities;

public class SingleStorageAlgorithm : IStorageAlgorithm
{
    public IStorage RunStorageAlgorithm(IArchive archive, IRepository repository, IReadOnlyList<BackupObject> objects, IPath path, string name)
    {
        IStorage newStorage = archive.Archive(repository, objects, path, name);
        return newStorage;
    }
}