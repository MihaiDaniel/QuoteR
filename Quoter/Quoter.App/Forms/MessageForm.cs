using Quoter.App.Helpers;
using Quoter.App.Helpers.Extensions;
using Quoter.App.Services;
using Quoter.App.Services.FormAnimation;
using Quoter.App.Services.Forms;
using Quoter.Framework.Enums;
using Quoter.Framework.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quoter.App.Views
{
	public partial class MessageForm : Form, IMonitoredForm
	{
		private const int TitleMaxChars = 35;
		private const int BodyMaxChars = 170;
		private const int FooterMaxChars = 40;

		private readonly IFormsManager _formsManager;
		private readonly ISettings _settings;
		private readonly IFormAnimationService _formAnimationService;
		private readonly IFormPositioningService _positioningService;

		private List<IFormMonitor> _lstFormMonitors;

		private readonly MessageModel _messageModel;

		public MessageForm(IFormsManager formsManager,
							ISettings settings,
							IFormPositioningService positioningService,
							IFormAnimationService animationService,
							MessageModel messageModel)
		{
			InitializeComponent();
			_formsManager = formsManager;
			_settings = settings;
			_formAnimationService = animationService;
			_positioningService = positioningService;
			_positioningService.RegisterFormDragableByControl(this, pnlTitle);

			_lstFormMonitors = new List<IFormMonitor>();

			_messageModel = messageModel;
			InitializeMessage(messageModel);
		}

		private void InitializeMessage(MessageModel messageModel)
		{
			SetControlText(lblTitle, messageModel.Title, TitleMaxChars);
			SetControlText(txtBody, messageModel.Body, BodyMaxChars);
			SetControlText(txtFooter, messageModel.Footer, FooterMaxChars);

			// Adjust the font size a little bit depending on the amount of text
			if(txtBody.Text.Length > 120) 
			{
				txtBody.Font = new Font("Calibri", 12, FontStyle.Italic);
			}
			else
			{
				txtBody.Font = new Font("Calibri", 14, FontStyle.Italic);
			}
			
		}

		public void RegisterFormMonitor(IFormMonitor formMonitor)
		{
			_lstFormMonitors.Add(formMonitor);
		}

		public bool CanClose()
		{
			bool keepOpen = _settings.Get<bool>(Const.Setting.KeepNotificationOpenOnMouseOver);

			// Keep open while mouse is over the form
			if(keepOpen)
			{
				return this.InvokeIfRequiredReturn<bool>(() =>
				{
					if (ClientRectangle.Contains(PointToClient(Control.MousePosition)))
					{
						return false;
					}
					return true;
				});
			}
			return true;
		}

		public async new void Close()
		{
			foreach(IFormMonitor formMonitor in _lstFormMonitors)
			{
				formMonitor.EventFormClosing(this);
			}
			await _formAnimationService.AnimateAsync(this, EnumAnimation.FadeOut);
			_formsManager.Close(this);
		}

		#region ShowWithoutStealingFocus

		protected override bool ShowWithoutActivation
		{
			get { return true; }
		}

		private const int WS_EX_TOPMOST = 0x00000008;
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.ExStyle |= WS_EX_TOPMOST;
				return createParams;
			}
		}

		#endregion ShowWithoutStealingFocus

		private void SetControlText(Control control, string text, int maxChars) 
		{
			if (text.Length > maxChars)
			{
				control.Text = text.Substring(0, TitleMaxChars);
			}
			else
			{
				control.Text = text;
			}
		}

		private async void MessageForm_Load(object sender, EventArgs e)
		{
			Debug.WriteLine($"Start {nameof(MessageForm_Load)} Thread: {Thread.CurrentThread.ManagedThreadId}");

			EnumAnimation openAnimation = _messageModel.OpenAnimation ?? EnumAnimation.FadeInFromBottomRight;

			await _formAnimationService.AnimateAsync(this, openAnimation);

			Debug.WriteLine($"Start {nameof(MessageForm_Load)}AfterAnimate Thread: {Thread.CurrentThread.ManagedThreadId}");

			//int autoCloseSeconds = (int)Properties.Settings.Default["AutoCloseNotificationSeconds"];
			//await _formAnimationService.CloseDelayedAsync(this, autoCloseSeconds * 1000, EnumAnimation.FadeOut);
			
		}

		private async void btnClose_Click(object sender, EventArgs e)
		{
			await _formAnimationService.AnimateAsync(this, EnumAnimation.FadeOut);
			this.Close();
		}
	}
}
