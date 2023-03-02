using Microsoft.EntityFrameworkCore;
using Quoter.App.Forms;
using Quoter.App.Services;
using Quoter.Framework.Data;
using Quoter.Framework.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.App.FormsControllers.FavouriteQuotes
{
	public class FavouriteQuotesFormController : IFavouriteQuotesFormController
	{
		private const int IdSelectAllItems = -1;

		private readonly QuoterContext _context;
		private readonly IStringResources _stringResources;
		private IFavouriteQuotesForm _form;

		public BindingList<Collection> Collections { get; private set; }

		public BindingList<Book> Books { get; private set; }

		public BindingList<Chapter> Chapters { get; private set; }

		public FavouriteQuotesFormController(QuoterContext context, IStringResources stringResources)
		{
			_context = context;
			_stringResources = stringResources;
			Collections = new BindingList<Collection>();
			Books = new BindingList<Book>();
			Chapters = new BindingList<Chapter>();
		}

		public void RegisterForm(IFavouriteQuotesForm form)
		{
			_form = form;
			LoadCollections();
		}

		public void LoadCollections()
		{
			List<Collection> lstCollections = _context.Collections
				.Include(c => c.LstBooks)
				.ThenInclude(b => b.LstChapters)
				.ToList();

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
					if(c.CollectionId == IdSelectAllItems)
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
			//throw new NotImplementedException();
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
				if(book.BookId == IdSelectAllItems)
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
				if(chapter.ChapterId == IdSelectAllItems)
				{
					continue;
				}
				chapter.IsFavourite = isFavourite;
			}
		}

	}
}
