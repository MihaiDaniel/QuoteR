using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.App.Services
{
	public interface ISettings
	{
		/// <summary>
		/// Gets the value of a setting thread-safe based on the <paramref name="key"/>
		/// </summary>
		/// <typeparam name="T">Type expected of the setting value</typeparam>
		/// <param name="key">Key of the setting</param>
		/// <returns></returns>
		T Get<T>(string key);

		/// <summary>
		/// Sets the value of a setting thread-safe if it's different than the current value and saves changes.
		/// </summary>
		/// <typeparam name="T">Type expected of the setting value</typeparam>
		/// <param name="key">Key of the setting</param>
		/// <param name="value">Value to set the setting to</param>
		void Set<T>(string key, T value);
	}
}
