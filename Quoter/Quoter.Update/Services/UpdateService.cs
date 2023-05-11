using System.IO.Compression;

namespace Quoter.Update.Services
{
	public class UpdateService
	{
		private readonly BackupService _backupHandler;
		private readonly ProcessService _processHandler;

		public UpdateService()
		{
			_backupHandler = new BackupService();
			_processHandler = new ProcessService();
		}

		public bool TryUpdate(string installDirPath, string applicationExeName, string updateZipPath)
		{
			try
			{
				Logger.Info("Begining update.");

				_processHandler.KillProcessIfRunning(Path.GetFileNameWithoutExtension(applicationExeName));

				_backupHandler.BackupCurrentVersion(installDirPath, applicationExeName);
				if (!_backupHandler.IsBackupAvailable)
				{
					return false; // return if backup failed just to be safe
				}
				
				// Extract zip to install dir
				Logger.Info("Extracting update");
				ZipFile.ExtractToDirectory(updateZipPath, installDirPath, true);
				Logger.Info("Extraction finished. Update applied");

				// Delete update files
				_backupHandler.DeleteBackups();

				Logger.Info("Update finished successfully.");
				return true;
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Error occured during update.");

				if (_backupHandler.IsBackupAvailable)
				{
					_backupHandler.TryRollbackUpdate();
				}
				return false;
			}
		}

	}
}
