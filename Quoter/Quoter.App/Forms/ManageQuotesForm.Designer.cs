namespace Quoter.App.Forms
{
	partial class ManageQuotesForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageQuotesForm));
			this.pnlTitle = new System.Windows.Forms.Panel();
			this.lblTitle = new System.Windows.Forms.Label();
			this.btnClose = new System.Windows.Forms.Button();
			this.tab1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.gbQuotes = new System.Windows.Forms.GroupBox();
			this.txtStatus = new System.Windows.Forms.TextBox();
			this.btnSaveQuotes = new System.Windows.Forms.Button();
			this.rtbQuotes = new System.Windows.Forms.RichTextBox();
			this.gbChapters = new System.Windows.Forms.GroupBox();
			this.btnDeleteChapter = new System.Windows.Forms.Button();
			this.btnAddChapter = new System.Windows.Forms.Button();
			this.lbChapters = new System.Windows.Forms.ListBox();
			this.gbBooks = new System.Windows.Forms.GroupBox();
			this.btnDeleteBook = new System.Windows.Forms.Button();
			this.cbBooks = new System.Windows.Forms.ComboBox();
			this.btnAddBook = new System.Windows.Forms.Button();
			this.gbCollections = new System.Windows.Forms.GroupBox();
			this.btnDeleteCollection = new System.Windows.Forms.Button();
			this.cbCollection = new System.Windows.Forms.ComboBox();
			this.btnAddCollection = new System.Windows.Forms.Button();
			this.pnlTitle.SuspendLayout();
			this.tab1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.gbQuotes.SuspendLayout();
			this.gbChapters.SuspendLayout();
			this.gbBooks.SuspendLayout();
			this.gbCollections.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlTitle
			// 
			this.pnlTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlTitle.BackColor = System.Drawing.Color.SlateGray;
			this.pnlTitle.Controls.Add(this.lblTitle);
			this.pnlTitle.Controls.Add(this.btnClose);
			this.pnlTitle.Location = new System.Drawing.Point(0, 0);
			this.pnlTitle.Name = "pnlTitle";
			this.pnlTitle.Size = new System.Drawing.Size(800, 40);
			this.pnlTitle.TabIndex = 1;
			// 
			// lblTitle
			// 
			this.lblTitle.AutoSize = true;
			this.lblTitle.Font = new System.Drawing.Font("Calibri Light", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblTitle.ForeColor = System.Drawing.Color.White;
			this.lblTitle.Location = new System.Drawing.Point(12, 7);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(144, 26);
			this.lblTitle.TabIndex = 2;
			this.lblTitle.Text = "Manage quotes";
			// 
			// btnClose
			// 
			this.btnClose.BackColor = System.Drawing.Color.Transparent;
			this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
			this.btnClose.FlatAppearance.BorderSize = 0;
			this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
			this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnClose.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			this.btnClose.ForeColor = System.Drawing.Color.White;
			this.btnClose.Location = new System.Drawing.Point(754, 0);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(46, 40);
			this.btnClose.TabIndex = 1;
			this.btnClose.Text = "X";
			this.btnClose.UseVisualStyleBackColor = false;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// tab1
			// 
			this.tab1.Controls.Add(this.tabPage1);
			this.tab1.Location = new System.Drawing.Point(12, 46);
			this.tab1.Name = "tab1";
			this.tab1.SelectedIndex = 0;
			this.tab1.Size = new System.Drawing.Size(776, 422);
			this.tab1.TabIndex = 2;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.gbQuotes);
			this.tabPage1.Controls.Add(this.gbChapters);
			this.tabPage1.Controls.Add(this.gbBooks);
			this.tabPage1.Controls.Add(this.gbCollections);
			this.tabPage1.Location = new System.Drawing.Point(4, 24);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(768, 394);
			this.tabPage1.TabIndex = 2;
			this.tabPage1.Text = "Create your own quotes library";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// gbQuotes
			// 
			this.gbQuotes.Controls.Add(this.txtStatus);
			this.gbQuotes.Controls.Add(this.btnSaveQuotes);
			this.gbQuotes.Controls.Add(this.rtbQuotes);
			this.gbQuotes.Location = new System.Drawing.Point(243, 6);
			this.gbQuotes.Name = "gbQuotes";
			this.gbQuotes.Size = new System.Drawing.Size(519, 382);
			this.gbQuotes.TabIndex = 17;
			this.gbQuotes.TabStop = false;
			this.gbQuotes.Text = "Quotes";
			// 
			// txtStatus
			// 
			this.txtStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtStatus.ForeColor = System.Drawing.Color.Green;
			this.txtStatus.Location = new System.Drawing.Point(6, 356);
			this.txtStatus.Name = "txtStatus";
			this.txtStatus.Size = new System.Drawing.Size(433, 16);
			this.txtStatus.TabIndex = 15;
			this.txtStatus.Text = "Status acesta este un status ca s-a facut ceva corect bravo tie mai ce sa zicem";
			// 
			// btnSaveQuotes
			// 
			this.btnSaveQuotes.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnSaveQuotes.ForeColor = System.Drawing.Color.Green;
			this.btnSaveQuotes.Location = new System.Drawing.Point(445, 352);
			this.btnSaveQuotes.Name = "btnSaveQuotes";
			this.btnSaveQuotes.Size = new System.Drawing.Size(68, 24);
			this.btnSaveQuotes.TabIndex = 9;
			this.btnSaveQuotes.Text = "Save";
			this.btnSaveQuotes.UseVisualStyleBackColor = true;
			this.btnSaveQuotes.Click += new System.EventHandler(this.btnSaveQuotes_Click);
			// 
			// rtbQuotes
			// 
			this.rtbQuotes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.rtbQuotes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.rtbQuotes.Location = new System.Drawing.Point(6, 17);
			this.rtbQuotes.Name = "rtbQuotes";
			this.rtbQuotes.Size = new System.Drawing.Size(507, 329);
			this.rtbQuotes.TabIndex = 13;
			this.rtbQuotes.Text = resources.GetString("rtbQuotes.Text");
			// 
			// gbChapters
			// 
			this.gbChapters.Controls.Add(this.btnDeleteChapter);
			this.gbChapters.Controls.Add(this.btnAddChapter);
			this.gbChapters.Controls.Add(this.lbChapters);
			this.gbChapters.Location = new System.Drawing.Point(6, 172);
			this.gbChapters.Name = "gbChapters";
			this.gbChapters.Size = new System.Drawing.Size(231, 216);
			this.gbChapters.TabIndex = 16;
			this.gbChapters.TabStop = false;
			this.gbChapters.Text = "Capitole";
			// 
			// btnDeleteChapter
			// 
			this.btnDeleteChapter.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnDeleteChapter.ForeColor = System.Drawing.Color.Red;
			this.btnDeleteChapter.Location = new System.Drawing.Point(6, 186);
			this.btnDeleteChapter.Name = "btnDeleteChapter";
			this.btnDeleteChapter.Size = new System.Drawing.Size(68, 24);
			this.btnDeleteChapter.TabIndex = 7;
			this.btnDeleteChapter.Text = "Șterge";
			this.btnDeleteChapter.UseVisualStyleBackColor = true;
			this.btnDeleteChapter.Click += new System.EventHandler(this.btnDeleteChapter_Click);
			// 
			// btnAddChapter
			// 
			this.btnAddChapter.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnAddChapter.ForeColor = System.Drawing.Color.Green;
			this.btnAddChapter.Location = new System.Drawing.Point(157, 186);
			this.btnAddChapter.Name = "btnAddChapter";
			this.btnAddChapter.Size = new System.Drawing.Size(68, 24);
			this.btnAddChapter.TabIndex = 6;
			this.btnAddChapter.Text = "Adaugă";
			this.btnAddChapter.UseVisualStyleBackColor = true;
			this.btnAddChapter.Click += new System.EventHandler(this.btnAddChapter_Click);
			// 
			// lbChapters
			// 
			this.lbChapters.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lbChapters.FormattingEnabled = true;
			this.lbChapters.ItemHeight = 18;
			this.lbChapters.Items.AddRange(new object[] {
            "test",
            "test1",
            "test4"});
			this.lbChapters.Location = new System.Drawing.Point(6, 16);
			this.lbChapters.Name = "lbChapters";
			this.lbChapters.Size = new System.Drawing.Size(219, 166);
			this.lbChapters.TabIndex = 5;
			this.lbChapters.SelectedValueChanged += new System.EventHandler(this.lbChapters_SelectedValueChanged);
			// 
			// gbBooks
			// 
			this.gbBooks.Controls.Add(this.btnDeleteBook);
			this.gbBooks.Controls.Add(this.cbBooks);
			this.gbBooks.Controls.Add(this.btnAddBook);
			this.gbBooks.Location = new System.Drawing.Point(6, 89);
			this.gbBooks.Name = "gbBooks";
			this.gbBooks.Size = new System.Drawing.Size(231, 77);
			this.gbBooks.TabIndex = 15;
			this.gbBooks.TabStop = false;
			this.gbBooks.Text = "Book";
			// 
			// btnDeleteBook
			// 
			this.btnDeleteBook.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnDeleteBook.ForeColor = System.Drawing.Color.Red;
			this.btnDeleteBook.Location = new System.Drawing.Point(6, 47);
			this.btnDeleteBook.Name = "btnDeleteBook";
			this.btnDeleteBook.Size = new System.Drawing.Size(68, 24);
			this.btnDeleteBook.TabIndex = 7;
			this.btnDeleteBook.Text = "Șterge";
			this.btnDeleteBook.UseVisualStyleBackColor = true;
			this.btnDeleteBook.Click += new System.EventHandler(this.btnDeleteBook_Click);
			// 
			// cbBooks
			// 
			this.cbBooks.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.cbBooks.FormattingEnabled = true;
			this.cbBooks.Location = new System.Drawing.Point(6, 17);
			this.cbBooks.Name = "cbBooks";
			this.cbBooks.Size = new System.Drawing.Size(219, 26);
			this.cbBooks.TabIndex = 2;
			this.cbBooks.SelectedValueChanged += new System.EventHandler(this.cbBooks_SelectedValueChanged);
			// 
			// btnAddBook
			// 
			this.btnAddBook.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnAddBook.ForeColor = System.Drawing.Color.Green;
			this.btnAddBook.Location = new System.Drawing.Point(157, 47);
			this.btnAddBook.Name = "btnAddBook";
			this.btnAddBook.Size = new System.Drawing.Size(68, 24);
			this.btnAddBook.TabIndex = 6;
			this.btnAddBook.Text = "Adaugă";
			this.btnAddBook.UseVisualStyleBackColor = true;
			this.btnAddBook.Click += new System.EventHandler(this.btnAddBook_Click);
			// 
			// gbCollections
			// 
			this.gbCollections.Controls.Add(this.btnDeleteCollection);
			this.gbCollections.Controls.Add(this.cbCollection);
			this.gbCollections.Controls.Add(this.btnAddCollection);
			this.gbCollections.Location = new System.Drawing.Point(6, 6);
			this.gbCollections.Name = "gbCollections";
			this.gbCollections.Size = new System.Drawing.Size(231, 77);
			this.gbCollections.TabIndex = 14;
			this.gbCollections.TabStop = false;
			this.gbCollections.Text = "Colectia";
			// 
			// btnDeleteCollection
			// 
			this.btnDeleteCollection.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.btnDeleteCollection.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnDeleteCollection.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnDeleteCollection.ForeColor = System.Drawing.Color.Red;
			this.btnDeleteCollection.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnDeleteCollection.Location = new System.Drawing.Point(6, 47);
			this.btnDeleteCollection.Name = "btnDeleteCollection";
			this.btnDeleteCollection.Size = new System.Drawing.Size(68, 24);
			this.btnDeleteCollection.TabIndex = 7;
			this.btnDeleteCollection.Text = "Delete";
			this.btnDeleteCollection.UseVisualStyleBackColor = true;
			this.btnDeleteCollection.Click += new System.EventHandler(this.btnDeleteCollection_Click);
			// 
			// cbCollection
			// 
			this.cbCollection.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.cbCollection.FormattingEnabled = true;
			this.cbCollection.Location = new System.Drawing.Point(6, 17);
			this.cbCollection.Name = "cbCollection";
			this.cbCollection.Size = new System.Drawing.Size(219, 26);
			this.cbCollection.TabIndex = 2;
			this.cbCollection.SelectedValueChanged += new System.EventHandler(this.cbCollection_SelectedValueChanged);
			// 
			// btnAddCollection
			// 
			this.btnAddCollection.BackColor = System.Drawing.Color.Transparent;
			this.btnAddCollection.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnAddCollection.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnAddCollection.ForeColor = System.Drawing.Color.Green;
			this.btnAddCollection.Location = new System.Drawing.Point(157, 47);
			this.btnAddCollection.Name = "btnAddCollection";
			this.btnAddCollection.Size = new System.Drawing.Size(68, 24);
			this.btnAddCollection.TabIndex = 6;
			this.btnAddCollection.Text = "Adaugă";
			this.btnAddCollection.UseVisualStyleBackColor = false;
			this.btnAddCollection.Click += new System.EventHandler(this.btnAddCollection_Click);
			// 
			// ManageQuotesForm
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.WhiteSmoke;
			this.ClientSize = new System.Drawing.Size(800, 480);
			this.ControlBox = false;
			this.Controls.Add(this.tab1);
			this.Controls.Add(this.pnlTitle);
			this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "ManageQuotesForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "ManageQuotesForm";
			this.Load += new System.EventHandler(this.ManageQuotesForm_Load);
			this.pnlTitle.ResumeLayout(false);
			this.pnlTitle.PerformLayout();
			this.tab1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.gbQuotes.ResumeLayout(false);
			this.gbQuotes.PerformLayout();
			this.gbChapters.ResumeLayout(false);
			this.gbBooks.ResumeLayout(false);
			this.gbCollections.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Panel pnlTitle;
		private Label lblTitle;
		private Button btnClose;
		private TabControl tab1;
		private TabPage tabPage1;
		private Button btnAddCollection;
		private ListBox lbChapters;
		private ComboBox cbCollection;
		private GroupBox gbChapters;
		private Button btnDeleteChapter;
		private Button btnAddChapter;
		private GroupBox gbBooks;
		private Button btnDeleteBook;
		private ComboBox cbBooks;
		private Button btnAddBook;
		private GroupBox gbCollections;
		private Button btnDeleteCollection;
		private RichTextBox rtbQuotes;
		private GroupBox gbQuotes;
		private Button btnSaveQuotes;
		private TextBox txtStatus;
	}
}