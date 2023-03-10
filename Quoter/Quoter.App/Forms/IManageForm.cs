using Quoter.Framework.Enums;

namespace Quoter.App.Forms
{
	public interface IManageForm
	{
		Task SetSelectedTab(EnumTab tab);

		void SetBackgroundTask(bool inProgress, string message);
	}
}
