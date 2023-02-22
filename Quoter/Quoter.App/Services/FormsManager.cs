using Quoter.Framework.Services.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quoter.App.Services
{
	public class FormsManager : IFormsManager
	{
		private readonly Dictionary<Type, Form> _dicOpenForms;

		private readonly DependencyInjectionContainer _diContainer;

		public FormsManager(DependencyInjectionContainer diContainer)
		{
			_diContainer = diContainer;
			_dicOpenForms = new Dictionary<Type, Form>();
		}

		public void Show<TForm>() where TForm : Form
		{
			Type formType = typeof(TForm);
			_dicOpenForms.TryGetValue(formType, out Form? form);
			if(form is null)
			{
				form = _diContainer.GetService<TForm>();
				_dicOpenForms.Add(formType, form);
				ShowForm(form, false);
			}
			else
			{
				form.TopMost= true;
			}
		}

		public void ShowDialog<TForm>(params object[] arrParameters) where TForm : Form
		{
			Form form = _diContainer.GetService<TForm>(arrParameters);
			ShowForm(form, true);
		}

		public void Close(Form form)
		{
			_dicOpenForms.Remove(form.GetType());
			if (form.Modal)
			{
				form.Close();
				form.Dispose();
			}
			else
			{
				form.Close();
			}
		}

		private void ShowForm(Form form, bool isModal)
		{
			if(isModal)
			{
				form.ShowDialog();
			}
			else
			{
				form.Show();
			}
		}

	}
}
