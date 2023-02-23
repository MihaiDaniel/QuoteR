using Quoter.App.Services;
using Quoter.App.Services.FormAnimation;
using Quoter.Framework.Models;
using Quoter.Framework.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
		private readonly IRepository _repository;

		private BindingList<CollectionModel> _collections;
		private BindingList<BookModel> _books;
		private BindingList<ChapterModel> _chapters;

		public ManageQuotesForm(IFormsManager formsManager,
								IFormPositioningService formPositioningService,
								IFormAnimationService formAnimationService,
								IRepository repository)
		{
			InitializeComponent();
			formPositioningService.RegisterFormDragableByControl(this, pnlTitle);

			_formsManager = formsManager;
			_formAnimationService = formAnimationService;
			_repository = repository;

			_collections = new BindingList<CollectionModel>();
			_books = new BindingList<BookModel>();
			_chapters = new BindingList<ChapterModel>();

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

			//cbCollection.DataBindings.Add("Text", )

			var lstCollections = await _repository.GetCollectionsAsync();
			foreach (var collection in lstCollections)
			{
				_collections.Add(collection);
			}
			cbCollection.SelectedIndex = 0;
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			_formsManager.Close(this);
		}

		private async void cbCollection_SelectedValueChanged(object sender, EventArgs e)
		{
			var lstBooks = await _repository.GetBooksAsync(cbCollection.SelectedValue as string);
			_books.Clear();
			foreach (var book in lstBooks)
			{
				_books.Add(book);
			}
			//cbBooks.SelectedIndex = 0;
		}

		private void cbBooks_SelectedValueChanged(object sender, EventArgs e)
		{
			BookModel bookModel = _books.FirstOrDefault(b => b.Name == ((BookModel)cbBooks.SelectedItem).Name);
			_chapters.Clear();
			foreach(var chapter in bookModel.LstChapters)
			{
				_chapters.Add(chapter);
			}
			lbChapters.SelectedIndex = 0;
		}

		private void lbChapters_SelectedValueChanged(object sender, EventArgs e)
		{
			// Throws exception here
			ChapterModel chapterModel = _chapters.FirstOrDefault(c => c.Name == ((ChapterModel)lbChapters.SelectedItem).Name);
			rtbQuotes.Clear();
			foreach(string quote in chapterModel.LstContent)
			{
				rtbQuotes.AppendText(quote + "\r\n");
			}
		}
	}
}
