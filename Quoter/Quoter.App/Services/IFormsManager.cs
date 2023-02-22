using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.App.Services
{
	public interface IFormsManager
	{
		void Show<TForm>() where TForm : Form;

		void ShowDialog<TForm>(params object[] arrParameters) where TForm : Form;

		void Close(Form form);

	}
}
