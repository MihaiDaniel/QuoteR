using Quoter.Framework.Enums;

namespace Quoter.App.Forms.Manage
{
	/// <summary>
	/// Handles the edit quotes form
	/// </summary>
	/// <remarks>
	/// This is actually a tab in the <see cref="Forms.ManageForm"/>
	/// </remarks>
	public interface IEditQuotesForm : IManageForm
	{
		void SetBooksControlsState(EnumCrudStates state);

		void SetChaptersControlsState(EnumCrudStates state);
	}
}
