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
			this.btnReloadEditCollections = new System.Windows.Forms.Button();
			this.gbQuotes = new System.Windows.Forms.GroupBox();
			this.btnQuotesOptions = new System.Windows.Forms.Button();
			this.pnlQuotesOptions = new System.Windows.Forms.Panel();
			this.chkWordWrap = new System.Windows.Forms.CheckBox();
			this.lblQuotesAppendEnd = new System.Windows.Forms.Label();
			this.txtQuotesAppendTextToEnd = new System.Windows.Forms.TextBox();
			this.lblQuotesAppendStart = new System.Windows.Forms.Label();
			this.txtQuotesAppendedTextToBeginning = new System.Windows.Forms.TextBox();
			this.txtQuotesExcludedChars = new System.Windows.Forms.TextBox();
			this.lblQuotesExcludeChars = new System.Windows.Forms.Label();
			this.chkQuotesTrimRow = new System.Windows.Forms.CheckBox();
			this.lineNumbers_For_RichTextBox1 = new LineNumbers.LineNumbers_For_RichTextBox();
			this.rtbQuotes = new System.Windows.Forms.RichTextBox();
			this.btnSaveQuotes = new System.Windows.Forms.Button();
			this.gbChapters = new System.Windows.Forms.GroupBox();
			this.btnAddFirstChapter = new System.Windows.Forms.Button();
			this.btnEditChapter = new System.Windows.Forms.Button();
			this.btnDeleteChapter = new System.Windows.Forms.Button();
			this.btnAddChapter = new System.Windows.Forms.Button();
			this.lbChapters = new System.Windows.Forms.ListBox();
			this.gbBooks = new System.Windows.Forms.GroupBox();
			this.btnAddFirstBook = new System.Windows.Forms.Button();
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
			this.btnRefreshFavouriteCollections = new System.Windows.Forms.Button();
			this.gbImport = new System.Windows.Forms.GroupBox();
			this.chkImportMerge = new System.Windows.Forms.CheckBox();
			this.chkImportIgnoreLanguage = new System.Windows.Forms.CheckBox();
			this.btnImport = new System.Windows.Forms.Button();
			this.gbExport = new System.Windows.Forms.GroupBox();
			this.chkExportFavCollections = new System.Windows.Forms.CheckBox();
			this.btnExportCollection = new System.Windows.Forms.Button();
			this.lblFavoritesText = new System.Windows.Forms.Label();
			this.lblFavouriteChapters = new System.Windows.Forms.Label();
			this.lblFavouriteBooks = new System.Windows.Forms.Label();
			this.lblFavouriteCollections = new System.Windows.Forms.Label();
			this.clbChapters = new System.Windows.Forms.CheckedListBox();
			this.clbBooks = new System.Windows.Forms.CheckedListBox();
			this.clbCollections = new System.Windows.Forms.CheckedListBox();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.tabBasicSettings = new System.Windows.Forms.TabControl();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.gbQuotesSettings = new System.Windows.Forms.GroupBox();
			this.lblNotificationFont = new System.Windows.Forms.Label();
			this.txtSelectedFont = new System.Windows.Forms.TextBox();
			this.btnNotificationFont = new System.Windows.Forms.Button();
			this.lblQuotesAutocloseTime = new System.Windows.Forms.Label();
			this.txtQuotesAutoCloseInterval = new System.Windows.Forms.TextBox();
			this.lblQuotesAutocloseInterval = new System.Windows.Forms.Label();
			this.lblNotificationLocation = new System.Windows.Forms.Label();
			this.panel3 = new System.Windows.Forms.Panel();
			this.rbAnimTopRight = new System.Windows.Forms.RadioButton();
			this.rbAnimBottomRight = new System.Windows.Forms.RadioButton();
			this.rbAnimBottomLeft = new System.Windows.Forms.RadioButton();
			this.rbAnimTopLeft = new System.Windows.Forms.RadioButton();
			this.lblQuotesFrequencyTime = new System.Windows.Forms.Label();
			this.btnAlwaysOnNotifications = new System.Windows.Forms.Button();
			this.txtQuotesInterval = new System.Windows.Forms.TextBox();
			this.lblQuotesFrequency = new System.Windows.Forms.Label();
			this.btnPopupNotifications = new System.Windows.Forms.Button();
			this.lblNotificationType = new System.Windows.Forms.Label();
			this.gbOtherSettings = new System.Windows.Forms.GroupBox();
			this.btnShowWelcomeMsgNo = new System.Windows.Forms.Button();
			this.btnShowWelcomeMsgYes = new System.Windows.Forms.Button();
			this.lblShowWelcomeMsg = new System.Windows.Forms.Label();
			this.gbThemeSettings = new System.Windows.Forms.GroupBox();
			this.lblOpacityPercent = new System.Windows.Forms.Label();
			this.lblOpacity = new System.Windows.Forms.Label();
			this.tbOpacity = new System.Windows.Forms.TrackBar();
			this.btnTheme1 = new System.Windows.Forms.Button();
			this.btnTheme2 = new System.Windows.Forms.Button();
			this.btnTheme3 = new System.Windows.Forms.Button();
			this.btnTheme6 = new System.Windows.Forms.Button();
			this.btnTheme5 = new System.Windows.Forms.Button();
			this.btnTheme4 = new System.Windows.Forms.Button();
			this.gbLanguageSettings = new System.Windows.Forms.GroupBox();
			this.btnLanguageFr = new System.Windows.Forms.Button();
			this.btnShowCollBasedOnLanguageNo = new System.Windows.Forms.Button();
			this.btnLanguageRo = new System.Windows.Forms.Button();
			this.btnShowCollBasedOnLanguageYes = new System.Windows.Forms.Button();
			this.btnLanguageEn = new System.Windows.Forms.Button();
			this.lblShowCollByLanguage = new System.Windows.Forms.Label();
			this.txtStatus = new System.Windows.Forms.TextBox();
			this.btnTabPage1 = new System.Windows.Forms.Button();
			this.btnTabPage2 = new System.Windows.Forms.Button();
			this.btnTabPage3 = new System.Windows.Forms.Button();
			this.pnlSelectedTab = new System.Windows.Forms.Panel();
			this.pbBackgroundTask = new System.Windows.Forms.PictureBox();
			this.lblBackgroundTask = new System.Windows.Forms.Label();
			this.lblStartWithWindows = new System.Windows.Forms.Label();
			this.btnStartWithWindowsNo = new System.Windows.Forms.Button();
			this.btnStartWithWindowsYes = new System.Windows.Forms.Button();
			this.pnlTitle.SuspendLayout();
			this.tabControl.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.gbQuotes.SuspendLayout();
			this.pnlQuotesOptions.SuspendLayout();
			this.gbChapters.SuspendLayout();
			this.gbBooks.SuspendLayout();
			this.gbCollections.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.gbImport.SuspendLayout();
			this.gbExport.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.tabBasicSettings.SuspendLayout();
			this.tabPage4.SuspendLayout();
			this.gbQuotesSettings.SuspendLayout();
			this.panel3.SuspendLayout();
			this.gbOtherSettings.SuspendLayout();
			this.gbThemeSettings.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.tbOpacity)).BeginInit();
			this.gbLanguageSettings.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbBackgroundTask)).BeginInit();
			this.SuspendLayout();
			// 
			// pnlTitle
			// 
			this.pnlTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlTitle.BackColor = System.Drawing.Color.SteelBlue;
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
			this.tabControl.Location = new System.Drawing.Point(0, 75);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(798, 429);
			this.tabControl.TabIndex = 2;
			// 
			// tabPage1
			// 
			this.tabPage1.BackColor = System.Drawing.Color.WhiteSmoke;
			this.tabPage1.Controls.Add(this.btnReloadEditCollections);
			this.tabPage1.Controls.Add(this.gbQuotes);
			this.tabPage1.Controls.Add(this.gbChapters);
			this.tabPage1.Controls.Add(this.gbBooks);
			this.tabPage1.Controls.Add(this.gbCollections);
			this.tabPage1.Location = new System.Drawing.Point(4, 25);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(790, 400);
			this.tabPage1.TabIndex = 2;
			this.tabPage1.Text = "Create your own quotes library";
			// 
			// btnReloadEditCollections
			// 
			this.btnReloadEditCollections.BackgroundImage = global::Quoter.App.Resources.Resources.refresh_32;
			this.btnReloadEditCollections.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.btnReloadEditCollections.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
			this.btnReloadEditCollections.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnReloadEditCollections.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnReloadEditCollections.Location = new System.Drawing.Point(207, 4);
			this.btnReloadEditCollections.Name = "btnReloadEditCollections";
			this.btnReloadEditCollections.Size = new System.Drawing.Size(24, 24);
			this.btnReloadEditCollections.TabIndex = 18;
			this.btnReloadEditCollections.UseVisualStyleBackColor = true;
			this.btnReloadEditCollections.Click += new System.EventHandler(this.btnReloadEditCollections_Click);
			// 
			// gbQuotes
			// 
			this.gbQuotes.BackColor = System.Drawing.Color.WhiteSmoke;
			this.gbQuotes.Controls.Add(this.btnQuotesOptions);
			this.gbQuotes.Controls.Add(this.pnlQuotesOptions);
			this.gbQuotes.Controls.Add(this.lineNumbers_For_RichTextBox1);
			this.gbQuotes.Controls.Add(this.btnSaveQuotes);
			this.gbQuotes.Controls.Add(this.rtbQuotes);
			this.gbQuotes.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.gbQuotes.Location = new System.Drawing.Point(243, 6);
			this.gbQuotes.Name = "gbQuotes";
			this.gbQuotes.Size = new System.Drawing.Size(541, 410);
			this.gbQuotes.TabIndex = 17;
			this.gbQuotes.TabStop = false;
			this.gbQuotes.Text = "Quotes";
			// 
			// btnQuotesOptions
			// 
			this.btnQuotesOptions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnQuotesOptions.Location = new System.Drawing.Point(42, 380);
			this.btnQuotesOptions.Name = "btnQuotesOptions";
			this.btnQuotesOptions.Size = new System.Drawing.Size(68, 24);
			this.btnQuotesOptions.TabIndex = 16;
			this.btnQuotesOptions.Text = "Options";
			this.btnQuotesOptions.UseVisualStyleBackColor = true;
			this.btnQuotesOptions.Click += new System.EventHandler(this.btnQuotesOptions_Click);
			// 
			// pnlQuotesOptions
			// 
			this.pnlQuotesOptions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlQuotesOptions.Controls.Add(this.chkWordWrap);
			this.pnlQuotesOptions.Controls.Add(this.lblQuotesAppendEnd);
			this.pnlQuotesOptions.Controls.Add(this.txtQuotesAppendTextToEnd);
			this.pnlQuotesOptions.Controls.Add(this.lblQuotesAppendStart);
			this.pnlQuotesOptions.Controls.Add(this.txtQuotesAppendedTextToBeginning);
			this.pnlQuotesOptions.Controls.Add(this.txtQuotesExcludedChars);
			this.pnlQuotesOptions.Controls.Add(this.lblQuotesExcludeChars);
			this.pnlQuotesOptions.Controls.Add(this.chkQuotesTrimRow);
			this.pnlQuotesOptions.Location = new System.Drawing.Point(42, 211);
			this.pnlQuotesOptions.Name = "pnlQuotesOptions";
			this.pnlQuotesOptions.Size = new System.Drawing.Size(274, 170);
			this.pnlQuotesOptions.TabIndex = 15;
			// 
			// chkWordWrap
			// 
			this.chkWordWrap.AutoSize = true;
			this.chkWordWrap.Checked = true;
			this.chkWordWrap.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkWordWrap.Location = new System.Drawing.Point(9, 139);
			this.chkWordWrap.Name = "chkWordWrap";
			this.chkWordWrap.Size = new System.Drawing.Size(87, 19);
			this.chkWordWrap.TabIndex = 7;
			this.chkWordWrap.Text = "Word wrap";
			this.chkWordWrap.UseVisualStyleBackColor = true;
			this.chkWordWrap.CheckedChanged += new System.EventHandler(this.chkWordWrap_CheckedChanged);
			// 
			// lblQuotesAppendEnd
			// 
			this.lblQuotesAppendEnd.Location = new System.Drawing.Point(77, 106);
			this.lblQuotesAppendEnd.Name = "lblQuotesAppendEnd";
			this.lblQuotesAppendEnd.Size = new System.Drawing.Size(192, 30);
			this.lblQuotesAppendEnd.TabIndex = 6;
			this.lblQuotesAppendEnd.Text = "Append text to end of quote";
			// 
			// txtQuotesAppendTextToEnd
			// 
			this.txtQuotesAppendTextToEnd.Location = new System.Drawing.Point(9, 109);
			this.txtQuotesAppendTextToEnd.Name = "txtQuotesAppendTextToEnd";
			this.txtQuotesAppendTextToEnd.Size = new System.Drawing.Size(62, 23);
			this.txtQuotesAppendTextToEnd.TabIndex = 5;
			// 
			// lblQuotesAppendStart
			// 
			this.lblQuotesAppendStart.Location = new System.Drawing.Point(77, 75);
			this.lblQuotesAppendStart.Name = "lblQuotesAppendStart";
			this.lblQuotesAppendStart.Size = new System.Drawing.Size(192, 30);
			this.lblQuotesAppendStart.TabIndex = 4;
			this.lblQuotesAppendStart.Text = "Append text to the beginning of quote";
			// 
			// txtQuotesAppendedTextToBeginning
			// 
			this.txtQuotesAppendedTextToBeginning.Location = new System.Drawing.Point(9, 80);
			this.txtQuotesAppendedTextToBeginning.Name = "txtQuotesAppendedTextToBeginning";
			this.txtQuotesAppendedTextToBeginning.Size = new System.Drawing.Size(62, 23);
			this.txtQuotesAppendedTextToBeginning.TabIndex = 3;
			// 
			// txtQuotesExcludedChars
			// 
			this.txtQuotesExcludedChars.Location = new System.Drawing.Point(9, 37);
			this.txtQuotesExcludedChars.Multiline = true;
			this.txtQuotesExcludedChars.Name = "txtQuotesExcludedChars";
			this.txtQuotesExcludedChars.Size = new System.Drawing.Size(62, 37);
			this.txtQuotesExcludedChars.TabIndex = 2;
			// 
			// lblQuotesExcludeChars
			// 
			this.lblQuotesExcludeChars.Location = new System.Drawing.Point(77, 36);
			this.lblQuotesExcludeChars.Name = "lblQuotesExcludeChars";
			this.lblQuotesExcludeChars.Size = new System.Drawing.Size(180, 48);
			this.lblQuotesExcludeChars.TabIndex = 1;
			this.lblQuotesExcludeChars.Text = "Exclude characters ( input characters with no spaces)";
			// 
			// chkQuotesTrimRow
			// 
			this.chkQuotesTrimRow.AutoSize = true;
			this.chkQuotesTrimRow.Location = new System.Drawing.Point(9, 12);
			this.chkQuotesTrimRow.Name = "chkQuotesTrimRow";
			this.chkQuotesTrimRow.Size = new System.Drawing.Size(222, 19);
			this.chkQuotesTrimRow.TabIndex = 0;
			this.chkQuotesTrimRow.Text = "Trim row untill first space character";
			this.chkQuotesTrimRow.UseVisualStyleBackColor = true;
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
			this.lineNumbers_For_RichTextBox1.Location = new System.Drawing.Point(12, 17);
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
			this.lineNumbers_For_RichTextBox1.Size = new System.Drawing.Size(29, 357);
			this.lineNumbers_For_RichTextBox1.TabIndex = 14;
			// 
			// rtbQuotes
			// 
			this.rtbQuotes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.rtbQuotes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.rtbQuotes.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.rtbQuotes.Location = new System.Drawing.Point(42, 17);
			this.rtbQuotes.Name = "rtbQuotes";
			this.rtbQuotes.Size = new System.Drawing.Size(493, 357);
			this.rtbQuotes.TabIndex = 13;
			this.rtbQuotes.Text = "1\n2\n3\n3\n5\n6\n7\n8\n1\n";
			// 
			// btnSaveQuotes
			// 
			this.btnSaveQuotes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSaveQuotes.BackColor = System.Drawing.Color.Honeydew;
			this.btnSaveQuotes.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnSaveQuotes.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnSaveQuotes.ForeColor = System.Drawing.Color.Green;
			this.btnSaveQuotes.Location = new System.Drawing.Point(467, 380);
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
			this.gbChapters.Controls.Add(this.btnAddFirstChapter);
			this.gbChapters.Controls.Add(this.btnEditChapter);
			this.gbChapters.Controls.Add(this.btnDeleteChapter);
			this.gbChapters.Controls.Add(this.btnAddChapter);
			this.gbChapters.Controls.Add(this.lbChapters);
			this.gbChapters.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.gbChapters.Location = new System.Drawing.Point(6, 180);
			this.gbChapters.Name = "gbChapters";
			this.gbChapters.Size = new System.Drawing.Size(231, 236);
			this.gbChapters.TabIndex = 16;
			this.gbChapters.TabStop = false;
			this.gbChapters.Text = "Capitole";
			// 
			// btnAddFirstChapter
			// 
			this.btnAddFirstChapter.BackColor = System.Drawing.Color.Honeydew;
			this.btnAddFirstChapter.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnAddFirstChapter.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnAddFirstChapter.ForeColor = System.Drawing.Color.DarkGreen;
			this.btnAddFirstChapter.Location = new System.Drawing.Point(81, 96);
			this.btnAddFirstChapter.Name = "btnAddFirstChapter";
			this.btnAddFirstChapter.Size = new System.Drawing.Size(68, 24);
			this.btnAddFirstChapter.TabIndex = 11;
			this.btnAddFirstChapter.Text = "Adaugă";
			this.btnAddFirstChapter.UseVisualStyleBackColor = false;
			this.btnAddFirstChapter.Click += new System.EventHandler(this.btnAddFirstChapter_Click);
			// 
			// btnEditChapter
			// 
			this.btnEditChapter.BackColor = System.Drawing.Color.WhiteSmoke;
			this.btnEditChapter.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnEditChapter.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnEditChapter.ForeColor = System.Drawing.Color.Black;
			this.btnEditChapter.Location = new System.Drawing.Point(80, 192);
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
			this.btnDeleteChapter.Location = new System.Drawing.Point(6, 192);
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
			this.btnAddChapter.Location = new System.Drawing.Point(157, 192);
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
			this.gbBooks.Controls.Add(this.btnAddFirstBook);
			this.gbBooks.Controls.Add(this.btnEditBook);
			this.gbBooks.Controls.Add(this.btnDeleteBook);
			this.gbBooks.Controls.Add(this.cbBooks);
			this.gbBooks.Controls.Add(this.btnAddBook);
			this.gbBooks.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.gbBooks.Location = new System.Drawing.Point(6, 97);
			this.gbBooks.Name = "gbBooks";
			this.gbBooks.Size = new System.Drawing.Size(231, 77);
			this.gbBooks.TabIndex = 15;
			this.gbBooks.TabStop = false;
			this.gbBooks.Text = "Book";
			// 
			// btnAddFirstBook
			// 
			this.btnAddFirstBook.BackColor = System.Drawing.Color.Honeydew;
			this.btnAddFirstBook.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnAddFirstBook.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnAddFirstBook.ForeColor = System.Drawing.Color.DarkGreen;
			this.btnAddFirstBook.Location = new System.Drawing.Point(81, 26);
			this.btnAddFirstBook.Name = "btnAddFirstBook";
			this.btnAddFirstBook.Size = new System.Drawing.Size(68, 24);
			this.btnAddFirstBook.TabIndex = 10;
			this.btnAddFirstBook.Text = "Adaugă";
			this.btnAddFirstBook.UseVisualStyleBackColor = false;
			this.btnAddFirstBook.Click += new System.EventHandler(this.btnAddFirstBook_Click);
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
			this.cbBooks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
			this.gbCollections.Size = new System.Drawing.Size(231, 85);
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
			this.btnEditCollection.Location = new System.Drawing.Point(80, 55);
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
			this.btnDeleteCollection.Location = new System.Drawing.Point(6, 55);
			this.btnDeleteCollection.Name = "btnDeleteCollection";
			this.btnDeleteCollection.Size = new System.Drawing.Size(68, 24);
			this.btnDeleteCollection.TabIndex = 7;
			this.btnDeleteCollection.Text = "Delete";
			this.btnDeleteCollection.UseVisualStyleBackColor = false;
			this.btnDeleteCollection.Click += new System.EventHandler(this.btnDeleteCollection_Click);
			// 
			// cbCollection
			// 
			this.cbCollection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbCollection.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.cbCollection.FormattingEnabled = true;
			this.cbCollection.Location = new System.Drawing.Point(6, 24);
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
			this.btnAddCollection.Location = new System.Drawing.Point(157, 55);
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
			this.tabPage2.Controls.Add(this.btnRefreshFavouriteCollections);
			this.tabPage2.Controls.Add(this.gbImport);
			this.tabPage2.Controls.Add(this.gbExport);
			this.tabPage2.Controls.Add(this.lblFavoritesText);
			this.tabPage2.Controls.Add(this.lblFavouriteChapters);
			this.tabPage2.Controls.Add(this.lblFavouriteBooks);
			this.tabPage2.Controls.Add(this.lblFavouriteCollections);
			this.tabPage2.Controls.Add(this.clbChapters);
			this.tabPage2.Controls.Add(this.clbBooks);
			this.tabPage2.Controls.Add(this.clbCollections);
			this.tabPage2.Location = new System.Drawing.Point(4, 25);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(790, 400);
			this.tabPage2.TabIndex = 3;
			this.tabPage2.Text = "Quotes collection";
			// 
			// btnRefreshFavouriteCollections
			// 
			this.btnRefreshFavouriteCollections.BackgroundImage = global::Quoter.App.Resources.Resources.refresh_32;
			this.btnRefreshFavouriteCollections.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.btnRefreshFavouriteCollections.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
			this.btnRefreshFavouriteCollections.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnRefreshFavouriteCollections.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnRefreshFavouriteCollections.Location = new System.Drawing.Point(221, 51);
			this.btnRefreshFavouriteCollections.Name = "btnRefreshFavouriteCollections";
			this.btnRefreshFavouriteCollections.Size = new System.Drawing.Size(24, 24);
			this.btnRefreshFavouriteCollections.TabIndex = 14;
			this.btnRefreshFavouriteCollections.UseVisualStyleBackColor = true;
			this.btnRefreshFavouriteCollections.Click += new System.EventHandler(this.btnRefreshFavouriteCollections_Click);
			// 
			// gbImport
			// 
			this.gbImport.Controls.Add(this.chkImportMerge);
			this.gbImport.Controls.Add(this.chkImportIgnoreLanguage);
			this.gbImport.Controls.Add(this.btnImport);
			this.gbImport.Location = new System.Drawing.Point(402, 309);
			this.gbImport.Name = "gbImport";
			this.gbImport.Size = new System.Drawing.Size(382, 81);
			this.gbImport.TabIndex = 15;
			this.gbImport.TabStop = false;
			this.gbImport.Text = "Import quotes";
			// 
			// chkImportMerge
			// 
			this.chkImportMerge.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.chkImportMerge.Location = new System.Drawing.Point(6, 20);
			this.chkImportMerge.Name = "chkImportMerge";
			this.chkImportMerge.Size = new System.Drawing.Size(362, 19);
			this.chkImportMerge.TabIndex = 15;
			this.chkImportMerge.Text = "Merge collections with the same name into existing ones";
			this.chkImportMerge.UseVisualStyleBackColor = true;
			// 
			// chkImportIgnoreLanguage
			// 
			this.chkImportIgnoreLanguage.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.chkImportIgnoreLanguage.Location = new System.Drawing.Point(6, 40);
			this.chkImportIgnoreLanguage.Name = "chkImportIgnoreLanguage";
			this.chkImportIgnoreLanguage.Size = new System.Drawing.Size(228, 38);
			this.chkImportIgnoreLanguage.TabIndex = 14;
			this.chkImportIgnoreLanguage.Text = "Ignore collection language on import";
			this.chkImportIgnoreLanguage.UseVisualStyleBackColor = true;
			// 
			// btnImport
			// 
			this.btnImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnImport.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnImport.Location = new System.Drawing.Point(261, 43);
			this.btnImport.Name = "btnImport";
			this.btnImport.Size = new System.Drawing.Size(107, 32);
			this.btnImport.TabIndex = 8;
			this.btnImport.Text = "Import";
			this.btnImport.UseVisualStyleBackColor = true;
			this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
			// 
			// gbExport
			// 
			this.gbExport.Controls.Add(this.chkExportFavCollections);
			this.gbExport.Controls.Add(this.btnExportCollection);
			this.gbExport.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.gbExport.Location = new System.Drawing.Point(6, 309);
			this.gbExport.Name = "gbExport";
			this.gbExport.Size = new System.Drawing.Size(390, 81);
			this.gbExport.TabIndex = 14;
			this.gbExport.TabStop = false;
			this.gbExport.Text = "Export quotes";
			// 
			// chkExportFavCollections
			// 
			this.chkExportFavCollections.AutoSize = true;
			this.chkExportFavCollections.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.chkExportFavCollections.Location = new System.Drawing.Point(6, 20);
			this.chkExportFavCollections.Name = "chkExportFavCollections";
			this.chkExportFavCollections.Size = new System.Drawing.Size(181, 19);
			this.chkExportFavCollections.TabIndex = 13;
			this.chkExportFavCollections.Text = "Export only favourite quotes";
			this.chkExportFavCollections.UseVisualStyleBackColor = true;
			// 
			// btnExportCollection
			// 
			this.btnExportCollection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnExportCollection.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnExportCollection.Location = new System.Drawing.Point(273, 43);
			this.btnExportCollection.Name = "btnExportCollection";
			this.btnExportCollection.Size = new System.Drawing.Size(111, 32);
			this.btnExportCollection.TabIndex = 7;
			this.btnExportCollection.Text = "Export";
			this.btnExportCollection.UseVisualStyleBackColor = true;
			this.btnExportCollection.Click += new System.EventHandler(this.btnExportCollection_Click);
			// 
			// lblFavoritesText
			// 
			this.lblFavoritesText.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblFavoritesText.Location = new System.Drawing.Point(127, 6);
			this.lblFavoritesText.Name = "lblFavoritesText";
			this.lblFavoritesText.Size = new System.Drawing.Size(525, 40);
			this.lblFavoritesText.TabIndex = 12;
			this.lblFavoritesText.Text = "Here you can set your favourite collections, books or chapters. Quotes that are d" +
    "isplayed will be selected from this list.";
			this.lblFavoritesText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblFavouriteChapters
			// 
			this.lblFavouriteChapters.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblFavouriteChapters.Location = new System.Drawing.Point(537, 48);
			this.lblFavouriteChapters.Name = "lblFavouriteChapters";
			this.lblFavouriteChapters.Size = new System.Drawing.Size(239, 27);
			this.lblFavouriteChapters.TabIndex = 11;
			this.lblFavouriteChapters.Text = "label2";
			this.lblFavouriteChapters.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblFavouriteBooks
			// 
			this.lblFavouriteBooks.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblFavouriteBooks.Location = new System.Drawing.Point(267, 48);
			this.lblFavouriteBooks.Name = "lblFavouriteBooks";
			this.lblFavouriteBooks.Size = new System.Drawing.Size(239, 27);
			this.lblFavouriteBooks.TabIndex = 10;
			this.lblFavouriteBooks.Text = "label2";
			this.lblFavouriteBooks.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblFavouriteCollections
			// 
			this.lblFavouriteCollections.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblFavouriteCollections.Location = new System.Drawing.Point(6, 48);
			this.lblFavouriteCollections.Name = "lblFavouriteCollections";
			this.lblFavouriteCollections.Size = new System.Drawing.Size(239, 27);
			this.lblFavouriteCollections.TabIndex = 9;
			this.lblFavouriteCollections.Text = "label2";
			this.lblFavouriteCollections.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// clbChapters
			// 
			this.clbChapters.BackColor = System.Drawing.SystemColors.Window;
			this.clbChapters.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.clbChapters.ColumnWidth = 50;
			this.clbChapters.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.clbChapters.ForeColor = System.Drawing.SystemColors.WindowText;
			this.clbChapters.FormattingEnabled = true;
			this.clbChapters.HorizontalScrollbar = true;
			this.clbChapters.Items.AddRange(new object[] {
            "This is one of the collections (Description)",
            "col2",
            "col3",
            "col4",
            "col5",
            "col6"});
			this.clbChapters.Location = new System.Drawing.Point(528, 78);
			this.clbChapters.Name = "clbChapters";
			this.clbChapters.Size = new System.Drawing.Size(256, 222);
			this.clbChapters.TabIndex = 2;
			this.clbChapters.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbChapters_ItemCheck);
			this.clbChapters.SelectedValueChanged += new System.EventHandler(this.clbChapters_SelectedValueChanged);
			// 
			// clbBooks
			// 
			this.clbBooks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.clbBooks.ColumnWidth = 50;
			this.clbBooks.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.clbBooks.FormattingEnabled = true;
			this.clbBooks.HorizontalScrollbar = true;
			this.clbBooks.Items.AddRange(new object[] {
            "This is one of the collections (Description)",
            "col2",
            "col3",
            "col4",
            "col5",
            "col6"});
			this.clbBooks.Location = new System.Drawing.Point(260, 78);
			this.clbBooks.Name = "clbBooks";
			this.clbBooks.Size = new System.Drawing.Size(253, 222);
			this.clbBooks.TabIndex = 1;
			this.clbBooks.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbBooks_ItemCheck);
			this.clbBooks.SelectedValueChanged += new System.EventHandler(this.clbBooks_SelectedValueChanged);
			// 
			// clbCollections
			// 
			this.clbCollections.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.clbCollections.ColumnWidth = 50;
			this.clbCollections.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.clbCollections.FormattingEnabled = true;
			this.clbCollections.HorizontalScrollbar = true;
			this.clbCollections.Items.AddRange(new object[] {
            "This is one of the collections (Description)",
            "col2",
            "col3",
            "col4",
            "col5",
            "col6"});
			this.clbCollections.Location = new System.Drawing.Point(6, 78);
			this.clbCollections.Name = "clbCollections";
			this.clbCollections.Size = new System.Drawing.Size(239, 222);
			this.clbCollections.TabIndex = 0;
			this.clbCollections.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbCollections_ItemCheck);
			this.clbCollections.SelectedValueChanged += new System.EventHandler(this.clbCollections_SelectedValueChanged);
			// 
			// tabPage3
			// 
			this.tabPage3.BackColor = System.Drawing.Color.WhiteSmoke;
			this.tabPage3.Controls.Add(this.tabBasicSettings);
			this.tabPage3.Location = new System.Drawing.Point(4, 25);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage3.Size = new System.Drawing.Size(790, 400);
			this.tabPage3.TabIndex = 4;
			this.tabPage3.Text = "Settings";
			// 
			// tabBasicSettings
			// 
			this.tabBasicSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabBasicSettings.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
			this.tabBasicSettings.Controls.Add(this.tabPage4);
			this.tabBasicSettings.Location = new System.Drawing.Point(6, 6);
			this.tabBasicSettings.Name = "tabBasicSettings";
			this.tabBasicSettings.SelectedIndex = 0;
			this.tabBasicSettings.Size = new System.Drawing.Size(778, 388);
			this.tabBasicSettings.TabIndex = 15;
			// 
			// tabPage4
			// 
			this.tabPage4.BackColor = System.Drawing.Color.WhiteSmoke;
			this.tabPage4.Controls.Add(this.gbQuotesSettings);
			this.tabPage4.Controls.Add(this.gbOtherSettings);
			this.tabPage4.Controls.Add(this.gbThemeSettings);
			this.tabPage4.Controls.Add(this.gbLanguageSettings);
			this.tabPage4.Location = new System.Drawing.Point(4, 25);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage4.Size = new System.Drawing.Size(770, 359);
			this.tabPage4.TabIndex = 0;
			this.tabPage4.Text = "Basic";
			// 
			// gbQuotesSettings
			// 
			this.gbQuotesSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.gbQuotesSettings.Controls.Add(this.lblNotificationFont);
			this.gbQuotesSettings.Controls.Add(this.txtSelectedFont);
			this.gbQuotesSettings.Controls.Add(this.btnNotificationFont);
			this.gbQuotesSettings.Controls.Add(this.lblQuotesAutocloseTime);
			this.gbQuotesSettings.Controls.Add(this.txtQuotesAutoCloseInterval);
			this.gbQuotesSettings.Controls.Add(this.lblQuotesAutocloseInterval);
			this.gbQuotesSettings.Controls.Add(this.lblNotificationLocation);
			this.gbQuotesSettings.Controls.Add(this.panel3);
			this.gbQuotesSettings.Controls.Add(this.lblQuotesFrequencyTime);
			this.gbQuotesSettings.Controls.Add(this.btnAlwaysOnNotifications);
			this.gbQuotesSettings.Controls.Add(this.txtQuotesInterval);
			this.gbQuotesSettings.Controls.Add(this.lblQuotesFrequency);
			this.gbQuotesSettings.Controls.Add(this.btnPopupNotifications);
			this.gbQuotesSettings.Controls.Add(this.lblNotificationType);
			this.gbQuotesSettings.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.gbQuotesSettings.Location = new System.Drawing.Point(374, 6);
			this.gbQuotesSettings.Name = "gbQuotesSettings";
			this.gbQuotesSettings.Size = new System.Drawing.Size(390, 347);
			this.gbQuotesSettings.TabIndex = 22;
			this.gbQuotesSettings.TabStop = false;
			this.gbQuotesSettings.Text = "Quotes";
			// 
			// lblNotificationFont
			// 
			this.lblNotificationFont.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblNotificationFont.Location = new System.Drawing.Point(39, 183);
			this.lblNotificationFont.Name = "lblNotificationFont";
			this.lblNotificationFont.Size = new System.Drawing.Size(167, 18);
			this.lblNotificationFont.TabIndex = 28;
			this.lblNotificationFont.Text = "Notification font";
			this.lblNotificationFont.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// txtSelectedFont
			// 
			this.txtSelectedFont.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.txtSelectedFont.Location = new System.Drawing.Point(19, 214);
			this.txtSelectedFont.Multiline = true;
			this.txtSelectedFont.Name = "txtSelectedFont";
			this.txtSelectedFont.ReadOnly = true;
			this.txtSelectedFont.Size = new System.Drawing.Size(215, 78);
			this.txtSelectedFont.TabIndex = 27;
			this.txtSelectedFont.Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor in" +
    "cididunt ut labore et dolore magna aliqua";
			// 
			// btnNotificationFont
			// 
			this.btnNotificationFont.BackColor = System.Drawing.Color.WhiteSmoke;
			this.btnNotificationFont.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnNotificationFont.Location = new System.Drawing.Point(19, 298);
			this.btnNotificationFont.Name = "btnNotificationFont";
			this.btnNotificationFont.Size = new System.Drawing.Size(75, 28);
			this.btnNotificationFont.TabIndex = 26;
			this.btnNotificationFont.Text = "Change";
			this.btnNotificationFont.UseVisualStyleBackColor = false;
			this.btnNotificationFont.Click += new System.EventHandler(this.btnNotificationFont_Click);
			// 
			// lblQuotesAutocloseTime
			// 
			this.lblQuotesAutocloseTime.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblQuotesAutocloseTime.Location = new System.Drawing.Point(293, 143);
			this.lblQuotesAutocloseTime.Name = "lblQuotesAutocloseTime";
			this.lblQuotesAutocloseTime.Size = new System.Drawing.Size(69, 18);
			this.lblQuotesAutocloseTime.TabIndex = 25;
			this.lblQuotesAutocloseTime.Text = "seconds";
			// 
			// txtQuotesAutoCloseInterval
			// 
			this.txtQuotesAutoCloseInterval.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.txtQuotesAutoCloseInterval.Location = new System.Drawing.Point(245, 140);
			this.txtQuotesAutoCloseInterval.Name = "txtQuotesAutoCloseInterval";
			this.txtQuotesAutoCloseInterval.Size = new System.Drawing.Size(42, 26);
			this.txtQuotesAutoCloseInterval.TabIndex = 24;
			this.txtQuotesAutoCloseInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtQuotesAutoCloseInterval.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQuotesAutoCloseInterval_KeyDown);
			this.txtQuotesAutoCloseInterval.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQuotesAutoCloseInterval_KeyPress);
			// 
			// lblQuotesAutocloseInterval
			// 
			this.lblQuotesAutocloseInterval.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblQuotesAutocloseInterval.Location = new System.Drawing.Point(6, 135);
			this.lblQuotesAutocloseInterval.Name = "lblQuotesAutocloseInterval";
			this.lblQuotesAutocloseInterval.Size = new System.Drawing.Size(228, 39);
			this.lblQuotesAutocloseInterval.TabIndex = 23;
			this.lblQuotesAutocloseInterval.Text = "Auto-close popup notifications after some time";
			this.lblQuotesAutocloseInterval.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblNotificationLocation
			// 
			this.lblNotificationLocation.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblNotificationLocation.Location = new System.Drawing.Point(240, 174);
			this.lblNotificationLocation.Name = "lblNotificationLocation";
			this.lblNotificationLocation.Size = new System.Drawing.Size(144, 37);
			this.lblNotificationLocation.TabIndex = 22;
			this.lblNotificationLocation.Text = "Notification location on screen";
			this.lblNotificationLocation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// panel3
			// 
			this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel3.Controls.Add(this.rbAnimTopRight);
			this.panel3.Controls.Add(this.rbAnimBottomRight);
			this.panel3.Controls.Add(this.rbAnimBottomLeft);
			this.panel3.Controls.Add(this.rbAnimTopLeft);
			this.panel3.Location = new System.Drawing.Point(249, 214);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(131, 78);
			this.panel3.TabIndex = 21;
			// 
			// rbAnimTopRight
			// 
			this.rbAnimTopRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.rbAnimTopRight.AutoSize = true;
			this.rbAnimTopRight.Location = new System.Drawing.Point(111, 4);
			this.rbAnimTopRight.Name = "rbAnimTopRight";
			this.rbAnimTopRight.Size = new System.Drawing.Size(14, 13);
			this.rbAnimTopRight.TabIndex = 3;
			this.rbAnimTopRight.UseVisualStyleBackColor = true;
			this.rbAnimTopRight.CheckedChanged += new System.EventHandler(this.rbAnimTopRight_CheckedChanged);
			// 
			// rbAnimBottomRight
			// 
			this.rbAnimBottomRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.rbAnimBottomRight.AutoSize = true;
			this.rbAnimBottomRight.Checked = true;
			this.rbAnimBottomRight.Location = new System.Drawing.Point(110, 59);
			this.rbAnimBottomRight.Name = "rbAnimBottomRight";
			this.rbAnimBottomRight.Size = new System.Drawing.Size(14, 13);
			this.rbAnimBottomRight.TabIndex = 2;
			this.rbAnimBottomRight.TabStop = true;
			this.rbAnimBottomRight.UseVisualStyleBackColor = true;
			this.rbAnimBottomRight.CheckedChanged += new System.EventHandler(this.rbAnimBottomRight_CheckedChanged);
			// 
			// rbAnimBottomLeft
			// 
			this.rbAnimBottomLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.rbAnimBottomLeft.AutoSize = true;
			this.rbAnimBottomLeft.Location = new System.Drawing.Point(4, 59);
			this.rbAnimBottomLeft.Name = "rbAnimBottomLeft";
			this.rbAnimBottomLeft.Size = new System.Drawing.Size(14, 13);
			this.rbAnimBottomLeft.TabIndex = 1;
			this.rbAnimBottomLeft.UseVisualStyleBackColor = true;
			this.rbAnimBottomLeft.CheckedChanged += new System.EventHandler(this.rbAnimBottomLeft_CheckedChanged);
			// 
			// rbAnimTopLeft
			// 
			this.rbAnimTopLeft.AutoSize = true;
			this.rbAnimTopLeft.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.rbAnimTopLeft.Location = new System.Drawing.Point(4, 4);
			this.rbAnimTopLeft.Name = "rbAnimTopLeft";
			this.rbAnimTopLeft.Size = new System.Drawing.Size(14, 13);
			this.rbAnimTopLeft.TabIndex = 0;
			this.rbAnimTopLeft.UseVisualStyleBackColor = true;
			this.rbAnimTopLeft.CheckedChanged += new System.EventHandler(this.rbAnimTopLeft_CheckedChanged);
			// 
			// lblQuotesFrequencyTime
			// 
			this.lblQuotesFrequencyTime.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblQuotesFrequencyTime.Location = new System.Drawing.Point(293, 111);
			this.lblQuotesFrequencyTime.Name = "lblQuotesFrequencyTime";
			this.lblQuotesFrequencyTime.Size = new System.Drawing.Size(69, 18);
			this.lblQuotesFrequencyTime.TabIndex = 19;
			this.lblQuotesFrequencyTime.Text = "minutes";
			// 
			// btnAlwaysOnNotifications
			// 
			this.btnAlwaysOnNotifications.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
			this.btnAlwaysOnNotifications.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnAlwaysOnNotifications.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnAlwaysOnNotifications.Location = new System.Drawing.Point(201, 45);
			this.btnAlwaysOnNotifications.Name = "btnAlwaysOnNotifications";
			this.btnAlwaysOnNotifications.Size = new System.Drawing.Size(183, 54);
			this.btnAlwaysOnNotifications.TabIndex = 20;
			this.btnAlwaysOnNotifications.Text = "Show quotes in a window that is always on screen";
			this.btnAlwaysOnNotifications.UseVisualStyleBackColor = true;
			this.btnAlwaysOnNotifications.Click += new System.EventHandler(this.btnAlwaysOnNotifications_Click);
			// 
			// txtQuotesInterval
			// 
			this.txtQuotesInterval.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.txtQuotesInterval.Location = new System.Drawing.Point(245, 108);
			this.txtQuotesInterval.Name = "txtQuotesInterval";
			this.txtQuotesInterval.Size = new System.Drawing.Size(42, 26);
			this.txtQuotesInterval.TabIndex = 6;
			this.txtQuotesInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtQuotesInterval.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQuotesInterval_KeyDown);
			this.txtQuotesInterval.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQuotesInterval_KeyPress);
			// 
			// lblQuotesFrequency
			// 
			this.lblQuotesFrequency.AutoSize = true;
			this.lblQuotesFrequency.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblQuotesFrequency.Location = new System.Drawing.Point(6, 111);
			this.lblQuotesFrequency.Name = "lblQuotesFrequency";
			this.lblQuotesFrequency.Size = new System.Drawing.Size(126, 18);
			this.lblQuotesFrequency.TabIndex = 18;
			this.lblQuotesFrequency.Text = "Show quotes every";
			// 
			// btnPopupNotifications
			// 
			this.btnPopupNotifications.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnPopupNotifications.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnPopupNotifications.Location = new System.Drawing.Point(6, 45);
			this.btnPopupNotifications.Name = "btnPopupNotifications";
			this.btnPopupNotifications.Size = new System.Drawing.Size(183, 54);
			this.btnPopupNotifications.TabIndex = 19;
			this.btnPopupNotifications.Text = "Show quotes as a popup notification";
			this.btnPopupNotifications.UseVisualStyleBackColor = true;
			this.btnPopupNotifications.Click += new System.EventHandler(this.btnPopupNotifications_Click);
			// 
			// lblNotificationType
			// 
			this.lblNotificationType.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblNotificationType.Location = new System.Drawing.Point(80, 19);
			this.lblNotificationType.Name = "lblNotificationType";
			this.lblNotificationType.Size = new System.Drawing.Size(237, 18);
			this.lblNotificationType.TabIndex = 19;
			this.lblNotificationType.Text = "Notification type";
			this.lblNotificationType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// gbOtherSettings
			// 
			this.gbOtherSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.gbOtherSettings.Controls.Add(this.btnStartWithWindowsNo);
			this.gbOtherSettings.Controls.Add(this.btnStartWithWindowsYes);
			this.gbOtherSettings.Controls.Add(this.lblStartWithWindows);
			this.gbOtherSettings.Controls.Add(this.btnShowWelcomeMsgNo);
			this.gbOtherSettings.Controls.Add(this.btnShowWelcomeMsgYes);
			this.gbOtherSettings.Controls.Add(this.lblShowWelcomeMsg);
			this.gbOtherSettings.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.gbOtherSettings.Location = new System.Drawing.Point(6, 240);
			this.gbOtherSettings.Name = "gbOtherSettings";
			this.gbOtherSettings.Size = new System.Drawing.Size(362, 113);
			this.gbOtherSettings.TabIndex = 21;
			this.gbOtherSettings.TabStop = false;
			this.gbOtherSettings.Text = "Other";
			// 
			// btnShowWelcomeMsgNo
			// 
			this.btnShowWelcomeMsgNo.FlatAppearance.BorderSize = 0;
			this.btnShowWelcomeMsgNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnShowWelcomeMsgNo.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnShowWelcomeMsgNo.Location = new System.Drawing.Point(316, 16);
			this.btnShowWelcomeMsgNo.Name = "btnShowWelcomeMsgNo";
			this.btnShowWelcomeMsgNo.Size = new System.Drawing.Size(40, 30);
			this.btnShowWelcomeMsgNo.TabIndex = 18;
			this.btnShowWelcomeMsgNo.Text = "NO";
			this.btnShowWelcomeMsgNo.UseVisualStyleBackColor = true;
			this.btnShowWelcomeMsgNo.Click += new System.EventHandler(this.btnShowWelcomeMsgNo_Click);
			// 
			// btnShowWelcomeMsgYes
			// 
			this.btnShowWelcomeMsgYes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnShowWelcomeMsgYes.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnShowWelcomeMsgYes.Location = new System.Drawing.Point(270, 16);
			this.btnShowWelcomeMsgYes.Name = "btnShowWelcomeMsgYes";
			this.btnShowWelcomeMsgYes.Size = new System.Drawing.Size(40, 30);
			this.btnShowWelcomeMsgYes.TabIndex = 18;
			this.btnShowWelcomeMsgYes.Text = "YES";
			this.btnShowWelcomeMsgYes.UseVisualStyleBackColor = true;
			this.btnShowWelcomeMsgYes.Click += new System.EventHandler(this.btnShowWelcomeMsgYes_Click);
			// 
			// lblShowWelcomeMsg
			// 
			this.lblShowWelcomeMsg.AutoSize = true;
			this.lblShowWelcomeMsg.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblShowWelcomeMsg.Location = new System.Drawing.Point(6, 22);
			this.lblShowWelcomeMsg.Name = "lblShowWelcomeMsg";
			this.lblShowWelcomeMsg.Size = new System.Drawing.Size(159, 18);
			this.lblShowWelcomeMsg.TabIndex = 18;
			this.lblShowWelcomeMsg.Text = "Show welcome message";
			// 
			// gbThemeSettings
			// 
			this.gbThemeSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.gbThemeSettings.Controls.Add(this.lblOpacityPercent);
			this.gbThemeSettings.Controls.Add(this.lblOpacity);
			this.gbThemeSettings.Controls.Add(this.tbOpacity);
			this.gbThemeSettings.Controls.Add(this.btnTheme1);
			this.gbThemeSettings.Controls.Add(this.btnTheme2);
			this.gbThemeSettings.Controls.Add(this.btnTheme3);
			this.gbThemeSettings.Controls.Add(this.btnTheme6);
			this.gbThemeSettings.Controls.Add(this.btnTheme5);
			this.gbThemeSettings.Controls.Add(this.btnTheme4);
			this.gbThemeSettings.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.gbThemeSettings.Location = new System.Drawing.Point(6, 106);
			this.gbThemeSettings.Name = "gbThemeSettings";
			this.gbThemeSettings.Size = new System.Drawing.Size(362, 135);
			this.gbThemeSettings.TabIndex = 20;
			this.gbThemeSettings.TabStop = false;
			this.gbThemeSettings.Text = "Theme";
			// 
			// lblOpacityPercent
			// 
			this.lblOpacityPercent.AutoSize = true;
			this.lblOpacityPercent.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblOpacityPercent.Location = new System.Drawing.Point(11, 93);
			this.lblOpacityPercent.Name = "lblOpacityPercent";
			this.lblOpacityPercent.Size = new System.Drawing.Size(33, 18);
			this.lblOpacityPercent.TabIndex = 21;
			this.lblOpacityPercent.Text = "90%";
			// 
			// lblOpacity
			// 
			this.lblOpacity.AutoSize = true;
			this.lblOpacity.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblOpacity.Location = new System.Drawing.Point(6, 75);
			this.lblOpacity.Name = "lblOpacity";
			this.lblOpacity.Size = new System.Drawing.Size(55, 18);
			this.lblOpacity.TabIndex = 20;
			this.lblOpacity.Text = "Opacity";
			// 
			// tbOpacity
			// 
			this.tbOpacity.LargeChange = 1;
			this.tbOpacity.Location = new System.Drawing.Point(67, 75);
			this.tbOpacity.Minimum = 1;
			this.tbOpacity.Name = "tbOpacity";
			this.tbOpacity.Size = new System.Drawing.Size(289, 45);
			this.tbOpacity.TabIndex = 15;
			this.tbOpacity.Value = 2;
			this.tbOpacity.Scroll += new System.EventHandler(this.tbOpacity_Scroll);
			// 
			// btnTheme1
			// 
			this.btnTheme1.BackColor = System.Drawing.Color.SlateGray;
			this.btnTheme1.FlatAppearance.BorderSize = 0;
			this.btnTheme1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnTheme1.Location = new System.Drawing.Point(6, 22);
			this.btnTheme1.Name = "btnTheme1";
			this.btnTheme1.Size = new System.Drawing.Size(46, 44);
			this.btnTheme1.TabIndex = 9;
			this.btnTheme1.UseVisualStyleBackColor = false;
			this.btnTheme1.Click += new System.EventHandler(this.btnTheme1_Click);
			// 
			// btnTheme2
			// 
			this.btnTheme2.BackColor = System.Drawing.Color.SteelBlue;
			this.btnTheme2.FlatAppearance.BorderSize = 0;
			this.btnTheme2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnTheme2.Location = new System.Drawing.Point(65, 22);
			this.btnTheme2.Name = "btnTheme2";
			this.btnTheme2.Size = new System.Drawing.Size(46, 44);
			this.btnTheme2.TabIndex = 10;
			this.btnTheme2.UseVisualStyleBackColor = false;
			this.btnTheme2.Click += new System.EventHandler(this.btnTheme2_Click);
			// 
			// btnTheme3
			// 
			this.btnTheme3.BackColor = System.Drawing.Color.Green;
			this.btnTheme3.FlatAppearance.BorderSize = 0;
			this.btnTheme3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnTheme3.Location = new System.Drawing.Point(124, 22);
			this.btnTheme3.Name = "btnTheme3";
			this.btnTheme3.Size = new System.Drawing.Size(46, 44);
			this.btnTheme3.TabIndex = 11;
			this.btnTheme3.UseVisualStyleBackColor = false;
			this.btnTheme3.Click += new System.EventHandler(this.btnTheme3_Click);
			// 
			// btnTheme6
			// 
			this.btnTheme6.BackColor = System.Drawing.Color.IndianRed;
			this.btnTheme6.FlatAppearance.BorderSize = 0;
			this.btnTheme6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnTheme6.Location = new System.Drawing.Point(299, 22);
			this.btnTheme6.Name = "btnTheme6";
			this.btnTheme6.Size = new System.Drawing.Size(46, 44);
			this.btnTheme6.TabIndex = 13;
			this.btnTheme6.UseVisualStyleBackColor = false;
			this.btnTheme6.Click += new System.EventHandler(this.btnTheme6_Click);
			// 
			// btnTheme5
			// 
			this.btnTheme5.BackColor = System.Drawing.Color.LightCoral;
			this.btnTheme5.FlatAppearance.BorderSize = 0;
			this.btnTheme5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnTheme5.Location = new System.Drawing.Point(240, 22);
			this.btnTheme5.Name = "btnTheme5";
			this.btnTheme5.Size = new System.Drawing.Size(46, 44);
			this.btnTheme5.TabIndex = 14;
			this.btnTheme5.UseVisualStyleBackColor = false;
			this.btnTheme5.Click += new System.EventHandler(this.btnTheme5_Click);
			// 
			// btnTheme4
			// 
			this.btnTheme4.BackColor = System.Drawing.Color.DarkOrange;
			this.btnTheme4.FlatAppearance.BorderSize = 0;
			this.btnTheme4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnTheme4.Location = new System.Drawing.Point(184, 22);
			this.btnTheme4.Name = "btnTheme4";
			this.btnTheme4.Size = new System.Drawing.Size(46, 44);
			this.btnTheme4.TabIndex = 12;
			this.btnTheme4.UseVisualStyleBackColor = false;
			this.btnTheme4.Click += new System.EventHandler(this.btnTheme4_Click);
			// 
			// gbLanguageSettings
			// 
			this.gbLanguageSettings.Controls.Add(this.btnLanguageFr);
			this.gbLanguageSettings.Controls.Add(this.btnShowCollBasedOnLanguageNo);
			this.gbLanguageSettings.Controls.Add(this.btnLanguageRo);
			this.gbLanguageSettings.Controls.Add(this.btnShowCollBasedOnLanguageYes);
			this.gbLanguageSettings.Controls.Add(this.btnLanguageEn);
			this.gbLanguageSettings.Controls.Add(this.lblShowCollByLanguage);
			this.gbLanguageSettings.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.gbLanguageSettings.Location = new System.Drawing.Point(6, 6);
			this.gbLanguageSettings.Name = "gbLanguageSettings";
			this.gbLanguageSettings.Size = new System.Drawing.Size(362, 100);
			this.gbLanguageSettings.TabIndex = 15;
			this.gbLanguageSettings.TabStop = false;
			this.gbLanguageSettings.Text = "Language";
			// 
			// btnLanguageFr
			// 
			this.btnLanguageFr.BackgroundImage = global::Quoter.App.Resources.Resources.flag_france_64;
			this.btnLanguageFr.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.btnLanguageFr.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
			this.btnLanguageFr.FlatAppearance.BorderSize = 2;
			this.btnLanguageFr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnLanguageFr.Location = new System.Drawing.Point(118, 22);
			this.btnLanguageFr.Name = "btnLanguageFr";
			this.btnLanguageFr.Size = new System.Drawing.Size(50, 35);
			this.btnLanguageFr.TabIndex = 18;
			this.btnLanguageFr.UseVisualStyleBackColor = true;
			this.btnLanguageFr.Click += new System.EventHandler(this.btnLanguageFr_Click);
			// 
			// btnShowCollBasedOnLanguageNo
			// 
			this.btnShowCollBasedOnLanguageNo.BackColor = System.Drawing.Color.WhiteSmoke;
			this.btnShowCollBasedOnLanguageNo.FlatAppearance.BorderSize = 0;
			this.btnShowCollBasedOnLanguageNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnShowCollBasedOnLanguageNo.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnShowCollBasedOnLanguageNo.Location = new System.Drawing.Point(316, 64);
			this.btnShowCollBasedOnLanguageNo.Name = "btnShowCollBasedOnLanguageNo";
			this.btnShowCollBasedOnLanguageNo.Size = new System.Drawing.Size(40, 30);
			this.btnShowCollBasedOnLanguageNo.TabIndex = 17;
			this.btnShowCollBasedOnLanguageNo.Text = "NO";
			this.btnShowCollBasedOnLanguageNo.UseVisualStyleBackColor = false;
			this.btnShowCollBasedOnLanguageNo.Click += new System.EventHandler(this.btnShowCollBasedOnLanguageNo_Click);
			// 
			// btnLanguageRo
			// 
			this.btnLanguageRo.BackgroundImage = global::Quoter.App.Resources.Resources.flag_romania_64;
			this.btnLanguageRo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.btnLanguageRo.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
			this.btnLanguageRo.FlatAppearance.BorderSize = 2;
			this.btnLanguageRo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnLanguageRo.Location = new System.Drawing.Point(62, 22);
			this.btnLanguageRo.Name = "btnLanguageRo";
			this.btnLanguageRo.Size = new System.Drawing.Size(50, 35);
			this.btnLanguageRo.TabIndex = 1;
			this.btnLanguageRo.UseVisualStyleBackColor = true;
			this.btnLanguageRo.Click += new System.EventHandler(this.btnLanguageRo_Click);
			// 
			// btnShowCollBasedOnLanguageYes
			// 
			this.btnShowCollBasedOnLanguageYes.BackColor = System.Drawing.Color.WhiteSmoke;
			this.btnShowCollBasedOnLanguageYes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnShowCollBasedOnLanguageYes.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnShowCollBasedOnLanguageYes.Location = new System.Drawing.Point(270, 63);
			this.btnShowCollBasedOnLanguageYes.Name = "btnShowCollBasedOnLanguageYes";
			this.btnShowCollBasedOnLanguageYes.Size = new System.Drawing.Size(40, 30);
			this.btnShowCollBasedOnLanguageYes.TabIndex = 16;
			this.btnShowCollBasedOnLanguageYes.Text = "YES";
			this.btnShowCollBasedOnLanguageYes.UseVisualStyleBackColor = false;
			this.btnShowCollBasedOnLanguageYes.Click += new System.EventHandler(this.btnShowCollBasedOnLanguageYes_Click);
			// 
			// btnLanguageEn
			// 
			this.btnLanguageEn.BackgroundImage = global::Quoter.App.Resources.Resources.flag_uk_64;
			this.btnLanguageEn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.btnLanguageEn.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
			this.btnLanguageEn.FlatAppearance.BorderSize = 2;
			this.btnLanguageEn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnLanguageEn.Location = new System.Drawing.Point(6, 22);
			this.btnLanguageEn.Name = "btnLanguageEn";
			this.btnLanguageEn.Size = new System.Drawing.Size(50, 35);
			this.btnLanguageEn.TabIndex = 0;
			this.btnLanguageEn.UseVisualStyleBackColor = true;
			this.btnLanguageEn.Click += new System.EventHandler(this.btnLanguageEn_Click);
			// 
			// lblShowCollByLanguage
			// 
			this.lblShowCollByLanguage.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblShowCollByLanguage.Location = new System.Drawing.Point(8, 58);
			this.lblShowCollByLanguage.Name = "lblShowCollByLanguage";
			this.lblShowCollByLanguage.Size = new System.Drawing.Size(256, 40);
			this.lblShowCollByLanguage.TabIndex = 7;
			this.lblShowCollByLanguage.Text = "Show collections based on language";
			this.lblShowCollByLanguage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtStatus
			// 
			this.txtStatus.BackColor = System.Drawing.Color.WhiteSmoke;
			this.txtStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtStatus.ForeColor = System.Drawing.Color.Green;
			this.txtStatus.Location = new System.Drawing.Point(0, 504);
			this.txtStatus.Multiline = true;
			this.txtStatus.Name = "txtStatus";
			this.txtStatus.Size = new System.Drawing.Size(580, 21);
			this.txtStatus.TabIndex = 15;
			this.txtStatus.Text = "Status acesta este un status ca s-a facut ceva corect bravo tie mai ce sa zicem";
			// 
			// btnTabPage1
			// 
			this.btnTabPage1.FlatAppearance.BorderSize = 0;
			this.btnTabPage1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnTabPage1.Font = new System.Drawing.Font("Calibri Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnTabPage1.Location = new System.Drawing.Point(0, 40);
			this.btnTabPage1.Name = "btnTabPage1";
			this.btnTabPage1.Size = new System.Drawing.Size(266, 30);
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
			this.btnTabPage2.Location = new System.Drawing.Point(266, 40);
			this.btnTabPage2.Name = "btnTabPage2";
			this.btnTabPage2.Size = new System.Drawing.Size(266, 30);
			this.btnTabPage2.TabIndex = 4;
			this.btnTabPage2.Text = "Quotes collection";
			this.btnTabPage2.UseVisualStyleBackColor = true;
			this.btnTabPage2.Click += new System.EventHandler(this.btnTabPage2_Click);
			// 
			// btnTabPage3
			// 
			this.btnTabPage3.BackColor = System.Drawing.Color.WhiteSmoke;
			this.btnTabPage3.FlatAppearance.BorderSize = 0;
			this.btnTabPage3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnTabPage3.Font = new System.Drawing.Font("Calibri Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnTabPage3.Location = new System.Drawing.Point(532, 40);
			this.btnTabPage3.Name = "btnTabPage3";
			this.btnTabPage3.Size = new System.Drawing.Size(266, 30);
			this.btnTabPage3.TabIndex = 5;
			this.btnTabPage3.Text = "Settings";
			this.btnTabPage3.UseVisualStyleBackColor = false;
			this.btnTabPage3.Click += new System.EventHandler(this.btnTabPage3_Click);
			// 
			// pnlSelectedTab
			// 
			this.pnlSelectedTab.BackColor = System.Drawing.Color.SlateGray;
			this.pnlSelectedTab.Location = new System.Drawing.Point(268, 67);
			this.pnlSelectedTab.Name = "pnlSelectedTab";
			this.pnlSelectedTab.Size = new System.Drawing.Size(100, 5);
			this.pnlSelectedTab.TabIndex = 16;
			// 
			// pbBackgroundTask
			// 
			this.pbBackgroundTask.BackColor = System.Drawing.Color.Transparent;
			this.pbBackgroundTask.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.pbBackgroundTask.Image = global::Quoter.App.Resources.Resources.loading_transparent_128;
			this.pbBackgroundTask.Location = new System.Drawing.Point(764, 504);
			this.pbBackgroundTask.Name = "pbBackgroundTask";
			this.pbBackgroundTask.Size = new System.Drawing.Size(29, 20);
			this.pbBackgroundTask.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pbBackgroundTask.TabIndex = 17;
			this.pbBackgroundTask.TabStop = false;
			// 
			// lblBackgroundTask
			// 
			this.lblBackgroundTask.Location = new System.Drawing.Point(579, 503);
			this.lblBackgroundTask.Name = "lblBackgroundTask";
			this.lblBackgroundTask.Size = new System.Drawing.Size(181, 20);
			this.lblBackgroundTask.TabIndex = 18;
			this.lblBackgroundTask.Text = "Work being done in background";
			this.lblBackgroundTask.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblStartWithWindows
			// 
			this.lblStartWithWindows.AutoSize = true;
			this.lblStartWithWindows.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblStartWithWindows.Location = new System.Drawing.Point(6, 58);
			this.lblStartWithWindows.Name = "lblStartWithWindows";
			this.lblStartWithWindows.Size = new System.Drawing.Size(127, 18);
			this.lblStartWithWindows.TabIndex = 19;
			this.lblStartWithWindows.Text = "Start with windows";
			// 
			// btnStartWithWindowsNo
			// 
			this.btnStartWithWindowsNo.FlatAppearance.BorderSize = 0;
			this.btnStartWithWindowsNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnStartWithWindowsNo.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnStartWithWindowsNo.Location = new System.Drawing.Point(316, 52);
			this.btnStartWithWindowsNo.Name = "btnStartWithWindowsNo";
			this.btnStartWithWindowsNo.Size = new System.Drawing.Size(40, 30);
			this.btnStartWithWindowsNo.TabIndex = 20;
			this.btnStartWithWindowsNo.Text = "NO";
			this.btnStartWithWindowsNo.UseVisualStyleBackColor = true;
			this.btnStartWithWindowsNo.Click += new System.EventHandler(this.btnStartWithWindowsNo_Click);
			// 
			// btnStartWithWindowsYes
			// 
			this.btnStartWithWindowsYes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnStartWithWindowsYes.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnStartWithWindowsYes.Location = new System.Drawing.Point(270, 52);
			this.btnStartWithWindowsYes.Name = "btnStartWithWindowsYes";
			this.btnStartWithWindowsYes.Size = new System.Drawing.Size(40, 30);
			this.btnStartWithWindowsYes.TabIndex = 21;
			this.btnStartWithWindowsYes.Text = "YES";
			this.btnStartWithWindowsYes.UseVisualStyleBackColor = true;
			this.btnStartWithWindowsYes.Click += new System.EventHandler(this.btnStartWithWindowsYes_Click);
			// 
			// ManageForm
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.WhiteSmoke;
			this.ClientSize = new System.Drawing.Size(800, 528);
			this.ControlBox = false;
			this.Controls.Add(this.lblBackgroundTask);
			this.Controls.Add(this.pbBackgroundTask);
			this.Controls.Add(this.pnlSelectedTab);
			this.Controls.Add(this.txtStatus);
			this.Controls.Add(this.btnTabPage3);
			this.Controls.Add(this.btnTabPage2);
			this.Controls.Add(this.btnTabPage1);
			this.Controls.Add(this.tabControl);
			this.Controls.Add(this.pnlTitle);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ManageForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "ManageQuotesForm";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ManageForm_FormClosing);
			this.Load += new System.EventHandler(this.ManageQuotesForm_Load);
			this.pnlTitle.ResumeLayout(false);
			this.pnlTitle.PerformLayout();
			this.tabControl.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.gbQuotes.ResumeLayout(false);
			this.pnlQuotesOptions.ResumeLayout(false);
			this.pnlQuotesOptions.PerformLayout();
			this.gbChapters.ResumeLayout(false);
			this.gbBooks.ResumeLayout(false);
			this.gbCollections.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.gbImport.ResumeLayout(false);
			this.gbExport.ResumeLayout(false);
			this.gbExport.PerformLayout();
			this.tabPage3.ResumeLayout(false);
			this.tabBasicSettings.ResumeLayout(false);
			this.tabPage4.ResumeLayout(false);
			this.gbQuotesSettings.ResumeLayout(false);
			this.gbQuotesSettings.PerformLayout();
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			this.gbOtherSettings.ResumeLayout(false);
			this.gbOtherSettings.PerformLayout();
			this.gbThemeSettings.ResumeLayout(false);
			this.gbThemeSettings.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.tbOpacity)).EndInit();
			this.gbLanguageSettings.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbBackgroundTask)).EndInit();
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
		private TabPage tabPage2;
		private TabPage tabPage3;
		private LineNumbers.LineNumbers_For_RichTextBox lineNumbers_For_RichTextBox1;
		private Label lblShowCollByLanguage;
		private TextBox txtQuotesInterval;
		private Button btnTheme5;
		private Button btnTheme6;
		private Button btnTheme4;
		private Button btnTheme3;
		private Button btnTheme2;
		private Button btnTheme1;
		private TabControl tabBasicSettings;
		private TabPage tabPage4;
		private GroupBox gbThemeSettings;
		private Label lblQuotesFrequencyTime;
		private Label lblQuotesFrequency;
		private GroupBox gbLanguageSettings;
		private Button btnShowCollBasedOnLanguageNo;
		private Button btnLanguageRo;
		private Button btnShowCollBasedOnLanguageYes;
		private Button btnLanguageEn;
		private Label lblOpacityPercent;
		private Label lblOpacity;
		private TrackBar tbOpacity;
		private GroupBox gbOtherSettings;
		private Button btnShowWelcomeMsgNo;
		private Button btnShowWelcomeMsgYes;
		private Label lblShowWelcomeMsg;
		private Button btnAddFirstChapter;
		private Button btnAddFirstBook;
		private GroupBox gbQuotesSettings;
		private Button btnAlwaysOnNotifications;
		private Button btnPopupNotifications;
		private Label lblNotificationType;
		private CheckedListBox clbCollections;
		private CheckedListBox clbChapters;
		private CheckedListBox clbBooks;
		private Label lblNotificationLocation;
		private Panel panel3;
		private RadioButton rbAnimTopRight;
		private RadioButton rbAnimBottomRight;
		private RadioButton rbAnimBottomLeft;
		private RadioButton rbAnimTopLeft;
		private Label lblQuotesAutocloseTime;
		private TextBox txtQuotesAutoCloseInterval;
		private Label lblQuotesAutocloseInterval;
		private Button btnImport;
		private Button btnExportCollection;
		private GroupBox gbImport;
		private CheckBox chkImportIgnoreLanguage;
		private GroupBox gbExport;
		private CheckBox chkExportFavCollections;
		private Label lblFavoritesText;
		private Label lblFavouriteChapters;
		private Label lblFavouriteBooks;
		private Label lblFavouriteCollections;
		private Button btnQuotesOptions;
		private Panel pnlQuotesOptions;
		private Label lblQuotesAppendEnd;
		private TextBox txtQuotesAppendTextToEnd;
		private Label lblQuotesAppendStart;
		private TextBox txtQuotesAppendedTextToBeginning;
		private TextBox txtQuotesExcludedChars;
		private Label lblQuotesExcludeChars;
		private CheckBox chkQuotesTrimRow;
		private CheckBox chkImportMerge;
		private Panel pnlSelectedTab;
		private Button btnNotificationFont;
		private Label lblNotificationFont;
		private TextBox textBox1;
		private TextBox txtSelectedFont;
		private Button btnLanguageFr;
		private CheckBox chkWordWrap;
		private PictureBox pbBackgroundTask;
		private Label lblBackgroundTask;
		private Button btnRefreshFavouriteCollections;
		private Button btnReloadEditCollections;
		private Button btnStartWithWindowsNo;
		private Button btnStartWithWindowsYes;
		private Label lblStartWithWindows;
	}
}