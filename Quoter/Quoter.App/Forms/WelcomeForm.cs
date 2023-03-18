using Quoter.App.FormsControllers.Welcome;
using Quoter.App.Helpers;
using Quoter.App.Models;
using Quoter.App.Services;
using Quoter.App.Services.FormAnimation;
using Quoter.App.Services.Forms;
using Quoter.Framework.Enums;
using Quoter.Framework.Models.ImportExport;

namespace Quoter.App.Forms
{
	public partial class WelcomeForm : Form, IWelcomeForm
	{
		private readonly IWelcomeFormController _controller;
		private readonly IFormsManager _formsManager;
		private readonly IStringResources _stringResources;
		private readonly IFormAnimationService _formAnimationService;
		private readonly IFormPositioningService _positioningService;

		public WelcomeForm(IWelcomeFormController controller,
							IFormsManager formsManager,
							IStringResources stringResources,
							IFormAnimationService formAnimationService,
							IFormPositioningService positioningService)
		{
			InitializeComponent();
			_controller = controller;
			_formsManager = formsManager;
			_stringResources = stringResources;
			_formAnimationService = formAnimationService;
			_positioningService = positioningService;

			_controller.RegisterForm(this);

			// Hide the tab controls by making the buttons really small
			tabControl.ItemSize = new Size(0, 1);
			tabControl.SelectTab(0);

			// stop the text from being selected
			txtTab1WelcomeMsg.TabStop = false;
			txtTab2Msg.TabStop = false;
			txtTab3Extra.TabStop = false;
			txtTab3NotificationInterval.TabStop = false;
			txtTab4Msg.TabStop = false;
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			DialogModel dialogModel = new DialogModel()
			{
				Title = "Are you sure?",
				Message = "Are you sure you waont to exit?",
				TitleColor = Const.ColorWarn,
				MessageBoxButtons = EnumDialogButtons.OkCancel
			};
			IDialogReturnable result = _formsManager.ShowDialog<DialogMessageForm>(dialogModel);
			if (result.DialogResult == DialogResult.OK)
			{
				Application.Exit();
			}
		}

		private async void WelcomeForm_Load(object sender, EventArgs e)
		{
			SetSelectedLanguage(EnumLanguage.English); // Normally is default

			await _controller.EventFormLoadedAsync();
			await _formAnimationService.AnimateAsync(this, EnumAnimation.FadeIn);
			_positioningService.RegisterFormDragableByControl(this, pnlTitle);
		}

		private void btnTab1Next_Click(object sender, EventArgs e)
		{
			//tabControl.SelectTab(1);
			_controller.SetSelectedTab(EnumWelcomeTab.SelectCollections);
		}

		private void btnTab2Back_Click(object sender, EventArgs e)
		{
			//tabControl.SelectTab(0);
			_controller.SetSelectedTab(EnumWelcomeTab.SetLanguage);
		}

		private void btnTab2Next_Click(object sender, EventArgs e)
		{
			//tabControl.SelectTab(2);
			_controller.SetSelectedTab(EnumWelcomeTab.SetNotificationSettings);
		}

		private void btnTab3Back_Click(object sender, EventArgs e)
		{
			//tabControl.SelectTab(1);
			_controller.SetSelectedTab(EnumWelcomeTab.SelectCollections);
		}

		private void btnTab3Next_Click(object sender, EventArgs e)
		{
			//tabControl.SelectTab(3);
			_controller.SetSelectedTab(EnumWelcomeTab.Finish);
		}

		private void btnTab4Finish_Click(object sender, EventArgs e)
		{
			_controller.FinishSetup();
			_formsManager.Close(this);
		}

		private void btnLanguageEn_Click(object sender, EventArgs e)
		{
			_controller.SetLanguage(EnumLanguage.English);
		}

		private void btnLanguageRo_Click(object sender, EventArgs e)
		{
			_controller.SetLanguage(EnumLanguage.Romanian);
		}

