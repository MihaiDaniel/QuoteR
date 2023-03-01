using Quoter.App.Helpers;
using Quoter.App.Models;
using Quoter.App.Services;
using Quoter.App.Services.FormAnimation;
using Quoter.App.Services.Forms;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Quoter.App.Forms
{
	public partial class DialogInputForm : Form, IDialogReturnable
	{
		private const int MaxLength = 38;
		private readonly IFormsManager _formsManager;
		private readonly IStringResources _stringResources;
		private readonly Regex _regexInput;

		public string StringResult { get; private set; }

		public DialogInputForm(IFormsManager formsManager,
							   IFormPositioningService formPositioningService,
							   IStringResources stringResources,
							   IThemeService themeService,
							   DialogModel dialogModel)
		{
			InitializeComponent();
			formPositioningService.RegisterFormDragableByControl(this, pnlTitle);

			_formsManager = formsManager;
			_stringResources = stringResources;

			// Allow characters valid for file names a-zA-Z0-9
			_regexInput = new Regex(@"^[\w\-. ]+$");

			// if default color get color from theme instead, else show the color set in dialogModal
			pnlTitle.BackColor = dialogModel.TitleColor != Const.ColorDefault ? dialogModel.TitleColor : themeService.GetCurrentTheme().TitleColor;
			lblTitle.Text = dialogModel.Title;
			txtMessage.Text = dialogModel.Message;
			btnCancel.Text = _stringResources["Cancel"];
			btnOk.Text = _stringResources["OK"];
			txtInput.Text = dialogModel.Value;
			txtStatus.Text = string.Empty;

			StringResult = string.Empty;
			DialogResult = DialogResult.None;
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(txtInput.Text))
			{
				SetStatus(_stringResources["ErrPleaseInputValue"], Color.Red);
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
				SetStatus(_stringResources["ErrTextTooLong"], Color.Red);
			}
			else if (txtInput.Text.Length > 0 && !_regexInput.IsMatch(txtInput.Text))
			{
				txtInput.Text = txtInput.Text.Substring(0, txtInput.Text.Length - 1);
				SetStatus(_stringResources["ErrTextInvalidChars"], Color.Red);
				txtInput.SelectionStart = txtInput.Text.Length;
				txtInput.SelectionLength = 0;
			}
			else
			{
				SetStatus(string.Empty, Color.Red);
			}
		}

		private void SetStatus(string message, Color color)
		{
			txtStatus.Text = message;
			txtStatus.ForeColor = color;
		}
	}
}
