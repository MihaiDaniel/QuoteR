using Quoter.Framework.Enums;

namespace Quoter.App.Services.Forms
{
	public interface IMonitoredForm
	{
		EnumFormCloseState IsClosable();

		void Close();
	}
}
