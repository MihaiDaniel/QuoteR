using Microsoft.EntityFrameworkCore;
using Quoter.App.Forms;
using Quoter.App.Forms.Manage;
using Quoter.App.Helpers;
using Quoter.App.Helpers.Extensions;
using Quoter.App.Models;
using Quoter.App.Services;
using Quoter.App.Services.Forms;
using Quoter.Framework.Data;
using Quoter.Framework.Entities;
using Quoter.Framework.Models;
using Quoter.Framework.Services;
using Quoter.Framework.Services.ImportExport;
using Quoter.Shared.Enums;
using System.ComponentModel;

namespace Quoter.App.FormsControllers.FavouriteQuotes
{
	/// <summary>
	/// Controller for the Favourite Tab of the <see cref="ManageForm"/>
	/// </summary>
	public class FavouriteQuotesFormController : IFavouriteQuotesFormController
	{
		/// <summary>
		/// Special item in the list boxes. If this is checked or unchecked it should check/uncheck
		/// all other items present in the respective list (and their children)
		/// </summary>
		private const int IdSelectAllItems = -1;

		private readonly QuoterContext _context;
		private readonly IStringResources _stringResources;
		private readonly IFormsManager _formsManager;
		private readonly IExportService _exportService;
		private readonly IImportService _importService;
		private readonly ISettings _settings;
		private IFavouriteQuotesForm _form;

		public BindingList<Collection> Collections { get; private set; }

		public BindingList<Book> Books { get; private set; }

		public BindingList<Chapter> Chapters { get; private set; }

		public FavouriteQuotesFormController(
			QuoterContext context,
			IStringResources stringResources,
			IFormsManager formsManager,
			ISettings settings,
			IExportService exportService,
			IImportService importService)
		{
			_context = context;
			_stringResources = stringResources;
			_formsManager = formsManager;
			_settings = settings;
			_exportService = exportService;
			_importService = importService;
			Collections = new BindingList<Collection>();
			Books = new BindingList<Book>();
			Chapters = new BindingList<Chapter>();
		}

		public void RegisterForm(IFavouriteQuotesForm form)
		{
			_form = form;
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
			EnumLanguage? languageFilter = null;
			if (_settings.ShowCollectionsBasedOnLanguage)
			{
				languageFilter = LanguageHelper.GetEnumLanguageFromString(_settings.Language);
			}

			IQueryable<Collection> queryCollections = _context.Collections
				.Include(c => c.LstBooks)
				.ThenInclude(b => b.LstChapters)
				.AsQueryable();

			if (languageFilter != null)
			{
				queryCollections = queryCollections.Where(c => c.Language == languageFilter);
			}
			List<Collection> lstCollections = await queryCollections.ToListAsync();

			Collections.Clear();
			Books.Clear();
			Chapters.Clear();

			if (lstCollections.Any())
			{
				Collections.Add(new Collection()
				{
					CollectionId = IdSelectAllItems,
					Name = _stringResources["All"]
				});
			}

			foreach (Collection collection in lstCollections)
			{
				Collections.Add(collection);
			}

			_form.SetChecksFavourites();
		}

		public void ReadCollection(Collection? selectedCollection)
		{
			if (selectedCollection == null || selectedCollection.CollectionId == IdSelectAllItems)
			{
				DialogMessageFormOptions dialogModel = new DialogMessageFormOptions()
				{
					Title = _stringResources["NoCollection"],
					TitleColor = Const.ColorWarn,
					Message = _stringResources["PleaseSelectACollection"],
					MessageBoxButtons = Framework.Enums.EnumDialogButtons.Ok
				};
				_formsManager.ShowDialog<DialogMessageForm>(dialogModel);
			}
			else
			{
				ReaderFormOptions options = new ReaderFormOptions()
				{
					CollectionId = selectedCollection.CollectionId
				};
				_formsManager.ShowAndCloseOthers<ReaderForm>(options);
			}
		}

