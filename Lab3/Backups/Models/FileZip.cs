using System.IO.Compression;
using Backups.Entities;
using Backups.Exceptions;
using Backups.Services;

namespace Backups.Models;

public class FileZip : IZipObject
{
    public FileZip(IPath path)
    {
        if (path.TooString() == string.Empty)
        {
            throw new NullPath();
        }

        Path = path;
    }

    public IPath Path { get; }
}