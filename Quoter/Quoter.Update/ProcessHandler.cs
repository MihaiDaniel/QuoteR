using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.Update
{
	public class ProcessHandler
	{
		public bool HasWriteRightOnFolder(string folder)
		{
			try
			{
				string testFile = Path.Combine(folder, "_test");
				File.WriteAllText(testFile, "1");
				File.Delete(testFile);
				return true;
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Can't write test file");
				return false;
			}
		}

		public void KillProcessIfRunning(string processName)
		{
			if (Process.GetProcessesByName(processName).Length > 0)
			{
				Logger.Info("Shutting down process: " + processName);
				Process[] arrProcess = Process.GetProcessesByName(processName);
				foreach (Process process in arrProcess)
				{
					process.Kill();
					process.WaitForExit();
					process.Dispose();
				}
			}
		}

		public void StartProcess(string exePath, string arguments)
		{
			try
			{
				Logger.Info("Starting process: " + exePath);
				Process.Start(exePath, arguments);
			}
			catch (Win32Exception ex)
			{
				Logger.Error(ex, "Error starting process: " + exePath);
			}
		}

		public void RestartProcessAsAdmin(string args)
		{
			System.Diagnostics.ProcessStartInfo StartInfo = new System.Diagnostics.ProcessStartInfo
			{
				UseShellExecute = true, //<- for elevation
				Verb = "runas",  //<- for elevation
				WorkingDirectory = Environment.CurrentDirectory,
				FileName = "Quoter.Update.exe",
				Arguments = args
			};
			System.Diagnostics.Process p = System.Diagnostics.Process.Start(StartInfo);
		}





	}
}
