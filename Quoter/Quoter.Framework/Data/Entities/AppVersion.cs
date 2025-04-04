using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Quoter.Framework.Data.Entities
{
	public class AppVersion
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		/// <summary>
		/// This is the application version id retrieved from the server
		/// </summary>
		public Guid VersionId { get; set; }

		public string Version { get; set; }

		public string FilePath { get; set; }

		public bool IsApplied { get; set; }
	}
}
