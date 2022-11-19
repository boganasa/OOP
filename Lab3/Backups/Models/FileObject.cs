using Backups.Exceptions;
using Backups.Services;

namespace Backups.Models;

public class FileObject : IRepoFile
{
    private Func<Stream> _openFunc;
    public FileObject(Func<Stream> openFunctor, IPath path)
    {
        if (path.TooString() == string.Empty)
        {
            throw new NullPath();
        }

        _openFunc = openFunctor;
        Path = path;
    }

    public Stream FileStream
    {
        get
        {
            return _openFunc();
        }
    }

    public IPath Path { get; }

    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
}