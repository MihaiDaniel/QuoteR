using Quoter.Update.Services;
using System.Text;

namespace Quoter.Update
{
	public partial class Main : Form
	{
		private UpdateService _updateHandler;
		private ProcessService _processHandler;

		private string InstallFolderPath;
		private string ApplicationExeName;
		private string ZipUpdateFolderPath;
		private bool IsRestartedAsAdmin;

		private string[] _args;

		public Main(string[] args)
		{
			InitializeComponent();
			_args = args;
			_updateHandler = new UpdateService();
			_processHandler = new ProcessService();
		}

		private async void Main_Load(object sender, EventArgs e)
		{
			// Show update window in bottom right corner
			Rectangle rectangle = Screen.FromControl(this).Bounds;
			this.Location = new Point(rectangle.Width - this.Width - 20, rectangle.Height - this.Height - 30);

			// Set a timer of 1 min to auto-close in case something bad happens
			System.Windows.Forms.Timer updateTimeoutTimer = new System.Windows.Forms.Timer();
			updateTimeoutTimer.Interval = 60000;
			updateTimeoutTimer.Tick += (sender, e) => EventUpdateTimeout();
			updateTimeoutTimer.Start();

			// Update in background
			await Task.Run(async () => { await UpdateApplicationAsync(); });
		}

		private void Main_FormClosing(object sender, FormClosingEventArgs e)
		{
			// Prevent user from closing the form
			if (e.CloseReason == CloseReason.UserClosing)
			{
				e.Cancel = true;
			}
		}

		private void EventUpdateTimeout()
		{
			Application.Exit();
		}

		private async Task UpdateApplicationAsync()
		{
			try
			{
				SetupArguments();
				if (VerifyArguments())
				{
					if (_processHandler.HasWriteRightOnFolder(InstallFolderPath))
					{
						bool isUpdateSuccesfull = _updateHandler.TryUpdate(InstallFolderPath, ApplicationExeName, ZipUpdateFolderPath);
						if (isUpdateSuccesfull)
						{
							_processHandler.StartProcess(Path.Combine(InstallFolderPath, ApplicationExeName), "-u=success");
						}
						else
						{
							_processHandler.StartProcess(Path.Combine(InstallFolderPath, ApplicationExeName), "-u=fail");
						}
					}
					else if (!IsRestartedAsAdmin)
					{
						_processHandler.RestartProcessAsAdmin($"-i {InstallFolderPath} -e {ApplicationExeName} -u {ZipUpdateFolderPath} -r true");
					}
					else
					{
						Logger.Error("Cannot update the folder even with admin rights");
					}
				}
				else
				{
					Logger.Error("Arguments are not valid, closing.");
				}
			}
			catch(Exception ex)
			{
				Logger.Error(ex, "General error.");
			}
			finally
			{
				try { await Logger.WriteAsync(); } catch { /* Nothing to do */ }
				if (this.InvokeRequired)
				{
					this.BeginInvoke(() =>
					{
						Application.Exit();
					});
				}
			}

		}

		private void SetupArguments()
		{
			StringBuilder sb = new StringBuilder();
			foreach(string? arg in _args)
			{
				sb.Append(arg);
				sb.Append(" ");
			}
			//0123456789
			// -i C:\My\Path to\install folder -e MyExeName -u C:\My\Path to\update.zip -r false
			string str = sb.ToString().Trim();

			InstallFolderPath = str.Substring(str.IndexOf("-i") + 2, str.IndexOf("-e") - 2).Trim();
			ApplicationExeName = str.Substring(str.IndexOf("-e") + 2, str.IndexOf("-u") - str.IndexOf("-e") - 3).Trim();
			ZipUpdateFolderPath = str.Substring(str.IndexOf("-u") + 2, str.IndexOf("-r") - str.IndexOf("-u") - 3).Trim();
			IsRestartedAsAdmin = str.Substring(str.IndexOf("-r") + 2, str.Length - str.IndexOf("-r") - 2).Trim().ToLower() == "true";

			Logger.Info("Starting arguments: ");
			Logger.Info(InstallFolderPath);
			Logger.Info(ApplicationExeName);
			Logger.Info(ZipUpdateFolderPath);
			Logger.Info(IsRestartedAsAdmin.ToString());
		}

		private bool VerifyArguments()
		{
			if (string.IsNullOrEmpty(InstallFolderPath)
				|| string.IsNullOrEmpty(ApplicationExeName)
				|| string.IsNullOrEmpty(ZipUpdateFolderPath))
			{
				return false;
			}
			return true;
		}
	}
}