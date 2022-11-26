using Backups.Exceptions;
using Backups.Services;

namespace Backups.Models;

public class DirectoryObject : IRepoDirectory
{
    private Func<IReadOnlyList<IRepoObject>> _func;
    public DirectoryObject(Func<IReadOnlyList<IRepoObject>> objects, IPath path)
    {
        if (path.TooString() == string.Empty)
        {
            throw new NullPath();
        }

        _func = objects;
        Path = path;
    }

    public IPath Path { get; }

    public IReadOnlyList<IRepoObject> Objects => _func();

    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
}