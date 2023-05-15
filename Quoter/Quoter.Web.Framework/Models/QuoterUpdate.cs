using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.Web.Framework.Models
{
	public class QuoterUpdate
	{
		public QuoterUpdate()
		{
			CreationDate = DateTime.UtcNow;
		}

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public string Version { get; set; }

		public string? Description { get; set; }

		/// <summary>
		/// Path on disk of the update file
		/// </summary>
		public string Path { get; set; }

		public DateTime CreationDate { get; set; }
	}
}
