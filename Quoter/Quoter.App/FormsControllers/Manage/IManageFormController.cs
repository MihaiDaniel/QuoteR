using Quoter.App.Forms.Manage;

namespace Quoter.App.FormsControllers.Manage
{
    public interface IManageFormController : IFormController<IManageForm>
	{
		string Version { get; set; }
	}
}
