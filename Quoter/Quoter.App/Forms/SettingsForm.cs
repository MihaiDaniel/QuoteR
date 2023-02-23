using Quoter.App.Forms;
using Quoter.App.Helpers;
using Quoter.App.Models;
using Quoter.App.Services;
using Quoter.App.Services.FormAnimation;
using Quoter.App.Views;
using Quoter.Framework.Models;
using Quoter.Framework.Services.DependencyInjection;
using System.ComponentModel;
using System.Resources;

namespace Quoter.App
{
	public partial class SettingsForm : Form
	{
		private readonly IFormAnimationService _formAnimationService;
		private readonly IFormPositioningService _positioningService;
		private readonly IStringResources _stringResources;
		private readonly IFormsManager _formsManager;

		public SettingsForm(IFormAnimationService formAnimationService,
							IFormPositioningService formPositioningService,
							IStringResources stringResources,
							IFormsManager formsManager)
		{
			InitializeComponent();
			_formAnimationService = formAnimationService;
			_positioningService = formPositioningService;
			_stringResources = stringResources;
			_formsManager = formsManager;

			lblTopBar.Text = _stringResources["Settings"];
		}


		private void button1_Click_1(object sender, EventArgs e)
		{
			//MessageModel messageModel = new()
			//{
			//	Title = "Matei 3:15",
			//	Body = "Acesta este un text lung care vorbeste despre un citat asa ca sa vedem ceva mai lung textul foarte bine!",
			//	Footer = "Acesta este un footer"
			//};
			DialogModel dialogModel = new()
			{
				TitleColor= Color.Red,
				Message = "Acesta este un mesaj",
				Title = "Acesta este un titlu"
			};
			IDialogReturnable result = _formsManager.ShowDialog<DialogInputForm>(dialogModel);
			if(result.DialogResult== DialogResult.OK)
			{
				lblTopBar.Text = result.StringResult;
			}
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			_formsManager.Close(this);
		}

		private void SettingsForm_Load(object sender, EventArgs e)
		{
			_positioningService.RegisterFormDragableByControl(this, pnlTitle);
			txtFolder.Text = Properties.Settings.Default["ScannedFolder"].ToString();
		}

		private void btnBrowse_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog folderDialog = new FolderBrowserDialog();
			folderDialog.ShowNewFolderButton = true;

			DialogResult result = folderDialog.ShowDialog();
			if (result == DialogResult.OK)
			{
				txtFolder.Text = folderDialog.SelectedPath;
				if (Directory.Exists(txtFolder.Text))
				{
					Properties.Settings.Default["ScannedFolder"] = folderDialog.SelectedPath;
					Properties.Settings.Default.Save();
				}
				else
				{
					ShowErrorMessageBox(_stringResources["error"], _stringResources["folderNotExists"]);
				}
				
			}
		}

		private void ShowErrorMessageBox(string caption, string message)
		{
			MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private void button2_Click(object sender, EventArgs e)
		{
			_formsManager.Show<WelcomeForm>();
		}
	}
}