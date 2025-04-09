namespace Quoter.Shared.Models
{
	public class LatestVersionInfoGetResponse
	{
		public string Id { get; set; }
		public string Version { get; set; }

		public LatestVersionInfoGetResponse(string id, string version)
		{
			Id = id;
			Version = version;
		}
	}
}
