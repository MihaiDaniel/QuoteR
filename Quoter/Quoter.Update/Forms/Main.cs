using Quoter.Update.Services;
using System.Text;

namespace Quoter.Update
{
	public partial class Main : Form
	{
		private UpdateService _updateHandler;
		private ProcessService _processHandler;

		private string _installFolderPath;      // -i
		private string _applicationExeName;     // -e
		private string _updateId;               // -uid
		private string _zipUpdateFolderPath;    // -u
		private bool _isSilent;                 // -s
		private bool _isRestartedAsAdmin;       // -r
		private string _labelText = "Updating, please wait..."; // -m

		private List<string> _argumentKeys = new List<string> { "-i", "-e", "-u", "-uid", "-s", "-r", "-m" };

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

			ParseArguments(_args);
			LogArguments(_args);

			// Show the form if not in silent mode and display the message
			InvokeIfRequired(this, SetVisibilityAndMessage);

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

		private void SetVisibilityAndMessage()
		{
			if (_isSilent)
			{
				this.Visible = false;
			}
			else
			{
				this.Visible = true;
				if (!string.IsNullOrEmpty(_labelText))
				{
					label.Text = _labelText;
				}
				else
				{
					// Default message if not provided
					_labelText = "Updating, please wait...";
				}
			}
		}

		private async Task UpdateApplicationAsync()
		{
			try
			{
				if (!AreArgumentsValid())
				{
					Logger.Error("Arguments are not valid, closing.");
					return;
				}
				if (_processHandler.HasWriteRightOnFolder(_installFolderPath))
				{
					bool isUpdateSuccesfull = _updateHandler.TryUpdate(_installFolderPath, _applicationExeName, _zipUpdateFolderPath);
					if (isUpdateSuccesfull)
					{
						_processHandler.StartProcess(Path.Combine(_installFolderPath, _applicationExeName), "");
					}
					else
					{
						_processHandler.StartProcess(Path.Combine(_installFolderPath, _applicationExeName), "");
					}
				}
				else if (!_isRestartedAsAdmin)
				{
					string restarArgs = $"-i {_installFolderPath} -e {_applicationExeName} -u {_zipUpdateFolderPath} -uid {_updateId} -s {_isSilent} -r true";
					_processHandler.RestartUpdaterProcessAsAdmin(restarArgs);
				}
				else
				{
					Logger.Error("Cannot update the folder even with admin rights");
				}

			}
			catch (Exception ex)
			{
				Logger.Error(ex, "General error, update may have failed.");
			}
			finally
			{
				try
				{
					await Logger.WriteAsync();
				}
				catch { /* Nothing to do */ }
				// Close the application
				InvokeIfRequired(this, () => { Application.Exit(); });
			}

		}

		private void ParseArguments(string[] args)
		{
			for (int index = 0; index < args.Length; index++)
			{
				string entry = args[index];
				switch (entry)
				{
					case "-i": _installFolderPath = GetArgumentFromIndex(args, ref index); break;
					case "-e": _applicationExeName = GetArgumentFromIndex(args, ref index).Replace(".dll", ".exe"); break;
					case "-u": _zipUpdateFolderPath = GetArgumentFromIndex(args, ref index); break;
					case "-uid": _updateId = GetArgumentFromIndex(args, ref index); break;
					case "-s": _isSilent = GetArgumentFromIndex(args, ref index).ToLower() == "true"; break;
					case "-r": _isRestartedAsAdmin = GetArgumentFromIndex(args, ref index).ToLower() == "true"; break;
					case "-m": _labelText = GetArgumentFromIndex(args, ref index); break;
				}
			}
		}

		private string GetArgumentFromIndex(string[] args, ref int index)
		{
			if (index < 0 || index >= args.Length)
			{
				return string.Empty;
			}
			// Skip the current argument key
			int currentIndex = index++;
			// Find the argument value until we reach the next argument key
			while (index < args.Length && !_argumentKeys.Contains(args[index]))
			{
				index++;
			}

			return string.Join(' ', args[currentIndex..index]);
		}

