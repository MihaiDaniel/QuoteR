using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.Framework.Services
{
	public interface IMemoryCache
	{
		void TryAddOrUpdate<T>(string key, T value);
		T GetOrDefault<T>(string key);
		bool Contains(string key);
	}
}
