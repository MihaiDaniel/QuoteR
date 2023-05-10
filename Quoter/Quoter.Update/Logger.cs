using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quoter.Update
{
	public static class Logger
	{
		public static List<string> LstLines = new List<string>();

		public static void Error(string message)
		{
			LstLines.Add(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss,fff") + " ERROR: " + message);
			LstLines.Add(Environment.NewLine);
		}

		public static void Error(Exception exception, string message) 
		{
			LstLines.Add(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss,fff") + " ERROR: " + message);
			LstLines.Add(Environment.NewLine);
			LstLines.Add($"Exception: {exception}");
		}

		public static void Info(string message) 
		{
			LstLines.Add(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss,fff") + " INFO: " + message);
			LstLines.Add(Environment.NewLine);
		}


		public static void Write()
		{
			try
			{
				LstLines.Add(DateTime.Now.ToString());
				LstLines.Add(Environment.NewLine);
				string logPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Log.txt");
				File.WriteAllLines(logPath, LstLines);
			}
			catch 
			{
				// nothing to do
			}
		}
	}
}
