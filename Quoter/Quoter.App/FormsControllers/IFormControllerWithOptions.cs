using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.App.FormsControllers
{
	public interface IFormControllerWithOptions<in TForm, TOptions> : IFormController<TForm>
	{
		 void RegisterOptions(TOptions options);
	}
}
