namespace Quoter.App.Forms
{
	partial class ReaderForm
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
			TreeNode treeNode1 = new TreeNode("BookName");
			TreeNode treeNode2 = new TreeNode("Node3");
			TreeNode treeNode3 = new TreeNode("Node9");
			TreeNode treeNode4 = new TreeNode("Node10");
			TreeNode treeNode5 = new TreeNode("Node11");
			TreeNode treeNode6 = new TreeNode("Node12");
			TreeNode treeNode7 = new TreeNode("Node13");
			TreeNode treeNode8 = new TreeNode("Node14");
			TreeNode treeNode9 = new TreeNode("Node15");
			TreeNode treeNode10 = new TreeNode("Node16");
			TreeNode treeNode11 = new TreeNode("Node17");
			TreeNode treeNode12 = new TreeNode("Node18");
			TreeNode treeNode13 = new TreeNode("Node19");
			TreeNode treeNode14 = new TreeNode("Node20");
			TreeNode treeNode15 = new TreeNode("Node21");
			TreeNode treeNode16 = new TreeNode("Node22");
			TreeNode treeNode17 = new TreeNode("Node23");
			TreeNode treeNode18 = new TreeNode("Node24");
			TreeNode treeNode19 = new TreeNode("Node25");
			TreeNode treeNode20 = new TreeNode("Node26");
			TreeNode treeNode21 = new TreeNode("Node27");
			TreeNode treeNode22 = new TreeNode("Node28");
			TreeNode treeNode23 = new TreeNode("Node29");
			TreeNode treeNode24 = new TreeNode("Node30");
			TreeNode treeNode25 = new TreeNode("Node31");
			TreeNode treeNode26 = new TreeNode("Node32");
			TreeNode treeNode27 = new TreeNode("Node33");
			TreeNode treeNode28 = new TreeNode("BookName2", new TreeNode[] { treeNode2, treeNode3, treeNode4, treeNode5, treeNode6, treeNode7, treeNode8, treeNode9, treeNode10, treeNode11, treeNode12, treeNode13, treeNode14, treeNode15, treeNode16, treeNode17, treeNode18, treeNode19, treeNode20, treeNode21, treeNode22, treeNode23, treeNode24, treeNode25, treeNode26, treeNode27 });
			TreeNode treeNode29 = new TreeNode("Node4");
			TreeNode treeNode30 = new TreeNode("Node5");
			TreeNode treeNode31 = new TreeNode("Node6");
			TreeNode treeNode32 = new TreeNode("Node7");
			TreeNode treeNode33 = new TreeNode("Node8");
			TreeNode treeNode34 = new TreeNode("BookName2", new TreeNode[] { treeNode1, treeNode28, treeNode29, treeNode30, treeNode31, treeNode32, treeNode33 });
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReaderForm));
			pnlTitle = new Panel();
			btnMinimize = new Button();
			pictureBox2 = new PictureBox();
			btnClose = new Button();
			lblTitle = new Label();
			pictureBox1 = new PictureBox();
			tvCollection = new TreeView();
			tableLayoutPanel1 = new TableLayoutPanel();
			panel1 = new Panel();
			btnPrevious = new Button();
			lineNumbers_For_RichTextBox1 = new LineNumbers.LineNumbers_For_RichTextBox();
			rtbQuotes = new RichTextBox();
			btnNext = new Button();
			lblLocationInCollection = new Label();
			btnBackToManage = new Button();
			pnlTitle.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			tableLayoutPanel1.SuspendLayout();
			panel1.SuspendLayout();
			SuspendLayout();
			// 
			// pnlTitle
			// 
			pnlTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			pnlTitle.BackColor = Color.SteelBlue;
			pnlTitle.Controls.Add(btnMinimize);
			pnlTitle.Controls.Add(pictureBox2);
			pnlTitle.Controls.Add(btnClose);
			pnlTitle.Controls.Add(lblTitle);
			pnlTitle.Location = new Point(0, 0);
			pnlTitle.Name = "pnlTitle";
			pnlTitle.Size = new Size(800, 30);
			pnlTitle.TabIndex = 2;
			// 
			// btnMinimize
			// 
			btnMinimize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			btnMinimize.BackColor = Color.Transparent;
			btnMinimize.BackgroundImageLayout = ImageLayout.Zoom;
			btnMinimize.FlatAppearance.BorderColor = Color.Silver;
			btnMinimize.FlatAppearance.BorderSize = 0;
			btnMinimize.FlatAppearance.MouseDownBackColor = Color.FromArgb(210, 210, 210);
			btnMinimize.FlatAppearance.MouseOverBackColor = SystemColors.ButtonFace;
			btnMinimize.FlatStyle = FlatStyle.Flat;
			btnMinimize.Font = new Font("Lucida Console", 12F, FontStyle.Bold, GraphicsUnit.Point);
			btnMinimize.ForeColor = Color.White;
			btnMinimize.Location = new Point(716, -9);
			btnMinimize.Name = "btnMinimize";
			btnMinimize.Size = new Size(40, 40);
			btnMinimize.TabIndex = 7;
			btnMinimize.Text = "_";
			btnMinimize.UseVisualStyleBackColor = false;
			btnMinimize.Click += btnMinimize_Click;
			// 
			// pictureBox2
			// 
			pictureBox2.BackColor = Color.Transparent;
			pictureBox2.BackgroundImage = Resources.Resources.book_96;
			pictureBox2.BackgroundImageLayout = ImageLayout.Zoom;
			pictureBox2.Location = new Point(6, 5);
			pictureBox2.Name = "pictureBox2";
			pictureBox2.Size = new Size(25, 20);
			pictureBox2.TabIndex = 6;
			pictureBox2.TabStop = false;
			// 
			// btnClose
			// 
			btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			btnClose.BackColor = Color.Transparent;
			btnClose.BackgroundImageLayout = ImageLayout.None;
			btnClose.FlatAppearance.BorderColor = Color.Black;
			btnClose.FlatAppearance.BorderSize = 0;
			btnClose.FlatAppearance.MouseDownBackColor = Color.FromArgb(225, 40, 40);
			btnClose.FlatAppearance.MouseOverBackColor = Color.FromArgb(225, 80, 80);
			btnClose.FlatStyle = FlatStyle.Flat;
			btnClose.Font = new Font("Calibri", 12F, FontStyle.Regular, GraphicsUnit.Point);
			btnClose.ForeColor = Color.White;
			btnClose.Location = new Point(760, 0);
			btnClose.Name = "btnClose";
			btnClose.Size = new Size(40, 30);
			btnClose.TabIndex = 3;
			btnClose.Text = "✕";
			btnClose.UseVisualStyleBackColor = false;
			btnClose.Click += btnClose_Click;
			// 
			// lblTitle
			// 
			lblTitle.AutoSize = true;
			lblTitle.Font = new Font("Calibri", 12F, FontStyle.Regular, GraphicsUnit.Point);
			lblTitle.ForeColor = Color.White;
			lblTitle.Location = new Point(32, 6);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new Size(116, 19);
			lblTitle.TabIndex = 2;
			lblTitle.Text = "Collection Name";
			// 
			// pictureBox1
			// 
			pictureBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			pictureBox1.Cursor = Cursors.SizeNWSE;
			pictureBox1.Enabled = false;
			pictureBox1.Image = Resources.Resources.resize_12;
			pictureBox1.Location = new Point(785, 509);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new Size(12, 12);
			pictureBox1.TabIndex = 21;
			pictureBox1.TabStop = false;
			// 
			// tvCollection
			// 
			tvCollection.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			tvCollection.BackColor = Color.FromArgb(252, 252, 252);
			tvCollection.BorderStyle = BorderStyle.FixedSingle;
			tvCollection.Font = new Font("Calibri", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
			tvCollection.Location = new Point(3, 3);
			tvCollection.Name = "tvCollection";
			treeNode1.Name = "Node1";
			treeNode1.Text = "BookName";
			treeNode2.Name = "Node3";
			treeNode2.Text = "Node3";
			treeNode3.Name = "Node9";
			treeNode3.Text = "Node9";
			treeNode4.Name = "Node10";
			treeNode4.Text = "Node10";
			treeNode5.Name = "Node11";
			treeNode5.Text = "Node11";
			treeNode6.Name = "Node12";
			treeNode6.Text = "Node12";
			treeNode7.Name = "Node13";
			treeNode7.Text = "Node13";
			treeNode8.Name = "Node14";
			treeNode8.Text = "Node14";
			treeNode9.Name = "Node15";
			treeNode9.Text = "Node15";
			treeNode10.Name = "Node16";
			treeNode10.Text = "Node16";
			treeNode11.Name = "Node17";
			treeNode11.Text = "Node17";
			treeNode12.Name = "Node18";
			treeNode12.Text = "Node18";
			treeNode13.Name = "Node19";
			treeNode13.Text = "Node19";
			treeNode14.Name = "Node20";
			treeNode14.Text = "Node20";
			treeNode15.Name = "Node21";
			treeNode15.Text = "Node21";
			treeNode16.Name = "Node22";
			treeNode16.Text = "Node22";
			treeNode17.Name = "Node23";
			treeNode17.Text = "Node23";
			treeNode18.Name = "Node24";
			treeNode18.Text = "Node24";
			treeNode19.Name = "Node25";
			treeNode19.Text = "Node25";
			treeNode20.Name = "Node26";
			treeNode20.Text = "Node26";
			treeNode21.Name = "Node27";
			treeNode21.Text = "Node27";
			treeNode22.Name = "Node28";
			treeNode22.Text = "Node28";
			treeNode23.Name = "Node29";
			treeNode23.Text = "Node29";
			treeNode24.Name = "Node30";
			treeNode24.Text = "Node30";
			treeNode25.Name = "Node31";
			treeNode25.Text = "Node31";
			treeNode26.Name = "Node32";
			treeNode26.Text = "Node32";
			treeNode27.Name = "Node33";
			treeNode27.Text = "Node33";
			treeNode28.Name = "Node2";
			treeNode28.Text = "BookName2";
			treeNode29.Name = "Node4";
			treeNode29.Text = "Node4";
			treeNode30.Name = "Node5";
			treeNode30.Text = "Node5";
			treeNode31.Name = "Node6";
			treeNode31.Text = "Node6";
			treeNode32.Name = "Node7";
			treeNode32.Text = "Node7";
			treeNode33.Name = "Node8";
			treeNode33.Text = "Node8";
			treeNode34.Name = "Node0";
			treeNode34.Text = "BookName2";
			tvCollection.Nodes.AddRange(new TreeNode[] { treeNode34 });
			tvCollection.Size = new Size(231, 439);
			tvCollection.TabIndex = 22;
			tvCollection.AfterSelect += tvCollection_AfterSelect;
			// 
			// tableLayoutPanel1
			// 
			tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			tableLayoutPanel1.ColumnCount = 2;
			tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
			tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
			tableLayoutPanel1.Controls.Add(tvCollection, 0, 0);
			tableLayoutPanel1.Controls.Add(panel1, 1, 0);
			tableLayoutPanel1.Location = new Point(5, 58);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.RowCount = 1;
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			tableLayoutPanel1.Size = new Size(791, 445);
			tableLayoutPanel1.TabIndex = 23;
			// 
			// panel1
			// 
			panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			panel1.Controls.Add(btnPrevious);
			panel1.Controls.Add(lineNumbers_For_RichTextBox1);
			panel1.Controls.Add(btnNext);
			panel1.Controls.Add(rtbQuotes);
			panel1.Location = new Point(240, 3);
			panel1.Name = "panel1";
			panel1.Size = new Size(548, 439);
			panel1.TabIndex = 23;
			// 
			// btnPrevious
			// 
			btnPrevious.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
			btnPrevious.BackColor = Color.Transparent;
			btnPrevious.FlatAppearance.BorderSize = 0;
			btnPrevious.FlatStyle = FlatStyle.Flat;
			btnPrevious.Font = new Font("Calibri", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
			btnPrevious.ForeColor = Color.Silver;
			btnPrevious.Location = new Point(3, 3);
			btnPrevious.Name = "btnPrevious";
			btnPrevious.Size = new Size(32, 433);
			btnPrevious.TabIndex = 5;
			btnPrevious.Text = "◀ ";
			btnPrevious.UseVisualStyleBackColor = false;
			btnPrevious.Click += btnPrevious_Click;
			// 
			// lineNumbers_For_RichTextBox1
			// 
			lineNumbers_For_RichTextBox1._SeeThroughMode_ = false;
			lineNumbers_For_RichTextBox1.AutoSizing = true;
			lineNumbers_For_RichTextBox1.BackgroundGradient_AlphaColor = Color.FromArgb(0, 0, 0, 0);
			lineNumbers_For_RichTextBox1.BackgroundGradient_BetaColor = Color.WhiteSmoke;
			lineNumbers_For_RichTextBox1.BackgroundGradient_Direction = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
			lineNumbers_For_RichTextBox1.BorderLines_Color = Color.WhiteSmoke;
			lineNumbers_For_RichTextBox1.BorderLines_Style = System.Drawing.Drawing2D.DashStyle.Dot;
			lineNumbers_For_RichTextBox1.BorderLines_Thickness = 1F;
			lineNumbers_For_RichTextBox1.DockSide = LineNumbers.LineNumbers_For_RichTextBox.LineNumberDockSide.Left;
			lineNumbers_For_RichTextBox1.Font = new Font("Calibri", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
			lineNumbers_For_RichTextBox1.ForeColor = SystemColors.ControlDarkDark;
			lineNumbers_For_RichTextBox1.GridLines_Color = Color.WhiteSmoke;
			lineNumbers_For_RichTextBox1.GridLines_Style = System.Drawing.Drawing2D.DashStyle.Solid;
			lineNumbers_For_RichTextBox1.GridLines_Thickness = 1F;
			lineNumbers_For_RichTextBox1.LineNrs_Alignment = ContentAlignment.TopRight;
			lineNumbers_For_RichTextBox1.LineNrs_AntiAlias = true;
			lineNumbers_For_RichTextBox1.LineNrs_AsHexadecimal = false;
			lineNumbers_For_RichTextBox1.LineNrs_ClippedByItemRectangle = true;
			lineNumbers_For_RichTextBox1.LineNrs_LeadingZeroes = true;
			lineNumbers_For_RichTextBox1.LineNrs_Offset = new Size(0, 0);
			lineNumbers_For_RichTextBox1.Location = new Point(44, 3);
			lineNumbers_For_RichTextBox1.Margin = new Padding(0);
			lineNumbers_For_RichTextBox1.MarginLines_Color = Color.SlateGray;
			lineNumbers_For_RichTextBox1.MarginLines_Side = LineNumbers.LineNumbers_For_RichTextBox.LineNumberDockSide.Right;
			lineNumbers_For_RichTextBox1.MarginLines_Style = System.Drawing.Drawing2D.DashStyle.Solid;
			lineNumbers_For_RichTextBox1.MarginLines_Thickness = 1F;
			lineNumbers_For_RichTextBox1.Name = "lineNumbers_For_RichTextBox1";
			lineNumbers_For_RichTextBox1.Padding = new Padding(0, 0, 2, 0);
			lineNumbers_For_RichTextBox1.ParentRichTextBox = rtbQuotes;
			lineNumbers_For_RichTextBox1.Show_BackgroundGradient = true;
			lineNumbers_For_RichTextBox1.Show_BorderLines = true;
			lineNumbers_For_RichTextBox1.Show_GridLines = true;
			lineNumbers_For_RichTextBox1.Show_LineNrs = true;
			lineNumbers_For_RichTextBox1.Show_MarginLines = true;
			lineNumbers_For_RichTextBox1.Size = new Size(19, 433);
			lineNumbers_For_RichTextBox1.TabIndex = 7;
			// 
			// rtbQuotes
			// 
			rtbQuotes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			rtbQuotes.BackColor = Color.FromArgb(252, 252, 252);
			rtbQuotes.BorderStyle = BorderStyle.FixedSingle;
			rtbQuotes.DetectUrls = false;
			rtbQuotes.Font = new Font("Calibri", 12F, FontStyle.Regular, GraphicsUnit.Point);
			rtbQuotes.ForeColor = SystemColors.WindowText;
			rtbQuotes.Location = new Point(64, 3);
			rtbQuotes.Name = "rtbQuotes";
			rtbQuotes.ReadOnly = true;
			rtbQuotes.Size = new Size(446, 433);
			rtbQuotes.TabIndex = 0;
			rtbQuotes.Text = "1";
			// 
			// btnNext
			// 
			btnNext.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
			btnNext.BackColor = Color.Transparent;
			btnNext.FlatAppearance.BorderSize = 0;
			btnNext.FlatStyle = FlatStyle.Flat;
			btnNext.Font = new Font("Calibri", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
			btnNext.ForeColor = Color.Silver;
			btnNext.Location = new Point(514, 3);
			btnNext.Name = "btnNext";
			btnNext.Size = new Size(29, 433);
			btnNext.TabIndex = 6;
			btnNext.Text = "▶ ";
			btnNext.UseVisualStyleBackColor = false;
			btnNext.Click += btnNext_Click;
			// 
			// lblLocationInCollection
			// 
			lblLocationInCollection.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			lblLocationInCollection.Font = new Font("Calibri", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
			lblLocationInCollection.Location = new Point(309, 34);
			lblLocationInCollection.Name = "lblLocationInCollection";
			lblLocationInCollection.Size = new Size(446, 21);
			lblLocationInCollection.TabIndex = 24;
			lblLocationInCollection.Text = "Book1 chapter 4";
			lblLocationInCollection.TextAlign = ContentAlignment.MiddleRight;
			// 
			// btnBackToManage
			// 
			btnBackToManage.FlatAppearance.BorderColor = Color.Gray;
			btnBackToManage.FlatStyle = FlatStyle.Flat;
			btnBackToManage.Font = new Font("Calibri", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
			btnBackToManage.ForeColor = SystemColors.ControlText;
			btnBackToManage.Location = new Point(5, 32);
			btnBackToManage.Name = "btnBackToManage";
			btnBackToManage.Size = new Size(87, 23);
			btnBackToManage.TabIndex = 25;
			btnBackToManage.Text = "◀     Back";
			btnBackToManage.UseVisualStyleBackColor = true;
			btnBackToManage.Click += btnBackToManage_Click;
			// 
			// ReaderForm
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.WhiteSmoke;
			ClientSize = new Size(800, 524);
			Controls.Add(btnBackToManage);
			Controls.Add(lblLocationInCollection);
			Controls.Add(tableLayoutPanel1);
			Controls.Add(pictureBox1);
			Controls.Add(pnlTitle);
			FormBorderStyle = FormBorderStyle.None;
			Icon = (Icon)resources.GetObject("$this.Icon");
			MinimumSize = new Size(800, 524);
			Name = "ReaderForm";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Quoter";
			Load += ReaderForm_Load;
			pnlTitle.ResumeLayout(false);
			pnlTitle.PerformLayout();
			((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			tableLayoutPanel1.ResumeLayout(false);
			panel1.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion

		private Panel pnlTitle;
		private Label lblTitle;
		private Button btnClose;
		private PictureBox pictureBox1;
		private TreeView tvCollection;
		private TableLayoutPanel tableLayoutPanel1;
		private Panel panel1;
		private RichTextBox rtbQuotes;
		private Button btnPrevious;
		private Button btnNext;
		private LineNumbers.LineNumbers_For_RichTextBox lineNumbers_For_RichTextBox1;
		private Label lblLocationInCollection;
		private Button btnBackToManage;
		private PictureBox pictureBox2;
		private Button btnMinimize;
	}
}