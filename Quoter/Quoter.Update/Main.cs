using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;

namespace Quoter.Update
{
	public partial class Main : Form
	{
		public string InstallFolderPath;
		public string ApplicationExeName;
		public string ZipUpdateFolderPath;

		private UpdateHandler _updateHandler;
		private ProcessHandler _processHandler;

		private string[] _args;

		public Main(string[] args)
		{
			InitializeComponent();
			//this.Opacity = 0;
			_args = args;
			_updateHandler = new UpdateHandler();
			_processHandler = new ProcessHandler();
		}


		private async void Main_Load(object sender, EventArgs e)
		{
			SetupArguments();
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
			else
			{
				_processHandler.RestartProcessAsAdmin($"{InstallFolderPath} {ApplicationExeName} {ZipUpdateFolderPath}");
			}
			
			Application.Exit();
		}

		private void SetupArguments()
		{
			try
			{
				InstallFolderPath = _args[0];
				ApplicationExeName = _args[1];
				ZipUpdateFolderPath = _args[2];
			}
			catch
			{
				this.Close();
				Application.Exit();
			}
		}


		

		private void Main_FormClosing(object sender, FormClosingEventArgs e)
		{
			Logger.Write();
		}
	}
}