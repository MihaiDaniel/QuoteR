using Quoter.Framework.Services;

namespace Quoter.App.Services
{
	public class AppConfiguration : IAppConfiguration
	{
		public string ConnectionString => Properties.Settings.Default["ConnectionString"] as string;

		public string ApplicationKey => Properties.Settings.Default["ApplicationKey"] as string;

		public string WebApiUrl => Properties.Settings.Default["WebApiUrl"] as string;
	}
}
