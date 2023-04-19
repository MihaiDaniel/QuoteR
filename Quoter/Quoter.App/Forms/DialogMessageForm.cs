using Quoter.App.Models;
using Quoter.App.Services.FormAnimation;
using Quoter.App.Services;
using Quoter.App.Helpers;
using Quoter.App.Services.Forms;
using Quoter.Framework.Enums;
using Quoter.App.Controls;

namespace Quoter.App.Forms
{
	/// <summary>
	/// Dialog form to show errors or simple choices. Expects a <see cref="DialogMessageFormOptions"/>
	/// </summary>
	public partial class DialogMessageForm : Form, IDialogReturnable
	{
		private readonly IFormsManager _formsManager;

		public string StringResult { get; private set; }

		public DialogMessageForm(IFormsManager formsManager,
							   IFormPositioningService formPositioningService,
							   IStringResources stringResources,
							   IThemeService themeService,
							   DialogMessageFormOptions options)
		{
			InitializeComponent();
			DropShadow.ApplyShadows(this);
			_formsManager = formsManager;

			formPositioningService.RegisterFormDragableByControl(this, pnlTitle);

			// if default color get color from theme instead, else show the color set in dialogModal
			pnlTitle.BackColor = options.TitleColor != Const.ColorDefault ? options.TitleColor : themeService.GetCurrentTheme().TitleColor;
			lblTopBar.Text = options.Title;
			txtMessage.Text = options.Message;
			txtMessage.TabStop = false; // Stop text from being selected
			btnOk.Text = stringResources["OK"];
			btnCancel.Text = stringResources["Cancel"];
			this.Text = stringResources["Quoter"];

			if (options.MessageBoxButtons == EnumDialogButtons.Ok)
			{
				btnOk.Location = new Point(btnCancel.Location.X, btnCancel.Location.Y);
				btnCancel.Visible = false;
				btnCancel.Enabled = false;
			}
			StringResult = string.Empty;
		}

		#region Show on top

		private const int WS_EX_TOPMOST = 0x00000008;
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.ExStyle |= WS_EX_TOPMOST;
				return createParams;
			}
		}

		#endregion Show on top

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
