using Quoter.Framework.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.Framework.Models
{
	public class MessageModel
	{
		public string Title { get; set; }

		public string Body { get; set; }

		public string Footer { get; set; }

		public EnumAnimation? OpenAnimation { get; set; }

		public EnumAnimation? CloseAnimation { get; set; }

		public MessageModel()
		{
			Title = string.Empty;
			Body = string.Empty;
			Footer = string.Empty;
		}
	}
}
