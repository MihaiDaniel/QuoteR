using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.Update
{
	public class UpdateHandler
	{
		private BackupHandler _backupHandler;
		private ProcessHandler _processHandler;

		public UpdateHandler()
		{
			_backupHandler = new BackupHandler();
			_processHandler = new ProcessHandler();
		}

		public bool TryUpdate(string installFolderPath, string applicationExeName, string updateZipPath)
		{
			try
			{
				Logger.Info("Begining update.");

				_processHandler.KillProcessIfRunning(Path.GetFileNameWithoutExtension(applicationExeName));

				_backupHandler.BackupCurrentVersion(installFolderPath, applicationExeName);
				if (!_backupHandler.IsBackupAvailable)
				{
					return false; // return if backup failed just to be safe
				}

				// Get zip and unzip
				string updateFolderPath = UnZipUpdate(updateZipPath);

				// Copy and replace data
				ApplyUpdate(installFolderPath, updateFolderPath);

				// Delete update files
				Directory.Delete(updateFolderPath, true);

				Logger.Info("Update finished successfully.");
				return true;
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Error occured during update.");

				if (_backupHandler.IsBackupAvailable)
				{
					_backupHandler.RollbackUpdate();
				}
				return false;
			}
		}

		private string UnZipUpdate(string zipPath)
		{
			string updateName = Path.GetFileNameWithoutExtension(zipPath);
			string updateFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), updateName);

			ZipFile.ExtractToDirectory(zipPath, updateFolder);
			return updateFolder;
		}

		private void ApplyUpdate(string installFolderPath, string updateFolderPath)
		{
			string[] arrFiles = Directory.GetFiles(updateFolderPath);
			foreach (string file in arrFiles)
			{
				string appFilePath = Path.Combine(installFolderPath, Path.GetFileName(file));
				File.Copy(file, appFilePath, true);
			}
		}
	}
}
