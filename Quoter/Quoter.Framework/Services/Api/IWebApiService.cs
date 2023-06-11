using Quoter.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.Framework.Services.Api
{
	public interface IWebApiService
	{
		/// <summary>
		/// Register the application and get back an registrationId as a Guid
		/// </summary>
		Task<Guid> RegisterAsync(string installId);

		Task<QuoterVersionInfo> GetLatestVersion();

		Task<bool> DownloadVersionAsync(Guid versionId);
	}
}
