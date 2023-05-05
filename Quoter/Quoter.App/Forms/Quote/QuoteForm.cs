using Quoter.App.Controls;
using Quoter.App.Forms;
using Quoter.App.Forms.Quote;
using Quoter.App.FormsControllers.QuoteController;
using Quoter.App.Helpers;
using Quoter.App.Helpers.Extensions;
using Quoter.App.Services;
using Quoter.App.Services.FormAnimation;
using Quoter.App.Services.Forms;
using Quoter.Framework.Enums;
using Quoter.Framework.Models;

namespace Quoter.App.Views
{
	/// <summary>
	/// Form used to display quotes as notifications.
	/// </summary>
	public partial class QuoteForm : Form, IMonitoredForm, IQuoteForm
	{
		private const int TitleMaxChars = 35;
		private const int FooterMaxChars = 40;

		private readonly IFormsManager _formsManager;
		private readonly ISettings _settings;
		private readonly IFormAnimationService _formAnimationService;
		private readonly IFormPositioningService _positioningService;
		private readonly IThemeService _themeService;
		private readonly IQuoteFormController _controller;

		public QuoteForm(IFormsManager formsManager,
							ISettings settings,
							IFormPositioningService positioningService,
							IFormAnimationService animationService,
							IThemeService themeService,
							IQuoteFormController formController)
		{
			InitializeComponent();
			DropShadow.ApplyShadows(this);
			_formsManager = formsManager;
			_settings = settings;
			_formAnimationService = animationService;
			_positioningService = positioningService;
			_positioningService.RegisterFormDragableByControl(this, pnlTitle);
			_themeService = themeService;
			_controller = formController;

			_controller.RegisterForm(this);
		}

		public QuoteForm(IFormsManager formsManager,
							ISettings settings,
							IFormPositioningService positioningService,
							IFormAnimationService animationService,
							IThemeService themeService,
							IQuoteFormController formController,
							QuoteFormOptions quoteModel)
		{
			InitializeComponent();
			DropShadow.ApplyShadows(this);

			_formsManager = formsManager;
			_settings = settings;
			_formAnimationService = animationService;
			_positioningService = positioningService;
			_positioningService.RegisterFormDragableByControl(this, pnlTitle);
			_themeService = themeService;
			_controller = formController;

			_controller.RegisterForm(this, quoteModel);
		}

		private async void MessageForm_Load(object sender, EventArgs e)
		{
			Theme theme = _themeService.GetCurrentTheme();
			await _controller.EventFormLoadedAsync();
			await _formAnimationService.AnimateAsync(this, theme.OpenNotificationAnimation);
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

		void IForm.SetTheme(Theme theme)
		{
			this.InvokeIfRequired(() =>
			{
				if (this.Disposing || this.IsDisposed)
				{
					return;
				}
				this.Opacity = theme.Opacity;
				this.BackColor = theme.BodyColor;
				txtBody.BackColor = theme.BodyColor;
				txtFooter.BackColor = theme.BodyColor;
				pnlTitle.BackColor = theme.TitleColor;
			});
		}

		Form IQuoteForm.GetForm()
		{
			return this;
		}

		void IQuoteForm.SetQuote(QuoteFormOptions quoteModel)
		{
			this.InvokeIfRequired(() =>
			{
				SetControlText(lblTitle, quoteModel.Title, TitleMaxChars);
				SetControlText(txtFooter, quoteModel.Footer, FooterMaxChars);

				// Adjust the font size a little bit depending on the amount of text/
				txtBody.Text = quoteModel.Body;
				float fontSize = GetOptimalFontSize(_settings.FontName, _settings.FontSize);
				txtBody.Font = new Font(_settings.FontName, fontSize, FontHelper.GetFontStyle(_settings.FontStyle));

				if (quoteModel.AllowNavigation)
				{
					btnNextQuote.Visible = true;
					btnPreviousQuote.Visible = true;
				}
				else
				{
					btnNextQuote.Visible = false;
					btnPreviousQuote.Visible = false;
				}
			});
		}

		EnumFormCloseState IMonitoredForm.IsClosable()
		{
			bool keepOpen = _settings.KeepNotificationOpenOnMouseOver;
			if (this.IsDisposed)
			{
				return EnumFormCloseState.Disposed;
			}
			// Keep open while mouse is over the form
			if (keepOpen)
			{
				return this.InvokeIfRequiredReturn<EnumFormCloseState>(() =>
				{
					if (!this.IsDisposed)
					{
						if (ClientRectangle.Contains(PointToClient(Control.MousePosition)))
						{
							return EnumFormCloseState.NotClosable;
						}
					}
					return EnumFormCloseState.IsClosable;
				});
			}
			return EnumFormCloseState.IsClosable;

		}

		void IMonitoredForm.Close()
		{
			Close();
		}

		public async new void Close()
		{
			await _formAnimationService.AnimateAsync(this, EnumAnimation.FadeOut);
			this.InvokeIfRequired(() => base.Close());
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			_formsManager.Close(this);
		}

		private async void btnPreviousQuote_Click(object sender, EventArgs e)
		{
			await _controller.GetPreviousQuoteAsync();
		}

		private async void btnNextQuote_Click(object sender, EventArgs e)
		{
			await _controller.GetNextQuoteAsync();
		}

		private void txtFooter_MouseEnter(object sender, EventArgs e)
		{
			FontStyle newFontStyle = FontStyle.Bold | FontStyle.Underline;
			txtFooter.Font = new Font(txtFooter.Font.Name, txtFooter.Font.Size, newFontStyle);
		}

		private void txtFooter_MouseLeave(object sender, EventArgs e)
		{
			FontStyle newFontStyle = FontStyle.Bold;
			txtFooter.Font = new Font(txtFooter.Font.Name, txtFooter.Font.Size, newFontStyle);
		}

		private async void txtFooter_Click(object sender, EventArgs e)
		{
			//btnClose_Click(sender, e);
			await _controller.OpenReaderForm();
		}

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

		/// <summary>
		/// Adjusts the fontSize for a particular font so that the text can be fully displayed in the txtBody.
		/// The minimum fontSize returned is 10.
		/// </summary>
		private float GetOptimalFontSize(string fontName, float fontSize)
		{
			int controllerWidth = txtBody.Width;
			int controllerHeight = txtBody.Height;
			SizeF size = TextRenderer.MeasureText(txtBody.Text, new Font(fontName, fontSize), new Size(controllerWidth, controllerHeight), TextFormatFlags.WordBreak);

			int lines = (int)Math.Round(size.Width / controllerWidth, 0, MidpointRounding.ToPositiveInfinity);
			int textHeightUsed = (int)(lines * size.Height);
			if (textHeightUsed > controllerHeight)
			{
				if (fontSize <= 10f)
				{
					txtBody.ScrollBars = ScrollBars.Vertical; // Enable scrollbars
					return 10; // Aprox 320 chars depending on font and word size
				}
				else
				{
					fontSize -= 1;
					return GetOptimalFontSize(fontName, fontSize);
				}
			}
			return fontSize;
		}
	}
}
