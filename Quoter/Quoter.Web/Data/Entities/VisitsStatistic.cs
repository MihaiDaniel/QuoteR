using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quoter.Web.Data.Entities
{
	public class VisitsStatistic
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public DateTime Date { get; set; }

		public string Url { get; set; }

		public int VisitsCount { get; set; }

		public int UniqueVisitorsCount { get; set; }

	}
}
