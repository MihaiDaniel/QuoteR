using Quoter.App.Services.FormAnimation;
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
	public partial class MessageForm : Form
	{
		private const int TitleMaxChars = 35;
		private const int BodyMaxChars = 170;
		private const int FooterMaxChars = 40;

		private readonly IFormAnimationService _formAnimationService;
		private readonly IFormPositioningService _positioningService;


		public MessageForm(MessageModel messageModel)
		{
			InitializeComponent();
			_formAnimationService = new FormAnimationsService();
			_positioningService = new FormPositioningService();
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

		private void textBox1_TextChanged(object sender, EventArgs e)
		{

		}

		private async void MessageForm_Load(object sender, EventArgs e)
		{
			Debug.WriteLine($"Start {nameof(MessageForm_Load)} Thread: {Thread.CurrentThread.ManagedThreadId}");

			await _formAnimationService.AnimateAsync(this, EnumAnimation.FadeInFromBottomRight);

			Debug.WriteLine($"Start {nameof(MessageForm_Load)}AfterAnimate Thread: {Thread.CurrentThread.ManagedThreadId}");

			int autoCloseSeconds = (int)Properties.Settings.Default["AutoCloseNotificationSeconds"];
			await _formAnimationService.CloseDelayedAsync(this, autoCloseSeconds * 1000, EnumAnimation.FadeOut);
			_positioningService.RegisterFormDragableByControl(this, pnlTitle);
		}

		private async void btnClose_Click(object sender, EventArgs e)
		{
			await _formAnimationService.AnimateAsync(this, EnumAnimation.FadeOut);
			this.Close();
		}
	}
}
