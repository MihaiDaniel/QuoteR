using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.Update.Helpers
{
	public class Const
	{
		/// <summary>
		/// Name of the dir where backup will be stored when doing an update
		/// </summary>
		public const string BackupDir = "Quoter.Update";

		public const string DirToSkip = "Quoter.Updater";

		/// <summary>
		/// Name of the executable of this project
		/// </summary>
		public const string UpdaterExe = "Quoter.Update.exe";
	}
}
