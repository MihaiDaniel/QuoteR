using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.App.FormsControllers
{
	public interface IFormController<in IForm>
	{
		/// <summary>
		/// Mandatory step before using other methods.
		/// Due to DI we would have a circular dependency between the 
		/// controller and the form, so we use this method to avoid this.
		/// </summary>
		void RegisterForm(IForm form);
	}
}
