using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.Framework.Services
{
	public interface ICollectionService
	{
		Task DeleteCollectionAsync(int collectionId);

		Task DeleteQuotesAsync(int collectionId, int? bookId, int? chapterId);

		Task<string> GetUniqueCollectionNameAsync(string collectionName);
	}
}
