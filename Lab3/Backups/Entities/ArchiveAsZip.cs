using System.Globalization;
using System.IO.Compression;
using System.Xml.XPath;
using Backups.Models;
using Backups.Services;

namespace Backups.Entities;

public class ArchiveAsZip : IArchive
{
    public IStorage Archive(IRepository repository, IReadOnlyList<BackupObject> backupObjects, IPath path, string name)
    {
        IPath zipPath = path.Concat($"{name}.zip");
        Visitor visitor;
        using (Stream stream = repository.OpenWrite(zipPath))
        {
            var zipArchive = new ZipArchive(stream, ZipArchiveMode.Create);
            visitor = new Visitor(zipArchive);
            foreach (BackupObject backupObject in backupObjects)
            {
                backupObject.Repository.GetObject(backupObject.Path).Accept(visitor);
            }
        }

        IReadOnlyList<IZipObject> objects = visitor.StackZipObjects.Peek();
        var newStorage = new StorageZip(repository, zipPath, objects);
        return newStorage;
    }
}