		public void CollectionSelected(Collection collection)
		{
			if (collection.CollectionId == IdSelectAllItems)
			{
				return;
			}
			Books.Clear();
			Chapters.Clear();
			List<Book> lstBooks = _context.Books
				.Include(b => b.LstChapters)
				.Where(b => b.CollectionId == collection.CollectionId).ToList();

			if (lstBooks.Any())
			{
				Books.Add(new Book()
				{
					BookId = IdSelectAllItems,
					CollectionId = collection.CollectionId, // To know which collection this check mark represents
					Name = _stringResources["All"]
				});
			}

			foreach (Book book in lstBooks)
			{
				Books.Add(book);
			}

			_form.SetChecksFavourites();
		}

		public void CollectionCheckChanged(Collection collection, bool isChecked)
		{
			if (collection.CollectionId == IdSelectAllItems)
			{
				foreach (Collection c in Collections)
				{
					if (c.CollectionId == IdSelectAllItems)
					{
						continue;
					}
					c.IsFavourite = isChecked;
					SetBooksIsFavourite(c.LstBooks, isChecked);
				}
			}
			else
			{
				collection.IsFavourite = isChecked;
				SetBooksIsFavourite(collection.LstBooks, isChecked);
			}
			_context.SaveChanges();
			_form.SetChecksFavourites();
		}

		public void BookSelected(Book book)
		{
			if (book.BookId == IdSelectAllItems)
			{
				return;
			}

			Chapters.Clear();
			List<Chapter> lstChapters = _context.Chapters.Where(c => c.BookId == book.BookId).ToList();

			if (lstChapters.Any())
			{
				Chapters.Add(new Chapter()
				{
					ChapterId = IdSelectAllItems,
					BookId = book.BookId, // To know which book this check mark represents
					Name = _stringResources["All"]
				});
			}

			foreach (Chapter chapter in lstChapters)
			{
				Chapters.Add(chapter);
			}

			_form.SetChecksFavourites();
		}

		public void BookCheckChanged(Book book, bool isChecked)
		{
			if (book.BookId == IdSelectAllItems)
			{
				SetBooksIsFavourite(Books, isChecked);
			}
			else
			{
				book.IsFavourite = isChecked;
				SetChaptersIsFavourite(book.LstChapters, isChecked);
			}

			// Find the parent collection and set it's favourite status
			Collection collection = Collections.Where(c => c.CollectionId == book.CollectionId).First();
			if (CollectionIsFavouriteBookCount(collection) > 0)
			{
				collection.IsFavourite = true;
			}
			else
			{
				collection.IsFavourite = false;
			}

			_context.SaveChanges();
			_form.SetChecksFavourites();
		}

		public void ChapterCheckChanged(Chapter chapter, bool isChecked)
		{
			if (chapter.ChapterId == IdSelectAllItems)
			{
				SetChaptersIsFavourite(Chapters, isChecked);
			}
			else
			{
				chapter.IsFavourite = isChecked;
			}

			// Find parent book and set it's favourite status
			Book book = Books.Where(b => b.BookId == chapter.BookId).First();
			if (BookIsFavouriteChapterCount(book) > 0)
			{
				book.IsFavourite = true;
			}
			else
			{
				book.IsFavourite = false;
			}
			// Then find the parent collection and set it's favourite status
			Collection collection = Collections.Where(c => c.CollectionId == book.CollectionId).First();
			if (CollectionIsFavouriteBookCount(collection) > 0)
			{
				collection.IsFavourite = true;
			}
			else
			{
				collection.IsFavourite = false;
			}

			_context.SaveChanges();
			_form.SetChecksFavourites();
		}


		public void ChapterSelected(Chapter chapter)
		{
			// Nothing to do
		}

		private int BookIsFavouriteChapterCount(Book book)
		{
			return book.LstChapters.Count(b => b.IsFavourite == true && b.BookId != IdSelectAllItems);
		}

