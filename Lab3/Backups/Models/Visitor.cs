using System.IO.Compression;
using Backups.Entities;
using Backups.Services;

namespace Backups.Models;

public class Visitor : IVisitor
{
    public Visitor(ZipArchive archive)
    {
        Archive = archive;
        StackZipArchives = new Stack<IPath>();
        StackZipObjects = new Stack<List<IZipObject>>();
        StackZipObjects.Push(new List<IZipObject>());
    }

    public ZipArchive Archive { get; }
    public Stack<IPath> StackZipArchives { get; }
    public Stack<List<IZipObject>> StackZipObjects { get; }

    public void Visit(IRepoFile file)
    {
        IPath filePath;
        if (StackZipArchives.Count > 0)
        {
            filePath = StackZipArchives.Peek().Concat(file.Path.GetName());
        }
        else
        {
            filePath = file.Path.GetName();
        }

        ZipArchiveEntry entry = Archive.CreateEntry(filePath.TooString());
        using (Stream stream = entry.Open())
        {
            file.FileStream.CopyTo(stream);
        }

        StackZipObjects.Peek().Add(new FileZip(filePath));
    }

    public void Visit(IRepoDirectory directory)
    {
        IPath directoryPath;
        if (StackZipArchives.Count > 0)
        {
            directoryPath = StackZipArchives.Peek().Concat(directory.Path.GetName());
        }
        else
        {
            directoryPath = directory.Path.GetName();
        }

        IReadOnlyList<IRepoObject> repoObjects = directory.Objects;
        if (repoObjects.Count == 0)
        {
            StackZipObjects.Peek().Add(new DirectoryZip(new List<IZipObject>(), directoryPath));
            Archive.CreateEntry($"{directoryPath}/");
        }
        else
        {
            StackZipArchives.Push(directoryPath);
            StackZipObjects.Push(new List<IZipObject>());
            foreach (IRepoObject repoObject in repoObjects)
            {
                repoObject.Accept(this);
            }

            IReadOnlyList<IZipObject> objects = StackZipObjects.Pop();
            StackZipObjects.Peek().Add(new DirectoryZip(objects, directoryPath));
            StackZipArchives.Pop();
        }
    }
}