using Quoter.App.Services.FormAnimation;
using Quoter.App.Services.Forms;
using Quoter.App.Services;
using Quoter.Framework.Models;
using Quoter.Framework.Services;
using Quoter.App.FormsControllers.Reader;
using Quoter.App.Forms.Common;
using Quoter.App.Helpers;
using Quoter.Framework.Entities;

namespace Quoter.App.Forms
{
	/// <summary>
	/// In this form the user can read a collection's quotes. Navigation is possible between
	/// the collection's books or chapters
	/// </summary>
	public partial class ReaderForm : ResizableForm, IReaderForm
	{
		private readonly IFormsManager _formsManager;
		private readonly IFormAnimationService _formAnimationService;
		private readonly IStringResources _stringResources;
		private readonly IReaderFormController _controller;
		private readonly IThemeService _themeService;
		private readonly ReaderFormOptions _options;

		public ReaderForm(IFormsManager formsManager,
							IFormPositioningService formPositioningService,
							IFormAnimationService formAnimationService,
							IStringResources stringResources,
							IReaderFormController readerFormController,
							IThemeService themeService,
							ILogger logger,
							ReaderFormOptions options)
		{
			InitializeComponent();

			_formsManager = formsManager;
			_formAnimationService = formAnimationService;
			_stringResources = stringResources;
			_controller = readerFormController;
			_themeService = themeService;
			_options = options;

			_controller.RegisterForm(this);
			_controller.SetFormOptions(options);

			formPositioningService.RegisterFormDragableByControl(this, pnlTitle);
			formPositioningService.RegisterFormDragableByControl(this, lblTitle);

			tvCollection.Nodes.Clear(); // Clear any nodes created in the Designer
			LocalizeControls();
		}

		private void LocalizeControls()
		{
			//rtbQuotes.DataBindings.Add("Text", _controller, nameof(_controller.Quotes));
		}

		void IForm.SetStatus(string message, Color color)
		{
			throw new NotImplementedException();
		}

		void IForm.SetTheme()
		{
			Theme theme = _themeService.GetCurrentTheme();
			this.BackColor = theme.BodyColor;
			pnlTitle.BackColor = theme.TitleColor;
		}

		void IResizableForm.SetSize(Size size)
		{
			this.Size = new Size(size.Width, size.Height);
		}

		void IReaderForm.BuildTreeNavigation(Collection collection)
		{
			lblTitle.Text = collection.Name;

			tvCollection.BeginUpdate();
			tvCollection.Nodes.Clear();

			List<TreeNode> lstBookNodes = new List<TreeNode>();
			foreach (Book book in collection.LstBooks)
			{
				if (book.LstChapters.Any())
				{
					List<TreeNode> lstChapterNodes = new List<TreeNode>();
					foreach (Chapter chapter in book.LstChapters)
					{
						TreeNode chapterNode = new TreeNode(chapter.Name);
						chapterNode.Tag = chapter;
						lstChapterNodes.Add(chapterNode);
					}
					TreeNode bookNode = new TreeNode(book.Name, lstChapterNodes.ToArray());
					bookNode.Tag = book;
					lstBookNodes.Add(bookNode);
				}
				else
				{
					TreeNode bookNode = new TreeNode(book.Name);
					bookNode.Tag = book;
					lstBookNodes.Add(bookNode);
				}
			}
			tvCollection.Nodes.AddRange(lstBookNodes.ToArray());
			tvCollection.EndUpdate();
		}
		// TODO: Expand ALL
		// TODO: Collapse ALL

		void IReaderForm.SetQuotesContent(string content)
		{
			rtbQuotes.Text = content;
			rtbQuotes.SelectionStart = 0;
			rtbQuotes.ScrollToCaret();
		}

		void IReaderForm.ScrollToQuote(string quoteContent)
		{
			int position = rtbQuotes.Find(quoteContent, RichTextBoxFinds.WholeWord);
			if (position >= 0)
			{
				rtbQuotes.Select(position, quoteContent.Length);
				rtbQuotes.SelectionBackColor = Color.LightYellow;
				rtbQuotes.SelectionStart = position;
				rtbQuotes.ScrollToCaret();
			}
		}

		void IReaderForm.SetLocationInCollection(string location)
		{
			lblLocationInCollection.Text = location;
		}

		private async void ReaderForm_Load(object sender, EventArgs e)
		{
			await _formAnimationService.AnimateAsync(this, Framework.Enums.EnumAnimation.FadeIn);
			await _controller.EventFormLoadedAsync();
		}

		private async void btnClose_Click(object sender, EventArgs e)
		{
			await _controller.EventFormClosingAsync();
			_formsManager.Close(this);
		}

		private async void btnNext_Click(object sender, EventArgs e)
		{
			await _controller.SetNextChapterAsync();

			rtbQuotes.SelectionStart = 0;
			rtbQuotes.ScrollToCaret();

		}

		private async void btnPrevious_Click(object sender, EventArgs e)
		{
			await _controller.SetPreviousChapterAsync();

			//rtbQuotes.SelectionStart = rtbQuotes.TextLength;
			//rtbQuotes.ScrollToCaret();
		}

		private async void tvCollection_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (e.Node?.Tag is Book book)
			{
				await _controller.SetSelectedBookAsync(book);
			}
			else if (e.Node?.Tag is Chapter chapter)
			{
				await _controller.SetSelectedChapterAsync(chapter);
			}
		}

		private async void btnBackToManage_Click(object sender, EventArgs e)
		{
			ManageFormOptions options = new ManageFormOptions()
			{
				Tab = Framework.Enums.EnumTab.FavouriteQuotes
			};
			_formsManager.ShowAndCloseOthers<ManageForm>(options);
			await _controller.EventFormClosingAsync();
		}


	}
}
