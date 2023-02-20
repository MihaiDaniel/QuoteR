using Quoter.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.Framework.Services
{
	public interface IFileReader
	{
		Task<List<string>> ReadAsync(string filePath);
	}
}
