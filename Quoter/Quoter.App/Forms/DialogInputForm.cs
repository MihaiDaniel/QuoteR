using Quoter.App.Helpers;
using Quoter.App.Models;
using Quoter.App.Services;
using Quoter.App.Services.FormAnimation;

namespace Quoter.App.Forms
{
	public partial class DialogInputForm : Form, IDialogReturnable
	{
		private readonly IFormsManager _formsManager;

		public string StringResult { get; private set; }

		public DialogInputForm(IFormsManager formsManager,
							   IFormPositioningService formPositioningService,
							   IStringResources stringResources,
							   DialogModel dialogModel)
		{
			InitializeComponent();
			formPositioningService.RegisterFormDragableByControl(this, pnlTitle);

			_formsManager = formsManager;

			pnlTitle.BackColor = dialogModel.TitleColor;
			lblTitle.Text = dialogModel.Title;
			txtMessage.Text = dialogModel.Message;
			btnCancel.Text = stringResources["Cancel"];
			btnOk.Text = stringResources["OK"];

			StringResult = string.Empty;
			DialogResult = DialogResult.None;
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			StringResult = txtInput.Text;
			_formsManager.Close(this);
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			_formsManager.Close(this);
		}
	}
}
