using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.Update
{
	public class BackupHandler
	{
		public bool IsBackupAvailable;

		private List<string> _backupFilePaths;
		private string InstallFolder;

		public BackupHandler()
		{
			_backupFilePaths = new List<string>();
		}

		public void BackupCurrentVersion(string installationFolder, string exeName)
		{
			try
			{
				Logger.Info("Creating backup");
				InstallFolder = installationFolder;
				string applicationExePath = Path.Combine(installationFolder, exeName);
				var versionInfo = FileVersionInfo.GetVersionInfo(applicationExePath);
				string currentVersion = versionInfo.ProductVersion ?? versionInfo.FileVersion ?? "backup";

				string backupCurrentVersion = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), currentVersion);
				if (Directory.Exists(backupCurrentVersion))
				{
					Directory.Delete(backupCurrentVersion, true);
				}
				else
				{
					Directory.CreateDirectory(backupCurrentVersion);
				}

				string[] arrCurrentVersionFiles = Directory.GetFiles(installationFolder);
				foreach (string file in arrCurrentVersionFiles)
				{
					string backupFilePath = Path.Combine(backupCurrentVersion, file);
					File.Copy(file, backupFilePath, true);
					_backupFilePaths.Add(backupFilePath);
				}
				Logger.Info("Backup created at: " + backupCurrentVersion);
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Backup failed.");
				IsBackupAvailable = false;
			}
			IsBackupAvailable = true;
		}

		public void RollbackUpdate()
		{
			try
			{
				Logger.Info("Rolling back changes.");
				foreach (string backupFilePath in _backupFilePaths)
				{
					string originalFilePath = Path.Combine(InstallFolder, Path.GetFileName(backupFilePath));
					File.Copy(backupFilePath, originalFilePath, true);
				}
				Logger.Info("Rollback succesfull.");
			}
			catch (Exception ex)
			{
				// Oops
				Logger.Error(ex, "Rollback failed.");
			}
		}
	}
}
