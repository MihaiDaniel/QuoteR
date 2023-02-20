using Quoter.App.Forms;
using Quoter.App.Services.FormAnimation;
using Quoter.App.Views;
using Quoter.Framework.Enums;
using Quoter.Framework.Models;
using System.Resources;

namespace Quoter.App.Forms
{
	public partial class WelcomeForm : Form
	{
		private readonly IFormAnimationService _formAnimationService;
		private readonly IFormPositioningService _positioningService;
		private readonly ResourceManager _resourceManager;

		public WelcomeForm()
		{
			InitializeComponent();
			_formAnimationService = new FormAnimationsService();
			_positioningService = new FormPositioningService();
			_resourceManager = new ResourceManager("Quoter.App.Resources.Strings", typeof(SettingsForm).Assembly);
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private async void WelcomeForm_Load(object sender, EventArgs e)
		{
			// Hide the tab controls by making the buttons really small
			tabControl.ItemSize = new Size(0,1); 
			tabControl.SelectTab(0);

			await _formAnimationService.AnimateAsync(this, EnumAnimation.FadeIn);
			_positioningService.RegisterFormDragableByControl(this, pnlTitle);
		}

		private void btnContinueToTab2_Click(object sender, EventArgs e)
		{
			tabControl.SelectTab(1);
		}


		private void btnContinueToTab3_Click(object sender, EventArgs e)
		{
			tabControl.SelectTab(2);
		}

		private void btnContinueToTab4_Click(object sender, EventArgs e)
		{
			tabControl.SelectTab(3);
		}

		private void btnFinishSetup_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void rbRare_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void rbRegular_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void rbOften_CheckedChanged(object sender, EventArgs e)
		{

		}
	
	}
}
