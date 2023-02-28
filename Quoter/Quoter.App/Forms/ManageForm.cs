using Quoter.App.FormsControllers;
using Quoter.App.Helpers;
using Quoter.App.Services;
using Quoter.App.Services.FormAnimation;
using Quoter.App.Services.Forms;
using Quoter.Framework.Entities;
using Quoter.Framework.Enums;

namespace Quoter.App.Forms
{
	public partial class ManageForm : Form, IEditQuotesForm, ISettingsForm
	{
		private readonly IFormsManager _formsManager;
		private readonly IFormAnimationService _formAnimationService;
		private readonly IStringResources _stringResources;
		private readonly IEditQuotesFormController _editQuotesController;
		private readonly ISettingsFormController _settingsController;
		private readonly IThemeService _themeService;

		public ManageForm(IFormsManager formsManager,
							IFormPositioningService formPositioningService,
							IFormAnimationService formAnimationService,
							IStringResources stringResources,
							IEditQuotesFormController editQuotesController,
							ISettingsFormController settingsController,
							IThemeService themeService)
		{
			InitializeComponent();
			formPositioningService.RegisterFormDragableByControl(this, pnlTitle);

			_formsManager = formsManager;
			_formAnimationService = formAnimationService;
			_stringResources = stringResources;
			_editQuotesController = editQuotesController;
			_settingsController = settingsController;
			_themeService = themeService;

			LocalizeControls();
			BindEditQuotesControls();
			BindSettingsControls();

			// Hide tabs
			tabControl.Appearance = TabAppearance.FlatButtons;
			tabControl.SizeMode = TabSizeMode.Fixed;
			tabControl.ItemSize = new Size(0, 1);
			tabControl.SelectTab(0);

			tabBasicSettings.Appearance = TabAppearance.FlatButtons;
			tabBasicSettings.SizeMode = TabSizeMode.Fixed;
			tabBasicSettings.ItemSize = new Size(0, 1);
			tabBasicSettings.SelectTab(0);

			SetTheme();
			SetStatus(string.Empty, Color.Black);
		}

		public void SetTheme()
		{
			Theme theme = _themeService.GetCurrentTheme();
			this.BackColor = theme.BodyColor;
			pnlTitle.BackColor = theme.TitleColor;
		}

		public void LocalizeControls()
		{
			// Navigation buttons for tabs
			btnTabPage1.Text = _stringResources["ModifyQuotes"];
			btnTabPage2.Text = _stringResources["ManageQuotes"];
			btnTabPage3.Text = _stringResources["Settings"];

			// Edit quotes tab
			lblTitle.Text = _stringResources["Manage"];
			tabPage1.Text = _stringResources["CreateEditQuotes"];

			gbCollections.Text = _stringResources["Collection"];
			btnAddCollection.Text = _stringResources["Add"];
			btnEditCollection.Text = _stringResources["Edit"];
			btnDeleteCollection.Text = _stringResources["Delete"];

			gbBooks.Text = _stringResources["Book"];
			btnAddBook.Text = _stringResources["Add"];
			btnEditBook.Text = _stringResources["Edit"];
			btnDeleteBook.Text = _stringResources["Delete"];

			gbChapters.Text = _stringResources["Chapters"];
			btnAddChapter.Text = _stringResources["Add"];
			btnEditChapter.Text = _stringResources["Edit"];
			btnDeleteChapter.Text = _stringResources["Delete"];
			btnSaveQuotes.Text = _stringResources["Save"];
			gbQuotes.Text = _stringResources["Quotes"];

			// Settings tab
			gbLanguageSettings.Text = _stringResources["Language"];
			lblShowCollByLanguage.Text = _stringResources["ShowCollectionsByLanguage"];
			btnShowCollBasedOnLanguageYes.Text = _stringResources["Yes"];
			btnShowCollBasedOnLanguageNo.Text = _stringResources["No"];
			gbQuotesSettings.Text = _stringResources["Quotes"];
			lblQuotesFrequency.Text = _stringResources["QuotesFrequency"];
			lblQuotesFrequencyTime.Text = _stringResources["Minutes"];
			gbThemeSettings.Text = _stringResources["Theme"];
			lblOpacity.Text = _stringResources["Opacity"];
			
		}

