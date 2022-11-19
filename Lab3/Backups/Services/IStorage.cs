namespace Backups.Services;

public interface IStorage
{
    IRepository Repository { get; }
    IPath Path { get; }
    IReadOnlyList<IZipObject> Objects { get; }
    IStorage Adapt(List<IStorage> storages);
}