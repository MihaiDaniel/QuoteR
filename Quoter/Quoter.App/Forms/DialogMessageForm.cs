using Quoter.App.Models;
using Quoter.App.Services.FormAnimation;
using Quoter.App.Services;
using Quoter.App.Helpers;
using Quoter.App.Services.Forms;
using Quoter.Framework.Enums;

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
			_formsManager = formsManager;

			formPositioningService.RegisterFormDragableByControl(this, pnlTitle);

			pnlTitle.BackColor = dialogModel.TitleColor;
			lblTopBar.Text = dialogModel.Title;
			txtMessage.Text = dialogModel.Message;
			btnOk.Text = stringResources["OK"];
			btnCancel.Text = stringResources["Cancel"];

			if (dialogModel.MessageBoxButtons == EnumDialogButtons.Ok)
			{
				btnOk.Location = new Point(btnCancel.Location.X, btnCancel.Location.Y);
				btnCancel.Visible = false;
				btnCancel.Enabled= false;
			}
			StringResult = string.Empty;
		}


		private void btnOk_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			_formsManager.Close(this);
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			_formsManager.Close(this);
		}
	}
}
