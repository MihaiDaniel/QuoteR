namespace Quoter.Web.Models.Configuration
{
	/// <summary>
	/// Configuration section from appsettings.json containing data about users
	/// </summary>
	public class UsersConfiguration
	{
		/// <summary>
		/// Key of the json section
		/// </summary>
		public const string JsonKey = "UsersConfiguration";

		/// <summary>
		/// Default user that will be created configuration. This user will be created in the <see cref="WebApplicationExtensions"/>
		/// </summary>
		public DefaultUserConfiguration DefaultUser { get; set; }
	}
}
