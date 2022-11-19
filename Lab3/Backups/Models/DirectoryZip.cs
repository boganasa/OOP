using Backups.Entities;
using Backups.Services;

namespace Backups.Models;

public class DirectoryZip : IZipObject
{
    public DirectoryZip(IReadOnlyList<IZipObject> objects, IPath path)
    {
        Objects = new List<IZipObject>(objects);
        Path = path;
    }

    public IReadOnlyList<IZipObject> Objects { get; private set; }
    public IPath Path { get; }
}