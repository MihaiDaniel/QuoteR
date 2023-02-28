﻿using Quoter.App.Forms;
using Quoter.App.FormsControllers;
using Quoter.App.Helpers;
using Quoter.App.Helpers.Extensions;
using Quoter.App.Services;
using Quoter.App.Services.FormAnimation;
using Quoter.App.Services.Forms;
using Quoter.Framework.Enums;
using Quoter.Framework.Models;
using System.Diagnostics;

namespace Quoter.App.Views
{
	public partial class QuoteForm : Form, IMonitoredForm, IQuoteForm
	{
		private const int TitleMaxChars = 35;
		private const int BodyMaxChars = 310; // at 10 font size
		private const int FooterMaxChars = 40;

		private readonly IFormsManager _formsManager;
		private readonly ISettings _settings;
		private readonly IFormAnimationService _formAnimationService;
		private readonly IFormPositioningService _positioningService;
		private readonly IThemeService _themeService;
		private readonly IQuoteFormController _formController;

		private List<IFormMonitor> _lstFormMonitors;

		private QuoteModel _quoteModel;

		public QuoteForm(IFormsManager formsManager,
							ISettings settings,
							IFormPositioningService positioningService,
							IFormAnimationService animationService,
							IThemeService themeService,
							IQuoteFormController formController,
							QuoteModel quoteModel)
		{
			InitializeComponent();
			_formsManager = formsManager;
			_settings = settings;
			_formAnimationService = animationService;
			_positioningService = positioningService;
			_positioningService.RegisterFormDragableByControl(this, pnlTitle);
			_themeService = themeService;
			_formController = formController;

			_lstFormMonitors = new List<IFormMonitor>();

			_quoteModel = quoteModel;		
			InitializeTheme();
			BindControls();

			_formController.RegisterForm(this);
			SetQuote(quoteModel);
		}

		private void BindControls()
		{
			// See if in the future use databindings for this
			//lblTitle.DataBindings.Add("Text", _formController, nameof(IQuoteFormController.Title));
			//txtBody.DataBindings.Add("Text", _formController, nameof(IQuoteFormController.Body));
			//txtFooter.DataBindings.Add("Text", _formController, nameof(IQuoteFormController.Footer));
		}

		private void InitializeTheme()
		{
			Theme theme = _themeService.GetCurrentTheme();
			this.Opacity= theme.Opacity;
			this.BackColor = theme.BodyColor;
			txtBody.BackColor = theme.BodyColor;
			txtFooter.BackColor = theme.BodyColor;
			pnlTitle.BackColor = theme.TitleColor;
		}

		public void SetQuote(QuoteModel quoteModel)
		{
			_quoteModel = quoteModel;
			this.InvokeIfRequired(() =>
			{
				SetControlText(lblTitle, quoteModel.Title, TitleMaxChars);
				SetControlText(txtBody, quoteModel.Body, BodyMaxChars);
				SetControlText(txtFooter, quoteModel.Footer, FooterMaxChars);

				// Adjust the font size a little bit depending on the amount of text
				if (txtBody.Text.Length < 126)
				{
					txtBody.Font = new Font("Calibri", 14, FontStyle.Italic);
				}
				else if (txtBody.Text.Length < 216)
				{
					txtBody.Font = new Font("Calibri", 12, FontStyle.Italic);
				}
				else if (txtBody.Text.Length < 246)
				{
					txtBody.Font = new Font("Calibri", 11, FontStyle.Italic);
				}
				else
				{
					txtBody.Font = new Font("Calibri", 10, FontStyle.Italic);
				}

				if (quoteModel.AllowNavigation)
				{
					btnNextQuote.Visible= true;
					btnPreviousQuote.Visible= true;
				}
				else
				{
					btnNextQuote.Visible = false;
					btnPreviousQuote.Visible = false;
				}

			});
		}

		public void RegisterFormMonitor(IFormMonitor formMonitor)
		{
			_lstFormMonitors.Add(formMonitor);
		}


		public EnumFormCloseState CanClose()
		{
			bool keepOpen = _settings.Get<bool>(Const.Setting.KeepNotificationOpenOnMouseOver);

			// Keep open while mouse is over the form
			if(this.IsDisposed)
			{
				return EnumFormCloseState.Disposed;
			}

			if(keepOpen)
			{
				return this.InvokeIfRequiredReturn<EnumFormCloseState>(() =>
				{
					if (ClientRectangle.Contains(PointToClient(Control.MousePosition)))
					{
						return EnumFormCloseState.NotClosable;
					}
					return EnumFormCloseState.IsClosable;
				});
			}
			return EnumFormCloseState.IsClosable;
			
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
			EnumAnimation openAnimation = _quoteModel.OpenAnimation ?? EnumAnimation.FadeInFromBottomRight;
			await _formAnimationService.AnimateAsync(this, openAnimation);
		}

		private async void btnClose_Click(object sender, EventArgs e)
		{
			_formsManager.Close(this);
		}

		private async void btnPreviousQuote_Click(object sender, EventArgs e)
		{
			await _formController.GetPreviousQuote(_quoteModel.QuoteId);
		}

		private async void btnNextQuote_Click(object sender, EventArgs e)
		{
			await _formController.GetNextQuote(_quoteModel.QuoteId);
		}

	}
}
