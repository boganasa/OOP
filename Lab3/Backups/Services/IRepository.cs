namespace Backups.Services;

public interface IRepository
{
    IRepoObject GetObject(IPath path);
    Stream OpenWrite(IPath path);
}