		private void BindEditQuotesControls()
		{
			_editQuotesController.RegisterForm(this);

			// Collections
			BindingSource bindingSourceCollections = new();
			bindingSourceCollections.DataSource = _editQuotesController.Collections;
			cbCollection.DataSource = bindingSourceCollections;
			cbCollection.DisplayMember = nameof(Collection.Name);
			cbCollection.ValueMember = nameof(Collection.Name);
			cbCollection.DataBindings.Add("SelectedItem", _editQuotesController, nameof(IEditQuotesFormController.SelectedCollection));

			// Books
			BindingSource bindingSourceBooks = new();
			bindingSourceBooks.DataSource = _editQuotesController.Books;
			cbBooks.DataSource = bindingSourceBooks;
			cbBooks.DisplayMember = nameof(Book.Name);
			cbBooks.ValueMember = nameof(Book.Name);
			cbBooks.DataBindings.Add("SelectedItem", _editQuotesController, nameof(IEditQuotesFormController.SelectedBook));

			// Chapters
			BindingSource bindingSourceChapters = new();
			bindingSourceChapters.DataSource = _editQuotesController.Chapters;
			lbChapters.DataSource = bindingSourceChapters;
			lbChapters.DisplayMember = nameof(Chapter.Name);
			lbChapters.ValueMember = nameof(Chapter.Name);
			lbChapters.DataBindings.Add("SelectedItem", _editQuotesController, nameof(IEditQuotesFormController.SelectedChapter));

			// Quotes
			rtbQuotes.DataBindings.Add("Text", _editQuotesController, "Quotes");
		}

		private void BindSettingsControls()
		{
			_settingsController.RegisterForm(this);

			txtQuotesInterval.DataBindings.Add("Text", _settingsController, nameof(ISettingsFormController.NotificationsIntervalMinutes));

			lblOpacityPercent.DataBindings.Add("Text", _settingsController, nameof(ISettingsFormController.OpacityValue));
		}

		private async void ManageQuotesForm_Load(object sender, EventArgs e)
		{
			await _formAnimationService.AnimateAsync(this, EnumAnimation.FadeIn);
		}

		void IForm.SetStatus(string message, Color color)
		{
			SetStatus(message, color);
		}

		public void SetSelectedLanguage(EnumLanguage language)
		{
			if (language == EnumLanguage.English)
			{
				btnLanguageEn.FlatAppearance.BorderColor = Color.Black;
				btnLanguageRo.FlatAppearance.BorderColor = Const.AppColor.ColorWindow;
			}
			else if (language == EnumLanguage.Romanian)
			{
				btnLanguageEn.FlatAppearance.BorderColor = Const.AppColor.ColorWindow;
				btnLanguageRo.FlatAppearance.BorderColor = Color.Black;
			}
			btnLanguageEn.Invalidate();
			btnLanguageRo.Invalidate();
		}

		public void SetSelectedCollectionByLanguage(bool isShowByLanguage)
		{
			if(isShowByLanguage)
			{
				btnShowCollBasedOnLanguageYes.FlatAppearance.BorderSize = 1;
				btnShowCollBasedOnLanguageNo.FlatAppearance.BorderSize = 0;
			}
			else
			{
				btnShowCollBasedOnLanguageYes.FlatAppearance.BorderSize = 0;
				btnShowCollBasedOnLanguageNo.FlatAppearance.BorderSize = 1;
			}
		}

		public void SetOpacitySlider(double opacity)
		{
			tbOpacity.Value = (int)(opacity * 10);
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			_formsManager.Close(this);
		}

		private void cbCollection_SelectedValueChanged(object sender, EventArgs e)
		{
			if (cbCollection.SelectedItem != null
				&& cbCollection.SelectedItem as Collection != _editQuotesController.SelectedCollection)
			{
				_editQuotesController.SelectedCollection = cbCollection.SelectedItem as Collection;
				// Scroll to the top
				rtbQuotes.SelectionStart = 0;
				rtbQuotes.ScrollToCaret();
			}
		}

		private void cbBooks_SelectedValueChanged(object sender, EventArgs e)
		{
			if (cbBooks.SelectedItem != null
				&& cbBooks.SelectedItem as Book != _editQuotesController.SelectedBook)
			{
				_editQuotesController.SelectedBook = cbBooks.SelectedItem as Book;
				// Scroll to the top
				rtbQuotes.SelectionStart = 0;
				rtbQuotes.ScrollToCaret();
			}
		}

		private void lbChapters_SelectedValueChanged(object sender, EventArgs e)
		{
			if (lbChapters.SelectedItem != null
				&& lbChapters.SelectedItem as Chapter != _editQuotesController.SelectedChapter)
			{
				_editQuotesController.SelectedChapter = lbChapters.SelectedItem as Chapter;
				// Scroll to the top
				rtbQuotes.SelectionStart = 0;
				rtbQuotes.ScrollToCaret();
			}
		}

		#region Button events Add, Edit, Delete Collections

		private void btnAddCollection_Click(object sender, EventArgs e)
		{
			_editQuotesController.AddCollection();
		}

		private void buttonEditCollection_Click(object sender, EventArgs e)
		{
			_editQuotesController.EditCollection();
		}

		private void btnDeleteCollection_Click(object sender, EventArgs e)
		{
			_editQuotesController.DeleteCollection();
		}

		#endregion Button events Add, Edit, Delete Collections

		#region Button events Add, Edit, Delete Books

		private void btnAddBook_Click(object sender, EventArgs e)
		{
			_editQuotesController.AddBook();
		}

		private void btnEditBook_Click(object sender, EventArgs e)
		{
			_editQuotesController.EditBook();
		}

