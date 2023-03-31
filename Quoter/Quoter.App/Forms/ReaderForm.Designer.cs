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
			TreeNode treeNode35 = new TreeNode("BookName");
			TreeNode treeNode36 = new TreeNode("Node3");
			TreeNode treeNode37 = new TreeNode("Node9");
			TreeNode treeNode38 = new TreeNode("Node10");
			TreeNode treeNode39 = new TreeNode("Node11");
			TreeNode treeNode40 = new TreeNode("Node12");
			TreeNode treeNode41 = new TreeNode("Node13");
			TreeNode treeNode42 = new TreeNode("Node14");
			TreeNode treeNode43 = new TreeNode("Node15");
			TreeNode treeNode44 = new TreeNode("Node16");
			TreeNode treeNode45 = new TreeNode("Node17");
			TreeNode treeNode46 = new TreeNode("Node18");
			TreeNode treeNode47 = new TreeNode("Node19");
			TreeNode treeNode48 = new TreeNode("Node20");
			TreeNode treeNode49 = new TreeNode("Node21");
			TreeNode treeNode50 = new TreeNode("Node22");
			TreeNode treeNode51 = new TreeNode("Node23");
			TreeNode treeNode52 = new TreeNode("Node24");
			TreeNode treeNode53 = new TreeNode("Node25");
			TreeNode treeNode54 = new TreeNode("Node26");
			TreeNode treeNode55 = new TreeNode("Node27");
			TreeNode treeNode56 = new TreeNode("Node28");
			TreeNode treeNode57 = new TreeNode("Node29");
			TreeNode treeNode58 = new TreeNode("Node30");
			TreeNode treeNode59 = new TreeNode("Node31");
			TreeNode treeNode60 = new TreeNode("Node32");
			TreeNode treeNode61 = new TreeNode("Node33");
			TreeNode treeNode62 = new TreeNode("BookName2", new TreeNode[] { treeNode36, treeNode37, treeNode38, treeNode39, treeNode40, treeNode41, treeNode42, treeNode43, treeNode44, treeNode45, treeNode46, treeNode47, treeNode48, treeNode49, treeNode50, treeNode51, treeNode52, treeNode53, treeNode54, treeNode55, treeNode56, treeNode57, treeNode58, treeNode59, treeNode60, treeNode61 });
			TreeNode treeNode63 = new TreeNode("Node4");
			TreeNode treeNode64 = new TreeNode("Node5");
			TreeNode treeNode65 = new TreeNode("Node6");
			TreeNode treeNode66 = new TreeNode("Node7");
			TreeNode treeNode67 = new TreeNode("Node8");
			TreeNode treeNode68 = new TreeNode("BookName2", new TreeNode[] { treeNode35, treeNode62, treeNode63, treeNode64, treeNode65, treeNode66, treeNode67 });
			pnlTitle = new Panel();
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
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			tableLayoutPanel1.SuspendLayout();
			panel1.SuspendLayout();
			SuspendLayout();
			// 
			// pnlTitle
			// 
			pnlTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			pnlTitle.BackColor = Color.SteelBlue;
			pnlTitle.Controls.Add(btnClose);
			pnlTitle.Controls.Add(lblTitle);
			pnlTitle.Location = new Point(0, 0);
			pnlTitle.Name = "pnlTitle";
			pnlTitle.Size = new Size(800, 30);
			pnlTitle.TabIndex = 2;
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
			btnClose.Font = new Font("Lucida Console", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
			btnClose.ForeColor = Color.White;
			btnClose.Location = new Point(750, 1);
			btnClose.Name = "btnClose";
			btnClose.Size = new Size(50, 30);
			btnClose.TabIndex = 3;
			btnClose.Text = "X";
			btnClose.UseVisualStyleBackColor = false;
			btnClose.Click += btnClose_Click;
			// 
			// lblTitle
			// 
			lblTitle.AutoSize = true;
			lblTitle.Font = new Font("Calibri", 12F, FontStyle.Regular, GraphicsUnit.Point);
			lblTitle.ForeColor = Color.White;
			lblTitle.Location = new Point(12, 5);
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
			treeNode35.Name = "Node1";
			treeNode35.Text = "BookName";
			treeNode36.Name = "Node3";
			treeNode36.Text = "Node3";
			treeNode37.Name = "Node9";
			treeNode37.Text = "Node9";
			treeNode38.Name = "Node10";
			treeNode38.Text = "Node10";
			treeNode39.Name = "Node11";
			treeNode39.Text = "Node11";
			treeNode40.Name = "Node12";
			treeNode40.Text = "Node12";
			treeNode41.Name = "Node13";
			treeNode41.Text = "Node13";
			treeNode42.Name = "Node14";
			treeNode42.Text = "Node14";
			treeNode43.Name = "Node15";
			treeNode43.Text = "Node15";
			treeNode44.Name = "Node16";
			treeNode44.Text = "Node16";
			treeNode45.Name = "Node17";
			treeNode45.Text = "Node17";
			treeNode46.Name = "Node18";
			treeNode46.Text = "Node18";
			treeNode47.Name = "Node19";
			treeNode47.Text = "Node19";
			treeNode48.Name = "Node20";
			treeNode48.Text = "Node20";
			treeNode49.Name = "Node21";
			treeNode49.Text = "Node21";
			treeNode50.Name = "Node22";
			treeNode50.Text = "Node22";
			treeNode51.Name = "Node23";
			treeNode51.Text = "Node23";
			treeNode52.Name = "Node24";
			treeNode52.Text = "Node24";
			treeNode53.Name = "Node25";
			treeNode53.Text = "Node25";
			treeNode54.Name = "Node26";
			treeNode54.Text = "Node26";
			treeNode55.Name = "Node27";
			treeNode55.Text = "Node27";
			treeNode56.Name = "Node28";
			treeNode56.Text = "Node28";
			treeNode57.Name = "Node29";
			treeNode57.Text = "Node29";
			treeNode58.Name = "Node30";
			treeNode58.Text = "Node30";
			treeNode59.Name = "Node31";
			treeNode59.Text = "Node31";
			treeNode60.Name = "Node32";
			treeNode60.Text = "Node32";
			treeNode61.Name = "Node33";
			treeNode61.Text = "Node33";
			treeNode62.Name = "Node2";
			treeNode62.Text = "BookName2";
			treeNode63.Name = "Node4";
			treeNode63.Text = "Node4";
			treeNode64.Name = "Node5";
			treeNode64.Text = "Node5";
			treeNode65.Name = "Node6";
			treeNode65.Text = "Node6";
			treeNode66.Name = "Node7";
			treeNode66.Text = "Node7";
			treeNode67.Name = "Node8";
			treeNode67.Text = "Node8";
			treeNode68.Name = "Node0";
			treeNode68.Text = "BookName2";
			tvCollection.Nodes.AddRange(new TreeNode[] { treeNode68 });
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
			lineNumbers_For_RichTextBox1.Location = new Point(40, 3);
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
			lineNumbers_For_RichTextBox1.Size = new Size(23, 433);
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
			rtbQuotes.Text = "1\n2\n3\n4\n5\n6\n7\n8 Longer text\n9 Even some longer text\n10 Again some other text\n11\n12\n13\n14\n15";
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
			MinimumSize = new Size(800, 524);
			Name = "ReaderForm";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Quoter";
			Load += ReaderForm_Load;
			pnlTitle.ResumeLayout(false);
			pnlTitle.PerformLayout();
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
	}
}