using Backups.Services;

namespace Backups.Models;

public class StorageZip : IStorage
{
    public StorageZip(IRepository repository, IPath path, IReadOnlyList<IZipObject> objects)
    {
        Repository = repository;
        Path = path;
        Objects = new List<IZipObject>(objects);
    }

    public IRepository Repository { get; }
    public IPath Path { get; }
    public IReadOnlyList<IZipObject> Objects { get; private set; }

    public IStorage Adapt(List<IStorage> storages)
    {
        var newListOfStorages = new List<IZipObject>(Objects);
        foreach (StorageZip storage in storages)
        {
            foreach (IZipObject zipObject in storage.Objects)
            {
                newListOfStorages.Add(zipObject);
            }
        }

        Objects = newListOfStorages;
        return this;
    }
}