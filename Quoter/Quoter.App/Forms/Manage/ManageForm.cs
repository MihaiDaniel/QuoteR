using Quoter.App.Controls;
using Quoter.App.Forms.Common;
using Quoter.App.Forms.Manage;
using Quoter.App.FormsControllers.EditQuotes;
using Quoter.App.FormsControllers.FavouriteQuotes;
using Quoter.App.FormsControllers.Manage;
using Quoter.App.FormsControllers.Settings;
using Quoter.App.Helpers;
using Quoter.App.Helpers.Extensions;
using Quoter.App.Models;
using Quoter.App.Services;
using Quoter.App.Services.FormAnimation;
using Quoter.App.Services.Forms;
using Quoter.Framework.Entities;
using Quoter.Framework.Enums;
using Quoter.Framework.Models;
using Quoter.Framework.Services;
using Quoter.Shared.Enums;

namespace Quoter.App.Forms
{
	/// <summary>
	/// Main form of the application that contains different tabs for settings and managing quotes.
	/// Expects a <see cref="ManageFormOptions"/>
	/// </summary>
	public partial class ManageForm : ResizableForm, IManageForm, IEditQuotesForm, ISettingsForm, IFavouriteQuotesForm
	{
		private readonly IFormsManager _formsManager;
		private readonly IFormAnimationService _formAnimationService;
		private readonly IStringResources _stringResources;
		private readonly IManageFormController _manageFormController;
		private readonly IEditQuotesFormController _editQuotesController;
		private readonly ISettingsFormController _settingsController;
		private readonly IFavouriteQuotesFormController _favouriteQuotesController;
		private readonly ManageFormOptions _options;
		ILogger _logger;
		/// <summary>
		/// Boolean to stop events from propaganding from form to controller,
		/// to eliminate a circular call and stack overflow. This is so we can add
		/// elements to isFavourites tab controllers and set the checked status without
		/// notifying the controller of any changes untill we finish the operation.
		/// </summary>
		private bool _allowFavouritesIsCheckedEventHandlers = true;

		/// <summary>
		/// The last key pressed by the user on the form. Used to handle keys combination shortcuts
		/// </summary>
		private Keys _lastKey = Keys.None;

		public ManageForm(IFormsManager formsManager,
							IFormPositioningService formPositioningService,
							IFormAnimationService formAnimationService,
							IStringResources stringResources,
							IManageFormController manageFormController,
							IEditQuotesFormController editQuotesController,
							ISettingsFormController settingsController,
							IFavouriteQuotesFormController favouriteQuotesController,
							ILogger logger,
							ManageFormOptions options)
		{
			InitializeComponent();
			CreateResizePictureBox();
			DropShadow.ApplyShadows(this);

			formPositioningService.RegisterFormDragableByControl(this, pnlTitle);
			formPositioningService.RegisterFormDragableByControl(this, lblTitle);

			_formsManager = formsManager;
			_formAnimationService = formAnimationService;
			_stringResources = stringResources;
			_manageFormController = manageFormController;
			_editQuotesController = editQuotesController;
			_settingsController = settingsController;
			_favouriteQuotesController = favouriteQuotesController;
			_logger = logger;
			_options = options;
			LocalizeControls();

			//Bind ui to controllers and register controllers
			BindManageControls();
			BindEditQuotesControls();
			BindFavouriteQuotesControls();
			BindSettingsControls();

			_manageFormController.RegisterForm(this);
			_editQuotesController.RegisterForm(this);
			_favouriteQuotesController.RegisterForm(this);
			_settingsController.RegisterForm(this);

			// Hide main navigation tabs (because we will use some buttons instead)
			tabControl.Appearance = TabAppearance.FlatButtons;
			tabControl.SizeMode = TabSizeMode.Fixed;
			tabControl.ItemSize = new Size(0, 1);
			tabControl.SelectTab(0);

#if DEBUG
			btnQuickAdd.Visible = true;
#else
			btnQuickAdd.Visible = false;
#endif

			pnlQuotesOptions.Visible = false;
		}

