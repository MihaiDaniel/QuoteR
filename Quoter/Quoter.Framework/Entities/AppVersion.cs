using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Quoter.Framework.Entities
{
	public class AppVersion
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public Guid VersionId { get; set; }

		public string Version { get; set; }

		public string FilePath { get; set; }

		public bool IsApplied { get; set; }
	}
}
