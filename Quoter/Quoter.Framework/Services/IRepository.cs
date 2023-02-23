using Quoter.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.Framework.Services
{
	public interface IRepository
	{
		Task<List<CollectionModel>> GetCollectionsAsync();

		Task<List<BookModel>> GetBooksAsync(string collectionName);

		Task<BookModel> GetBookAsync(string name);

		Task SaveAsync(CollectionModel collectionModel);

		Task SaveAsync(BookModel collectionModel);
	}
}