		private int CollectionIsFavouriteBookCount(Collection collection)
		{
			return collection.LstBooks.Count(c => c.IsFavourite == true && c.CollectionId != IdSelectAllItems);
		}

		private void SetBooksIsFavourite(IEnumerable<Book> books, bool isFavourite)
		{
			foreach (Book book in books)
			{
				if (book.BookId == IdSelectAllItems)
				{
					continue;
				}
				book.IsFavourite = isFavourite;
				if (book.LstChapters.Any())
				{
					SetChaptersIsFavourite(book.LstChapters, isFavourite);
				}
			}
		}

		private void SetChaptersIsFavourite(IEnumerable<Chapter> chapters, bool isFavourite)
		{
			foreach (Chapter chapter in chapters)
			{
				if (chapter.ChapterId == IdSelectAllItems)
				{
					continue;
				}
				chapter.IsFavourite = isFavourite;
			}
		}

		public void Export(bool isExportOnlyFavourites)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = "Quoter file|.qter";
			saveFileDialog.Title = _stringResources["ChooseExportFilename"];
			saveFileDialog.AddExtension = true;
			saveFileDialog.DefaultExt = ".qter";
			saveFileDialog.FileName = _stringResources["QuotesCollection"] + "-" + DateTime.Now.ToString("yyyyMMdd-HHmmss");

			if (isExportOnlyFavourites)
			{
				// Show error message if no favourite collection is set
				if (!Collections.Any(c => c.IsFavourite == true))
				{
					_formsManager.ShowDialogErr(_stringResources["ErrCantExport"], _stringResources["ErrCantExportMsg"]);
					return;
				}
			}

			DialogResult result = saveFileDialog.ShowDialog();
			if (result != DialogResult.OK)
			{
				return;
			}
			string fileName = saveFileDialog.FileName;
			string dirPath = Path.GetDirectoryName(fileName);
			if (!Directory.Exists(dirPath) || string.IsNullOrWhiteSpace(Path.GetFileName(fileName)))
			{
				// Show error if path is invalid
				_formsManager.ShowDialogErr(_stringResources["ErrCantExport"], _stringResources["ErrCantExportMsgBadFileName"]);
			}
			else
			{
				_formsManager.ShowDialogOk(_stringResources["Exporting"], _stringResources["ExportingInBackground"]);
				ExportParameters exportParameters = new ExportParameters()
				{
					IsExportOnlyFavouriteCollections = isExportOnlyFavourites,
					ExportFilePath = fileName,
					Language = TryGetExportLanguage()
				};
				_exportService.QueueExportJob(exportParameters);
			}
		}

		private EnumLanguage? TryGetExportLanguage()
		{
			if (_settings.ShowCollectionsBasedOnLanguage)
			{
				return LanguageHelper.GetEnumLanguageFromString(_settings.Language);
			}
			return null;
		}

		public void Import(bool isImportMerge, bool isImportIgnoreLang)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Quoter file (.qter) |*.qter";
			openFileDialog.Multiselect = true;
			openFileDialog.Title = _stringResources["ChooseImportFilename"];
			DialogResult result = openFileDialog.ShowDialog();
			if (result != DialogResult.OK)
			{
				return;
			}
			string[] fileNames = openFileDialog.FileNames;
			foreach (string fileName in fileNames)
			{
				if (string.IsNullOrWhiteSpace(fileName) || Path.GetExtension(fileName) != ".qter")
				{
					_formsManager.ShowDialogErr(_stringResources["ErrCantImport"], _stringResources["ErrCantImportMsgBadFileName"]);
					return;
				}
			}
			_formsManager.ShowDialogOk(_stringResources["Importing"], _stringResources["ImportingInBackground"]);

			ImportParameters importParameters = new ImportParameters()
			{
				Files = fileNames,
				IsIgnoreLanguage = isImportIgnoreLang,
				IsMergeCollections = isImportMerge,
				Language = LanguageHelper.GetEnumLanguageFromString(_settings.Language)
			};
			_importService.QueueImportJob(importParameters);
		}

	}
}
