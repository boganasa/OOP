using Backups.Exceptions;
using Backups.Services;
using Zio;

namespace Backups.Entities;

public class PathLocal : IPath
{
    public PathLocal(string path)
    {
        if (path == string.Empty)
        {
            throw new NullPath();
        }

        Path = path;
    }

    public string Path { get; }

    public bool Equals(IPath? other)
    {
        return other is not null && other.ToString() ! == ToString();
    }

    public IPath Concat(IPath path)
    {
        if (path.TooString() == string.Empty)
        {
            throw new NullPath();
        }

        var newPath = new PathLocal($"{this.TooString()}/{path.TooString()}");
        return newPath;
    }

    public IPath Concat(string path)
    {
        if (path == string.Empty)
        {
            throw new NullPath();
        }

        var newPath = new PathLocal($"{this.TooString()}/{path}");
        return newPath;
    }

    public string TooString()
    {
        return Path;
    }

    public IPath ToIPath(string path)
    {
        var newPath = new PathLocal($"{path}");
        return newPath;
    }

    public IPath FullPath()
    {
        var newPath = new PathLocal(new DirectoryInfo(Path).Name);
        return newPath;
    }

    public IPath GetName()
    {
        var newPath = new PathLocal(new DirectoryInfo(Path).Name);
        return newPath;
    }
}