		private void LogArguments(string[] args)
		{
			Logger.Info("Starting arguments: ");
			Logger.Info("args: " + string.Join(' ', args));
			Logger.Info("----------------------");
			Logger.Info($"{nameof(_installFolderPath)}: " + _installFolderPath);
			Logger.Info($"{nameof(_applicationExeName)}: " + _applicationExeName);
			Logger.Info($"{nameof(_zipUpdateFolderPath)}: " + _zipUpdateFolderPath);
			Logger.Info($"{nameof(_updateId)}: " + _updateId);
			Logger.Info($"{nameof(_isSilent)}: " + _isSilent.ToString());
			Logger.Info($"{nameof(_isRestartedAsAdmin)}: " + _isRestartedAsAdmin.ToString());
			Logger.Info($"{nameof(_labelText)}: " + _labelText.ToString());
		}

		private bool AreArgumentsValid()
		{
			bool isValid = true;
			if (string.IsNullOrEmpty(_installFolderPath))
			{
				Logger.Info("Install folder path is empty");
				isValid = false;
			}
			if (string.IsNullOrEmpty(_applicationExeName))
			{
				Logger.Info("Application exe name is empty");
				isValid = false;
			}
			if (string.IsNullOrEmpty(_zipUpdateFolderPath))
			{
				Logger.Info("Zip update folder path is empty");
				isValid = false;
			}

			return isValid;
		}

		private void InvokeIfRequired(Form form, Action action)
		{
			if (form.InvokeRequired)
			{
				form.Invoke(action);
			}
			else
			{
				action();
			}
		}

		//private void SetupArgumentsOld()
		//{
		//	StringBuilder sb = new StringBuilder();
		//	foreach (string? arg in _args)
		//	{
		//		sb.Append(arg);
		//		sb.Append(" ");
		//	}
		//	//0123456789
		//	// -i C:\My\Path to\install folder -e MyExeName -u C:\My\Path to\update.zip -uid 4 -s false -r false -m Updating, please wait...
		//	string str = sb.ToString().Trim();

		//	_installFolderPath = str.Substring(str.IndexOf("-i") + 2, str.IndexOf("-e") - 2).Trim();
		//	_applicationExeName = str.Substring(str.IndexOf("-e") + 2, str.IndexOf("-u") - str.IndexOf("-e") - 3).Trim();
		//	_zipUpdateFolderPath = str.Substring(str.IndexOf("-u") + 2, str.IndexOf("-uid") - str.IndexOf("-u") - 3).Trim();
		//	_updateId = str.Substring(str.IndexOf("-uid") + 4, str.IndexOf("-s") - str.IndexOf("-uid") - 5).Trim();
		//	_isSilent = str.Substring(str.IndexOf("-s") + 2, str.IndexOf("-r") - str.IndexOf("-s") - 2).Trim().ToLower() == "true";
		//	_isRestartedAsAdmin = str.Substring(str.IndexOf("-r") + 2, str.Length - str.IndexOf("-r") - 2).Trim().ToLower() == "true";

		//	_applicationExeName = _applicationExeName.Replace(".dll", ".exe"); // Just in case we receive the dll name instead of the actual exe

		//	Logger.Info("Starting arguments: ");
		//	Logger.Info("args: " + str);
		//	Logger.Info("----------------------");
		//	Logger.Info("InstallFolderPath: " + _installFolderPath);
		//	Logger.Info("ApplicationExeName: " + _applicationExeName);
		//	Logger.Info("ZipUpdateFolderPath: " + _zipUpdateFolderPath);
		//	Logger.Info("UpdateId: " + _updateId);
		//	Logger.Info("IsSilent: " + _isSilent.ToString());
		//	Logger.Info("IsRestartedAsAdmin: " + _isRestartedAsAdmin.ToString());
		//}
	}
}