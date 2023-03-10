using Quoter.App.Forms;
using Quoter.App.Helpers;
using Quoter.App.Services;
using Quoter.App.Services.Forms;
using Quoter.Framework.Entities;
using Quoter.Framework.Enums;
using Quoter.Framework.Models;
using Quoter.Framework.Services;
using Quoter.Framework.Services.Messaging;
using System.Media;

namespace Quoter.App.FormsControllers.QuoteController
{
	public class QuoteFormController : IQuoteFormController, IMessagingSubscriber
	{
		private readonly IQuoteService _quoteService;
		private readonly IMessagingService _messagingService;
		private readonly IThemeService _themeService;
		private readonly ISettings _settings;
		private readonly IStringResources _stringResources;
		private IQuoteForm _form;

		private QuoteFormOptions? _quoteModel;

		public QuoteFormController(IQuoteService quoteService,
									IMessagingService messagingService,
									IThemeService themeService,
									ISettings settings,
									IStringResources stringResources)
		{
			_quoteService = quoteService;
			_messagingService = messagingService;
			_themeService = themeService;
			_settings = settings;
			_stringResources = stringResources;
		}

		public void RegisterForm(IQuoteForm quoteForm)
		{
			_form = quoteForm;
			_form.SetTheme(_themeService.GetCurrentTheme());
			_messagingService.Subscribe(this);
		}

		public void RegisterForm(IQuoteForm form, QuoteFormOptions quoteModel)
		{
			_form = form;
			_quoteModel = quoteModel;
			_form.SetTheme(_themeService.GetCurrentTheme());
			_messagingService.Subscribe(this);
			_form.SetQuote(quoteModel);
		}

		public async Task EventFormLoaded()
		{
			if (_quoteModel == null)
			{
				await GetRandomQuote();
			}
		}

		public Task EventFormClosing()
		{
			// Nothing to do
			return Task.CompletedTask;
		}

		public async void OnMessageEvent(string message, object? argument)
		{
			// This might be displayed or it might not when the setting changes,
			// just as a precaution to not open more than one window close
			if (message == Event.NotificationTypeChanged || message == Event.OpeningQuoteWindow)
			{
				if (_form.GetForm().IsDisposed)
				{
					return;
				}
				_form.Close();
				_messagingService.Unsubscribe(this);
			}
			if (message == Event.NotificationTimerElapsed || message == Event.ShowQuoteButtonEvent)
			{
				await GetRandomQuote();
			}
			if (message == Event.ThemeChanged)
			{
				Theme theme = _themeService.GetCurrentTheme();
				_form.SetTheme(theme);
			}
		}

		public async Task GetRandomQuote()
		{
			EnumLanguage language = EnumLanguage.None;
			if (_settings.ShowCollectionsBasedOnLanguage)
			{
				language = LanguageHelper.GetEnumLanguageFromString(_settings.Language);
			}
			Quote? quote = await _quoteService.GetRandomQuote(language);
			if (quote != null)
			{
				_quoteModel = GetQuoteModel(quote);
				_form.SetQuote(_quoteModel);
			}
			else
			{
				_quoteModel = new QuoteFormOptions()
				{
					Title = "",
					Body = _stringResources["NoQuotes"],
					Footer = ""
				};
				_form.SetQuote(_quoteModel);
			}
		}

		public async Task GetNextQuote()
		{
			if (_quoteModel != null)
			{
				
				Quote? nextQuote = await _quoteService.GetNextQuote(_quoteModel.QuoteId);
				if (nextQuote != null)
				{
					_quoteModel = GetQuoteModel(nextQuote);
					_form.SetQuote(_quoteModel);
				}
			}

		}

		public async Task GetPreviousQuote()
		{
			if (_quoteModel != null)
			{
				
				Quote? previousQuote = await _quoteService.GetPreviousQuote(_quoteModel.QuoteId);
				if (previousQuote != null)
				{
					_quoteModel = GetQuoteModel(previousQuote);
					_form.SetQuote(_quoteModel);
				}
			}
		}

		private QuoteFormOptions GetQuoteModel(Quote quote)
		{
			string title;
			string footer = "";

			if (quote.Book != null && quote.Chapter != null)
			{
				title = quote.Book.Name;
				footer = quote.Chapter.Name + ":" + quote.QuoteIndex;
			}
			else if (quote.Book != null)
			{
				title = quote.Book.Name;
			}
			else
			{
				title = quote.Collection.Name;
			}

			return new QuoteFormOptions()
			{
				QuoteId = quote.QuoteId,
				Title = title,
				Footer = footer,
				Body = quote.Content,
				OpenAnimation = EnumAnimation.FadeInFromBottomRight,
				CloseAnimation = EnumAnimation.FadeOut,
				AllowNavigation = true
			};
		}
	}
}
