using Quoter.App.Forms;
using Quoter.App.Helpers;
using Quoter.App.Models;
using Quoter.App.Services;
using Quoter.App.Services.Forms;
using Quoter.Framework.Data;
using Quoter.Framework.Entities;
using Quoter.Framework.Enums;
using Quoter.Framework.Services.Messaging;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Linq;

namespace Quoter.App.FormsControllers.EditQuotes
{
	public class EditQuotesFormController : IEditQuotesFormController, IMessageSubscriber, INotifyPropertyChanged
	{
		private readonly QuoterContext _context;
		private readonly IStringResources _stringResources;
		private readonly IFormsManager _formsManager;
		private readonly ISettings _settings;
		private readonly IMessagingService _messagingService;

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
				//Debug.WriteLine($"SelectedCollection:{SelectedCollection?.CollectionId} {SelectedCollection?.Name}");
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
				//Debug.WriteLine($"SelectedBook:{SelectedBook?.BookId} {SelectedBook?.Name}");
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
										IFormsManager formManager,
										ISettings settings,
										IMessagingService messagingService)
		{
			_context = quoterContext;
			_stringResources = stringResources;
			_formsManager = formManager;
			_settings = settings;
			_messagingService = messagingService;

			Collections = new BindingList<Collection>();
			Books = new BindingList<Book>();
			Chapters = new BindingList<Chapter>();

			_messagingService.Subscribe(this);
		}

		public void OnMessageEvent(string message, object? argument)
		{
			if (message == Event.ShowCollectionsBasedOnLanguageChanged || message == Event.LanguageChanged)
			{
				LoadCollections();
				LoadCollectionBooks();
				LoadBookChapters();
				// Quotes loaded by SelectedChapter property
			}
		}

		public void RegisterForm(IEditQuotesForm editQuotesForm)
		{
			_form = editQuotesForm;
			Collections.Clear();
			Quotes = "";
			LoadCollections();
			LoadCollectionBooks();
			LoadBookChapters();
			// Quotes loaded by SelectedChapter property
		}

		public void OnClose()
		{
			_messagingService.Unsubscribe(this);
		}

		private void LoadCollections()
		{
			UnloadDataFromForm(true, true, true);

			bool isShowCollectionsByLanguage = _settings.Get<bool>(Const.Setting.ShowCollectionsBasedOnLanguage);

			List<Collection> lstCollections;
			if (isShowCollectionsByLanguage)
			{
				EnumLanguage language = LanguageHelper.GetEnumLanguageFromString(_settings.Get<string>(Const.Setting.Language));
				lstCollections = _context.Collections
					.Where(c => c.Language == language || c.Language == default).ToList();
			}
			else
			{
				lstCollections = _context.Collections.ToList();
			}

			foreach (Collection collection in lstCollections)
			{
				Collections.Add(collection);
			}

			if (!lstCollections.Any())
			{
				_form.SetBooksControlsState(EnumCrudStates.None);
				_form.SetChaptersControlsState(EnumCrudStates.None);
			}
		}

