using Microsoft.EntityFrameworkCore;
using Quoter.App.Forms.Reader;
using Quoter.App.Helpers;
using Quoter.App.Services;
using Quoter.App.Services.Forms;
using Quoter.Framework.Data;
using Quoter.Framework.Entities;
using Quoter.Framework.Helpers;
using Quoter.Framework.Models;
using Quoter.Framework.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Quoter.App.FormsControllers.Reader
{
    public class ReaderFormController : IReaderFormController, INotifyPropertyChanged
	{
		private readonly QuoterContext _context;
		private readonly IQuoteService _quoteService;
		private readonly IFormsManager _formsManager;
		private readonly IStringResources _stringResources;
		private readonly ISettings _settings;
		private readonly ILogger _logger;
		private readonly IThemeService _themeService;

		private IReaderForm _form;

		private ReaderFormOptions _options;

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

		private int SelectedCollectionId { get; set; }

		private int? SelectedBookId { get; set; }

		private int? SelectedChapterId { get; set; }

		public ReaderFormController(QuoterContext context,
									ILogger logger,
									IQuoteService quoteService,
									IFormsManager formsManager,
									IStringResources stringResources,
									ISettings settings,
									IThemeService themeService)
		{
			_context = context;
			_logger = logger;
			_quoteService = quoteService;
			_formsManager = formsManager;
			_stringResources = stringResources;
			_settings = settings;
			_themeService = themeService;
		}

		public void RegisterForm(IReaderForm form)
		{
			_form = form;
			_form.SetSize(_settings.ManageWindowSize);
			_form.SetTheme(_themeService.GetCurrentTheme());
		}

		public void RegisterOptions(ReaderFormOptions options)
		{
			_options = options;
		}

		public async Task EventFormLoadedAsync()
		{
			try
			{
				Collection? collection = await _context.Collections
															.Include(c => c.LstBooks)
																.ThenInclude(c => c.LstChapters)
															.FirstOrDefaultAsync(c => c.CollectionId == _options.CollectionId);

				if (collection == null)
				{
					ShowWarningNoCollectionFound();
				}
				else
				{
					await LoadCollectionAsync(collection);
				}
			}
			catch (Exception ex)
			{
				_logger.Error(ex);
			}
		}

		public async Task EventFormClosingAsync()
		{
			// Nothing to do
		}

		public async Task SetNextChapterAsync()
		{
			try
			{
				bool isNextChapterSet = false;

				if (SelectedChapterId.HasValue)
				{
					isNextChapterSet = await TrySetNextChapter(SelectedChapterId.Value);
				}
				if (!isNextChapterSet && SelectedBookId != null)
				{
					await TrySetNextChapterFromNextBookAsync(SelectedBookId.Value);
				}
			}
			catch (Exception ex)
			{
				_logger.Error(ex);
			}

		}

		public async Task SetPreviousChapterAsync()
		{
			try
			{
				bool iPreviousChapterSet = false;

				if (SelectedChapterId.HasValue)
				{
					iPreviousChapterSet = await TrySetPreviousChapterAsync(SelectedChapterId.Value);
				}
				if (!iPreviousChapterSet && SelectedBookId != null)
				{
					await TrySetNextChapterFromPreviousBookAsync(SelectedBookId.Value);
				}
			}
			catch (Exception ex)
			{
				_logger.Error(ex);
			}
		}

		public async Task SetSelectedBookAsync(Book book)
		{
			try
			{
				SelectedBookId = book.BookId;

				if (book.LstChapters.Any())
				{
					Chapter chapterToSelect = book.LstChapters.First();
					SelectedChapterId = chapterToSelect.ChapterId;
					await SetSelectedChapterAsync(book.LstChapters.First());
				}
				else
				{
					SelectedChapterId = null;
					await LoadQuotesAndSetInForm(SelectedCollectionId, SelectedBookId, SelectedChapterId);
				}
			}
			catch (Exception ex)
			{
				_logger.Error(ex);
			}
		}

		public async Task SetSelectedChapterAsync(Chapter chapter)
		{
			try
			{
				SelectedChapterId = chapter.ChapterId;
				SelectedBookId = chapter.BookId;

				await LoadQuotesAndSetInForm(SelectedCollectionId, SelectedBookId, SelectedChapterId);
			}
			catch (Exception ex)
			{
				_logger.Error(ex);
			}
		}

		#region Private

		private void ShowWarningNoCollectionFound()
		{
			DialogHelper.ShowWarning(_formsManager, _stringResources["WarnReaderNoCollection"], _stringResources["WarnReaderNoCollectionMsg"]);
			_form.SetQuotesContent(string.Empty);
			_form.SetLocationInCollection(string.Empty);
			_form.BuildTreeNavigation(new Collection());
		}

		private async Task LoadCollectionAsync(Collection collection)
		{
			SelectedCollectionId = _options.CollectionId;
			SelectedBookId = _options.BookId;
			SelectedChapterId = _options.ChapterId;

			if (_options.QuoteId != null)
			{
				await LoadQuotesAndSetInForm(_options.CollectionId, _options.BookId, _options.ChapterId);
				string quoteContent = await _context.Quotes.Where(q => q.QuoteId == _options.QuoteId).Select(q => q.Content).FirstAsync();
				_form.ScrollToQuote(quoteContent);
			}
			else
			{
				_form.SetQuotesContent("");
				_form.SetLocationInCollection("");
			}
			_form.BuildTreeNavigation(collection);
		}

		private async Task LoadQuotesAndSetInForm(int collectionId, int? bookId, int? chapterId)
		{
			// Load quotes and set them in the form
			List<Quote> lstQuotes = await _quoteService.GetQuotesAsync(collectionId, bookId, chapterId);
			Quotes = QuoteHelper.GetString(lstQuotes) ?? string.Empty;
			_form.SetQuotesContent(Quotes);

			// Set the location text aswell
			string? bookName = await _context.Books.Where(b => b.BookId == bookId).Select(b => b.Name).FirstOrDefaultAsync();
			string? chapterName = await _context.Chapters.Where(c => c.ChapterId == chapterId).Select(c => c.Name).FirstOrDefaultAsync();
			string locationInCollection = $"{bookName}, {chapterName}";
			_form.SetLocationInCollection(locationInCollection);
		}

		private async Task<bool> TrySetNextChapter(int currentChapterId)
		{
			Book bookOfCurrChapter = await GetBookOfChapterAsyncAsync(currentChapterId);
			// Best ideea would be to use the chapterIndex. But for now we use an uglier method.
			int chapterIndexInBook = bookOfCurrChapter.LstChapters.FindIndex(c => c.ChapterId == currentChapterId);
			if (chapterIndexInBook < bookOfCurrChapter.LstChapters.Count - 1)
			{
				await SetSelectedChapterAsync(bookOfCurrChapter.LstChapters[chapterIndexInBook + 1]);
				return true;
			}
			else
			{
				// This is the last chapter so we must search the next book
				return false;
			}
		}

		private async Task TrySetNextChapterFromNextBookAsync(int currentBookId)
		{
			Collection collectionOfCurrBook = await GetCollectionOfBookAsync(currentBookId);
			int bookIndexInCollection = collectionOfCurrBook.LstBooks.FindIndex(b => b.BookId == currentBookId);
			if (bookIndexInCollection < collectionOfCurrBook.LstBooks.Count - 1)
			{
				int nextBookId = bookIndexInCollection + 1;
				if (collectionOfCurrBook.LstBooks[nextBookId].LstChapters.Any())
				{
					Chapter chapterToSelect = collectionOfCurrBook.LstBooks[nextBookId].LstChapters.First();
					SelectedChapterId = chapterToSelect.ChapterId;
					await SetSelectedChapterAsync(chapterToSelect);
				}
				else
				{
					// Book has no chapters, so just display any quotes if exists
					SelectedChapterId = null;
					SelectedBookId = nextBookId;
					await LoadQuotesAndSetInForm(SelectedCollectionId, SelectedBookId, null);
				}
			}
			else
			{
				// This means this is the last book in the collection so there is nothing to show
			}
		}

		private async Task<bool> TrySetPreviousChapterAsync(int currentChapterId)
		{
			Book bookOfChapter = await GetBookOfChapterAsyncAsync(currentChapterId);

			// Best ideea would be to use the chapterIndex. But for now we use an uglier method.
			int chapterIndexInBook = bookOfChapter.LstChapters.FindIndex(c => c.ChapterId == currentChapterId);
			if (chapterIndexInBook > 0)
			{
				await SetSelectedChapterAsync(bookOfChapter.LstChapters[chapterIndexInBook - 1]);
				return true;
			}
			else
			{
				// This is the first chapter so we must search the previous book
				return false;
			}
		}

		private async Task TrySetNextChapterFromPreviousBookAsync(int currentBookId)
		{
			Collection bookCollection = await GetCollectionOfBookAsync(currentBookId);
			int bookIndexInCollection = bookCollection.LstBooks.FindIndex(b => b.BookId == currentBookId);
			if (bookIndexInCollection > 0)
			{
				int previousBookId = bookIndexInCollection - 1;
				if (bookCollection.LstBooks[previousBookId].LstChapters.Any())
				{
					Chapter chapterToSelect = bookCollection.LstBooks[previousBookId].LstChapters.Last();
					SelectedChapterId = chapterToSelect.ChapterId;
					await SetSelectedChapterAsync(chapterToSelect);
				}
				else
				{
					// Book has no chapters, so just display any quotes if exists
					SelectedChapterId = null;
					SelectedBookId = previousBookId;
					await LoadQuotesAndSetInForm(SelectedCollectionId, SelectedBookId, null);
				}
			}
			else
			{
				// This means this is the first book in the collection so there is nothing to show
			}
		}

		private async Task<Book> GetBookOfChapterAsyncAsync(int chapterId)
		{
			return await _context.Books
								 .Include(b => b.LstChapters)
								 .FirstAsync(b => b.LstChapters.Any(c => c.ChapterId == chapterId));
		}

		private async Task<Collection> GetCollectionOfBookAsync(int bookId)
		{
			return await _context.Collections
								 .Include(c => c.LstBooks)
									.ThenInclude(b => b.LstChapters)
								 .FirstAsync(c => c.LstBooks.Any(b => b.BookId == bookId));
		}

		#endregion Private
	}
}
