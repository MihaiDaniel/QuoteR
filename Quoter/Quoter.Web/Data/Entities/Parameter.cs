using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quoter.Web.Data.Entities
{
	public class Parameter
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public long Id { get; set; }

		public string Name { get; set; }

		public string Value { get; set; }
	}

	public static class ParameterName
	{
		public const string DownloadableType = "DownloadableType";
	}
}
