using Quoter.App.Controls;
using Quoter.App.Helpers;
using Quoter.App.Models;
using Quoter.App.Services;
using Quoter.App.Services.FormAnimation;
using Quoter.App.Services.Forms;

namespace Quoter.App.Forms
{
	public partial class DialogExportFinishedForm : Form, IDialogResult
	{
		private readonly IFormsManager _formsManager;
		private readonly IStringResources _stringResources;

		public string StringResult { get; private set; } = string.Empty;


		public DialogExportFinishedForm(IFormsManager formsManager,
							   IFormPositioningService formPositioningService,
							   IStringResources stringResources,
							   IThemeService themeService,
							   DialogOptions dialogOptions)
		{
			InitializeComponent();
			DropShadow.ApplyShadows(this);
			formPositioningService.RegisterFormDragableByControl(this, pnlTitle);

			_formsManager = formsManager;
			_stringResources = stringResources;

			pnlTitle.BackColor = themeService.GetCurrentTheme().TitleColor;

			lblTitle.Text = dialogOptions.Title;
			txtMessage.Text = dialogOptions.Message;

			btnOpenExplorer.Text = _stringResources["OpenFolder"];
			btnOk.Text = _stringResources["OK"];
			this.Text = _stringResources["Quoter"];

			StringResult = string.Empty;
			DialogResult = DialogResult.None;
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

		private void btnOpenExplorer_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			_formsManager.Close(this);
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			_formsManager.Close(this);
		}

		private void txtInput_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				btnOk_Click(sender, e);
			}
			else if (e.KeyCode == Keys.Escape)
			{
				btnOk_Click(sender, e);
			}
		}

	}
}
