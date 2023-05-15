using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.Web.Framework.Models
{
	public class User
	{
		public User()
		{
			Id = new Guid();
		}

		public Guid Id { get; set; }

		public string UserName { get; set; }

		public string Password { get; set; }


	}
}
