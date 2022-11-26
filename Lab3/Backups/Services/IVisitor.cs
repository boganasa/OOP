namespace Backups.Services;

public interface IVisitor
{
    void Visit(IRepoFile file);
    void Visit(IRepoDirectory directory);
}