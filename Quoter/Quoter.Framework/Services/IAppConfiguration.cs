namespace Quoter.Framework.Services
{
	public interface IAppConfiguration
	{
		public string ConnectionString { get; }
		public string ApplicationKey { get; }
		public string WebApiUrl { get; }
	}
}
