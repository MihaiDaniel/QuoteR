using Microsoft.EntityFrameworkCore;
using Quoter.App.Forms;
using Quoter.App.Forms.Manage;
using Quoter.App.Helpers;
using Quoter.App.Models;
using Quoter.App.Services;
using Quoter.App.Services.Forms;
using Quoter.Framework.Data;
using Quoter.Framework.Entities;
using Quoter.Shared.Enums;
using Quoter.Framework.Helpers;
using Quoter.Framework.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Quoter.Framework.Enums;

namespace Quoter.App.FormsControllers.EditQuotes
{
	/// <summary>
	/// Controller for the edit/add/delete quotes Tab of the <see cref="ManageForm"/>
	/// </summary>
	public class EditQuotesFormController : IEditQuotesFormController, INotifyPropertyChanged
	{
		private readonly QuoterContext _context;
		private readonly IStringResources _stringResources;
		private readonly IFormsManager _formsManager;
		private readonly ISettings _settings;
		private readonly ILogger _logger;

		private readonly Regex _regexQuote;

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
				}
				_logger.Debug($"SelectedCollection:{SelectedCollection?.CollectionId} {SelectedCollection?.Name}");
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
				}
				_logger.Debug($"SelectedBook:{SelectedBook?.BookId} {SelectedBook?.Name}");
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
				}
				_logger.Debug($"SelectedChapter:{SelectedChapter?.BookId} {SelectedChapter?.Name}");
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
					_logger.Debug($"Quotes set");
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
										ILogger logger)
		{
			_context = quoterContext;
			_stringResources = stringResources;
			_formsManager = formManager;
			_settings = settings;
			_logger = logger;

			// Match any number and punctuation mark
			_regexQuote = new Regex("^[0-9*\\+.,;-]+$");

			Collections = new BindingList<Collection>();
			Books = new BindingList<Book>();
			Chapters = new BindingList<Chapter>();

			System.Diagnostics.Debug.WriteLine($"EditQuotesFormController Context: {_context.InstanceID}");
		}

		public void RegisterForm(IEditQuotesForm editQuotesForm)
		{
			_form = editQuotesForm;
			_form.SetBooksControlsState(EnumCrudStates.None);
			_form.SetChaptersControlsState(EnumCrudStates.None);
		}

		public async Task EventFormLoadedAsync()
		{
			await LoadCollections();
		}

		public Task EventFormClosingAsync()
		{
			// Nothing to do
			return Task.CompletedTask;
		}

		public async Task LoadCollections()
		{
			_logger.Debug("LoadCollections()");

			Collections.Clear();
			SelectedCollection = null;

			Books.Clear();
			SelectedBook = null;

			Chapters.Clear();
			SelectedChapter = null;

			Quotes = string.Empty;

			List<Collection> lstCollections;
			if (_settings.ShowCollectionsBasedOnLanguage)
			{
				EnumLanguage language = LanguageHelper.GetEnumLanguageFromString(_settings.Language);
				lstCollections = await _context.Collections
					.Where(c => c.Language == language || c.Language == default).ToListAsync();
			}
			else
			{
				lstCollections = await _context.Collections.ToListAsync();
			}

			if (lstCollections.Any())
			{
				foreach (Collection collection in lstCollections)
				{
					Collections.Add(collection);
				}
				// After Collection are added SelectedValueChanged should be triggered, and from there we load remaining data
			}
			else
			{
				_form.SetBooksControlsState(EnumCrudStates.None);
				_form.SetChaptersControlsState(EnumCrudStates.None);
			}
		}

		public async Task LoadCollectionBooksOrQuotes()
		{
			_logger.Debug("LoadCollectionBooksOrQuotes()");

			if (string.IsNullOrEmpty(SelectedCollection?.Name))
			{
				return;
			}
			Books.Clear();
			SelectedBook = null;
			Chapters.Clear();
			SelectedChapter = null;
			Quotes = string.Empty;

			List<Book> lstBooks = await _context.Books.Where(b => b.CollectionId == SelectedCollection.CollectionId)
												.ToListAsync();
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
				await LoadQuotes(); // Load quotes that are in collection but not assigned to any book
				_form.SetBooksControlsState(EnumCrudStates.Add);
				_form.SetChaptersControlsState(EnumCrudStates.None);
			}
		}

		public async Task LoadBookChaptersOrQuotes()
		{
			_logger.Debug("LoadBookChaptersOrQuotes()");
			if (string.IsNullOrEmpty(SelectedBook?.Name))
			{
				return;
			}
			Chapters.Clear();
			SelectedChapter = null;
			Quotes = string.Empty;

			List<Chapter> lstChapters = await _context.Chapters.Where(c => c.BookId == SelectedBook.BookId)
												.ToListAsync();
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
				await LoadQuotes(); // Load quotes that are in this book, but not assinged to a chapter
				_form.SetChaptersControlsState(EnumCrudStates.Add);
			}
		}

		public async Task LoadQuotes()
		{
			_logger.Debug("LoadQuotes()");
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

			List<Quote> lstQuotes = await queryQuotes.ToListAsync();

			Quotes = QuoteHelper.GetString(lstQuotes) ?? string.Empty;
		}

		#region Add, Edit, Delete collections

		public async Task AddCollection()
		{
			IDialogReturnable result = _formsManager.ShowDialog<DialogInputForm>(new DialogOptions()
			{
				Title = _stringResources["NewCollection"],
				Message = _stringResources["NewCollectionName"],
			});

			if (result.DialogResult == DialogResult.OK)
			{
				string newCollectionName = result.StringResult;
				if (Collections.Any(c => c.Name == newCollectionName))
				{
					ShowDialogWarn(_stringResources["NameAlreadyTaken"], _stringResources["CollectionAlreadyExists"]);
					await AddCollection();
				}
				else
				{
					Collection newCollection = new()
					{
						Name = result.StringResult,
						Language = LanguageHelper.GetEnumLanguageFromString(_settings.Language),
					};
					_context.Collections.Add(newCollection);
					await _context.SaveChangesAsync();

					Collections.Add(newCollection);
					SelectedCollection = newCollection;
					await LoadCollectionBooksOrQuotes();

					await _form.SetStatus(_stringResources["CollectionCreated", newCollection.Name], Constants.ColorOk);
				}
			}
		}

		public async Task EditCollection()
		{
			if (SelectedCollection == null)
			{
				return;
			}
			IDialogReturnable result = _formsManager.ShowDialog<DialogInputForm>(new DialogOptions()
			{
				Title = _stringResources["EditCollection"],
				Message = _stringResources["EditCollectionMsg"],
				Value = SelectedCollection.Name
			});

			if (result.DialogResult == DialogResult.OK)
			{
				string newCollectionName = result.StringResult;
				if (Collections.Any(c => c.Name == newCollectionName && c.Name != SelectedCollection.Name))
				{
					ShowDialogWarn(_stringResources["NameAlreadyTaken"], _stringResources["CollectionAlreadyExists"]);
					await EditCollection();
				}
				else
				{
					SelectedCollection.Name = result.StringResult;
					await _context.SaveChangesAsync();

					await LoadCollections();

					SelectedCollection = Collections.First(c => c.Name == SelectedCollection.Name);
					await LoadCollectionBooksOrQuotes();
				}
			}
		}

		public async Task DeleteCollection()
		{
			if (SelectedCollection == null)
			{
				return;
			}
			IDialogReturnable result = _formsManager.ShowDialog<DialogMessageForm>(new DialogOptions()
			{
				Title = _stringResources["DeleteCollection"],
				Message = _stringResources["DeleteCollectionMsg", SelectedCollection.Name],
				MessageBoxButtons = EnumDialogButtons.OkCancel
			});

			if (result.DialogResult == DialogResult.OK)
			{
				try
				{
					// For some reason using linq triggers a FK exception even though there is no problem on SQLite side
					await _context.Database.ExecuteSqlRawAsync(
						$"DELETE FROM {nameof(_context.Collections)} WHERE {nameof(Collection.CollectionId)} = {SelectedCollection.CollectionId}");

					//Collection collectionToRemove = await _context.Collections.FirstAsync(c => c.CollectionId == SelectedCollection.CollectionId);
					//_context.Collections.Remove(collectionToRemove);
					//await _context.SaveChangesAsync();

					await LoadCollections();
				}
				catch (Exception ex)
				{
					_logger.Error(ex);
				}
				//SelectedCollection = Collections.FirstOrDefault();
			}
		}

		#endregion  Add, Edit, Delete collections

		#region  Add, Edit, Delete books

		public async Task AddBook()
		{
			if (string.IsNullOrEmpty(SelectedCollection?.Name))
			{
				return;
			}
			IDialogReturnable result = _formsManager.ShowDialog<DialogInputForm>(new DialogOptions()
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
				ShowDialogWarn(_stringResources["NameAlreadyTaken"], _stringResources["BookAlreadyExists"]);
				await AddBook();
			}
			else
			{
				Book newBook = new()
				{
					Name = result.StringResult,
					CollectionId = SelectedCollection.CollectionId
				};
				_context.Books.Add(newBook);
				await _context.SaveChangesAsync();

				// Check for 'loose' quotes and add them to the new book
				// so after the ui is updated we load the quotes
				await CheckForQuotesInCollectionWithNoAssignedBookAndAssignThem(newBook);

				Books.Add(newBook);
				SelectedBook = newBook;
				await LoadBookChaptersOrQuotes();

				_form.SetBooksControlsState(EnumCrudStates.ViewAddEditDelete);
				await _form.SetStatus(_stringResources["BookCreated", newBook.Name, SelectedCollection.Name], Constants.ColorOk);
			}
		}

		private async Task CheckForQuotesInCollectionWithNoAssignedBookAndAssignThem(Book book)
		{
			if (SelectedCollection == null)
			{
				return;
			}
			List<Quote> lstQuotes = await _context.Quotes.Where(q => q.CollectionId == SelectedCollection.CollectionId
												&& q.BookId == null
												&& q.ChapterId == null)
												.ToListAsync();
			if (lstQuotes.Any())
			{
				foreach (Quote quote in lstQuotes)
				{
					quote.BookId = book.BookId;
				}
				await _context.SaveChangesAsync();
				_formsManager.ShowDialog<DialogMessageForm>(new DialogOptions()
				{
					Title = _stringResources["QuotesAddedToBook"],
					Message = _stringResources["QuotesAddedToBookMsg", book.Name],
					DialogTheme = Enums.DialogOptionsTheme.Default,
				});
			}
		}

		public async Task EditBook()
		{
			if (string.IsNullOrEmpty(SelectedBook?.Name))
			{
				return;
			}
			IDialogReturnable result = _formsManager.ShowDialog<DialogInputForm>(new DialogOptions()
			{
				Title = _stringResources["EditBook"],
				Message = _stringResources["EditBookMsg"],
				Value = SelectedBook.Name
			});

			if (result.DialogResult == DialogResult.OK)
			{
				string newBookName = result.StringResult;
				if (Books.Any(b => b.Name == newBookName && b.Name != SelectedBook.Name))
				{
					ShowDialogWarn(_stringResources["NameAlreadyTaken"], _stringResources["BookAlreadyExists"]);
					await EditBook();
				}
				else
				{
					SelectedBook.Name = result.StringResult;
					await _context.SaveChangesAsync();

					await ReloadBooks(SelectedCollection.CollectionId, SelectedBook.Name);
					await ReloadChapters(SelectedBook.BookId, SelectedChapter.Name);
				}
			}
		}

		private async Task ReloadBooks(int collectionId, string selectedName)
		{
			Books.Clear();
			List<Book> lstBooks = await _context.Books.Where(b => b.CollectionId == collectionId)
												.ToListAsync();
			if (lstBooks.Any())
			{
				foreach (Book book in lstBooks)
				{
					Books.Add(book);
				}
				_form.SetBooksControlsState(EnumCrudStates.ViewAddEditDelete);
				SelectedBook = Books.First(c => c.Name == selectedName);
			}
		}

		public async Task DeleteBook()
		{
			if (string.IsNullOrEmpty(SelectedBook?.Name))
			{
				return;
			}
			IDialogReturnable result = _formsManager.ShowDialog<DialogMessageForm>(new DialogOptions()
			{
				Title = _stringResources["DeleteBook"],
				Message = _stringResources["DeleteBookMsg", SelectedBook.Name],
				MessageBoxButtons = EnumDialogButtons.OkCancel
			});

			if (result.DialogResult == DialogResult.OK)
			{
				_context.Books.Remove(SelectedBook);
				await _context.SaveChangesAsync();
				await LoadCollectionBooksOrQuotes();

				//SelectedBook = Books.FirstOrDefault();
			}
		}

		#endregion  Add, Edit, Delete books

		#region Add, Edit, Delete chapters

		public async Task AddChapter()
		{
			if (string.IsNullOrEmpty(SelectedBook?.Name))
			{
				return;
			}
			IDialogReturnable result = _formsManager.ShowDialog<DialogInputForm>(new DialogOptions()
			{
				Title = _stringResources["NewChapter"],
				Message = _stringResources["NewChapterName", SelectedBook.Name],
			});

			if (result.DialogResult == DialogResult.OK)
			{
				string newChapterName = result.StringResult;
				if (Chapters.Any(c => c.Name == newChapterName))
				{
					ShowDialogWarn(_stringResources["NameAlreadyTaken"], _stringResources["ChapterAlreadyExists"]);
					await AddChapter();
				}
				else
				{
					Chapter newChapter = new()
					{
						Name = result.StringResult,
						BookId = SelectedBook.BookId
					};
					_context.Chapters.Add(newChapter);
					await _context.SaveChangesAsync();

					await CheckForQuotesInBookWithNoAssignedChapterAndAssignThem(newChapter);

					Chapters.Add(newChapter);
					SelectedChapter = newChapter;
					await LoadQuotes();

					await _form.SetStatus(_stringResources["ChapterCreated", newChapter.Name, SelectedBook.Name], Constants.ColorOk);
					_form.SetChaptersControlsState(EnumCrudStates.ViewAddEditDelete);
				}
			}
		}

		private async Task CheckForQuotesInBookWithNoAssignedChapterAndAssignThem(Chapter newChapter)
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
				await _context.SaveChangesAsync();
				_formsManager.ShowDialog<DialogMessageForm>(new DialogOptions()
				{
					Title = _stringResources["QuotesAddedToChapter"],
					Message = _stringResources["QuotesAddedToChapterMsg", SelectedBook.Name, newChapter.Name],
					DialogTheme = Enums.DialogOptionsTheme.Default
				});
			}
		}

		public async Task EditChapter()
		{
			if (string.IsNullOrEmpty(SelectedChapter?.Name))
			{
				return;
			}
			IDialogReturnable result = _formsManager.ShowDialog<DialogInputForm>(new DialogOptions()
			{
				Title = _stringResources["EditChapter"],
				Message = _stringResources["EditChapterMsg"],
				Value = SelectedChapter.Name
			});

			if (result.DialogResult == DialogResult.OK)
			{
				string newChapterName = result.StringResult;
				if (Chapters.Any(c => c.Name == newChapterName && c.Name != SelectedChapter.Name))
				{
					ShowDialogWarn(_stringResources["NameAlreadyTaken"], _stringResources["ChapterAlreadyExists"]);
					await EditChapter();
				}
				else
				{
					SelectedChapter.Name = result.StringResult;
					await _context.SaveChangesAsync();

					await ReloadChapters(SelectedBook.BookId, SelectedChapter.Name);
				}
			}
		}

		private async Task ReloadChapters(int bookId, string selectedName)
		{
			Chapters.Clear();
			List<Chapter> lstChapters = await _context.Chapters.Where(c => c.BookId == bookId)
												.ToListAsync();
			if (lstChapters.Any())
			{
				foreach (Chapter chapter in lstChapters)
				{
					Chapters.Add(chapter);
				}
				SelectedChapter = Chapters.First(c => c.Name == selectedName);
				await LoadQuotes();

			}
		}

		public async Task DeleteChapter()
		{
			if (string.IsNullOrEmpty(SelectedChapter?.Name))
			{
				return;
			}
			IDialogReturnable result = _formsManager.ShowDialog<DialogMessageForm>(new DialogOptions()
			{
				Title = _stringResources["DeleteChapter"],
				Message = _stringResources["DeleteChapterMsg", SelectedChapter.Name],
				MessageBoxButtons = EnumDialogButtons.OkCancel
			});

			if (result.DialogResult == DialogResult.OK)
			{
				_context.Chapters.Remove(SelectedChapter);
				await _context.SaveChangesAsync();
				await LoadBookChaptersOrQuotes();
			}
		}

		#endregion  Add, Edit, Delete chapters

		public async Task QuickAdd()
		{
			// Quick Add chapter
			bool parseOk = int.TryParse(SelectedChapter?.Name, out int chapterNo);
			if (parseOk)
			{
				Chapter newChapter = new()
				{
					Name = (chapterNo + 1).ToString(),
					BookId = SelectedBook.BookId
				};
				_context.Chapters.Add(newChapter);
				await _context.SaveChangesAsync();
				Chapters.Add(newChapter);
				SelectedChapter = newChapter;
				await LoadQuotes();
				_form.SetChaptersControlsState(EnumCrudStates.ViewAddEditDelete);
			}
		}

		public async Task AddQuotes(QuoteSaveOptions saveOptions)
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

				List<Quote> quotesToDelete = await queryQuotes.ToListAsync();
				if (quotesToDelete.Any())
				{
					_context.Quotes.RemoveRange(quotesToDelete);
					await _context.SaveChangesAsync();
				}

				// Then add the quotes in the database
				_context.Quotes.AddRange(lstQuotes);
				await _context.SaveChangesAsync();
				await LoadQuotes();

				await _form.SetStatus(_stringResources["QuotesSaved"], Constants.ColorOk);
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
				string newQuoteContent = ApplyQuoteSaveOptions(quoteContent, saveOptions);

				if (_regexQuote.IsMatch(newQuoteContent))
				{
					continue; // If it only has numbers and symbols it might not be a valid quote so ignore it.
				}

				if (!string.IsNullOrWhiteSpace(newQuoteContent))
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

		private string ApplyQuoteSaveOptions(string quoteContent, QuoteSaveOptions saveOptions)
		{
			string newQuoteContent = quoteContent;

			// Apply trim start untill first string detected
			if (!string.IsNullOrEmpty(saveOptions.TrimUntil))
			{
				newQuoteContent = newQuoteContent.Trim();
				int firstSpaceIndex = newQuoteContent.IndexOf(saveOptions.TrimUntil);
				if (firstSpaceIndex >= 0 && newQuoteContent.Length > firstSpaceIndex + 1)
				{
					newQuoteContent = newQuoteContent.Substring(firstSpaceIndex + 1, newQuoteContent.Length - firstSpaceIndex - 1);
				}
				newQuoteContent = newQuoteContent.TrimStart();
			}
			// Delete all excluded chars
			if (!string.IsNullOrEmpty(saveOptions.ExcludeChars))
			{
				char[] excludedChars = saveOptions.ExcludeChars.ToCharArray();
				foreach (char c in excludedChars)
				{
					newQuoteContent = newQuoteContent.Replace(c.ToString(), "");
				}
			}
			// Replace chars with replacements
			// Ex: Ab,5,*	->	 ab, ,&
			if (!string.IsNullOrWhiteSpace(saveOptions.ReplaceChars))
			{
				string[] replacebleChars = saveOptions.ReplaceChars.Split(",", StringSplitOptions.None).ToArray();
				string[] replacements = saveOptions.ReplacedCharsReplacement.Split(",", StringSplitOptions.None).ToArray();

				if (replacebleChars.Length != replacements.Length)
				{
					_logger.Warn($"{nameof(ApplyQuoteSaveOptions)} Can't apply replacements! No valid replacements for all strings!");
				}
				else
				{
					for (int i = 0; i < replacebleChars.Length; i++)
					{
						string strToReplce = replacebleChars[i];
						string newStr = replacements[i];
						if (strToReplce == "\\n" || strToReplce == "\\r\\n")
						{
							newQuoteContent = newQuoteContent.Replace(strToReplce, Environment.NewLine);
						}
						else
						{
							newQuoteContent = newQuoteContent.Replace(strToReplce, newStr);
						}
					}
				}
			}

			// If it only has numbers and symbols it might not be a valid quote so don't apply appends and just return.
			if (_regexQuote.IsMatch(newQuoteContent))
			{
				return newQuoteContent;
			}

			// Apply append to start and end
			if (!string.IsNullOrEmpty(saveOptions.AppendTextToBegining))
			{
				newQuoteContent = saveOptions.AppendTextToBegining + newQuoteContent;
			}
			if (!string.IsNullOrEmpty(saveOptions.AppendTextToEnd))
			{
				newQuoteContent = newQuoteContent + saveOptions.AppendTextToEnd;
			}
			// Final trim and return
			newQuoteContent = newQuoteContent.Trim();
			return newQuoteContent;
		}

		private void ShowDialogWarn(string title, string message)
		{
			_formsManager.ShowDialog<DialogMessageForm>(new DialogOptions()
			{
				Title = title,
				Message = message,
				DialogTheme = Enums.DialogOptionsTheme.Warning
			});
		}
	}
}
