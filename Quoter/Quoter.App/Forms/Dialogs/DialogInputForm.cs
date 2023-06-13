using Quoter.App.Controls;
using Quoter.App.Helpers;
using Quoter.App.Helpers.Extensions;
using Quoter.App.Models;
using Quoter.App.Services;
using Quoter.App.Services.FormAnimation;
using Quoter.App.Services.Forms;
using System.Text.RegularExpressions;

namespace Quoter.App.Forms
{
	public partial class DialogInputForm : Form, IDialogReturnable
	{
		private readonly IFormsManager _formsManager;
		private readonly IStringResources _stringResources;
		private readonly Regex _regexInput;

		public string StringResult { get; private set; }

		private int MaxLength;

		public DialogInputForm(IFormsManager formsManager,
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

			// Allow characters valid for file names a-zA-Z0-9
			_regexInput = new Regex(@"^[\w\-. ]+$");


			switch (dialogOptions.DialogTheme)
			{
				case Enums.DialogOptionsTheme.Default:
					pnlTitle.BackColor = themeService.GetCurrentTheme().TitleColor;
					break;
				case Enums.DialogOptionsTheme.Warning:
					pnlTitle.BackColor = Constants.Colors.Warn;
					break;
				case Enums.DialogOptionsTheme.Error:
					pnlTitle.BackColor = Constants.Colors.Error;
					break;
			}
			
			lblTitle.Text = dialogOptions.Title;
			txtMessage.Text = dialogOptions.Message;
			MaxLength = dialogOptions.ValueMaxLength;
			txtInput.Text = dialogOptions.Value;

			btnCancel.Text = _stringResources["Cancel"];
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

		private void btnOk_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(txtInput.Text))
			{
				txtInput.SetValidationError(_stringResources["ErrPleaseInputValue"]);
			}
			else
			{
				DialogResult = DialogResult.OK;
				StringResult = txtInput.Text;
				_formsManager.Close(this);
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			_formsManager.Close(this);
		}

		private void txtInput_TextChanged(object sender, EventArgs e)
		{
			if (txtInput.Text.Length > MaxLength)
			{
				txtInput.Text = txtInput.Text.Substring(0, MaxLength);
				txtInput.SetValidationError(_stringResources["ErrTextTooLong"]);
				txtInput.SelectionStart = txtInput.Text.Length;
				txtInput.SelectionLength = 0;
			}
			else if (txtInput.Text.Length > 0 && !_regexInput.IsMatch(txtInput.Text))
			{
				txtInput.Text = txtInput.Text.Substring(0, txtInput.Text.Length - 1);
				txtInput.SetValidationError(_stringResources["ErrTextInvalidChars"]);
				txtInput.SelectionStart = txtInput.Text.Length;
				txtInput.SelectionLength = 0;
			}
			else
			{
				txtInput.SetValidationError(string.Empty);
			}
		}

		private void txtInput_KeyDown(object sender, KeyEventArgs e)
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
	}
}
