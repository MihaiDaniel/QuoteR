using Quoter.Update.Helpers;
using System.Diagnostics;
using System.Xml.Linq;

namespace Quoter.Update.Services
{
	public class BackupService
	{
		public bool IsBackupAvailable { get; set; }

		private List<string> _lstBackupFilePaths;
		private string _installDir;
		private string _backupDir;

		public BackupService()
		{
			_lstBackupFilePaths = new List<string>();
		}

		public void BackupCurrentVersion(string installationDir, string exeName)
		{
			DateTime maxRetryTime = DateTime.Now.AddSeconds(1);
			while (!IsBackupAvailable)
			{
				if (DateTime.Now > maxRetryTime)
				{
					Logger.Error("Backup failed.");
					break;
				}
				TryBackup(installationDir, exeName);
				Thread.Sleep(1000);
			}
		}

		private void TryBackup(string installationDir, string exeName)
		{
			try
			{
				Logger.Info($"Try creating backup {installationDir} {exeName}");

				_installDir = installationDir;
				_backupDir = GetBackupDirPath(installationDir, exeName);

				// Cleanup dir if exists
				if (Directory.Exists(_backupDir))
				{
					Directory.Delete(_backupDir, true);
				}
				Directory.CreateDirectory(_backupDir);

				// Copy files to backup dir
				string[] arrCurrentVersionFilePaths = Directory.GetFiles(installationDir, "", SearchOption.AllDirectories);
				foreach (string filePath in arrCurrentVersionFilePaths)
				{
					if(filePath.Contains(Const.DirToSkip))
					{
						continue; // Skip the directory containing the updater itself
					}
					string backupFilePath = GetFileBackupPath(filePath, _installDir, _backupDir);

					Logger.Info($"Copying [{filePath}] to [{backupFilePath}]");

					if (!Directory.Exists(Path.GetDirectoryName(backupFilePath)))
					{
						Directory.CreateDirectory(Path.GetDirectoryName(backupFilePath));
					}

					File.Copy(filePath, backupFilePath, true); // Don't want to move because we would also have the updater in the same directory
					_lstBackupFilePaths.Add(backupFilePath);
				}

				IsBackupAvailable = true;

				Logger.Info("Backup created succesfully at: " + _backupDir);
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Backup error.");
				IsBackupAvailable = false;
			}
		}

		private string GetBackupDirPath(string installationDir, string exeName)
		{
			string applicationExePath = Path.Combine(installationDir, exeName);
			FileVersionInfo? versionInfo = FileVersionInfo.GetVersionInfo(applicationExePath);
			string currentVersion = versionInfo.ProductVersion ?? versionInfo.FileVersion ?? "backup";

			Logger.Info("Version of backup: " + currentVersion);

			return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Const.BackupDir, currentVersion);
		}

		private string GetFileBackupPath(string filePath, string installDir, string backupDir)
		{
			string fileRelativePath = Path.GetRelativePath(installDir, filePath);
			return Path.Combine(backupDir, fileRelativePath);
		}

		public void TryRollbackUpdate()
		{
			try
			{
				if (IsBackupAvailable)
				{
					Logger.Info("Rolling back changes.");
					foreach (string backupFilePath in _lstBackupFilePaths)
					{
						string fileRelativePath = Path.GetRelativePath(_backupDir, backupFilePath);
						string originalFilePath = Path.Combine(_installDir, fileRelativePath);

						if (!Directory.Exists(Path.GetDirectoryName(originalFilePath)))
						{
							Directory.CreateDirectory(Path.GetDirectoryName(originalFilePath));
						}

						File.Copy(backupFilePath, originalFilePath, true);
					}
					Logger.Info("Rollback succesfull.");
				}
				else
				{
					Logger.Error("No backup available to rollback.");
				}
			}
			catch (Exception ex)
			{
				// Oops
				Logger.Error(ex, "Rollback failed.");
			}
		}

		internal void DeleteBackups()
		{
			try
			{
				Logger.Info("Deleting backups.");
				Directory.Delete(_backupDir, true);
				Logger.Info("Deletion of backups successfull.");
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Deletion of backups failed.");
			}

		}
	}
}
