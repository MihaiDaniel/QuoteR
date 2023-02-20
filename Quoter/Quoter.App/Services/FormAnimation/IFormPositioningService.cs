using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.App.Services.FormAnimation
{
	public interface IFormPositioningService
	{
		void RegisterFormDragableByControl(Form form, Control control);
	}
}
