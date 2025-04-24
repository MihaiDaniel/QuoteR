using Microsoft.EntityFrameworkCore;
using Quoter.Framework.Data;
using Quoter.Framework.Data.Entities;
using Quoter.Framework.Services.Api;
using Quoter.Framework.Services.Messaging;
using Quoter.Framework.Services.Registration;
using Quoter.Shared.Enums;
using Quoter.Shared.Models;
using System.Diagnostics;
using System.Reflection;

namespace Quoter.Framework.Services.Versioning
{
	/// <summary>
	/// Service responible for updating the application
	/// </summary>
	public class UpdateService : IUpdateService
	{
		private readonly ILogger _logger;
		private readonly IRegistrationService _registrationService;
		private readonly IWebApiService _webApiService;
		private readonly IVersionService _versionService;
		private readonly QuoterContext _context;
		private readonly IMessagingService _messagingService;

		public UpdateService(ILogger logger,
							IRegistrationService registrationService,
							IWebApiService webApiService,
							IVersionService versionService,
							QuoterContext context,
							IMessagingService messagingService)
		{
			_logger = logger;
			_registrationService = registrationService;
			_webApiService = webApiService;
			_versionService = versionService;
			_context = context;
			_messagingService = messagingService;
		}

		public async Task<bool> VerifyIfNewVersionAvailable()
		{
			try
			{
				_logger.Debug("Verifying if a new update is available");
				if (!_registrationService.IsRegistered())
				{
					_logger.Warn("Can't check if new version is available because application is not registered.");
					return false;
				}

				QuoterVersionInfo currentVersion = _versionService.GetCurrentAppVersion();
				QuoterVersionInfo latestVersion = await _webApiService.GetLatestVersion();

				if (currentVersion.IsOlderThan(latestVersion))
				{
					_logger.Debug("A new version update has been found");
					return true;
				}
			}
			catch (Exception ex)
			{
				_logger.Error(ex, "An error occured while checking VerifyIfNewVersionAvailable");
			}
			_logger.Debug("No new versions have been found. This it the latest version");
			return false;
		}

		public async Task<ActionResult> VerifyIfUpdateAppliedAsync()
		{
			_logger.Debug("Verifying if a new update has been applied");

			QuoterVersionInfo currentVersion = _versionService.GetCurrentAppVersion();
			//_logger.Warn("Current version: " + currentVersion.ToString());

			//var vers = _context.AppVersions.ToList();
			//foreach (var version in vers)
			//{
			//	_logger.Debug($"Db version: Id={version.Id}, Version={version.Version}, IsApplied={version.IsApplied}");
			//}

			AppVersion? appVersion = await _context.AppVersions.FirstOrDefaultAsync(v => v.Version == currentVersion.ToString());
			if (appVersion != null)
			{
				if (!appVersion.IsApplied)
				{
					_logger.Info($"Update applied - {appVersion.Version}");
					appVersion.IsApplied = true;
					await _context.SaveChangesAsync();
					return ActionResult.Success(appVersion.Version);
				}
				_logger.Debug("No update has been applied");
			}
			return ActionResult.Fail();
			// Ideea: verify if an update was not applied and maybe retry?
		}

		public ActionResult MarkUpdateAsAppliedIfAppUpdated()
		{
			_logger.Debug("Verifying if a new update has been applied");
			QuoterVersionInfo currentVersion = _versionService.GetCurrentAppVersion();
			AppVersion? appVersion = _context.AppVersions.FirstOrDefault(v => v.Version == currentVersion.ToString());
			if (appVersion != null)
			{
				if (!appVersion.IsApplied)
				{
					_logger.Info($"Update applied - {appVersion.Version}");
					appVersion.IsApplied = true;
					_context.SaveChanges();
					return ActionResult.Success(appVersion.Version);
				}
				_logger.Debug("No update has been applied");
			}
			return ActionResult.Fail();
		}

		public string GetLastAppliedVersion()
		{
			AppVersion? version = _context.AppVersions.Where(v => v.IsApplied).OrderBy(v => v.Id).LastOrDefault();
			if (version != null)
			{
				return version.Version;
			}
			return string.Empty;
		}

