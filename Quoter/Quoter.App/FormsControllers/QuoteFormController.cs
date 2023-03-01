using Quoter.App.Forms;
using Quoter.App.Helpers;
using Quoter.App.Helpers.Extensions;
using Quoter.App.Services.Forms;
using Quoter.Framework.Models;
using Quoter.Framework.Services;
using Quoter.Framework.Services.Messaging;

namespace Quoter.App.FormsControllers
{
	public class QuoteFormController : IQuoteFormController, IMessageSubscriber
	{
		private readonly IQuoteService _quoteService;
		private readonly IMessagingService _messagingService;
		private readonly IThemeService _themeService;
		private IQuoteForm _form;

		private QuoteModel? _quoteModel;

		public QuoteFormController(IQuoteService quoteService, 
									IMessagingService messagingService,
									IThemeService themeService)
		{
			_quoteService = quoteService;
			_messagingService = messagingService;
			_themeService = themeService;
		}

		public void RegisterForm(IQuoteForm quoteForm)
		{
			_form = quoteForm;
			_form.SetTheme(_themeService.GetCurrentTheme());
			_messagingService.Subscribe(this);
			GetRandomQuote();
		}

		public async void OnMessageEvent(string message, object? argument)
		{
			// This might be displayed or it might not when the setting changes,
			// just as a precaution to not open more than one window close
			if (message == Const.Event.NotificationTypeChanged || message == Const.Event.OpeningQuoteWindow)
			{
				if (_form.GetForm().IsDisposed)
				{
					return;
				}
				_form.Close();
				_messagingService.Unsubscribe(this);
			}
			if (message == Const.Event.NotificationTimerElapsed || message == Const.Event.ShowQuoteButtonEvent)
			{
				await GetRandomQuote();
			}
			if (message == Const.Event.ThemeChanged)
			{
				Theme theme = _themeService.GetCurrentTheme();
				_form.SetTheme(theme);
			}
		}

		public async Task GetRandomQuote()
		{
			_quoteModel = await _quoteService.GetRandomQuote();
			if (_quoteModel != null)
			{
				_form.SetQuote(_quoteModel);
			}
		}

		public async Task GetNextQuote()
		{
			if(_quoteModel != null)
			{
				_quoteModel = await _quoteService.GetNextQuote(_quoteModel.QuoteId);
				if (_quoteModel != null)
				{
					_form.SetQuote(_quoteModel);
				}
			}
			
		}

		public async Task GetPreviousQuote()
		{
			if (_quoteModel != null)
			{
				_quoteModel = await _quoteService.GetPreviousQuote(_quoteModel.QuoteId);
				if (_quoteModel != null)
				{
					_form.SetQuote(_quoteModel);
				}
			}
			
			
		}

	}
}
