using Microsoft.EntityFrameworkCore;
using Quoter.Framework.Data;
using Quoter.Framework.Entities;
using Quoter.Shared.Enums;
using Quoter.Shared.Models;
using System.Diagnostics;
using System.Reflection;

namespace Quoter.Framework.Services.Api
{
	public class UpdateService : IUpdateService
	{
		private readonly ILogger _logger;
		private readonly IRegistrationService _registrationService;
		private readonly IWebApiService _webApiService;
		private readonly QuoterContext _context;

		public UpdateService(ILogger logger,
							IRegistrationService registrationService,
							IWebApiService webApiService,
							QuoterContext context)
		{
			_logger = logger;
			_registrationService = registrationService;
			_webApiService = webApiService;
			_context = context;
		}

		public async Task<bool> VerifyIfNewVersionAvailable()
		{
			try
			{
				if (!_registrationService.IsRegistered())
				{
					_logger.Warn("Can't check if new version is available because application is not registered.");
					return false;
				}
				QuoterVersionInfo latestVersion = await _webApiService.GetLatestVersion();
				QuoterVersionInfo currentVersion = GetCurrentVersion();

				if (currentVersion.CompareWith(latestVersion) == EnumVersionCompare.Older)
				{
					return true;
				}
			}
			catch(Exception ex)
			{
				_logger.Error(ex, "An error occured while checking IsNewVersionAvailable");
			}
			return false;
		}

		public async Task<ActionResult> VerifyIfUpdateApplied()
		{
			QuoterVersionInfo currentVersion = GetCurrentVersion();
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
			}
			return ActionResult.Fail();
			// Ideea verify if an update was not applied and maybe retry?
		}

		public async Task TryUpdate(bool isSilent)
		{
			try
			{
				if (!_registrationService.IsRegistered())
				{
					_logger.Warn("Can't update because application is not registered.");
					return;
				}
				QuoterVersionInfo latestVersion = await _webApiService.GetLatestVersion();
				QuoterVersionInfo currentVersion = GetCurrentVersion();

				if (currentVersion.CompareWith(latestVersion) == EnumVersionCompare.Older)
				{
					ActionResult result = await DownloadVersionAsync(latestVersion);
					if(result.IsSuccess)
					{
						_logger.Info("Downloaded update file. Starting updater...");
						string updaterExePath = GetUpdaterAppExePath();
						string updaterArgs = GetUpdaterArgs(result.GetValue<AppVersion>(), isSilent);
						Process.Start(updaterExePath, updaterArgs); // Try like this, Updater should close the app automatically
					}
				}
			}
			catch (Exception ex)
			{
				_logger.Error(ex);
			}
		}

		private QuoterVersionInfo GetCurrentVersion()
		{
			Version? version = Assembly.GetEntryAssembly().GetName().Version;
			QuoterVersionInfo currentVersion = new(version.ToString());
			return currentVersion;
		}

		private async Task<ActionResult> DownloadVersionAsync(QuoterVersionInfo latestVersion)
		{
			ActionResult result = await _webApiService.DownloadVersionAsync(latestVersion.Id);
			if (result.IsSuccess)
			{
				string filePath = result.GetValue<string>();
				AppVersion? appVersion = await _context.AppVersions.FirstOrDefaultAsync(v => v.VersionId == latestVersion.Id);
				if (appVersion == null)
				{
					appVersion = new()
					{
						VersionId = latestVersion.Id,
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
				}
				await _context.SaveChangesAsync();
				return ActionResult.Success(appVersion);
			}
			return ActionResult.Fail();
		}

		private string GetUpdaterAppExePath()
		{
			string currentDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			return Path.Combine(currentDir, "Updater", "Quoter.Update.exe");
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