		public void LocalizeControls()
		{
			this.Text = _stringResources["Quoter"];

			// Navigation buttons for tabs
			btnTabPage1.Text = _stringResources["ModifyQuotes"];
			btnTabPage2.Text = _stringResources["ManageQuotes"];
			btnTabPage3.Text = _stringResources["Settings"];

			// Edit quotes tab
			lblTitle.Text = _stringResources["Quoter"];
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

			lblQuotesExcludeCharsUntill.Text = _stringResources["OptionTrimCharsUntil"];
			lblQuotesReplaceChars.Text = _stringResources["OptionReplaceChars"];
			lblQuotesExcludeChars.Text = _stringResources["OptionExcludeChars"];
			lblQuotesAppendStart.Text = _stringResources["OptionAppendTextStart"];
			lblQuotesAppendEnd.Text = _stringResources["OptionAppendTextEnd"];
			btnQuotesOptions.Text = _stringResources["Options"];

			// Favourite quotes tab
			lblFavoritesText.Text = _stringResources["FavoritesText"];
			lblFavouriteCollections.Text = _stringResources["Collections"];
			lblFavouriteBooks.Text = _stringResources["Books"];
			lblFavouriteChapters.Text = _stringResources["Chapters"];

			gbExport.Text = _stringResources["ExportCollections"];
			chkExportFavCollections.Text = _stringResources["ExportOnlyFavourites"];
			btnExportCollection.Text = _stringResources["Export"];

			gbImport.Text = _stringResources["Import"];
			chkImportMerge.Text = _stringResources["MergeCollections"];
			chkImportIgnoreLanguage.Text = _stringResources["IgnoreLanguage"];
			btnImport.Text = _stringResources["Import"];

			// Settings tab
			gbLanguageSettings.Text = _stringResources["Language"];
			lblShowCollByLanguage.Text = _stringResources["ShowCollectionsByLanguage"];
			btnShowCollBasedOnLanguageYes.Text = _stringResources["Yes"];
			btnShowCollBasedOnLanguageNo.Text = _stringResources["No"];

			gbThemeSettings.Text = _stringResources["Theme"];
			lblOpacity.Text = _stringResources["Opacity"];

			gbQuotesSettings.Text = _stringResources["Quotes"];
			lblQuotesFrequency.Text = _stringResources["QuotesFrequency"];
			lblQuotesFrequencyTime.Text = _stringResources["Minutes"];
			lblNotificationType.Text = _stringResources["NotificationType"];
			btnPopupNotifications.Text = _stringResources["PopupNotifications"];
			btnAlwaysOnNotifications.Text = _stringResources["AlwaysOnNotifications"];

			lblQuotesAutocloseInterval.Text = _stringResources["QuotesAutoclose"];
			lblQuotesAutocloseTime.Text = _stringResources["Seconds"];

			gbOtherSettings.Text = _stringResources["OtherSettings"];
			lblShowWelcomeMsg.Text = _stringResources["ShowWelcomeMessage"];
			btnShowWelcomeMsgYes.Text = _stringResources["Yes"];
			btnShowWelcomeMsgNo.Text = _stringResources["No"];

			lblStartWithWindows.Text = _stringResources["StartWithWindows"];
			btnStartWithWindowsYes.Text = _stringResources["Yes"];
			btnStartWithWindowsNo.Text = _stringResources["No"];

			lblUpdateMode.Text = _stringResources["UpdateMode"];

			lblNotificationLocation.Text = _stringResources["NotificationLocation"];
			lblNotificationFont.Text = _stringResources["NotificationFont"];
			btnNotificationFont.Text = _stringResources["Change"];

			lblNotificationSound.Text = _stringResources["NotificationSound"];
		}

		private void BindManageControls()
		{
			lblVersion.DataBindings.Add("Text", _manageFormController, nameof(_manageFormController.Version));
		}

		private void BindEditQuotesControls()
		{
			// Collections
			BindingSource bindingSourceCollections = new();
			bindingSourceCollections.DataSource = _editQuotesController.Collections;
			cbCollection.DataSource = bindingSourceCollections;
			cbCollection.DisplayMember = nameof(Collection.Name);
			cbCollection.ValueMember = nameof(Collection.CollectionId);
			cbCollection.DataBindings.Add("SelectedItem", _editQuotesController, nameof(IEditQuotesFormController.SelectedCollection), false, DataSourceUpdateMode.Never);

			// Books
			BindingSource bindingSourceBooks = new();
			bindingSourceBooks.DataSource = _editQuotesController.Books;
			cbBooks.DataSource = bindingSourceBooks;
			cbBooks.DisplayMember = nameof(Book.Name);
			cbBooks.ValueMember = nameof(Book.BookId);
			cbBooks.DataBindings.Add("SelectedItem", _editQuotesController, nameof(IEditQuotesFormController.SelectedBook), false, DataSourceUpdateMode.Never);

			// Chapters
			BindingSource bindingSourceChapters = new();
			bindingSourceChapters.DataSource = _editQuotesController.Chapters;
			lbChapters.DataSource = bindingSourceChapters;
			lbChapters.DisplayMember = nameof(Chapter.Name);
			lbChapters.ValueMember = nameof(Chapter.BookId);
			lbChapters.DataBindings.Add("SelectedItem", _editQuotesController, nameof(IEditQuotesFormController.SelectedChapter), false, DataSourceUpdateMode.Never);

			// Quotes
			rtbQuotes.DataBindings.Add("Text", _editQuotesController, nameof(_editQuotesController.Quotes));
		}

		private void BindSettingsControls()
		{
			txtQuotesInterval.DataBindings.Add("Text", _settingsController, nameof(ISettingsFormController.NotificationsIntervalMinutes));
			txtQuotesAutoCloseInterval.DataBindings.Add("Text", _settingsController, nameof(ISettingsFormController.NotificationsAutoCloseSeconds));
			lblOpacityPercent.DataBindings.Add("Text", _settingsController, nameof(ISettingsFormController.OpacityValue));

			cbNotificationSound.DataBindings.Add("SelectedItem", _settingsController, nameof(ISettingsFormController.SelectedNotificationSound), false, DataSourceUpdateMode.Never);

			//BindingSource bindingSourceUpdateModes = new();
			//bindingSourceUpdateModes.DataSource = _settingsController.UpdateModes;
			//cbUpdateMode.DataSource = bindingSourceUpdateModes;
			//cbUpdateMode.DisplayMember = nameof(UpdateModeItem.DisplayName);
			//cbUpdateMode.ValueMember = nameof(UpdateModeItem.UpdateMode);
			cbUpdateMode.DataBindings.Add("SelectedItem", _settingsController, nameof(ISettingsFormController.SelectedUpdateMode), false, DataSourceUpdateMode.Never);
		}

		private void BindFavouriteQuotesControls()
		{
			BindingSource bindingSourceCollections = new();
			bindingSourceCollections.DataSource = _favouriteQuotesController.Collections;
			clbCollections.DataSource = bindingSourceCollections;
			clbCollections.DisplayMember = nameof(Collection.Name);
			clbCollections.ValueMember = nameof(Collection.Name);

			BindingSource bindingSourceBooks = new();
			bindingSourceBooks.DataSource = _favouriteQuotesController.Books;
			clbBooks.DataSource = bindingSourceBooks;
			clbBooks.DisplayMember = nameof(Book.Name);
			clbBooks.ValueMember = nameof(Book.Name);

			BindingSource bindingSourceChapters = new();
			bindingSourceChapters.DataSource = _favouriteQuotesController.Chapters;
			clbChapters.DataSource = bindingSourceChapters;
			clbChapters.DisplayMember = nameof(Chapter.Name);
			clbChapters.ValueMember = nameof(Chapter.Name);

			// Register at the end, because we need the bindings to check the checkboxes
		}

