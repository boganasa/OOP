namespace Backups.Services;

public interface IRepoDirectory : IRepoObject
{
    IReadOnlyList<IRepoObject> Objects { get; }
}