using System.IO.Compression;
using Backups.Entities;
using Backups.Services;

namespace Backups.Models;

public class FileZip : IZipObject
{
    public FileZip(IPath path)
    {
        Path = path;
    }

    public IPath Path { get; }
}