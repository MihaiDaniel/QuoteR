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
		private string UpdateId;
		private string ZipUpdateFolderPath;
		private bool IsSilent;
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
				if (this.InvokeRequired)
				{
					this.Invoke(new Action(() =>
					{
						if(IsSilent)
						{
							this.Visible = false;
						}
						else
						{
							this.Visible = true;
						}
					}));
				}
				if (VerifyArguments())
				{
					if (_processHandler.HasWriteRightOnFolder(InstallFolderPath))
					{
						bool isUpdateSuccesfull = _updateHandler.TryUpdate(InstallFolderPath, ApplicationExeName, ZipUpdateFolderPath);
						if (isUpdateSuccesfull)
						{
							_processHandler.StartProcess(Path.Combine(InstallFolderPath, ApplicationExeName), "");
						}
						else
						{
							_processHandler.StartProcess(Path.Combine(InstallFolderPath, ApplicationExeName), "");
						}
					}
					else if (!IsRestartedAsAdmin)
					{
						string restarArgs = $"-i {InstallFolderPath} -e {ApplicationExeName} -u {ZipUpdateFolderPath} -uid {UpdateId} -s {IsSilent} -r true";
						_processHandler.RestartUpdaterProcessAsAdmin(restarArgs);
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
			catch (Exception ex)
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
			foreach (string? arg in _args)
			{
				sb.Append(arg);
				sb.Append(" ");
			}
			//0123456789
			// -i C:\My\Path to\install folder -e MyExeName -u C:\My\Path to\update.zip -uid 4 -s false -r false
			string str = sb.ToString().Trim();

			InstallFolderPath = str.Substring(str.IndexOf("-i") + 2, str.IndexOf("-e") - 2).Trim();
			ApplicationExeName = str.Substring(str.IndexOf("-e") + 2, str.IndexOf("-u") - str.IndexOf("-e") - 3).Trim();
			ZipUpdateFolderPath = str.Substring(str.IndexOf("-u") + 2, str.IndexOf("-uid") - str.IndexOf("-u") - 3).Trim();
			UpdateId = str.Substring(str.IndexOf("-uid") + 4, str.IndexOf("-s") - str.IndexOf("-uid") - 5).Trim();
			IsSilent = str.Substring(str.IndexOf("-s") + 2, str.IndexOf("-r") - str.IndexOf("-s") - 2).Trim().ToLower() == "true";
			IsRestartedAsAdmin = str.Substring(str.IndexOf("-r") + 2, str.Length - str.IndexOf("-r") - 2).Trim().ToLower() == "true";

			ApplicationExeName = ApplicationExeName.Replace(".dll", ".exe"); // Just in case we receive the dll name instead of the actual exe

			Logger.Info("Starting arguments: ");
			Logger.Info("args: " + str);
			Logger.Info("----------------------");
			Logger.Info("InstallFolderPath: " + InstallFolderPath);
			Logger.Info("ApplicationExeName: " + ApplicationExeName);
			Logger.Info("ZipUpdateFolderPath: " + ZipUpdateFolderPath);
			Logger.Info("UpdateId: " + UpdateId);
			Logger.Info("IsSilent: " + IsSilent.ToString());
			Logger.Info("IsRestartedAsAdmin: " + IsRestartedAsAdmin.ToString());
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