using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.App.Services
{
	/// <summary>
	/// Interface for managing string resources in the project
	/// </summary>
	public interface IStringResources
	{
		/// <summary>
		/// Returns the localized string resource with the name of <paramref name="name"/>
		/// </summary>
		/// <param name="name">Name of the string resource</param>
		/// <returns>Localized string</returns>
		/// <exception cref="ArgumentNullException">When <paramref name="name"/> is null or empty</exception>
		/// <exception cref="ArgumentException">When no string resource found with <paramref name="name"/></exception>
		string this[string name]
		{
			get;
		}

		string this[string name, params string[] param]
		{
			get;
		}
	}
}
