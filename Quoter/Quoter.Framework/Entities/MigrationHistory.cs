using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.Framework.Entities
{
	public class MigrationHistory
	{
		public string MigrationId { get; set; }

		public string ProductVersion { get; set; }
	}
}
