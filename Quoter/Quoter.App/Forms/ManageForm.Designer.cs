namespace Quoter.App.Forms
{
	partial class ManageForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageForm));
			this.pnlTitle = new System.Windows.Forms.Panel();
			this.lblTitle = new System.Windows.Forms.Label();
			this.btnClose = new System.Windows.Forms.Button();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.gbQuotes = new System.Windows.Forms.GroupBox();
			this.lineNumbers_For_RichTextBox1 = new LineNumbers.LineNumbers_For_RichTextBox();
			this.rtbQuotes = new System.Windows.Forms.RichTextBox();
			this.btnSaveQuotes = new System.Windows.Forms.Button();
			this.gbChapters = new System.Windows.Forms.GroupBox();
			this.btnEditChapter = new System.Windows.Forms.Button();
			this.btnDeleteChapter = new System.Windows.Forms.Button();
			this.btnAddChapter = new System.Windows.Forms.Button();
			this.lbChapters = new System.Windows.Forms.ListBox();
			this.gbBooks = new System.Windows.Forms.GroupBox();
			this.btnEditBook = new System.Windows.Forms.Button();
			this.btnDeleteBook = new System.Windows.Forms.Button();
			this.cbBooks = new System.Windows.Forms.ComboBox();
			this.btnAddBook = new System.Windows.Forms.Button();
			this.gbCollections = new System.Windows.Forms.GroupBox();
			this.btnEditCollection = new System.Windows.Forms.Button();
			this.btnDeleteCollection = new System.Windows.Forms.Button();
			this.cbCollection = new System.Windows.Forms.ComboBox();
			this.btnAddCollection = new System.Windows.Forms.Button();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.txtStatus = new System.Windows.Forms.TextBox();
			this.btnTabPage1 = new System.Windows.Forms.Button();
			this.btnTabPage2 = new System.Windows.Forms.Button();
			this.btnTabPage3 = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.lblLanguage = new System.Windows.Forms.Label();
			this.cbLanguage = new System.Windows.Forms.ComboBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.btnThemeGray = new System.Windows.Forms.Button();
			this.btnThemeBlue = new System.Windows.Forms.Button();
			this.btnThemeGreen = new System.Windows.Forms.Button();
			this.btnThemeOrange = new System.Windows.Forms.Button();
			this.btnThemeIndianRed = new System.Windows.Forms.Button();
			this.btnThemeLightCoral = new System.Windows.Forms.Button();
			this.pnlTitle.SuspendLayout();
			this.tabControl.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.gbQuotes.SuspendLayout();
			this.gbChapters.SuspendLayout();
			this.gbBooks.SuspendLayout();
			this.gbCollections.SuspendLayout();
			this.tabPage3.SuspendLayout();
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
			// tabControl
			// 
			this.tabControl.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
			this.tabControl.Controls.Add(this.tabPage1);
			this.tabControl.Controls.Add(this.tabPage2);
			this.tabControl.Controls.Add(this.tabPage3);
			this.tabControl.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.tabControl.Location = new System.Drawing.Point(12, 82);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(776, 422);
			this.tabControl.TabIndex = 2;
			// 
			// tabPage1
			// 
			this.tabPage1.BackColor = System.Drawing.Color.WhiteSmoke;
			this.tabPage1.Controls.Add(this.gbQuotes);
			this.tabPage1.Controls.Add(this.gbChapters);
			this.tabPage1.Controls.Add(this.gbBooks);
			this.tabPage1.Controls.Add(this.gbCollections);
			this.tabPage1.Location = new System.Drawing.Point(4, 25);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(768, 393);
			this.tabPage1.TabIndex = 2;
			this.tabPage1.Text = "Create your own quotes library";
			// 
			// gbQuotes
			// 
			this.gbQuotes.BackColor = System.Drawing.Color.WhiteSmoke;
			this.gbQuotes.Controls.Add(this.lineNumbers_For_RichTextBox1);
			this.gbQuotes.Controls.Add(this.btnSaveQuotes);
			this.gbQuotes.Controls.Add(this.rtbQuotes);
			this.gbQuotes.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.gbQuotes.Location = new System.Drawing.Point(243, 6);
			this.gbQuotes.Name = "gbQuotes";
			this.gbQuotes.Size = new System.Drawing.Size(519, 382);
			this.gbQuotes.TabIndex = 17;
			this.gbQuotes.TabStop = false;
			this.gbQuotes.Text = "Quotes";
			// 
			// lineNumbers_For_RichTextBox1
			// 
			this.lineNumbers_For_RichTextBox1._SeeThroughMode_ = false;
			this.lineNumbers_For_RichTextBox1.AutoSizing = true;
			this.lineNumbers_For_RichTextBox1.BackgroundGradient_AlphaColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.lineNumbers_For_RichTextBox1.BackgroundGradient_BetaColor = System.Drawing.Color.WhiteSmoke;
			this.lineNumbers_For_RichTextBox1.BackgroundGradient_Direction = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
			this.lineNumbers_For_RichTextBox1.BorderLines_Color = System.Drawing.Color.WhiteSmoke;
			this.lineNumbers_For_RichTextBox1.BorderLines_Style = System.Drawing.Drawing2D.DashStyle.Solid;
			this.lineNumbers_For_RichTextBox1.BorderLines_Thickness = 1F;
			this.lineNumbers_For_RichTextBox1.DockSide = LineNumbers.LineNumbers_For_RichTextBox.LineNumberDockSide.Left;
			this.lineNumbers_For_RichTextBox1.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lineNumbers_For_RichTextBox1.GridLines_Color = System.Drawing.Color.LightGray;
			this.lineNumbers_For_RichTextBox1.GridLines_Style = System.Drawing.Drawing2D.DashStyle.Solid;
			this.lineNumbers_For_RichTextBox1.GridLines_Thickness = 1F;
			this.lineNumbers_For_RichTextBox1.LineNrs_Alignment = System.Drawing.ContentAlignment.TopRight;
			this.lineNumbers_For_RichTextBox1.LineNrs_AntiAlias = true;
			this.lineNumbers_For_RichTextBox1.LineNrs_AsHexadecimal = false;
			this.lineNumbers_For_RichTextBox1.LineNrs_ClippedByItemRectangle = true;
			this.lineNumbers_For_RichTextBox1.LineNrs_LeadingZeroes = true;
			this.lineNumbers_For_RichTextBox1.LineNrs_Offset = new System.Drawing.Size(0, 0);
			this.lineNumbers_For_RichTextBox1.Location = new System.Drawing.Point(5, 17);
			this.lineNumbers_For_RichTextBox1.Margin = new System.Windows.Forms.Padding(0);
			this.lineNumbers_For_RichTextBox1.MarginLines_Color = System.Drawing.Color.SlateGray;
			this.lineNumbers_For_RichTextBox1.MarginLines_Side = LineNumbers.LineNumbers_For_RichTextBox.LineNumberDockSide.Right;
			this.lineNumbers_For_RichTextBox1.MarginLines_Style = System.Drawing.Drawing2D.DashStyle.Solid;
			this.lineNumbers_For_RichTextBox1.MarginLines_Thickness = 1F;
			this.lineNumbers_For_RichTextBox1.Name = "lineNumbers_For_RichTextBox1";
			this.lineNumbers_For_RichTextBox1.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
			this.lineNumbers_For_RichTextBox1.ParentRichTextBox = this.rtbQuotes;
			this.lineNumbers_For_RichTextBox1.Show_BackgroundGradient = true;
			this.lineNumbers_For_RichTextBox1.Show_BorderLines = true;
			this.lineNumbers_For_RichTextBox1.Show_GridLines = true;
			this.lineNumbers_For_RichTextBox1.Show_LineNrs = true;
			this.lineNumbers_For_RichTextBox1.Show_MarginLines = false;
			this.lineNumbers_For_RichTextBox1.Size = new System.Drawing.Size(18, 329);
			this.lineNumbers_For_RichTextBox1.TabIndex = 14;
			// 
			// rtbQuotes
			// 
			this.rtbQuotes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.rtbQuotes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.rtbQuotes.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.rtbQuotes.Location = new System.Drawing.Point(24, 17);
			this.rtbQuotes.Name = "rtbQuotes";
			this.rtbQuotes.Size = new System.Drawing.Size(489, 329);
			this.rtbQuotes.TabIndex = 13;
			this.rtbQuotes.Text = resources.GetString("rtbQuotes.Text");
			// 
			// btnSaveQuotes
			// 
			this.btnSaveQuotes.BackColor = System.Drawing.Color.Honeydew;
			this.btnSaveQuotes.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnSaveQuotes.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnSaveQuotes.ForeColor = System.Drawing.Color.Green;
			this.btnSaveQuotes.Location = new System.Drawing.Point(445, 352);
			this.btnSaveQuotes.Name = "btnSaveQuotes";
			this.btnSaveQuotes.Size = new System.Drawing.Size(68, 24);
			this.btnSaveQuotes.TabIndex = 9;
			this.btnSaveQuotes.Text = "Save";
			this.btnSaveQuotes.UseVisualStyleBackColor = false;
			this.btnSaveQuotes.Click += new System.EventHandler(this.btnSaveQuotes_Click);
			// 
			// gbChapters
			// 
			this.gbChapters.BackColor = System.Drawing.Color.WhiteSmoke;
			this.gbChapters.Controls.Add(this.btnEditChapter);
			this.gbChapters.Controls.Add(this.btnDeleteChapter);
			this.gbChapters.Controls.Add(this.btnAddChapter);
			this.gbChapters.Controls.Add(this.lbChapters);
			this.gbChapters.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.gbChapters.Location = new System.Drawing.Point(6, 172);
			this.gbChapters.Name = "gbChapters";
			this.gbChapters.Size = new System.Drawing.Size(231, 216);
			this.gbChapters.TabIndex = 16;
			this.gbChapters.TabStop = false;
			this.gbChapters.Text = "Capitole";
			// 
			// btnEditChapter
			// 
			this.btnEditChapter.BackColor = System.Drawing.Color.WhiteSmoke;
			this.btnEditChapter.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnEditChapter.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnEditChapter.ForeColor = System.Drawing.Color.Black;
			this.btnEditChapter.Location = new System.Drawing.Point(80, 186);
			this.btnEditChapter.Name = "btnEditChapter";
			this.btnEditChapter.Size = new System.Drawing.Size(71, 24);
			this.btnEditChapter.TabIndex = 10;
			this.btnEditChapter.Text = "Modifica";
			this.btnEditChapter.UseVisualStyleBackColor = false;
			this.btnEditChapter.Click += new System.EventHandler(this.btnEditChapter_Click);
			// 
			// btnDeleteChapter
			// 
			this.btnDeleteChapter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
			this.btnDeleteChapter.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnDeleteChapter.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnDeleteChapter.ForeColor = System.Drawing.Color.DarkRed;
			this.btnDeleteChapter.Location = new System.Drawing.Point(6, 186);
			this.btnDeleteChapter.Name = "btnDeleteChapter";
			this.btnDeleteChapter.Size = new System.Drawing.Size(68, 24);
			this.btnDeleteChapter.TabIndex = 7;
			this.btnDeleteChapter.Text = "Șterge";
			this.btnDeleteChapter.UseVisualStyleBackColor = false;
			this.btnDeleteChapter.Click += new System.EventHandler(this.btnDeleteChapter_Click);
			// 
			// btnAddChapter
			// 
			this.btnAddChapter.BackColor = System.Drawing.Color.Honeydew;
			this.btnAddChapter.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnAddChapter.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnAddChapter.ForeColor = System.Drawing.Color.DarkGreen;
			this.btnAddChapter.Location = new System.Drawing.Point(157, 186);
			this.btnAddChapter.Name = "btnAddChapter";
			this.btnAddChapter.Size = new System.Drawing.Size(68, 24);
			this.btnAddChapter.TabIndex = 6;
			this.btnAddChapter.Text = "Adaugă";
			this.btnAddChapter.UseVisualStyleBackColor = false;
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
			this.gbBooks.BackColor = System.Drawing.Color.WhiteSmoke;
			this.gbBooks.Controls.Add(this.btnEditBook);
			this.gbBooks.Controls.Add(this.btnDeleteBook);
			this.gbBooks.Controls.Add(this.cbBooks);
			this.gbBooks.Controls.Add(this.btnAddBook);
			this.gbBooks.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.gbBooks.Location = new System.Drawing.Point(6, 89);
			this.gbBooks.Name = "gbBooks";
			this.gbBooks.Size = new System.Drawing.Size(231, 77);
			this.gbBooks.TabIndex = 15;
			this.gbBooks.TabStop = false;
			this.gbBooks.Text = "Book";
			// 
			// btnEditBook
			// 
			this.btnEditBook.BackColor = System.Drawing.Color.WhiteSmoke;
			this.btnEditBook.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnEditBook.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnEditBook.ForeColor = System.Drawing.Color.Black;
			this.btnEditBook.Location = new System.Drawing.Point(80, 47);
			this.btnEditBook.Name = "btnEditBook";
			this.btnEditBook.Size = new System.Drawing.Size(71, 24);
			this.btnEditBook.TabIndex = 9;
			this.btnEditBook.Text = "Modifica";
			this.btnEditBook.UseVisualStyleBackColor = false;
			this.btnEditBook.Click += new System.EventHandler(this.btnEditBook_Click);
			// 
			// btnDeleteBook
			// 
			this.btnDeleteBook.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
			this.btnDeleteBook.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnDeleteBook.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnDeleteBook.ForeColor = System.Drawing.Color.DarkRed;
			this.btnDeleteBook.Location = new System.Drawing.Point(6, 47);
			this.btnDeleteBook.Name = "btnDeleteBook";
			this.btnDeleteBook.Size = new System.Drawing.Size(68, 24);
			this.btnDeleteBook.TabIndex = 7;
			this.btnDeleteBook.Text = "Șterge";
			this.btnDeleteBook.UseVisualStyleBackColor = false;
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
			this.btnAddBook.BackColor = System.Drawing.Color.Honeydew;
			this.btnAddBook.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnAddBook.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnAddBook.ForeColor = System.Drawing.Color.DarkGreen;
			this.btnAddBook.Location = new System.Drawing.Point(157, 47);
			this.btnAddBook.Name = "btnAddBook";
			this.btnAddBook.Size = new System.Drawing.Size(68, 24);
			this.btnAddBook.TabIndex = 6;
			this.btnAddBook.Text = "Adaugă";
			this.btnAddBook.UseVisualStyleBackColor = false;
			this.btnAddBook.Click += new System.EventHandler(this.btnAddBook_Click);
			// 
			// gbCollections
			// 
			this.gbCollections.BackColor = System.Drawing.Color.WhiteSmoke;
			this.gbCollections.Controls.Add(this.btnEditCollection);
			this.gbCollections.Controls.Add(this.btnDeleteCollection);
			this.gbCollections.Controls.Add(this.cbCollection);
			this.gbCollections.Controls.Add(this.btnAddCollection);
			this.gbCollections.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.gbCollections.Location = new System.Drawing.Point(6, 6);
			this.gbCollections.Name = "gbCollections";
			this.gbCollections.Size = new System.Drawing.Size(231, 77);
			this.gbCollections.TabIndex = 14;
			this.gbCollections.TabStop = false;
			this.gbCollections.Text = "Colectia";
			// 
			// btnEditCollection
			// 
			this.btnEditCollection.BackColor = System.Drawing.Color.WhiteSmoke;
			this.btnEditCollection.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnEditCollection.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnEditCollection.ForeColor = System.Drawing.Color.Black;
			this.btnEditCollection.Location = new System.Drawing.Point(80, 47);
			this.btnEditCollection.Name = "btnEditCollection";
			this.btnEditCollection.Size = new System.Drawing.Size(71, 24);
			this.btnEditCollection.TabIndex = 8;
			this.btnEditCollection.Text = "Modifica";
			this.btnEditCollection.UseVisualStyleBackColor = false;
			this.btnEditCollection.Click += new System.EventHandler(this.buttonEditCollection_Click);
			// 
			// btnDeleteCollection
			// 
			this.btnDeleteCollection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
			this.btnDeleteCollection.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.btnDeleteCollection.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnDeleteCollection.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnDeleteCollection.ForeColor = System.Drawing.Color.DarkRed;
			this.btnDeleteCollection.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnDeleteCollection.Location = new System.Drawing.Point(6, 47);
			this.btnDeleteCollection.Name = "btnDeleteCollection";
			this.btnDeleteCollection.Size = new System.Drawing.Size(68, 24);
			this.btnDeleteCollection.TabIndex = 7;
			this.btnDeleteCollection.Text = "Delete";
			this.btnDeleteCollection.UseVisualStyleBackColor = false;
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
			this.btnAddCollection.BackColor = System.Drawing.Color.Honeydew;
			this.btnAddCollection.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnAddCollection.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnAddCollection.ForeColor = System.Drawing.Color.DarkGreen;
			this.btnAddCollection.Location = new System.Drawing.Point(157, 47);
			this.btnAddCollection.Name = "btnAddCollection";
			this.btnAddCollection.Size = new System.Drawing.Size(68, 24);
			this.btnAddCollection.TabIndex = 6;
			this.btnAddCollection.Text = "Adaugă";
			this.btnAddCollection.UseVisualStyleBackColor = false;
			this.btnAddCollection.Click += new System.EventHandler(this.btnAddCollection_Click);
			// 
			// tabPage2
			// 
			this.tabPage2.BackColor = System.Drawing.Color.WhiteSmoke;
			this.tabPage2.Location = new System.Drawing.Point(4, 25);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(768, 393);
			this.tabPage2.TabIndex = 3;
			this.tabPage2.Text = "Quotes collection";
			// 
			// tabPage3
			// 
			this.tabPage3.BackColor = System.Drawing.Color.WhiteSmoke;
			this.tabPage3.Controls.Add(this.btnThemeLightCoral);
			this.tabPage3.Controls.Add(this.btnThemeIndianRed);
			this.tabPage3.Controls.Add(this.btnThemeOrange);
			this.tabPage3.Controls.Add(this.btnThemeGreen);
			this.tabPage3.Controls.Add(this.btnThemeBlue);
			this.tabPage3.Controls.Add(this.btnThemeGray);
			this.tabPage3.Controls.Add(this.label5);
			this.tabPage3.Controls.Add(this.label4);
			this.tabPage3.Controls.Add(this.textBox2);
			this.tabPage3.Controls.Add(this.label3);
			this.tabPage3.Controls.Add(this.label2);
			this.tabPage3.Controls.Add(this.label1);
			this.tabPage3.Controls.Add(this.textBox1);
			this.tabPage3.Controls.Add(this.cbLanguage);
			this.tabPage3.Controls.Add(this.lblLanguage);
			this.tabPage3.Location = new System.Drawing.Point(4, 25);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage3.Size = new System.Drawing.Size(768, 393);
			this.tabPage3.TabIndex = 4;
			this.tabPage3.Text = "Settings";
			// 
			// txtStatus
			// 
			this.txtStatus.BackColor = System.Drawing.Color.WhiteSmoke;
			this.txtStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtStatus.ForeColor = System.Drawing.Color.Green;
			this.txtStatus.Location = new System.Drawing.Point(16, 504);
			this.txtStatus.Multiline = true;
			this.txtStatus.Name = "txtStatus";
			this.txtStatus.Size = new System.Drawing.Size(762, 19);
			this.txtStatus.TabIndex = 15;
			this.txtStatus.Text = "Status acesta este un status ca s-a facut ceva corect bravo tie mai ce sa zicem";
			// 
			// btnTabPage1
			// 
			this.btnTabPage1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnTabPage1.Font = new System.Drawing.Font("Calibri Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnTabPage1.Location = new System.Drawing.Point(12, 46);
			this.btnTabPage1.Name = "btnTabPage1";
			this.btnTabPage1.Size = new System.Drawing.Size(249, 30);
			this.btnTabPage1.TabIndex = 3;
			this.btnTabPage1.Text = "Create and edit quotes";
			this.btnTabPage1.UseVisualStyleBackColor = true;
			this.btnTabPage1.Click += new System.EventHandler(this.btnTabPage1_Click);
			// 
			// btnTabPage2
			// 
			this.btnTabPage2.FlatAppearance.BorderSize = 0;
			this.btnTabPage2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnTabPage2.Font = new System.Drawing.Font("Calibri Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnTabPage2.Location = new System.Drawing.Point(267, 46);
			this.btnTabPage2.Name = "btnTabPage2";
			this.btnTabPage2.Size = new System.Drawing.Size(258, 30);
			this.btnTabPage2.TabIndex = 4;
			this.btnTabPage2.Text = "Quotes collection";
			this.btnTabPage2.UseVisualStyleBackColor = true;
			this.btnTabPage2.Click += new System.EventHandler(this.btnTabPage2_Click);
			// 
			// btnTabPage3
			// 
			this.btnTabPage3.FlatAppearance.BorderSize = 0;
			this.btnTabPage3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnTabPage3.Font = new System.Drawing.Font("Calibri Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnTabPage3.Location = new System.Drawing.Point(531, 46);
			this.btnTabPage3.Name = "btnTabPage3";
			this.btnTabPage3.Size = new System.Drawing.Size(253, 30);
			this.btnTabPage3.TabIndex = 5;
			this.btnTabPage3.Text = "Settings";
			this.btnTabPage3.UseVisualStyleBackColor = true;
			this.btnTabPage3.Click += new System.EventHandler(this.btnTabPage3_Click);
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.LightGray;
			this.panel1.Location = new System.Drawing.Point(262, 46);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(3, 30);
			this.panel1.TabIndex = 6;
			// 
			// panel2
			// 
			this.panel2.BackColor = System.Drawing.Color.LightGray;
			this.panel2.Location = new System.Drawing.Point(527, 46);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(3, 30);
			this.panel2.TabIndex = 7;
			// 
			// lblLanguage
			// 
			this.lblLanguage.AutoSize = true;
			this.lblLanguage.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblLanguage.Location = new System.Drawing.Point(15, 26);
			this.lblLanguage.Name = "lblLanguage";
			this.lblLanguage.Size = new System.Drawing.Size(66, 18);
			this.lblLanguage.TabIndex = 0;
			this.lblLanguage.Text = "Language";
			// 
			// cbLanguage
			// 
			this.cbLanguage.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.cbLanguage.FormattingEnabled = true;
			this.cbLanguage.Location = new System.Drawing.Point(204, 23);
			this.cbLanguage.Name = "cbLanguage";
			this.cbLanguage.Size = new System.Drawing.Size(199, 26);
			this.cbLanguage.TabIndex = 1;
			// 
			// textBox1
			// 
			this.textBox1.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.textBox1.Location = new System.Drawing.Point(253, 55);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(42, 26);
			this.textBox1.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label1.Location = new System.Drawing.Point(15, 58);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(157, 18);
			this.label1.TabIndex = 3;
			this.label1.Text = "Arată notificări la fiecare";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label2.Location = new System.Drawing.Point(301, 58);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(59, 18);
			this.label2.TabIndex = 4;
			this.label2.Text = "minutes";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label3.Location = new System.Drawing.Point(15, 90);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(232, 18);
			this.label3.TabIndex = 5;
			this.label3.Text = "Auto-close notification window after";
			// 
			// textBox2
			// 
			this.textBox2.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.textBox2.Location = new System.Drawing.Point(253, 87);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(42, 26);
			this.textBox2.TabIndex = 6;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label4.Location = new System.Drawing.Point(301, 90);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(58, 18);
			this.label4.TabIndex = 7;
			this.label4.Text = "seconds";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label5.Location = new System.Drawing.Point(15, 124);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(104, 18);
			this.label5.TabIndex = 8;
			this.label5.Text = "Window theme";
			// 
			// btnThemeGray
			// 
			this.btnThemeGray.BackColor = System.Drawing.Color.SlateGray;
			this.btnThemeGray.FlatAppearance.BorderSize = 2;
			this.btnThemeGray.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnThemeGray.Location = new System.Drawing.Point(253, 119);
			this.btnThemeGray.Name = "btnThemeGray";
			this.btnThemeGray.Size = new System.Drawing.Size(46, 44);
			this.btnThemeGray.TabIndex = 9;
			this.btnThemeGray.UseVisualStyleBackColor = false;
			// 
			// btnThemeBlue
			// 
			this.btnThemeBlue.BackColor = System.Drawing.Color.SteelBlue;
			this.btnThemeBlue.FlatAppearance.BorderSize = 0;
			this.btnThemeBlue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnThemeBlue.Location = new System.Drawing.Point(305, 119);
			this.btnThemeBlue.Name = "btnThemeBlue";
			this.btnThemeBlue.Size = new System.Drawing.Size(46, 44);
			this.btnThemeBlue.TabIndex = 10;
			this.btnThemeBlue.UseVisualStyleBackColor = false;
			// 
			// btnThemeGreen
			// 
			this.btnThemeGreen.BackColor = System.Drawing.Color.Green;
			this.btnThemeGreen.FlatAppearance.BorderSize = 0;
			this.btnThemeGreen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnThemeGreen.Location = new System.Drawing.Point(357, 119);
			this.btnThemeGreen.Name = "btnThemeGreen";
			this.btnThemeGreen.Size = new System.Drawing.Size(46, 44);
			this.btnThemeGreen.TabIndex = 11;
			this.btnThemeGreen.UseVisualStyleBackColor = false;
			// 
			// btnThemeOrange
			// 
			this.btnThemeOrange.BackColor = System.Drawing.Color.DarkOrange;
			this.btnThemeOrange.FlatAppearance.BorderSize = 0;
			this.btnThemeOrange.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnThemeOrange.Location = new System.Drawing.Point(253, 169);
			this.btnThemeOrange.Name = "btnThemeOrange";
			this.btnThemeOrange.Size = new System.Drawing.Size(46, 44);
			this.btnThemeOrange.TabIndex = 12;
			this.btnThemeOrange.UseVisualStyleBackColor = false;
			// 
			// btnThemeIndianRed
			// 
			this.btnThemeIndianRed.BackColor = System.Drawing.Color.IndianRed;
			this.btnThemeIndianRed.FlatAppearance.BorderSize = 0;
			this.btnThemeIndianRed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnThemeIndianRed.Location = new System.Drawing.Point(357, 169);
			this.btnThemeIndianRed.Name = "btnThemeIndianRed";
			this.btnThemeIndianRed.Size = new System.Drawing.Size(46, 44);
			this.btnThemeIndianRed.TabIndex = 13;
			this.btnThemeIndianRed.UseVisualStyleBackColor = false;
			// 
			// btnThemeLightCoral
			// 
			this.btnThemeLightCoral.BackColor = System.Drawing.Color.LightCoral;
			this.btnThemeLightCoral.FlatAppearance.BorderSize = 0;
			this.btnThemeLightCoral.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnThemeLightCoral.Location = new System.Drawing.Point(305, 169);
			this.btnThemeLightCoral.Name = "btnThemeLightCoral";
			this.btnThemeLightCoral.Size = new System.Drawing.Size(46, 44);
			this.btnThemeLightCoral.TabIndex = 14;
			this.btnThemeLightCoral.UseVisualStyleBackColor = false;
			// 
			// ManageQuotesForm
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.WhiteSmoke;
			this.ClientSize = new System.Drawing.Size(800, 528);
			this.ControlBox = false;
			this.Controls.Add(this.txtStatus);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.btnTabPage3);
			this.Controls.Add(this.btnTabPage2);
			this.Controls.Add(this.btnTabPage1);
			this.Controls.Add(this.tabControl);
			this.Controls.Add(this.pnlTitle);
			this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ManageQuotesForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "ManageQuotesForm";
			this.Load += new System.EventHandler(this.ManageQuotesForm_Load);
			this.pnlTitle.ResumeLayout(false);
			this.pnlTitle.PerformLayout();
			this.tabControl.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.gbQuotes.ResumeLayout(false);
			this.gbChapters.ResumeLayout(false);
			this.gbBooks.ResumeLayout(false);
			this.gbCollections.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			this.tabPage3.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Panel pnlTitle;
		private Label lblTitle;
		private Button btnClose;
		private TabControl tabControl;
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
		private Button btnEditChapter;
		private Button btnEditBook;
		private Button btnEditCollection;
		private Button btnTabPage1;
		private Button btnTabPage2;
		private Button btnTabPage3;
		private Panel panel1;
		private Panel panel2;
		private TabPage tabPage2;
		private TabPage tabPage3;
		private LineNumbers.LineNumbers_For_RichTextBox lineNumbers_For_RichTextBox1;
		private ComboBox cbLanguage;
		private Label lblLanguage;
		private Label label4;
		private TextBox textBox2;
		private Label label3;
		private Label label2;
		private Label label1;
		private TextBox textBox1;
		private Button btnThemeLightCoral;
		private Button btnThemeIndianRed;
		private Button btnThemeOrange;
		private Button btnThemeGreen;
		private Button btnThemeBlue;
		private Button btnThemeGray;
		private Label label5;
	}
}