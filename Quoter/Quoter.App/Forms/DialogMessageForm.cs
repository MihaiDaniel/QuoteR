using Quoter.App.Models;
using Quoter.App.Services.FormAnimation;
using Quoter.App.Services;
using Quoter.App.Helpers;
using Quoter.App.Services.Forms;

namespace Quoter.App.Forms
{
    public partial class DialogMessageForm : Form, IDialogReturnable
	{
		private readonly IFormsManager _formsManager;

		public string StringResult { get; private set; }

		public DialogMessageForm(IFormsManager formsManager,
							   IFormPositioningService formPositioningService,
							   IStringResources stringResources,
							   DialogModel dialogModel)
		{
			InitializeComponent();
			formPositioningService.RegisterFormDragableByControl(this, pnlTitle);

			pnlTitle.BackColor = dialogModel.TitleColor;
			lblTopBar.Text = dialogModel.Title;
			txtMessage.Text = dialogModel.Message;
			btnOk.Text = stringResources["OK"];

			_formsManager = formsManager;
		}


		private void btnOk_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			_formsManager.Close(this);
		}
	}
}
