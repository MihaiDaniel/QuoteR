using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.App.Helpers.Extensions
{
	public static class Extensions
	{
		public static T InvokeIfRequiredReturn<T>(this ISynchronizeInvoke control, Func<T> function)
		{
			if (control.InvokeRequired)
			{
				var args = new object[0];
				return (T)control.Invoke(function, args);
			}
			else
			{
				return function();
			}
		}

		/// <summary>
		/// Checks if the UI object needs an Invoke before making any changes.
		/// In case the call is made from a separate thread
		/// </summary>
		/// <param name="obj">Object, usually a control</param>
		/// <param name="action">Action to invoke</param>
		public static void InvokeIfRequired(this ISynchronizeInvoke obj, MethodInvoker action)
		{
			if (obj.InvokeRequired)
			{
				var args = new object[0];
				obj.Invoke(action, args);
			}
			else
			{
				action();
			}
		}
	}
}
