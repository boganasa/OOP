using Backups.Services;

namespace Backups.Entities;

public class Config
{
    public Config(IRepository repository, IStorageAlgorithm algorithm)
    {
        Repository = repository;
        Algorithm = algorithm;
    }

    public IRepository Repository { get; }
    public IStorageAlgorithm Algorithm { get; }
}