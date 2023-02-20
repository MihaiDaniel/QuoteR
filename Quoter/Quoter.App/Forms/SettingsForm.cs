using Quoter.App.Forms;
using Quoter.App.Services.FormAnimation;
using Quoter.App.Views;
using Quoter.Framework.Models;
using System.Resources;

namespace Quoter.App
{
	public partial class SettingsForm : Form
	{
		private readonly IFormAnimationService _formAnimationService;
		private readonly IFormPositioningService _positioningService;
		private readonly ResourceManager _resourceManager;

		public SettingsForm()
		{
			InitializeComponent();
			_formAnimationService = new FormAnimationsService();
			_positioningService = new FormPositioningService();
			_resourceManager = new ResourceManager("Quoter.App.Resources.Strings", typeof(SettingsForm).Assembly);
			lblTopBar.Text = _resourceManager.GetString("settings");
		}


		private void button1_Click_1(object sender, EventArgs e)
		{
			MessageModel messageModel = new()
			{
				Title = "Matei 3:15",
				Body = "Acesta este un text lung care vorbeste despre un citat asa ca sa vedem ceva mai lung textul foarte bine!",
				Footer = "Acesta este un footer"
			};
			new MessageForm(messageModel).Show();
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
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
					ShowErrorMessageBox(_resourceManager.GetString("error"), _resourceManager.GetString("folderNotExists"));
				}
				
			}
		}

		private void ShowErrorMessageBox(string caption, string message)
		{
			MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private void button2_Click(object sender, EventArgs e)
		{
			new WelcomeForm().Show();
		}
	}
}