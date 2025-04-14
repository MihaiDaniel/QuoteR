using Quoter.App.Forms.Common;
using Quoter.Framework.Enums;

namespace Quoter.App.Forms.Manage
{
	/// <summary>
	/// Interface for the <see cref="Forms.ManageForm"/>
	/// </summary>
	public interface IManageForm : IForm, IResizableForm
	{
		/// <summary>
		/// Set the current selected tab
		/// </summary>
		Task SetSelectedTab(EnumTab tab);

		/// <summary>
		/// Set a message on the form and a loading spinner indicating background tasks
		/// </summary>
		void SetBackgroundTask(bool inProgress, string message);

		/// <summary>
		/// Set a status on the form
		/// </summary>
		Task SetStatus(string message, Color color);
	}
}
