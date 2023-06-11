namespace Quoter.Shared.Models
{
	public class LatestVersionInfoGetResponse
	{
		public Guid Id { get; set; }
		public string Version { get; set; }

		public LatestVersionInfoGetResponse(Guid id, string version)
		{
			Id = id;
			Version = version;
		}
	}
}
