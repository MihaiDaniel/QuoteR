using Quoter.App.Models;
using Quoter.App.Services.FormAnimation;
using Quoter.App.Services;
using Quoter.App.Helpers;
using Quoter.App.Services.Forms;
using Quoter.Framework.Enums;
using Quoter.App.Controls;
using Quoter.Framework.Services;

namespace Quoter.App.Forms
{
	/// <summary>
	/// Dialog form to show errors or simple choices. Expects a <see cref="Dialog_options"/>
	/// </summary>
	public partial class DialogMessageForm : Form, IDialogResult
	{
		private readonly IFormsManager _formsManager;
		private readonly IFormAnimationService _formAnimationService;
		private readonly IStringResources _stringResources;
		private readonly IThemeService _themeService;
		private readonly ISoundService _soundService;
		private readonly DialogOptions _options;

		public string StringResult { get; private set; } = string.Empty;

		public DialogMessageForm(IFormsManager formsManager,
							   IFormPositioningService formPositioningService,
							   IFormAnimationService formAnimationService,
							   IStringResources stringResources,
							   IThemeService themeService,
							   ISoundService soundService,
							   DialogOptions options)
		{
			InitializeComponent();
			DropShadow.ApplyShadows(this);
			_formsManager = formsManager;
			_formAnimationService = formAnimationService;
			_stringResources = stringResources;
			_themeService = themeService;
			_soundService = soundService;
			_options = options;

			formPositioningService.RegisterFormDragableByControl(this, pnlTitle);

			InitializeDialog();
		}

		private void InitializeDialog()
		{
			// if default color get color from theme instead, else show the color set in dialogModal
			switch (_options.DialogTheme)
			{
				case Enums.DialogOptionsTheme.Default:
					pnlTitle.BackColor = _themeService.GetCurrentTheme().TitleColor;
					break;
				case Enums.DialogOptionsTheme.Warning:
					pnlTitle.BackColor = Constants.Colors.Warn;
					break;
				case Enums.DialogOptionsTheme.Error:
					pnlTitle.BackColor = Constants.Colors.Error;
					break;
			}

			lblTopBar.Text = _options.Title;
			txtMessage.Text = _options.Message;
			txtMessage.TabStop = false; // Stop text from being selected

			this.Text = _stringResources["Quoter"];

			switch (_options.MessageBoxButtons)
			{
				case EnumDialogButtons.Ok:
					btnOk.Location = new Point(btnCancel.Location.X, btnCancel.Location.Y); // move to the right in place of the cancel button
					btnOk.Text = _stringResources["OK"];
					btnCancel.Visible = false;
					btnCancel.Enabled = false;
					break;
				case EnumDialogButtons.OkCancel:
					btnOk.Text = _stringResources["OK"];
					btnCancel.Text = _stringResources["Cancel"];
					break;
				case EnumDialogButtons.YesNo:
					btnOk.Text = _stringResources["Yes"];
					btnCancel.Text = _stringResources["No"];
					break;
				case EnumDialogButtons.YesLater:
					btnOk.Text = _stringResources["Yes"];
					btnCancel.Text = _stringResources["Later"];
					break;
			}
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

		private async void DialogMessageForm_Load(object sender, EventArgs e)
		{
			switch (_options.DialogSound)
			{
				case Enums.DialogOptionsSound.Default:
					_soundService.PlayNotificationSound();
					break;
				case Enums.DialogOptionsSound.Warning:
					_soundService.PlayWarningSound();
					break;
			}
			if (_options.OpenAnimation != EnumAnimation.None)
			{
				await _formAnimationService.AnimateAsync(this, _options.OpenAnimation);
			}
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
