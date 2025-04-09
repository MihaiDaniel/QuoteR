namespace Quoter.Web.Data.Entities
{
	/// <summary>
	/// Application keys are used by the desktop application to authorize calls to the API
	/// </summary>
	public class AppKey
	{
		public int AppKeyId { get; set; }

		public string Key { get; set; }

		public DateTime CreatedDate { get; set; }

		public AppKey()
		{
			CreatedDate = DateTime.UtcNow;
		}
	}
}
