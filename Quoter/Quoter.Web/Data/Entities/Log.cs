using System.ComponentModel.DataAnnotations.Schema;

namespace Quoter.Web.Data.Entities
{
	public class Log
	{
		[Column("id")]
		public int LogId { get; set; }

		public DateTime Timestamp { get; set; }

		public string Level { get; set; }

		public string Exception { get; set; }

		public string RenderedMessage { get; set; }

		public string Properties { get; set; }
	}
}
