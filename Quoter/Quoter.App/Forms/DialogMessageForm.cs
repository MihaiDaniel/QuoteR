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
							   IThemeService themeService,
							   DialogModel dialogModel)
		{
			InitializeComponent();
			_formsManager = formsManager;

			formPositioningService.RegisterFormDragableByControl(this, pnlTitle);
			
			// if default color get color from theme instead, else show the color set in dialogModal
			pnlTitle.BackColor = dialogModel.TitleColor != Const.ColorDefault ? dialogModel.TitleColor : themeService.GetCurrentTheme().TitleColor;
			lblTopBar.Text = dialogModel.Title;
			txtMessage.Text = dialogModel.Message;
			txtMessage.TabStop = false; // Stop text from being selected
			btnOk.Text = stringResources["OK"];
			btnCancel.Text = stringResources["Cancel"];
			this.Text = stringResources["Quoter"];

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

		private void btnOk_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				btnOk_Click(sender, e);
			}
			else if (e.KeyCode == Keys.Escape)
			{
				btnCancel_Click(sender, e);
			}
		}

		private void btnCancel_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				btnCancel_Click(sender, e);
			}
		}
	}
}
