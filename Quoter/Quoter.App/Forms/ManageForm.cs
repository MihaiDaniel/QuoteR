using Microsoft.EntityFrameworkCore;
using Quoter.App.FormsControllers;
using Quoter.App.Helpers;
using Quoter.App.Models;
using Quoter.App.Services;
using Quoter.App.Services.FormAnimation;
using Quoter.App.Services.Forms;
using Quoter.Framework.Data;
using Quoter.Framework.Entities;
using Quoter.Framework.Models;
using Quoter.Framework.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quoter.App.Forms
{
	public partial class ManageForm : Form, IEditQuotesForm
	{
		private readonly IFormsManager _formsManager;
		private readonly IFormAnimationService _formAnimationService;
		private readonly IStringResources _stringResources;
		private readonly IEditQuotesFormController _editQuotesController;
		private readonly ISettingsFormController _settingsController;


		public ManageForm(IFormsManager formsManager,
								IFormPositioningService formPositioningService,
								IFormAnimationService formAnimationService,
								IStringResources stringResources,
								IEditQuotesFormController editQuotesController,
								ISettingsFormController settingsController)
		{
			InitializeComponent();
			formPositioningService.RegisterFormDragableByControl(this, pnlTitle);

			_formsManager = formsManager;
			_formAnimationService = formAnimationService;
			_stringResources = stringResources;
			_editQuotesController = editQuotesController;
			_settingsController = settingsController;

			LocalizeControls();
			BindControls();

			// Hide tabs
			tabControl.Appearance = TabAppearance.FlatButtons;
			tabControl.SizeMode = TabSizeMode.Fixed;
			tabControl.ItemSize = new Size(0, 1);
			tabControl.SelectTab(0);

			SetStatus(string.Empty, Color.Black);
		}

		private void LocalizeControls()
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
			btnDeleteCollection.Text = _stringResources["Delete"];
			gbBooks.Text = _stringResources["Book"];
			btnAddBook.Text = _stringResources["Add"];
			btnDeleteBook.Text = _stringResources["Delete"];
			gbChapters.Text = _stringResources["Chapters"];
			btnAddChapter.Text = _stringResources["Add"];
			btnDeleteChapter.Text = _stringResources["Delete"];
			btnSaveQuotes.Text = _stringResources["Save"];
			gbQuotes.Text = _stringResources["Quotes"];

			// Settings tab
		}

		private void BindControls()
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

		private async void ManageQuotesForm_Load(object sender, EventArgs e)
		{
			await _formAnimationService.AnimateAsync(this, Framework.Enums.EnumAnimation.FadeIn);
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
			_editQuotesController.Save();
		}

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

		void IEditQuotesForm.SetStatus(string message, Color color)
		{
			SetStatus(message, color);
		}

		private void SetStatus(string message, Color color)
		{
			txtStatus.Text = message;
			txtStatus.ForeColor = color;
		}




	}
}
