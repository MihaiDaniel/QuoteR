using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.Shared.Models
{
	public class RegisterPostRequestModel
	{
		[Required]
		public string InstallId { get; set; }
	}
}
