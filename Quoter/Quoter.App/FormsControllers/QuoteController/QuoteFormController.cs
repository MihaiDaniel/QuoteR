using Quoter.App.Forms;
using Quoter.App.Forms.Quote;
using Quoter.App.Helpers;
using Quoter.App.Services;
using Quoter.App.Services.Forms;
using Quoter.Framework.Entities;
using Quoter.Framework.Enums;
using Quoter.Framework.Models;
using Quoter.Framework.Services;
using Quoter.Framework.Services.Messaging;
using Quoter.Shared.Enums;
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
		private readonly ISoundService _soundService;
		private readonly IFormsManager _formsManager;
		private IQuoteForm _form;

		private Quote? _currentQuote;
		private QuoteFormOptions? _quoteFormOptions;

		public QuoteFormController(IQuoteService quoteService,
									IMessagingService messagingService,
									IThemeService themeService,
									ISettings settings,
									IStringResources stringResources,
									ISoundService soundService,
									IFormsManager formsManager)
		{
			_quoteService = quoteService;
			_messagingService = messagingService;
			_themeService = themeService;
			_settings = settings;
			_stringResources = stringResources;
			_soundService = soundService;
			_formsManager = formsManager;
		}

		public void RegisterForm(IQuoteForm quoteForm)
		{
			_form = quoteForm;
			_form.SetTheme(_themeService.GetCurrentTheme());
			_messagingService.Subscribe(this);
		}

		public void RegisterForm(IQuoteForm form, QuoteFormOptions quoteFormOptions)
		{
			_form = form;
			_quoteFormOptions = quoteFormOptions;
			_form.SetTheme(_themeService.GetCurrentTheme());
			_messagingService.Subscribe(this);
			_form.SetQuote(quoteFormOptions);
		}

		public async Task EventFormLoadedAsync()
		{
			if (_quoteFormOptions == null)
			{
				_soundService.Play(_settings.NotificationSound);
				await GetRandomQuoteAsync();
			}
		}

		public Task EventFormClosingAsync()
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
				return; // Don't process other messages
			}
			if (message == Event.RequestDisplayNewQuote)
			{
				_soundService.Play(_settings.NotificationSound);
				await GetRandomQuoteAsync();
			}
			if (message == Event.ThemeChanged)
			{
				Theme theme = _themeService.GetCurrentTheme();
				_form.SetTheme(theme);
			}
		}

		public async Task GetRandomQuoteAsync()
		{
			EnumLanguage language = EnumLanguage.None;
			if (_settings.ShowCollectionsBasedOnLanguage)
			{
				language = LanguageHelper.GetEnumLanguageFromString(_settings.Language);
			}
			Quote? quote = await _quoteService.GetRandQuoteFromFavsAsync(language);
			if (quote != null)
			{
				_currentQuote = quote;
				_quoteFormOptions = GetQuoteFormOptions(quote);
				_form.SetQuote(_quoteFormOptions);
			}
			else
			{
				_quoteFormOptions = new QuoteFormOptions()
				{
					Title = "",
					Body = _stringResources["NoQuotes"],
					Footer = ""
				};
				_form.SetQuote(_quoteFormOptions);
			}
		}

		public async Task GetNextQuoteAsync()
		{
			if (_quoteFormOptions != null)
			{
				Quote? nextQuote = await _quoteService.GetNextQuoteAsync(_quoteFormOptions.QuoteId);
				if (nextQuote != null)
				{
					_currentQuote = nextQuote;
					_quoteFormOptions = GetQuoteFormOptions(nextQuote);
					_form.SetQuote(_quoteFormOptions);
				}
			}

		}

		public async Task GetPreviousQuoteAsync()
		{
			if (_quoteFormOptions != null)
			{
				Quote? previousQuote = await _quoteService.GetPreviousQuoteAsync(_quoteFormOptions.QuoteId);
				if (previousQuote != null)
				{
					_currentQuote = previousQuote;
					_quoteFormOptions = GetQuoteFormOptions(previousQuote);
					_form.SetQuote(_quoteFormOptions);
				}
			}
		}

		public async Task OpenReaderForm()
		{
			ReaderFormOptions options = new ReaderFormOptions()
			{
				CollectionId = _currentQuote.CollectionId,
				BookId = _currentQuote.BookId,
				ChapterId = _currentQuote.ChapterId,
				QuoteId = _currentQuote.QuoteId,
			};
			//await Task.Delay(1); // Try to release callstack see if this helps with ReaderForm hanging
			_formsManager.ShowAndCloseOthers<ReaderForm>(options);

		}

		private QuoteFormOptions GetQuoteFormOptions(Quote quote)
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
