using Backups.Services;

namespace Backups.Entities;

public class Config
{
    public Config(IArchive archiver, IRepository repository, IStorageAlgorithm algorithm)
    {
        Repository = repository;
        Algorithm = algorithm;
        Archiver = archiver;
    }

    public IArchive Archiver { get; }
    public IRepository Repository { get; }
    public IStorageAlgorithm Algorithm { get; }
}