		private void btnDeleteBook_Click(object sender, EventArgs e)
		{
			_editQuotesController.DeleteBook();
		}

		#endregion Button events Add, Edit, Delete Books

		#region Button events Add, Edit, Delete Chapters

		private void btnAddChapter_Click(object sender, EventArgs e)
		{
			_editQuotesController.AddChapter();
		}

		private void btnEditChapter_Click(object sender, EventArgs e)
		{
			_editQuotesController.EditChapter();
		}

		private void btnDeleteChapter_Click(object sender, EventArgs e)
		{
			_editQuotesController.DeleteChapter();
		}

		private void btnSaveQuotes_Click(object sender, EventArgs e)
		{
			_editQuotesController.AddQuotes();
		}

		#endregion  Button events Add, Edit, Delete Chapters

		private void btnTabPage1_Click(object sender, EventArgs e)
		{
			tabControl.SelectTab(tabPage1);
			btnTabPage1.FlatAppearance.BorderSize = 1;
			btnTabPage2.FlatAppearance.BorderSize = 0;
			btnTabPage3.FlatAppearance.BorderSize = 0;
		}

		private void btnTabPage2_Click(object sender, EventArgs e)
		{
			tabControl.SelectTab(tabPage2);
			btnTabPage1.FlatAppearance.BorderSize = 0;
			btnTabPage2.FlatAppearance.BorderSize = 1;
			btnTabPage3.FlatAppearance.BorderSize = 0;
		}

		private void btnTabPage3_Click(object sender, EventArgs e)
		{
			tabControl.SelectTab(tabPage3);
			btnTabPage1.FlatAppearance.BorderSize = 0;
			btnTabPage2.FlatAppearance.BorderSize = 0;
			btnTabPage3.FlatAppearance.BorderSize = 1;
		}

		private void SetStatus(string message, Color color)
		{
			txtStatus.Text = message;
			txtStatus.ForeColor = color;
		}

		private void btnLanguageEn_Click(object sender, EventArgs e)
		{
			_settingsController.SetLanguage(EnumLanguage.English);
		}

		private void btnLanguageRo_Click(object sender, EventArgs e)
		{
			_settingsController.SetLanguage(EnumLanguage.Romanian);
		}


		private void txtQuotesInterval_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!char.IsControl(e.KeyChar) 
				&& !char.IsDigit(e.KeyChar) 
				&& (e.KeyChar != '.'))
			{
				e.Handled = true;
			}
		}

		private void btnShowCollBasedOnLanguageYes_Click(object sender, EventArgs e)
		{
			btnShowCollBasedOnLanguageYes.FlatAppearance.BorderSize = 1;
			btnShowCollBasedOnLanguageNo.FlatAppearance.BorderSize = 0;
			_settingsController.SetShowCollectionsBasedOnLanguage(true);
		}

		private void btnShowCollBasedOnLanguageNo_Click(object sender, EventArgs e)
		{
			btnShowCollBasedOnLanguageYes.FlatAppearance.BorderSize = 0;
			btnShowCollBasedOnLanguageNo.FlatAppearance.BorderSize = 1;
			_settingsController.SetShowCollectionsBasedOnLanguage(false);
		}

		private void btnShowWelcomeMsgYes_Click(object sender, EventArgs e)
		{
			btnShowWelcomeMsgYes.FlatAppearance.BorderSize = 1;
			btnShowWelcomeMsgNo.FlatAppearance.BorderSize = 0;
			_settingsController.SetShowWelcomeMessage(true);
		}

		private void btnShowWelcomeMsgNo_Click(object sender, EventArgs e)
		{
			btnShowWelcomeMsgYes.FlatAppearance.BorderSize = 0;
			btnShowWelcomeMsgNo.FlatAppearance.BorderSize = 1;
			_settingsController.SetShowWelcomeMessage(false);
		}

		private void btnTheme1_Click(object sender, EventArgs e)
		{
			_settingsController.SetTheme(EnumTheme.SlateGray);
		}

		private void btnTheme2_Click(object sender, EventArgs e)
		{
			_settingsController.SetTheme(EnumTheme.Blue);
		}

		private void btnTheme3_Click(object sender, EventArgs e)
		{
			_settingsController.SetTheme(EnumTheme.Green);
		}

		private void btnTheme4_Click(object sender, EventArgs e)
		{
			_settingsController.SetTheme(EnumTheme.Orange);
		}

		private void btnTheme5_Click(object sender, EventArgs e)
		{
			_settingsController.SetTheme(EnumTheme.LightCoral);
		}

		private void btnTheme6_Click(object sender, EventArgs e)
		{
			_settingsController.SetTheme(EnumTheme.IndianRed);
		}

		private void tbOpacity_Scroll(object sender, EventArgs e)
		{
			double opacity = ((double)tbOpacity.Value) / 10;
			_settingsController.SetOpacity(opacity);
		}
	}
}
