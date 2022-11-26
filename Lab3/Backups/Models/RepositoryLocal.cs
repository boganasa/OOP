using Backups.Exceptions;
using Backups.Services;

namespace Backups.Models;

public class RepositoryLocal : IRepository
{
    public IRepoObject GetObject(IPath path)
    {
        if (path.TooString() == string.Empty)
        {
            throw new NullPath();
        }

        if (File.Exists(path.ToString()))
        {
            var newFunc = new Func<Stream>(() => File.OpenRead(path.TooString()));
            var file = new FileObject(newFunc, path);
            return file;
        }
        else if (Directory.Exists(path.ToString()))
        {
            var newFunc = new Func<IReadOnlyList<IRepoObject>>(() =>
            {
                return Directory.GetFiles(path.TooString(), "*", SearchOption.TopDirectoryOnly).Select(x => GetObject(path.ToIPath(x))).ToList();
            });
            var directory = new DirectoryObject(newFunc, path);
            return directory;
        }

        throw new Existance(path.TooString());
    }

    public Stream OpenWrite(IPath path)
    {
        Directory.CreateDirectory(path.FullPath().TooString());
        return File.OpenWrite(path.TooString());
    }
}