		private async void ManageQuotesForm_Load(object sender, EventArgs e)
		{
			SetBackgroundTask(false, "");
			await SetStatus(string.Empty, Color.Black);
			await _formAnimationService.AnimateAsync(this, EnumAnimation.FadeIn);
			await SetSelectedTab(_options.Tab, false);
			await _manageFormController.EventFormLoadedAsync();
		}

		private async void ManageForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			await _manageFormController.EventFormClosingAsync();
			await _editQuotesController.EventFormClosingAsync();
			await _favouriteQuotesController.EventFormClosingAsync();
			await _settingsController.EventFormClosingAsync();
		}

		private void ManageForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Shift)
			{
				switch (e.KeyCode)
				{
					case Keys.F1:
						btnTabPage1_Click(sender, e);
						break;
					case Keys.F2:
						btnTabPage2_Click(sender, e);
						break;
					case Keys.F3:
						btnTabPage3_Click(sender, e);
						break;
				}
			}
			if (tabControl.SelectedIndex == 0)   // Edit quotes tab
			{
				if (e.Control)
				{
					if (e.KeyCode == Keys.Enter)    // Save quotes
					{
						btnSaveQuotes_Click(sender, e);
					}
					else if (_lastKey == Keys.A) // Add something
					{
						switch (e.KeyCode)
						{
							case Keys.V:
								btnAddCollection_Click(sender, e);
								break;
							case Keys.B:
								btnAddBook_Click(sender, e);
								break;
							case Keys.C:
								btnAddChapter_Click(sender, e);
								break;
						}
					}
					else if (_lastKey == Keys.E) // Edit something
					{
						switch (e.KeyCode)
						{
							case Keys.V:
								btnEditCollection_Click(sender, e);
								break;
							case Keys.B:
								btnEditBook_Click(sender, e);
								break;
							case Keys.C:
								btnEditChapter_Click(sender, e);
								break;
						}
					}
					else if (_lastKey == Keys.D) // Delete something
					{
						switch (e.KeyCode)
						{
							case Keys.V:
								btnDeleteCollection_Click(sender, e);
								break;
							case Keys.B:
								btnDeleteBook_Click(sender, e);
								break;
							case Keys.C:
								btnDeleteChapter_Click(sender, e);
								break;
						}
					}
				}
			}
			_lastKey = e.KeyCode;
		}

		private void ManageForm_ResizeBegin(object sender, EventArgs e)
		{
			// Instead of trying to recalculate the position on resize of the 
			// selected tab highlight just recalculate it at the end of the resize event
			// and keep the panel hidden durint the resize
			pnlSelectedTab.Visible = false;

			// To reduce flickering just stop the control updates untill resize ends.
			lbChapters.BeginUpdate();
			clbCollections.BeginUpdate();
			clbBooks.BeginUpdate();
			clbChapters.BeginUpdate();
		}

		private void ManageForm_ResizeEnd(object sender, EventArgs e)
		{
			pnlSelectedTab.Visible = true;
			switch (tabControl.SelectedIndex)
			{
				case 0: SetSelectedTabHighlight(btnTabPage1); break;
				case 1: SetSelectedTabHighlight(btnTabPage2); break;
				case 2: SetSelectedTabHighlight(btnTabPage3); break;
			}
			_settingsController.SetWindowSize(this.Size);
			lbChapters.EndUpdate();
			clbCollections.EndUpdate();
			clbBooks.EndUpdate();
			clbChapters.EndUpdate();
		}


		void IForm.SetTheme(Theme theme)
		{
			this.BackColor = theme.BodyColor;
			pnlTitle.BackColor = theme.TitleColor;
			pnlSelectedTab.BackColor = theme.TitleColor;
			tabPage1.BackColor = theme.BodyColor;
			tabPage2.BackColor = theme.BodyColor;
			tabPage3.BackColor = theme.BodyColor;
			tabPage3.BackColor = theme.BodyColor;
			gbCollections.BackColor = theme.BodyColor;
			gbBooks.BackColor = theme.BodyColor;
			gbChapters.BackColor = theme.BodyColor;
			gbQuotes.BackColor = theme.BodyColor;
			gbLanguageSettings.BackColor = theme.BodyColor;
			gbThemeSettings.BackColor = theme.BodyColor;
			gbOtherSettings.BackColor = theme.BodyColor;
			gbQuotesSettings.BackColor = theme.BodyColor;
			pnlStatusBar.BackColor = theme.BodyColor;
			txtStatus.BackColor = theme.BodyColor;
			pnlTitle.BackColor = theme.TitleColor;
			pnlSelectedTab.BackColor = theme.TitleColor;
			btnMinimize.FlatAppearance.MouseOverBackColor = theme.HighlightColor;
			btnMinimize.FlatAppearance.MouseDownBackColor = theme.PressedColor;

			//btnTabPage1.BackColor = theme.BodyColor;
			//btnTabPage2.BackColor = theme.BodyColor;
			//btnTabPage3.BackColor = theme.BodyColor;
			//btnTabPage1.ForeColor = theme.ForeColor;
			//btnTabPage2.ForeColor = theme.ForeColor;
			//btnTabPage3.ForeColor = theme.ForeColor;

			//pnlStatusBar.BackColor = theme.BodyColor;
			//txtStatus.BackColor = theme.BodyColor;
			//txtStatus.ForeColor = theme.ForeColor;
			//lblVersion.ForeColor = theme.ForeColor;
			//lblBackgroundTask.ForeColor = theme.ForeColor;

			//gbCollections.BackColor = theme.BodyColor;
			//gbCollections.ForeColor = theme.ForeColor;
			//gbBooks.BackColor = theme.BodyColor;
			//gbBooks.ForeColor = theme.ForeColor;
			//gbChapters.BackColor = theme.BodyColor;
			//gbChapters.ForeColor = theme.ForeColor;
			//gbQuotes.BackColor = theme.BodyColor;
			//gbQuotes.ForeColor = theme.ForeColor;
			//rtbQuotes.BackColor = theme.BodyColor;
			//rtbQuotes.ForeColor = theme.ForeColor;
			//pnlQuotesOptions.BackColor = theme.BodyColor;
			//pnlQuotesOptions.ForeColor = theme.ForeColor;

			//lblFavoritesText.ForeColor = theme.ForeColor;
			//lblFavouriteCollections.ForeColor = theme.ForeColor;
			//lblFavouriteBooks.ForeColor = theme.ForeColor;
			//lblFavouriteChapters.ForeColor = theme.ForeColor;
			//clbCollections.BackColor = theme.BodyColor;
			//clbCollections.ForeColor = theme.ForeColor;
			//clbBooks.BackColor = theme.BodyColor;
			//clbBooks.ForeColor = theme.ForeColor;
			//clbChapters.BackColor = theme.BodyColor;
			//clbChapters.ForeColor = theme.ForeColor;
			//gbExport.BackColor = theme.BodyColor;
			//gbExport.ForeColor = theme.ForeColor;
			//gbImport.BackColor = theme.BodyColor;
			//gbImport.ForeColor = theme.ForeColor;
			//chkExportFavCollections.ForeColor = theme.ForeColor;
			//chkImportIgnoreLanguage.ForeColor = theme.ForeColor;
			//chkImportMerge.ForeColor = theme.ForeColor;

		}

		void IResizableForm.SetSize(Size size)
		{
			this.Size = new Size(size.Width, size.Height);
		}

		#region IManageForm

		async Task IManageForm.SetSelectedTab(EnumTab tab)
		{
			await this.SetSelectedTab(tab);
		}

		void IManageForm.SetBackgroundTask(bool inProgress, string message)
		{
			SetBackgroundTask(inProgress, message);
		}

		async Task IManageForm.SetStatus(string message, Color color)
		{
			await SetStatus(message, color);
		}


		#endregion IManageForm

		#region ISettingsForm

		void ISettingsForm.SetSelectedLanguage(EnumLanguage language)
		{
			if (language == EnumLanguage.English)
			{
				btnLanguageEn.FlatAppearance.BorderColor = Color.Black;
				btnLanguageRo.FlatAppearance.BorderColor = Constants.AppColor.ColorWindow;
				btnLanguageFr.FlatAppearance.BorderColor = Constants.AppColor.ColorWindow;
			}
			else if (language == EnumLanguage.Romanian)
			{
				btnLanguageEn.FlatAppearance.BorderColor = Constants.AppColor.ColorWindow;
				btnLanguageRo.FlatAppearance.BorderColor = Color.Black;
				btnLanguageFr.FlatAppearance.BorderColor = Constants.AppColor.ColorWindow;
			}
			else if (language == EnumLanguage.French)
			{
				btnLanguageEn.FlatAppearance.BorderColor = Constants.AppColor.ColorWindow;
				btnLanguageRo.FlatAppearance.BorderColor = Constants.AppColor.ColorWindow;
				btnLanguageFr.FlatAppearance.BorderColor = Color.Black;
			}
			btnLanguageEn.Invalidate();
			btnLanguageRo.Invalidate();
			btnLanguageFr.Invalidate();
		}

		void ISettingsForm.SetSelectedCollectionByLanguage(bool isShowByLanguage)
		{
			if (isShowByLanguage)
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

		void ISettingsForm.SetOpacitySlider(double opacity)
		{
			tbOpacity.Value = (int)(opacity * 10);
		}

		void ISettingsForm.SetShowWelcomeMessage(bool value)
		{
			if (value)
			{
				btnShowWelcomeMsgYes.FlatAppearance.BorderSize = 1;
				btnShowWelcomeMsgNo.FlatAppearance.BorderSize = 0;
			}
			else
			{
				btnShowWelcomeMsgYes.FlatAppearance.BorderSize = 0;
				btnShowWelcomeMsgNo.FlatAppearance.BorderSize = 1;
			}
		}

		void ISettingsForm.SetNotificationsType(EnumNotificationType type)
		{
			if (type == EnumNotificationType.AlwaysOn)
			{
				btnAlwaysOnNotifications.FlatAppearance.BorderColor = Color.Black;
				btnPopupNotifications.FlatAppearance.BorderColor = Color.Gainsboro;
			}
			else
			{
				btnAlwaysOnNotifications.FlatAppearance.BorderColor = Color.Gainsboro;
				btnPopupNotifications.FlatAppearance.BorderColor = Color.Black;
			}
		}

		void ISettingsForm.SetNotificationsLocation(EnumAnimation notificationOpenAnimation)
		{
			switch (notificationOpenAnimation)
			{
				case EnumAnimation.FadeInFromBottomLeft:
					rbAnimTopLeft.Checked = true;
					break;
				case EnumAnimation.FadeInFromBottomRight:
					rbAnimBottomRight.Checked = true;
					break;
				case EnumAnimation.FadeInFromTopRight:
					rbAnimTopRight.Checked = true;
					break;
				case EnumAnimation.FadeInFromTopLeft:
					rbAnimTopLeft.Checked = true;
					break;
			}
		}

		void ISettingsForm.SetNotificationFont(string fontName, string fontStyle, float fontSize)
		{
			txtSelectedFont.Font = new Font(fontName, fontSize, FontHelper.GetFontStyle(fontStyle));
		}

		void ISettingsForm.SetIsStartWithWindows(bool isStartWithWindows)
		{
			if (isStartWithWindows)
			{
				btnStartWithWindowsYes.FlatAppearance.BorderSize = 1;
				btnStartWithWindowsNo.FlatAppearance.BorderSize = 0;
			}
			else
			{
				btnStartWithWindowsYes.FlatAppearance.BorderSize = 0;
				btnStartWithWindowsNo.FlatAppearance.BorderSize = 1;
			}
		}

		void ISettingsForm.SetNotificationSounds(List<string> notificationSounds)
		{
			cbNotificationSound.Items.Clear();
			foreach (string item in notificationSounds)
			{
				cbNotificationSound.Items.Add(item);
			}
		}

		void ISettingsForm.SetUpdateModes(List<UpdateModeItem> lstUpdateModeItems)
		{
			cbUpdateMode.BeginUpdate();
			cbUpdateMode.Items.Clear();
			cbUpdateMode.DisplayMember = nameof(UpdateModeItem.DisplayName);
			cbUpdateMode.ValueMember = nameof(UpdateModeItem.UpdateMode);
			foreach (UpdateModeItem item in lstUpdateModeItems)
			{
				cbUpdateMode.Items.Add(item);
			}
			cbUpdateMode.EndUpdate();
		}

		#endregion ISettingsForm

		#region IFavouriteQuotesForm

		void IFavouriteQuotesForm.SetChecksFavourites()
		{
			_allowFavouritesIsCheckedEventHandlers = false;

			// Collections
			foreach (Collection collection in _favouriteQuotesController.Collections)
			{
				int index = clbCollections.Items.IndexOf(collection);
				if (index >= 1)
				{
					clbCollections.SetItemChecked(index, IsFavourite(collection.IsFavourite));
				}
			}
			if (_favouriteQuotesController.Collections.Any())
				clbCollections.SetItemChecked(0, IsAllCollectionsFavourites());

			// Books
			foreach (Book book in _favouriteQuotesController.Books)
			{
				int index = clbBooks.Items.IndexOf(book);
				if (index >= 1)
				{
					clbBooks.SetItemChecked(index, IsFavourite(book.IsFavourite));

				}

			}
			if (_favouriteQuotesController.Books.Any())
				clbBooks.SetItemChecked(0, IsAllBooksFavourites());

			// Chapters
			foreach (Chapter chapter in _favouriteQuotesController.Chapters)
			{
				int index = clbChapters.Items.IndexOf(chapter);
				if (index >= 1)
				{
					clbChapters.SetItemChecked(index, IsFavourite(chapter.IsFavourite));

				}

			}
			if (_favouriteQuotesController.Chapters.Any())
				clbChapters.SetItemChecked(0, IsAllChaptersFavourites());

			_allowFavouritesIsCheckedEventHandlers = true;
		}

		bool IsFavourite(bool? value)
		{
			if (value == null)
			{
				return false;
			}
			else
			{
				return value.Value;
			}
		}

		bool IsAllCollectionsFavourites()
		{
			// Skip the first "All" item
			return _favouriteQuotesController.Collections.Skip(1).All(c => c.IsFavourite == true);
		}

		bool IsAllBooksFavourites()
		{
			// Skip the first "All" item
			return _favouriteQuotesController.Books.Skip(1).All(c => c.IsFavourite == true);
		}

		bool IsAllChaptersFavourites()
		{
			// Skip the first "All" item
			return _favouriteQuotesController.Chapters.Skip(1).All(c => c.IsFavourite == true);
		}

		void IFavouriteQuotesForm.SetCollections(List<Collection> lstCollections)
		{
			//clbCollections.Items.Clear();
			//foreach(Collection collection in lstCollections)
			//{
			//	clbCollections.Items.Add(collection, collection.IsFavourite ?? false);
			//}
		}

		#endregion IFavouriteQuotesForm

		#region IEditQuotesForm

		void IEditQuotesForm.SetBooksControlsState(EnumCrudStates state)
		{
			if (state == EnumCrudStates.Add)
			{
				gbBooks.Visible = true;
				btnAddFirstBook.Visible = true;
				cbBooks.Visible = false;
				btnDeleteBook.Visible = false;
				btnEditBook.Visible = false;
				btnAddBook.Visible = false;
			}
			else if (state == EnumCrudStates.ViewAddEditDelete)
			{
				gbBooks.Visible = true;
				btnAddFirstBook.Visible = false;
				cbBooks.Visible = true;
				btnDeleteBook.Visible = true;
				btnEditBook.Visible = true;
				btnAddBook.Visible = true;
			}
			else if (state == EnumCrudStates.None)
			{
				gbBooks.Visible = false;
				btnAddFirstBook.Visible = false;
				cbBooks.Visible = false;
				btnDeleteBook.Visible = false;
				btnEditBook.Visible = false;
				btnAddBook.Visible = false;
			}
		}

		void IEditQuotesForm.SetChaptersControlsState(EnumCrudStates state)
		{
			if (state == EnumCrudStates.Add)
			{
				gbChapters.Visible = true;
				btnAddFirstChapter.Visible = true;
				lbChapters.Visible = false;
				btnDeleteChapter.Visible = false;
				btnEditChapter.Visible = false;
				btnAddChapter.Visible = false;

			}
			else if (state == EnumCrudStates.ViewAddEditDelete)
			{
				gbChapters.Visible = true;
				btnAddFirstChapter.Visible = false;
				lbChapters.Visible = true;
				btnDeleteChapter.Visible = true;
				btnEditChapter.Visible = true;
				btnAddChapter.Visible = true;
			}
			else if (state == EnumCrudStates.None)
			{
				gbChapters.Visible = false;
				btnAddFirstChapter.Visible = false;
				lbChapters.Visible = false;
				btnDeleteChapter.Visible = false;
				btnEditChapter.Visible = false;
				btnAddChapter.Visible = false;
			}
		}

		#endregion IEditQuotesForm

		#region Events Edit quotes tab

		private async void cbCollection_SelectedValueChanged(object sender, EventArgs e)
		{
			_logger.Debug($"cbCollection_SelectedValueChanged {cbCollection.SelectedIndex} {((Collection)cbCollection.SelectedItem)?.Name}");

			if (cbCollection.SelectedItem != null && cbCollection.SelectedItem as Collection != _editQuotesController.SelectedCollection)
			{
				_editQuotesController.SelectedCollection = cbCollection.SelectedItem as Collection;
				await _editQuotesController.LoadCollectionBooksOrQuotes();

				//Scroll to the top
				rtbQuotes.SelectionStart = 0;
				rtbQuotes.ScrollToCaret();
			}
		}

		private async void cbBooks_SelectedValueChanged(object sender, EventArgs e)
		{
			if (cbBooks.SelectedItem != null && cbBooks.SelectedItem as Book != _editQuotesController.SelectedBook)
			{
				_logger.Debug($"cbBooks_SelectedValueChanged {cbBooks.SelectedIndex} {((Book)cbBooks.SelectedItem)?.Name}");

				_editQuotesController.SelectedBook = cbBooks.SelectedItem as Book;
				await _editQuotesController.LoadBookChaptersOrQuotes();

				// Scroll to the top
				rtbQuotes.SelectionStart = 0;
				rtbQuotes.ScrollToCaret();
			}
		}

		private async void lbChapters_SelectedValueChanged(object sender, EventArgs e)
		{

			if (lbChapters.SelectedItem != null
				&& lbChapters.SelectedItem as Chapter != _editQuotesController.SelectedChapter)
			{
				_logger.Debug($"lbChapters_SelectedValueChanged {lbChapters.SelectedIndex} {((Chapter)lbChapters.SelectedItem)?.Name}");

				_editQuotesController.SelectedChapter = lbChapters.SelectedItem as Chapter;
				await _editQuotesController.LoadQuotes();
				// Scroll to the top
				rtbQuotes.SelectionStart = 0;
				rtbQuotes.ScrollToCaret();
			}
		}

		#region Button events Add, Edit, Delete Collections

		private async void btnAddCollection_Click(object sender, EventArgs e)
		{
			await _editQuotesController.AddCollection();
		}

		private async void btnEditCollection_Click(object sender, EventArgs e)
		{
			await _editQuotesController.EditCollection();
		}

		private async void btnDeleteCollection_Click(object sender, EventArgs e)
		{
			await _editQuotesController.DeleteCollection();
		}

		#endregion Button events Add, Edit, Delete Collections

		#region Button events Add, Edit, Delete Books

		private async void btnAddBook_Click(object sender, EventArgs e)
		{
			await _editQuotesController.AddBook();
		}

		private async void btnEditBook_Click(object sender, EventArgs e)
		{
			await _editQuotesController.EditBook();
		}

		private async void btnDeleteBook_Click(object sender, EventArgs e)
		{
			await _editQuotesController.DeleteBook();
		}

		#endregion Button events Add, Edit, Delete Books

		#region Button events Add, Edit, Delete Chapters

		private async void btnAddChapter_Click(object sender, EventArgs e)
		{
			await _editQuotesController.AddChapter();
		}

		private async void btnEditChapter_Click(object sender, EventArgs e)
		{
			await _editQuotesController.EditChapter();
		}

		private async void btnDeleteChapter_Click(object sender, EventArgs e)
		{
			await _editQuotesController.DeleteChapter();
		}

		private async void btnSaveQuotes_Click(object sender, EventArgs e)
		{
			QuoteSaveOptions saveOptions = new()
			{
				//TrimUntillFirstWhiteSpace = chkQuotesTrimRow.Checked,
				TrimUntil = txtQuotesExcludeUntillChars.Text,
				ExcludeChars = txtQuotesExcludedChars.Text,
				AppendTextToBegining = txtQuotesAppendedTextToBeginning.Text,
				AppendTextToEnd = txtQuotesAppendTextToEnd.Text,
				ReplaceChars = txtQuotesReplaceChars.Text,
				ReplacedCharsReplacement = txtQuotesReplaceCharsReplacement.Text,
			};
			await _editQuotesController.AddQuotes(saveOptions);
		}

		#endregion  Button events Add, Edit, Delete Chapters

		private async void btnAddFirstBook_Click(object sender, EventArgs e)
		{
			await _editQuotesController.AddBook();
		}

		private async void btnAddFirstChapter_Click(object sender, EventArgs e)
		{
			await _editQuotesController.AddChapter();
		}

		private void btnQuotesOptions_Click(object sender, EventArgs e)
		{
			if (pnlQuotesOptions.Visible)
			{
				pnlQuotesOptions.Visible = false;
			}
			else
			{
				pnlQuotesOptions.Visible = true;
			}

		}

		private async void btnReloadEditCollections_Click(object sender, EventArgs e)
		{
			await _editQuotesController.LoadCollections();
		}

		private void btnQuickAdd_Click(object sender, EventArgs e)
		{
			_editQuotesController.QuickAdd();
		}

		private void chkWordWrap_CheckedChanged(object sender, EventArgs e)
		{
			if (chkWordWrap.Checked)
			{
				rtbQuotes.WordWrap = true;
			}
			else
			{
				rtbQuotes.WordWrap = false;
			}
		}

		#endregion  Events Edit quotes tab

		#region Events Settings tab

		private void btnLanguageEn_Click(object sender, EventArgs e)
		{
			_settingsController.SetLanguage(EnumLanguage.English);
		}

		private void btnLanguageRo_Click(object sender, EventArgs e)
		{
			_settingsController.SetLanguage(EnumLanguage.Romanian);
		}

		private void btnLanguageFr_Click(object sender, EventArgs e)
		{
			_settingsController.SetLanguage(EnumLanguage.French);
		}

		private void txtQuotesInterval_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!char.IsControl(e.KeyChar)
				&& !char.IsDigit(e.KeyChar))
			{
				e.Handled = true;
			}
		}

		private void txtQuotesInterval_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				this.tabPage3.Focus();
			}
		}

		private void txtQuotesAutoCloseInterval_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!char.IsControl(e.KeyChar)
				&& !char.IsDigit(e.KeyChar))
			{
				e.Handled = true;
			}
		}

		private void txtQuotesAutoCloseInterval_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				this.tabPage3.Focus();
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

		private void btnNightMode_Click(object sender, EventArgs e)
		{
			_settingsController.SetNightMode();
		}

		private void btnPopupNotifications_Click(object sender, EventArgs e)
		{
			btnAlwaysOnNotifications.FlatAppearance.BorderColor = Color.Gainsboro;
			btnPopupNotifications.FlatAppearance.BorderColor = Color.Black;
			_settingsController.SetNotificationType(EnumNotificationType.Popup);
		}

		private void btnAlwaysOnNotifications_Click(object sender, EventArgs e)
		{
			btnAlwaysOnNotifications.FlatAppearance.BorderColor = Color.Black;
			btnPopupNotifications.FlatAppearance.BorderColor = Color.Gainsboro;
			_settingsController.SetNotificationType(EnumNotificationType.AlwaysOn);
		}

		private void rbAnimTopLeft_CheckedChanged(object sender, EventArgs e)
		{
			_settingsController.SetNotificationAnimation(EnumAnimation.FadeInFromTopLeft);
		}

		private void rbAnimTopRight_CheckedChanged(object sender, EventArgs e)
		{
			_settingsController.SetNotificationAnimation(EnumAnimation.FadeInFromTopRight);
		}

		private void rbAnimBottomLeft_CheckedChanged(object sender, EventArgs e)
		{
			_settingsController.SetNotificationAnimation(EnumAnimation.FadeInFromBottomLeft);
		}

		private void rbAnimBottomRight_CheckedChanged(object sender, EventArgs e)
		{
			_settingsController.SetNotificationAnimation(EnumAnimation.FadeInFromBottomRight);
		}

		private void btnNotificationFont_Click(object sender, EventArgs e)
		{
			_settingsController.SelectNotificationFont();
		}

		private void btnStartWithWindowsYes_Click(object sender, EventArgs e)
		{
			btnStartWithWindowsYes.FlatAppearance.BorderSize = 1;
			btnStartWithWindowsNo.FlatAppearance.BorderSize = 0;

			_settingsController.SetStartWithWindows(true);
		}

		private void btnStartWithWindowsNo_Click(object sender, EventArgs e)
		{
			btnStartWithWindowsYes.FlatAppearance.BorderSize = 0;
			btnStartWithWindowsNo.FlatAppearance.BorderSize = 1;

			_settingsController.SetStartWithWindows(false);
		}

		private void cbNotificationSound_SelectedValueChanged(object sender, EventArgs e)
		{
			if ((string)cbNotificationSound.SelectedValue != _settingsController.SelectedNotificationSound)
			{
				_settingsController.SetSelectedNotificationSound((EnumSound)cbNotificationSound.SelectedIndex);
			}
		}

		private void cbUpdateMode_SelectedValueChanged(object sender, EventArgs e)
		{
			if (cbUpdateMode.SelectedItem != null
				&& ((UpdateModeItem)cbUpdateMode.SelectedItem).UpdateMode != _settingsController.SelectedUpdateMode?.UpdateMode)
			{
				_logger.Debug($"cbUpdateMode_SelectedValueChanged {cbUpdateMode.SelectedIndex} {((UpdateModeItem)cbUpdateMode.SelectedItem)?.DisplayName}");
				_settingsController.SetSelectedUpdateMode(cbUpdateMode.SelectedItem as UpdateModeItem);
			}
		}

		private void btnPlayNotificationSound_Click(object sender, EventArgs e)
		{
			_settingsController.PlayCurrentNotificationSound();
		}

		#endregion Events  Settings tab

		#region Events  Favourites tab

		private void clbCollections_SelectedValueChanged(object sender, EventArgs e)
		{
			if (sender is CheckedListBox)
			{
				Collection? selectedCollection = ((CheckedListBox)sender).SelectedItem as Collection;
				if (selectedCollection != null)
				{
					_favouriteQuotesController.CollectionSelected(selectedCollection);
				}
			}
		}

		private void clbCollections_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			if (sender is CheckedListBox && _allowFavouritesIsCheckedEventHandlers)
			{
				Collection? selectedCollection = ((CheckedListBox)sender).SelectedItem as Collection;
				if (selectedCollection != null)
				{
					_favouriteQuotesController.CollectionCheckChanged(selectedCollection, e.NewValue == CheckState.Checked);
				}
			}
		}

		private void clbBooks_SelectedValueChanged(object sender, EventArgs e)
		{
			if (sender is CheckedListBox)
			{
				Book? selectedBook = ((CheckedListBox)sender).SelectedItem as Book;
				if (selectedBook != null)
				{
					_favouriteQuotesController.BookSelected(selectedBook);
				}
			}
		}

		private void clbBooks_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			if (sender is CheckedListBox && _allowFavouritesIsCheckedEventHandlers)
			{
				Book? selectedBook = ((CheckedListBox)sender).SelectedItem as Book;
				if (selectedBook != null)
				{
					_favouriteQuotesController.BookCheckChanged(selectedBook, e.NewValue == CheckState.Checked);
				}
			}
		}

		private void clbChapters_SelectedValueChanged(object sender, EventArgs e)
		{
			// Nothing to handle when chapter selected
		}

		private void clbChapters_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			if (sender is CheckedListBox && _allowFavouritesIsCheckedEventHandlers)
			{
				Chapter? selectedChapter = ((CheckedListBox)sender).SelectedItem as Chapter;
				if (selectedChapter != null)
				{
					_favouriteQuotesController.ChapterCheckChanged(selectedChapter, e.NewValue == CheckState.Checked);
				}
			}
		}


		private void btnExportCollection_Click(object sender, EventArgs e)
		{
			_favouriteQuotesController.Export(chkExportFavCollections.Checked);
		}

		private void btnImport_Click(object sender, EventArgs e)
		{
			_favouriteQuotesController.Import(chkImportMerge.Checked, chkImportIgnoreLanguage.Checked);
		}

		private async void btnRefreshFavouriteCollections_Click(object sender, EventArgs e)
		{
			await _favouriteQuotesController.LoadCollections();
		}

		private void btnReadCollection_Click(object sender, EventArgs e)
		{
			Collection? selectedCollection = clbCollections.SelectedItem as Collection;
			_favouriteQuotesController.ReadCollection(selectedCollection);
		}

		#endregion Events Favourites tab

		private async void btnTabPage1_Click(object sender, EventArgs e)
		{
			if (tabControl.SelectedTab != tabPage1)
			{
				await SetSelectedTab(EnumTab.EditQuotes);
			}
		}

		private async void btnTabPage2_Click(object sender, EventArgs e)
		{
			if (tabControl.SelectedTab != tabPage2)
			{
				await SetSelectedTab(EnumTab.FavouriteQuotes);
			}
		}

		private async void btnTabPage3_Click(object sender, EventArgs e)
		{
			if (tabControl.SelectedTab != tabPage3)
			{
				await SetSelectedTab(EnumTab.Settings);
			}
		}

		private void btnMaximize_Click(object sender, EventArgs e)
		{
			if (this.WindowState == FormWindowState.Maximized)
			{
				this.WindowState = FormWindowState.Normal;
			}
			else
			{
				this.WindowState = FormWindowState.Maximized;
			}
			ManageForm_ResizeEnd(this, null);
		}

		private void btnMinimize_Click(object sender, EventArgs e)
		{
			this.WindowState = FormWindowState.Minimized;
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			_formsManager.Close(this);
		}

		private async Task SetSelectedTab(EnumTab tab, bool doEvents = true)
		{
			switch (tab)
			{
				case EnumTab.EditQuotes:
					SetSelectedTabHighlight(btnTabPage1);
					tabControl.SelectTab(tabPage1);
					// Load the actual panel before starting to load data, to reduce bad flickering
					if (doEvents)
					{
						Application.DoEvents();
					}

					await _editQuotesController.EventFormLoadedAsync();
					break;
				case EnumTab.FavouriteQuotes:
					tabControl.SelectTab(tabPage2);
					// Load the actual panel before starting to load data, to reduce bad flickering
					if (doEvents)
					{
						Application.DoEvents();
					}
					SetSelectedTabHighlight(btnTabPage2);
					await _favouriteQuotesController.EventFormLoadedAsync();
					break;
				case EnumTab.Settings:
					tabControl.SelectTab(tabPage3);
					SetSelectedTabHighlight(btnTabPage3);
					await _settingsController.EventFormLoadedAsync();
					break;
			}
			await SetStatus("", Constants.ColorDefault);
		}

		private void SetSelectedTabHighlight(Button button)
		{
			// Location.X of the button is relative to the form, but the Y is relative to the tableLayout parent
			// for some reason, that's why use constant for Y
			pnlSelectedTab.Location = new Point(button.Location.X, 60);
			pnlSelectedTab.Size = new Size(button.Size.Width, 2);

			// this is hidden in as default, so check if it's visible after setting position
			if (!pnlSelectedTab.Visible)
			{
				pnlSelectedTab.Visible = true;
			}
		}

		private async Task SetStatus(string message, Color color)
		{
			Action changeStatusAction = () =>
			{
				txtStatus.Text = message;
				txtStatus.ForeColor = color;
			};
			if (!string.IsNullOrWhiteSpace(message))
			{
				await _formAnimationService.AnimateStatusAsync(txtStatus, changeStatusAction);
			}
			else
			{
				changeStatusAction.Invoke();
			}
		}

		private void SetBackgroundTask(bool inProgress, string message)
		{
			this.InvokeIfRequired(() =>
			{
				if (inProgress)
				{
					lblBackgroundTask.Text = message;
					pbBackgroundTask.Visible = true;
					pbBackgroundTask.Enabled = true;
				}
				else
				{
					lblBackgroundTask.Text = "";
					pbBackgroundTask.Visible = false;
					pbBackgroundTask.Enabled = false;
				}
			});
		}

	}
}
