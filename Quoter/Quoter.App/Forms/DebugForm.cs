using Microsoft.EntityFrameworkCore;
using Quoter.App.Services.Forms;
using Quoter.App.Views;
using Quoter.Framework.Data;
using Quoter.Framework.Data.Entities;
using Quoter.Framework.Models;
using Quoter.Framework.Services.AppSettings;
using Quoter.Framework.Services.Messaging;

namespace Quoter.App.Forms
{
	public partial class DebugForm : Form
	{
		private readonly QuoterContext _context;
		private readonly IAppSettings _settings;
		private readonly IFormsManager _formsManager;
		private readonly IMessagingService _messagingService;

		public DebugForm(QuoterContext context, IAppSettings settings, IFormsManager formsManager, IMessagingService messagingService)
		{
			InitializeComponent();
			_context = context;
			_settings = settings;
			_formsManager = formsManager;
			_messagingService = messagingService;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			_messagingService.SendMessage(Event.OpeningQuoteWindow);
			_formsManager.Show<QuoteForm>(_settings.AutoCloseNotificationSeconds);
		}

		private void btnShowQuoteById_Click(object sender, EventArgs e)
		{
			bool isValidInt = int.TryParse(txtQuoteId.Text, out int quoteId);
			if (!isValidInt)
			{
				MessageBox.Show("Please enter a valid quote ID.");
				return;
			}
			var quote = _context.Quotes.Include(q => q.Book).Include(q => q.Chapter).FirstOrDefault(q => q.QuoteId == quoteId);
			if (quote == null)
			{
				MessageBox.Show("Quote not found.");
				return;
			}
			QuoteFormOptions quoteModel = new QuoteFormOptions
			{
				Body = quote.Content,
				Title = quote.Book?.Name ?? string.Empty,
				Footer = quote.Chapter?.Name + ":" + quote.QuoteIndex,
				QuoteId = quote.QuoteId,
				AllowNavigation = true,
			};
			_messagingService.SendMessage(Event.OpeningQuoteWindow);
			_formsManager.Show<QuoteForm>(_settings.AutoCloseNotificationSeconds, quoteModel);
		}

		private void btnShowQuoteWithParams_Click(object sender, EventArgs e)
		{
			QuoteFormOptions quoteModel = new QuoteFormOptions
			{
				Body = txtBody.Text,
				Title = txtTitle.Text,
				Footer = txtFooter.Text,
				QuoteId = 1
			};
			_messagingService.SendMessage(Event.OpeningQuoteWindow);
			_formsManager.Show<QuoteForm>(_settings.AutoCloseNotificationSeconds, quoteModel);
		}

		private void btnFindQuoteId_Click(object sender, EventArgs e)
		{
			string searchText = txtSearchBar.Text.ToLowerInvariant();
			var quotes = _context.Quotes.Where(q => q.Content.ToLower().Contains(searchText)).FirstOrDefault();

			txtQuoteIdFound.Text = quotes?.QuoteId.ToString() ?? string.Empty;
			txtSearchFindContent.Text = quotes?.Content ?? string.Empty;
		}
	}
}