		private void LoadCollectionBooks()
		{
			UnloadDataFromForm(false, true, true);
			if (string.IsNullOrEmpty(SelectedCollection?.Name))
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
				_form.SetBooksControlsState(EnumCrudStates.ViewAddEditDelete);
			}
			else
			{
				_form.SetBooksControlsState(EnumCrudStates.Add);
				_form.SetChaptersControlsState(EnumCrudStates.None);
			}
		}

		private void LoadBookChapters()
		{
			UnloadDataFromForm(false, false, true);
			if (string.IsNullOrEmpty(SelectedBook?.Name))
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
				_form.SetChaptersControlsState(EnumCrudStates.ViewAddEditDelete);
			}
			else
			{
				LoadQuotes(); // Load quotes that are in this book, but not assinged to a chapter
				_form.SetChaptersControlsState(EnumCrudStates.Add);
			}
		}

		private void LoadCollectionsData()
		{
			if (string.IsNullOrEmpty(SelectedCollection?.Name))
			{
				return;
			}
			LoadCollectionBooks();
			LoadQuotes();
		}

		private void LoadQuotes()
		{
			if (string.IsNullOrEmpty(SelectedCollection?.Name))
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

		private void UnloadDataFromForm(bool unloadCollection, bool unloadBooks, bool unloadChapters)
		{
			if (unloadCollection)
			{
				Collections.Clear();
				SelectedCollection = null;
			}
			if (unloadBooks)
			{
				Books.Clear();
				SelectedBook = null;
			}
			if (unloadChapters)
			{
				Chapters.Clear();
				SelectedChapter = null;
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
						Title = _stringResources["NameAlreadyTaken"],
						Message = _stringResources["CollectionAlreadyExists"],
						TitleColor = Const.ColorWarn,
					});
					AddCollection();
				}
				else
				{
					Collection newCollection = new()
					{
						Name = result.StringResult,
						Language = LanguageHelper.GetEnumLanguageFromString(_settings.Get<string>(Const.Setting.Language)),
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
			if (string.IsNullOrEmpty(SelectedCollection?.Name))
			{
				return;
			}
			IDialogReturnable result = _formsManager.ShowDialog<DialogInputForm>(new DialogModel()
			{
				Title = _stringResources["NewBook"],
				Message = _stringResources["NewBookName", SelectedCollection.Name],
			});

			if (result.DialogResult != DialogResult.OK)
			{
				return;
			}
			string newBookName = result.StringResult;
			if (Books.Any(c => c.Name == newBookName))
			{
				_formsManager.ShowDialog<DialogMessageForm>(new DialogModel()
				{
					Title = _stringResources["NameAlreadyTaken"],
					Message = _stringResources["BookAlreadyExists"],
					TitleColor = Const.ColorWarn,
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

				// Check for 'loose' quotes and add them to the new book
				// so after the ui is updated we load the quotes
				CheckForQuotesInCollectionWithNoAssignedBookAndAssignThem(newBook);

				Books.Add(newBook);
				SelectedBook = newBook;

				_form.SetBooksControlsState(EnumCrudStates.ViewAddEditDelete);
				_form.SetStatus(_stringResources["BookCreated", newBook.Name, SelectedCollection.Name], Const.ColorOk);
			}
		}

		private void CheckForQuotesInCollectionWithNoAssignedBookAndAssignThem(Book book)
		{
			if (SelectedCollection == null)
			{
				return;
			}
			List<Quote> lstQuotes = _context.Quotes.Where(q => q.CollectionId == SelectedCollection.CollectionId
												&& q.BookId == null
												&& q.ChapterId == null)
												.ToList();
			if (lstQuotes.Any())
			{
				foreach (Quote quote in lstQuotes)
				{
					quote.BookId = book.BookId;
				}
				_context.SaveChanges();
				_formsManager.ShowDialog<DialogMessageForm>(new DialogModel()
				{
					Title = _stringResources["QuotesAddedToBook"],
					Message = _stringResources["QuotesAddedToBookMsg", book.Name],
					TitleColor = Const.ColorDefault,
				});
			}
		}

		public void EditBook()
		{
			if (string.IsNullOrEmpty(SelectedBook?.Name))
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
			if (string.IsNullOrEmpty(SelectedBook?.Name))
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
			if (string.IsNullOrEmpty(SelectedBook?.Name))
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
						Title = _stringResources["NameAlreadyTaken"],
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

					CheckForQuotesInBookWithNoAssignedChapterAndAssignThem(newChapter);

					Chapters.Add(newChapter);
					SelectedChapter = newChapter;

					_form.SetStatus(_stringResources["ChapterCreated", newChapter.Name, SelectedBook.Name], Const.ColorOk);
					_form.SetChaptersControlsState(EnumCrudStates.ViewAddEditDelete);
				}
			}
		}

		private void CheckForQuotesInBookWithNoAssignedChapterAndAssignThem(Chapter newChapter)
		{
			if (SelectedBook == null || SelectedCollection == null)
			{
				return;
			}
			List<Quote> lstQuotes = _context.Quotes.Where(q => q.CollectionId == SelectedCollection.CollectionId
												&& q.BookId == SelectedBook.BookId
												&& q.ChapterId == null)
												.ToList();
			if (lstQuotes.Any())
			{
				foreach (Quote quote in lstQuotes)
				{
					quote.ChapterId = newChapter.ChapterId;
				}
				_context.SaveChanges();
				_formsManager.ShowDialog<DialogMessageForm>(new DialogModel()
				{
					Title = _stringResources["QuotesAddedToChapter"],
					Message = _stringResources["QuotesAddedToChapterMsg", SelectedBook.Name, newChapter.Name],
					TitleColor = Const.ColorDefault,
				});
			}
		}

		public void EditChapter()
		{
			if (string.IsNullOrEmpty(SelectedChapter?.Name))
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
				_form.SetChaptersControlsState(EnumCrudStates.ViewAddEditDelete);
			}
		}

		public void DeleteChapter()
		{
			if (string.IsNullOrEmpty(SelectedChapter?.Name))
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

		public void AddQuotes(QuoteSaveOptions saveOptions)
		{
			if (string.IsNullOrWhiteSpace(Quotes) || string.IsNullOrEmpty(SelectedCollection?.Name))
			{
				return;
			}

			string[]? arrQuoteContent = SplitStringByNewLine(Quotes);
			List<Quote> lstQuotes = GetQuotesFromArrayOfStrings(arrQuoteContent, saveOptions);

			if (lstQuotes.Any())
			{
				// First remove existing quotes for chapter, book and collection
				// Otherwise we might add again existing ones
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
				if (quotesToDelete.Any())
				{
					_context.Quotes.RemoveRange(quotesToDelete);
				}

				// Then add the quotes in the database
				_context.Quotes.AddRange(lstQuotes);
				_context.SaveChanges();
				_form.SetStatus(_stringResources["QuotesSaved"], Const.ColorOk);
				LoadQuotes();
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

		private List<Quote> GetQuotesFromArrayOfStrings(string[] arrQuoteContent, QuoteSaveOptions saveOptions)
		{
			List<Quote> lstQuotes = new List<Quote>();
			int quoteIndex = 1;
			foreach (string quoteContent in arrQuoteContent)
			{
				string newQuoteContent = quoteContent;

				if (saveOptions.TrimUntillFirstWhiteSpace)
				{
					newQuoteContent = newQuoteContent.Trim();
					int firstSpaceIndex = newQuoteContent.IndexOf(" ");
					if (firstSpaceIndex >= 0 && newQuoteContent.Length > firstSpaceIndex + 1)
					{
						newQuoteContent = newQuoteContent.Substring(firstSpaceIndex, newQuoteContent.Length - firstSpaceIndex);
					}
					newQuoteContent = newQuoteContent.TrimStart();
				}
				if (!string.IsNullOrEmpty(saveOptions.ExcludeChars))
				{
					char[] excludedChars = saveOptions.ExcludeChars.ToCharArray();
					foreach (char c in excludedChars)
					{
						newQuoteContent = newQuoteContent.Replace(c.ToString(), "");
					}
				}

				if (!string.IsNullOrEmpty(saveOptions.AppendTextToBegining))
				{
					newQuoteContent = saveOptions.AppendTextToBegining + newQuoteContent;
				}
				if (!string.IsNullOrEmpty(saveOptions.AppendTextToEnd))
				{
					newQuoteContent = newQuoteContent + saveOptions.AppendTextToEnd;
				}

				newQuoteContent = newQuoteContent.Trim();

				if (!string.IsNullOrEmpty(newQuoteContent))
				{
					Quote quote = new()
					{
						Content = newQuoteContent,
						CollectionId = SelectedCollection.CollectionId,
						BookId = SelectedBook?.BookId,
						ChapterId = SelectedChapter?.ChapterId,
						QuoteIndex = quoteIndex,
					};
					lstQuotes.Add(quote);
					quoteIndex++;
				}
			}
			return lstQuotes;
		}

	}
}
