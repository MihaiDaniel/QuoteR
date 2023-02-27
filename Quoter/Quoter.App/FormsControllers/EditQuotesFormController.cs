using Quoter.App.Forms;
using Quoter.App.Helpers;
using Quoter.App.Models;
using Quoter.App.Services;
using Quoter.App.Services.Forms;
using Quoter.Framework.Data;
using Quoter.Framework.Entities;
using Quoter.Framework.Enums;
using Quoter.Framework.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace Quoter.App.FormsControllers
{
	public class EditQuotesFormController : IEditQuotesFormController, INotifyPropertyChanged
	{
		private readonly QuoterContext _context;
		private readonly IStringResources _stringResources;
		private readonly IFormsManager _formsManager;
		private IEditQuotesForm _form;

		#region Data binding

		private Collection? _selectedCollection;
		public Collection? SelectedCollection
		{
			get => _selectedCollection;
			set
			{
				if (_selectedCollection != value)
				{
					_selectedCollection = value;
					OnPropertyChanged();
					LoadCollectionsData();
				}
				Debug.WriteLine($"SelectedCollection:{SelectedCollection?.CollectionId} {SelectedCollection?.Name}");
			}
		}

		public BindingList<Collection> Collections { get; set; }

		private Book? _selectedBook;
		public Book? SelectedBook
		{
			get => _selectedBook;
			set
			{
				if (_selectedBook != value)
				{
					_selectedBook = value;
					OnPropertyChanged();
					LoadBookChapters();
				}
				Debug.WriteLine($"SelectedBook:{SelectedBook?.BookId} {SelectedBook?.Name}");
			}
		}

		public BindingList<Book> Books { get; set; }

		private Chapter? _chapter;
		public Chapter? SelectedChapter
		{
			get => _chapter;
			set
			{
				if (_chapter != value)
				{
					_chapter = value;
					OnPropertyChanged();
					LoadQuotes();
				}
			}
		}

		public BindingList<Chapter> Chapters { get; set; }

		private string _quotes;
		public string Quotes
		{
			get => _quotes;
			set
			{
				if (_quotes != value)
				{
					_quotes = value;
					OnPropertyChanged();
				}
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		#endregion Data binding

		public EditQuotesFormController(QuoterContext quoterContext,
										IStringResources stringResources,
										IFormsManager formManager)
		{
			_context = quoterContext;
			_stringResources = stringResources;
			_formsManager = formManager;
			Collections = new BindingList<Collection>();
			Books = new BindingList<Book>();
			Chapters = new BindingList<Chapter>();
		}


		public void RegisterForm(IEditQuotesForm editQuotesForm)
		{
			_form = editQuotesForm;
			Collections.Clear();
			Quotes = "";
			LoadCollections();
			LoadCollectionBooks();
			LoadBookChapters();
		}

		private void LoadCollections()
		{
			Collections.Clear();
			List<Collection> lstCollections = _context.Collections.ToList();
			foreach (Collection collection in lstCollections)
			{
				Collections.Add(collection);
			}
		}

		private void LoadCollectionBooks()
		{
			Books.Clear();
			Chapters.Clear();
			if (SelectedCollection == null)
			{
				return;
			}
			List<Book> lstBooks = _context.Books.Where(b => b.CollectionId == SelectedCollection.CollectionId)
												.ToList();
			if (lstBooks.Any())
			{
				foreach (Book book in lstBooks)
				{
					Books.Add(book);
				}
			}
		}

		private void LoadBookChapters()
		{
			Chapters.Clear();
			if (SelectedBook == null)
			{
				return;
			}
			List<Chapter> lstChapters = _context.Chapters.Where(c => c.BookId == SelectedBook.BookId)
												.ToList();
			if (lstChapters.Any())
			{
				foreach (Chapter chapter in lstChapters)
				{
					Chapters.Add(chapter);
				}
			}
		}

		private void LoadCollectionsData()
		{
			if (SelectedCollection == null)
			{
				return;
			}
			LoadCollectionBooks();
			LoadQuotes();
		}

		private void LoadQuotes()
		{
			if (SelectedCollection == null)
			{
				return;
			}

			IQueryable<Quote> queryQuotes = _context.Quotes.Where(q => q.CollectionId == SelectedCollection.CollectionId);

			if (SelectedBook != null)
			{
				queryQuotes = queryQuotes.Where(q => q.BookId == SelectedBook.BookId);
			}
			if (SelectedChapter != null)
			{
				queryQuotes = queryQuotes.Where(q => q.ChapterId == SelectedChapter.ChapterId);
			}

			List<Quote> lstQuotes = queryQuotes.ToList();

			if (lstQuotes.Any())
			{
				StringBuilder stringBuilder = new();
				foreach (Quote quote in lstQuotes)
				{
					stringBuilder.Append(quote.Content);
					stringBuilder.Append(Environment.NewLine);
				}
				Quotes = stringBuilder.ToString();
			}
			else
			{
				Quotes = "";
			}
		}

		#region Add, Edit, Delete collections

		public void AddCollection()
		{
			IDialogReturnable result = _formsManager.ShowDialog<DialogInputForm>(new DialogModel()
			{
				Title = _stringResources["NewCollection"],
				Message = _stringResources["NewCollectionName"],
			});

			if (result.DialogResult == DialogResult.OK)
			{
				string newCollectionName = result.StringResult;
				if (Collections.Any(c => c.Name == newCollectionName))
				{
					_formsManager.ShowDialog<DialogMessageForm>(new DialogModel()
					{
						Title = _stringResources["Quoter"],
						Message = _stringResources["CollectionAlreadyExists"],
						TitleColor = Helpers.Const.ColorWarn,
					});
					AddCollection();
				}
				else
				{
					Collection newCollection = new()
					{
						Name = result.StringResult
					};
					_context.Collections.Add(newCollection);
					_context.SaveChanges();
					Collections.Add(newCollection);
					_form.SetStatus(_stringResources["CollectionCreated", newCollection.Name], Const.ColorOk);
					SelectedCollection = newCollection;
				}
			}
		}

		public void EditCollection()
		{
			if (SelectedCollection == null)
			{
				return;
			}
			IDialogReturnable result = _formsManager.ShowDialog<DialogInputForm>(new DialogModel()
			{
				Title = _stringResources["EditCollection"],
				Message = _stringResources["EditCollectionMsg"],
				Value = SelectedCollection.Name
			});

			if (result.DialogResult == DialogResult.OK)
			{
				SelectedCollection.Name = result.StringResult;
				_context.SaveChanges();
				LoadCollections();
				SelectedCollection = Collections.First(c => c.Name == SelectedCollection.Name);
			}
		}

		public void DeleteCollection()
		{
			if (SelectedCollection == null)
			{
				return;
			}
			IDialogReturnable result = _formsManager.ShowDialog<DialogMessageForm>(new DialogModel()
			{
				Title = _stringResources["DeleteCollection"],
				Message = _stringResources["DeleteCollectionMsg", SelectedCollection.Name],
				MessageBoxButtons = EnumDialogButtons.OkCancel
			});

			if (result.DialogResult == DialogResult.OK)
			{
				_context.Collections.Remove(SelectedCollection);
				_context.SaveChanges();
				LoadCollections();
				SelectedCollection = Collections.FirstOrDefault();
			}
		}

		#endregion  Add, Edit, Delete collections

		#region  Add, Edit, Delete books

		public void AddBook()
		{
			if (SelectedCollection == null)
			{
				return;
			}
			IDialogReturnable result = _formsManager.ShowDialog<DialogInputForm>(new DialogModel()
			{
				Title = _stringResources["NewBook"],
				Message = _stringResources["NewBookName", SelectedCollection.Name],
			});

			if (result.DialogResult == DialogResult.OK)
			{
				string newBookName = result.StringResult;
				if (Books.Any(c => c.Name == newBookName))
				{
					_formsManager.ShowDialog<DialogMessageForm>(new DialogModel()
					{
						Title = _stringResources["Quoter"],
						Message = _stringResources["BookAlreadyExists"],
						TitleColor = Helpers.Const.ColorWarn,
					});
					AddBook();
				}
				else
				{
					Book newBook = new()
					{
						Name = result.StringResult,
						CollectionId = SelectedCollection.CollectionId
					};
					_context.Books.Add(newBook);
					_context.SaveChanges();
					Books.Add(newBook);
					_form.SetStatus(_stringResources["BookCreated", newBook.Name, SelectedCollection.Name], Const.ColorOk);
					SelectedBook = newBook;
				}
			}
		}

		public void EditBook()
		{
			if (SelectedBook == null)
			{
				return;
			}
			IDialogReturnable result = _formsManager.ShowDialog<DialogInputForm>(new DialogModel()
			{
				Title = _stringResources["EditBook"],
				Message = _stringResources["EditBookMsg"],
				Value = SelectedBook.Name
			});

			if (result.DialogResult == DialogResult.OK)
			{
				SelectedBook.Name = result.StringResult;
				_context.SaveChanges();
				LoadCollectionBooks();
				SelectedBook = Books.First(c => c.Name == SelectedBook.Name);
			}
		}

		public void DeleteBook()
		{
			if (SelectedBook == null)
			{
				return;
			}
			IDialogReturnable result = _formsManager.ShowDialog<DialogMessageForm>(new DialogModel()
			{
				Title = _stringResources["DeleteBook"],
				Message = _stringResources["DeleteBookMsg", SelectedBook.Name],
				MessageBoxButtons = EnumDialogButtons.OkCancel
			});

			if (result.DialogResult == DialogResult.OK)
			{
				_context.Books.Remove(SelectedBook);
				_context.SaveChanges();
				LoadCollectionBooks();
				SelectedBook = Books.FirstOrDefault();
			}
		}

		#endregion  Add, Edit, Delete books

		#region Add, Edit, Delete chapters

		public void AddChapter()
		{
			if (SelectedBook == null)
			{
				return;
			}
			IDialogReturnable result = _formsManager.ShowDialog<DialogInputForm>(new DialogModel()
			{
				Title = _stringResources["NewChapter"],
				Message = _stringResources["NewChapterName", SelectedBook.Name],
			});

			if (result.DialogResult == DialogResult.OK)
			{
				string newChapterName = result.StringResult;
				if (Chapters.Any(c => c.Name == newChapterName))
				{
					_formsManager.ShowDialog<DialogMessageForm>(new DialogModel()
					{
						Title = _stringResources["Quoter"],
						Message = _stringResources["ChapterAlreadyExists"],
						TitleColor = Const.ColorWarn,
					});
					AddChapter();
				}
				else
				{
					Chapter newChapter = new()
					{
						Name = result.StringResult,
						BookId = SelectedBook.BookId
					};
					_context.Chapters.Add(newChapter);
					_context.SaveChanges();
					Chapters.Add(newChapter);
					_form.SetStatus(_stringResources["ChapterCreated", newChapter.Name, SelectedBook.Name], Const.ColorOk);
					SelectedChapter = newChapter;
				}
			}
		}

		public void EditChapter()
		{
			if (SelectedChapter == null)
			{
				return;
			}
			IDialogReturnable result = _formsManager.ShowDialog<DialogInputForm>(new DialogModel()
			{
				Title = _stringResources["EditChapter"],
				Message = _stringResources["EditChapterMsg"],
				Value = SelectedChapter.Name
			});

			if (result.DialogResult == DialogResult.OK)
			{
				SelectedChapter.Name = result.StringResult;
				_context.SaveChanges();
				LoadBookChapters();
				SelectedChapter = Chapters.First(c => c.Name == SelectedChapter.Name);
			}
		}

		public void DeleteChapter()
		{
			if (SelectedChapter == null)
			{
				return;
			}
			IDialogReturnable result = _formsManager.ShowDialog<DialogMessageForm>(new DialogModel()
			{
				Title = _stringResources["DeleteChapter"],
				Message = _stringResources["DeleteChapterMsg", SelectedChapter.Name],
				MessageBoxButtons = EnumDialogButtons.OkCancel
			});

			if (result.DialogResult == DialogResult.OK)
			{
				_context.Chapters.Remove(SelectedChapter);
				_context.SaveChanges();
				LoadBookChapters();
				SelectedChapter = Chapters.FirstOrDefault();
			}
		}

		#endregion  Add, Edit, Delete chapters

		public void Save()
		{
			if (string.IsNullOrWhiteSpace(Quotes) || SelectedCollection == null)
			{
				return;
			}

			string[]? arrQuoteContent = SplitStringByNewLine(Quotes);
			List<Quote> lstQuotes = GetQuotesFromArrayOfStrings(arrQuoteContent);

			if (lstQuotes.Any())
			{
				// First remove existing quotes for chapter, book and collection
				IQueryable<Quote> queryQuotes = _context.Quotes.Where(q => q.CollectionId == SelectedCollection.CollectionId);
				if (SelectedBook != null)
				{
					queryQuotes = queryQuotes.Where(q => q.BookId == SelectedBook.BookId);
				}
				if (SelectedChapter != null)
				{
					queryQuotes = queryQuotes.Where(q => q.ChapterId == SelectedChapter.ChapterId);
				}

				List<Quote> quotesToDelete = queryQuotes.ToList();
				_context.Quotes.RemoveRange(quotesToDelete);

				// Then add the quotes in the ui
				_context.Quotes.AddRange(lstQuotes);
				_context.SaveChanges();
				_form.SetStatus(_stringResources["QuotesSaved"], Const.ColorOk);
			}
		}

		private string[] SplitStringByNewLine(string content)
		{
			string[] arrQuoteContent;
			if (content.Contains("\r\n"))
			{
				arrQuoteContent = content.Split("\r\n");
			}
			else if (content.Contains("\n"))
			{
				arrQuoteContent = content.Split("\n");
			}
			else
			{
				arrQuoteContent = content.Split(Environment.NewLine);
			}
			return arrQuoteContent;
		}

		private List<Quote> GetQuotesFromArrayOfStrings(string[] arrQuoteContent)
		{
			List<Quote> lstQuotes = new List<Quote>();
			foreach (string quoteContent in arrQuoteContent)
			{
				string newQuoteContent = quoteContent.Trim();
				if (!string.IsNullOrEmpty(newQuoteContent))
				{
					Quote quote = new()
					{
						Content = newQuoteContent,
						CollectionId = SelectedCollection.CollectionId,
						BookId = SelectedBook?.BookId,
						ChapterId = SelectedChapter?.ChapterId,
					};
					lstQuotes.Add(quote);
				}
			}
			return lstQuotes;
		}
	}
}
