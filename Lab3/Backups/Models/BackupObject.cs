using Backups.Exceptions;
using Backups.Services;

namespace Backups.Models;

public class BackupObject
{
    public BackupObject(IRepository repository, IPath path)
    {
        if (path.TooString() == string.Empty)
        {
            throw new NullPath();
        }

        Path = path;
        Repository = repository;
    }

    public IPath Path { get; }
    public IRepository Repository { get; }
}