using Backups.Entities;

namespace Backups.Services;

public interface IBackup
{
    void AddRestorePoint(RestorePoint point);
}