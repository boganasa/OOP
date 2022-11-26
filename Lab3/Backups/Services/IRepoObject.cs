namespace Backups.Services;

public interface IRepoObject
{
    IPath Path { get; }

    void Accept(IVisitor visitor);
}