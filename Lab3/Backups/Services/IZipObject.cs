using Backups.Entities;

namespace Backups.Services;

public interface IZipObject
{
    IPath Path { get; }
}