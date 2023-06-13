﻿using Quoter.Shared.Models;

namespace Quoter.Framework.Services.Api
{
	public interface IUpdateService
	{
		/// <summary>
		/// Verifies if a new version is available by querying the web server and comparing the latest version available
		/// with the current version
		/// </summary>
		Task<bool> VerifyIfNewVersionAvailable();

		/// <summary>
		/// Verifies if in the local db we have a version with the same number as the app exe version. 
		/// If yes it marks the version as IsApplied=true.
		/// </summary>
		Task<ActionResult> VerifyIfUpdateApplied();

		/// <summary>
		/// Verifies if a new version is available by querying the web server and comparing the latest version available
		/// with the current version then attempts to download the latest version. Finally it starts the Quoter.Update.exe process
		/// that will handle overwriting the current version files. The Quoter.Update.exe would also handle restarting the app.
		/// </summary>
		Task TryUpdate(bool isSilent);
	}
}
