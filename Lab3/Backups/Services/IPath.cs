namespace Backups.Services;

public interface IPath : IEquatable<IPath>
{
    IPath Concat(IPath path);
    IPath Concat(string path);
    string TooString();
    IPath ToIPath(string path);
    IPath FullPath();
    IPath GetName();
}