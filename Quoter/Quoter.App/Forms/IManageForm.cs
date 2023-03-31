using Quoter.Framework.Enums;

namespace Quoter.App.Forms
{
	public interface IManageForm : IResizableForm
	{
		Task SetSelectedTab(EnumTab tab);

		void SetBackgroundTask(bool inProgress, string message);
	}
}
