using Quoter.App.Helpers;
using Quoter.App.Models;
using Quoter.App.Services;
using Quoter.App.Services.FormAnimation;
using Quoter.App.Services.Forms;
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
    public partial class ManageQuotesForm : Form
	{
		private readonly IFormsManager _formsManager;
		private readonly IFormAnimationService _formAnimationService;
		private readonly IStringResources _stringResources;
		private readonly IRepository _repository;

		private BindingList<CollectionModel> _collections;
		private BindingList<BookModel> _books;
		private BindingList<ChapterModel> _chapters;

		public ManageQuotesForm(IFormsManager formsManager,
								IFormPositioningService formPositioningService,
								IFormAnimationService formAnimationService,
								IStringResources stringResources,
								IRepository repository)
		{
			InitializeComponent();
			formPositioningService.RegisterFormDragableByControl(this, pnlTitle);

			_formsManager = formsManager;
			_formAnimationService = formAnimationService;
			_stringResources = stringResources;
			_repository = repository;

			_collections = new BindingList<CollectionModel>();
			_books = new BindingList<BookModel>();
			_chapters = new BindingList<ChapterModel>();

			LocalizeControls();
			BindControls();

			SetStatus(string.Empty, Color.Black);
		}

		private void LocalizeControls()
		{
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
		}

		private void BindControls()
		{
			BindingSource bindingSourceCollections = new();
			bindingSourceCollections.DataSource = _collections;

			cbCollection.DataSource = bindingSourceCollections;
			cbCollection.DisplayMember = nameof(CollectionModel.Name);
			cbCollection.ValueMember = nameof(CollectionModel.Name);

			BindingSource bindingSourceBooks = new();
			bindingSourceBooks.DataSource = _books;

			cbBooks.DataSource = bindingSourceBooks;
			cbBooks.DisplayMember = nameof(BookModel.Name);
			cbBooks.ValueMember = nameof(BookModel.Name);

			BindingSource bindingSourceChapters = new();
			bindingSourceChapters.DataSource = _chapters;

			lbChapters.DataSource = bindingSourceChapters;
			lbChapters.DisplayMember = nameof(ChapterModel.Name);
			lbChapters.ValueMember = nameof(ChapterModel.Name);
		}

		private async void ManageQuotesForm_Load(object sender, EventArgs e)
		{
			await _formAnimationService.AnimateAsync(this, Framework.Enums.EnumAnimation.FadeIn);

			List<CollectionModel> lstCollections = await _repository.GetCollectionsAsync();
			foreach (CollectionModel collection in lstCollections)
			{
				_collections.Add(collection);
			}
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			_formsManager.Close(this);
		}

		private async void cbCollection_SelectedValueChanged(object sender, EventArgs e)
		{
			if (cbCollection.SelectedItem != null)
			{
				CollectionModel selectedCollection = cbCollection.SelectedItem as CollectionModel;
				List<BookModel> lstBooks = await _repository.GetBooksAsync(selectedCollection.Name);
				_books.Clear();
				foreach (BookModel book in lstBooks)
				{
					_books.Add(book);
				}
			}
		}

		private void cbBooks_SelectedValueChanged(object sender, EventArgs e)
		{
			if (cbBooks.SelectedItem != null)
			{
				BookModel bookModel = cbBooks.SelectedItem as BookModel;
				_chapters.Clear();
				if (bookModel != null)
				{
					foreach (var chapter in bookModel.LstChapters)
					{
						_chapters.Add(chapter);
					}
				}
			}
			else
			{
				_chapters.Clear();
			}

		}

		private void lbChapters_SelectedValueChanged(object sender, EventArgs e)
		{
			if (lbChapters.SelectedItem != null)
			{
				ChapterModel selectedChapter = lbChapters.SelectedItem as ChapterModel;
				rtbQuotes.Clear();
				foreach (string quote in selectedChapter.LstContent)
				{
					rtbQuotes.AppendText(quote + "\r\n");
				}
			}
			else
			{
				rtbQuotes.Clear();
			}
		}

		private void btnAddCollection_Click(object sender, EventArgs e)
		{
			IDialogReturnable result = _formsManager.ShowDialog<DialogInputForm>(new DialogModel()
			{
				Title = _stringResources["NewCollection"],
				Message = _stringResources["SetCollectionName"],
			});

			if (result.DialogResult == DialogResult.OK)
			{
				string newCollectionName = result.StringResult;
				if (_collections.Any(c => c.Name == newCollectionName))
				{
                    _formsManager.ShowDialog<DialogMessageForm>(new DialogModel()
					{
						Title = _stringResources["Quoter"],
						Message = _stringResources["CollectionAlreadyExists"],
						TitleColor = Helpers.Const.ColorWarn,
					});
					btnAddCollection_Click(sender, e);
				}
				else
				{
					CollectionModel newCollection = new() { Name = result.StringResult };
					_collections.Add(newCollection);
					cbCollection.SelectedItem = newCollection;
                    SetStatus(_stringResources["CollectionCreated", result.StringResult], Helpers.Const.ColorOk);
				}
			}

		}

		private void btnDeleteCollection_Click(object sender, EventArgs e)
		{

		}

		private void btnAddBook_Click(object sender, EventArgs e)
		{

		}

		private void btnDeleteBook_Click(object sender, EventArgs e)
		{

		}

		private void btnAddChapter_Click(object sender, EventArgs e)
		{

		}

		private void btnDeleteChapter_Click(object sender, EventArgs e)
		{

		}

		private void btnSaveQuotes_Click(object sender, EventArgs e)
		{

		}

		private void SetStatus(string message, Color color)
		{
			txtStatus.Text = message;
			txtStatus.ForeColor = color;
		}
	}
}
