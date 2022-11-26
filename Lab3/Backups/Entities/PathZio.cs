using Backups.Exceptions;
using Backups.Services;
using Zio;

namespace Backups.Entities;

public class PathZio : IPath
{
    public PathZio(UPath path)
    {
        if (path == string.Empty)
        {
            throw new NullPath();
        }

        Path = path;
    }

    public UPath Path { get; }

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

        var newPath = new PathZio($"{this.TooString()}/{path.TooString()}");
        return newPath;
    }

    public IPath Concat(string path)
    {
        if (path == string.Empty)
        {
            throw new NullPath();
        }

        var newPath = new PathZio($"{this.TooString()}/{path}");
        return newPath;
    }

    public string TooString()
    {
        return Path.ToString();
    }

    public IPath ToIPath(string path)
    {
        if (path == string.Empty)
        {
            throw new NullPath();
        }

        var newPath = new PathZio($"{path}");
        return newPath;
    }

    public IPath FullPath()
    {
        var newPath = new PathZio(Path.GetDirectory().ToAbsolute().ToString());
        return newPath;
    }

    public IPath GetName()
    {
        var newPath = new PathZio(Path.GetName());
        return newPath;
    }
}