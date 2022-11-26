using Backups.Exceptions;
using Backups.Services;
using Zio;

namespace Backups.Models;

public class RepositoryZio : IRepository
{
    public RepositoryZio(IFileSystem fileSystem)
    {
        FileSystem = fileSystem;
    }

    public IFileSystem FileSystem { get; }
    public IRepoObject GetObject(IPath path)
    {
        if (path.TooString() == string.Empty)
        {
            throw new NullPath();
        }

        if (FileSystem.FileExists(path.TooString()))
        {
            var newFunc = new Func<Stream>(() => FileSystem.OpenFile(path.TooString(), FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
            var file = new FileObject(newFunc, path);
            return file;
        }
        else if (FileSystem.DirectoryExists(path.TooString()))
        {
            var newFunc = new Func<IReadOnlyList<IRepoObject>>(() =>
                FileSystem.EnumerateItems(path.TooString(), SearchOption.TopDirectoryOnly)
                    .Select(x => GetObject(path.ToIPath(x.ToString()))).ToList());
            return new DirectoryObject(newFunc, path);
        }

        throw new Existance(path.TooString());
    }

    public Stream OpenWrite(IPath path)
    {
        if (path.TooString() == string.Empty)
        {
            throw new NullPath();
        }

        Directory.CreateDirectory(path.FullPath().TooString());
        return File.OpenWrite(path.TooString());
    }
}