		public async Task TryUpdateAsync(bool isSilent)
		{
			try
			{
				_logger.Debug($"isSilent: {isSilent}");

				if (!_registrationService.IsRegistered())
				{
					_logger.Warn("Can't update because application is not registered.");
					return;
				}

				QuoterVersionInfo latestVersionFromServer = await _webApiService.GetLatestVersion();
				QuoterVersionInfo currentVersion = _versionService.GetCurrentAppVersion();

				if (currentVersion.IsOlderThan(latestVersionFromServer))
				{
					ActionResult result = await DownloadVersionAsync(latestVersionFromServer);
					if (result.IsSuccess)
					{
						_logger.Info("Succesfully downloaded update file.");
						await WaitForBackgroundTasksToFinish();

						_logger.Info("Starting update executable...");
						string updaterExePath = GetUpdaterAppExePath();
						string updaterArgs = GetUpdaterArgs(result.GetValue<AppVersion>(), isSilent);
						Process.Start(updaterExePath, updaterArgs); // Try like this, Updater should close the app automatically
					}
				}
			}
			catch (Exception ex)
			{
				_logger.Error(ex, "Application update failed!");
			}
		}

		private async Task WaitForBackgroundTasksToFinish()
		{
			_logger.Info("Waiting for any background tasks to finish processing");
			DateTime startWait = DateTime.Now;
			DateTime endWait = startWait.AddHours(1);
			while (DateTime.Now < endWait)
			{
				if (_messagingService.ExistsAnnouncement(Event.ImportInProgress))
				{
					await Task.Delay(1000);
					continue;
				}
				if (_messagingService.ExistsAnnouncement(Event.ExportInProgress))
				{
					await Task.Delay(1000);
					continue;
				}
				_logger.Info("Waiting for background task has ended, continuing...");
				return;
			}
			throw new Exception("WaitForBackgroundTasksToFinish has not completed succesfully. Import/Export tasks seem to be still working after 1 hour");
		}

		private async Task<ActionResult> DownloadVersionAsync(QuoterVersionInfo latestVersion)
		{
			ActionResult result = await _webApiService.DownloadVersionAsync(latestVersion.PublicId);
			if (result.IsSuccess)
			{
				string filePath = result.GetValue<string>();
				AppVersion? appVersion = await _context.AppVersions.FirstOrDefaultAsync(v => v.PublicId == latestVersion.PublicId);
				if (appVersion == null)
				{
					appVersion = new()
					{
						PublicId = latestVersion.PublicId,
						Version = latestVersion.ToString(),
						FilePath = filePath,
						IsApplied = false
					};
					_context.AppVersions.Add(appVersion);
				}
				else
				{
					// If same version was re-downloaded just re-update the file path in case something changed like version number
					appVersion.FilePath = filePath;
					appVersion.Version = latestVersion.ToString();
				}
				await _context.SaveChangesAsync();
				return ActionResult.Success(appVersion);
			}
			return ActionResult.Fail();
		}

		private string GetUpdaterAppExePath()
		{
			string currentDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			return Path.Combine(currentDir, "Updater", "Updater.exe");
		}

		/// <remarks>
		/// -i = install dir path
		/// -e = quoter exe name
		/// -u = update file path
		/// -uid = update id (as in quoter local db)
		/// -s = silent update (true/false, if true the updater form is not visible)
		/// -r = is restarted (used by updater, should always be false)
		/// </remarks>
		private string GetUpdaterArgs(AppVersion appVersion, bool isSilent)
		{
			// -i C:\My\Path to\install folder -e MyExeName -u C:\My\Path to\update.zip -uid 3 -s false -r false
			string appExePath = Assembly.GetEntryAssembly().Location;
			string appExe = Path.GetFileName(appExePath);
			string installDir = Path.GetDirectoryName(appExePath);

			return $"-i {installDir} -e {appExe} -u {appVersion.FilePath} -uid {appVersion.Id} -s {isSilent} -r false";
		}

	}
}
