using Backups.Entities;
using Backups.Exceptions;
using Backups.Services;

namespace Backups.Models;

public class DirectoryZip : IZipObject
{
    public DirectoryZip(IReadOnlyList<IZipObject> objects, IPath path)
    {
        if (path.TooString() == string.Empty)
        {
            throw new NullPath();
        }

        Objects = new List<IZipObject>(objects);
        Path = path;
    }

    public IReadOnlyList<IZipObject> Objects { get; private set; }
    public IPath Path { get; }
}