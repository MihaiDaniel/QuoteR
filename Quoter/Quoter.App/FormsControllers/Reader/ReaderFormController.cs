using Quoter.App.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.App.FormsControllers.Reader
{
	public class ReaderFormController : IReaderFormController, INotifyPropertyChanged
	{
		private string _quotes;
		public string Quotes
		{
			get => _quotes;
			set
			{
				if (_quotes != value)
				{
					_quotes = value;
					OnPropertyChanged();
				}
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public ReaderFormController()
		{

		}

		public Task EventFormClosingAsync()
		{
			throw new NotImplementedException();
		}

		public Task EventFormLoadedAsync()
		{
			throw new NotImplementedException();
		}

		public void RegisterForm(IReaderForm form)
		{
			throw new NotImplementedException();
		}

		public Task SelectionChanged(string selection)
		{
			throw new NotImplementedException();
		}

		public Task SetNextChapter()
		{
			throw new NotImplementedException();
		}

		public Task SetPreviousChapter()
		{
			throw new NotImplementedException();
		}
	}
}
