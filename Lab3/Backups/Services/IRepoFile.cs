namespace Backups.Services;

public interface IRepoFile : IRepoObject
{
    Stream FileStream { get; }
}