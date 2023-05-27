namespace Quoter.Web.Models
{
	public class VersionFile
	{
		public string FileName { get; set; }
		public string Version { get; set; }
		public byte[] Content { get; set; }
	}
}