		private void btnLanguageFr_Click(object sender, EventArgs e)
		{
			_controller.SetLanguage(EnumLanguage.French);
		}


		private void btnTab3Often_Click(object sender, EventArgs e)
		{
			btnTab3Rare.FlatAppearance.BorderColor = Color.LightGray;
			btnTab3Often.FlatAppearance.BorderColor = Color.Black;
			btnTab3Normal.FlatAppearance.BorderColor = Color.LightGray;
			_controller.SetNotificationsInterval(15);
		}

		private void btnTab3Normal_Click(object sender, EventArgs e)
		{
			btnTab3Rare.FlatAppearance.BorderColor = Color.LightGray;
			btnTab3Often.FlatAppearance.BorderColor = Color.LightGray;
			btnTab3Normal.FlatAppearance.BorderColor = Color.Black;
			_controller.SetNotificationsInterval(30);
		}

		private void btnTab3Rare_Click(object sender, EventArgs e)
		{
			btnTab3Rare.FlatAppearance.BorderColor = Color.Black;
			btnTab3Often.FlatAppearance.BorderColor = Color.LightGray;
			btnTab3Normal.FlatAppearance.BorderColor = Color.LightGray;
			_controller.SetNotificationsInterval(60);
		}

		void IWelcomeForm.SetImportableCollections(List<CollectionFileModel> collectionFiles)
		{
			clbTab2ImportCollections.Items.Clear();
			clbTab2ImportCollections.DisplayMember = nameof(CollectionFileModel.Name);
			clbTab2ImportCollections.ValueMember = nameof(CollectionFileModel.Name);

			foreach (CollectionFileModel collectionFile in collectionFiles)
			{
				clbTab2ImportCollections.Items.Add(collectionFile);
			}
		}

		List<CollectionFileModel> IWelcomeForm.GetSelectedCollections()
		{
			List<CollectionFileModel> selectedCollections = new List<CollectionFileModel>();
			foreach (object? item in clbTab2ImportCollections.CheckedItems)
			{
				selectedCollections.Add((CollectionFileModel)item);
			}
			return selectedCollections;
		}

		void IWelcomeForm.SetTab(EnumWelcomeTab tab)
		{
			switch (tab)
			{
				case EnumWelcomeTab.SetLanguage:
					tabControl.SelectTab(0);
					break;
				case EnumWelcomeTab.SelectCollections:
					tabControl.SelectTab(1);
					break;
				case EnumWelcomeTab.SetNotificationSettings:
					tabControl.SelectTab(2);
					break;
				case EnumWelcomeTab.Finish:
					tabControl.SelectTab(3);
					break;
			}
		}

		void IWelcomeForm.SetSelectedLanguage(EnumLanguage language)
		{
			SetSelectedLanguage(language);
		}

		void SetSelectedLanguage(EnumLanguage language)
		{
			if (language == EnumLanguage.English)
			{
				btnLanguageEn.FlatAppearance.BorderColor = Color.Black;
				btnLanguageRo.FlatAppearance.BorderColor = Const.AppColor.ColorWindow;
				btnLanguageFr.FlatAppearance.BorderColor = Const.AppColor.ColorWindow;
			}
			else if (language == EnumLanguage.Romanian)
			{
				btnLanguageEn.FlatAppearance.BorderColor = Const.AppColor.ColorWindow;
				btnLanguageRo.FlatAppearance.BorderColor = Color.Black;
				btnLanguageFr.FlatAppearance.BorderColor = Const.AppColor.ColorWindow;
			}
			else if (language == EnumLanguage.French)
			{
				btnLanguageEn.FlatAppearance.BorderColor = Const.AppColor.ColorWindow;
				btnLanguageRo.FlatAppearance.BorderColor = Const.AppColor.ColorWindow;
				btnLanguageFr.FlatAppearance.BorderColor = Color.Black;
			}
			btnLanguageEn.Invalidate();
			btnLanguageRo.Invalidate();
			btnLanguageFr.Invalidate();
		}
	}
}
