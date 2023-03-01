using Quoter.Framework.Entities;
using Quoter.Framework.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.App.Forms
{
	public interface IEditQuotesForm : IForm
	{
		void SetBooksControlsState(EnumCrudStates state);

		void SetChaptersControlsState(EnumCrudStates state);
	}
}
