using Backups.Services;

namespace Backups.Models;

public class BackupObject
{
    public BackupObject(IRepository repository, IPath path)
    {
        Path = path;
        Repository = repository;
    }

    public IPath Path { get; }
    public IRepository Repository { get; }
}