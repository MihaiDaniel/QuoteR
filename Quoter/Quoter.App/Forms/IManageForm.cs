using Quoter.Framework.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.App.Forms
{
	public interface IManageForm
	{
		Task SetSelectedTab(EnumTab tab);
	}
}
