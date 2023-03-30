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
			TreeNode treeNode239 = new TreeNode("BookName");
			TreeNode treeNode240 = new TreeNode("Node3");
			TreeNode treeNode241 = new TreeNode("Node9");
			TreeNode treeNode242 = new TreeNode("Node10");
			TreeNode treeNode243 = new TreeNode("Node11");
			TreeNode treeNode244 = new TreeNode("Node12");
			TreeNode treeNode245 = new TreeNode("Node13");
			TreeNode treeNode246 = new TreeNode("Node14");
			TreeNode treeNode247 = new TreeNode("Node15");
			TreeNode treeNode248 = new TreeNode("Node16");
			TreeNode treeNode249 = new TreeNode("Node17");
			TreeNode treeNode250 = new TreeNode("Node18");
			TreeNode treeNode251 = new TreeNode("Node19");
			TreeNode treeNode252 = new TreeNode("Node20");
			TreeNode treeNode253 = new TreeNode("Node21");
			TreeNode treeNode254 = new TreeNode("Node22");
			TreeNode treeNode255 = new TreeNode("Node23");
			TreeNode treeNode256 = new TreeNode("Node24");
			TreeNode treeNode257 = new TreeNode("Node25");
			TreeNode treeNode258 = new TreeNode("Node26");
			TreeNode treeNode259 = new TreeNode("Node27");
			TreeNode treeNode260 = new TreeNode("Node28");
			TreeNode treeNode261 = new TreeNode("Node29");
			TreeNode treeNode262 = new TreeNode("Node30");
			TreeNode treeNode263 = new TreeNode("Node31");
			TreeNode treeNode264 = new TreeNode("Node32");
			TreeNode treeNode265 = new TreeNode("Node33");
			TreeNode treeNode266 = new TreeNode("BookName2", new TreeNode[] { treeNode240, treeNode241, treeNode242, treeNode243, treeNode244, treeNode245, treeNode246, treeNode247, treeNode248, treeNode249, treeNode250, treeNode251, treeNode252, treeNode253, treeNode254, treeNode255, treeNode256, treeNode257, treeNode258, treeNode259, treeNode260, treeNode261, treeNode262, treeNode263, treeNode264, treeNode265 });
			TreeNode treeNode267 = new TreeNode("Node4");
			TreeNode treeNode268 = new TreeNode("Node5");
			TreeNode treeNode269 = new TreeNode("Node6");
			TreeNode treeNode270 = new TreeNode("Node7");
			TreeNode treeNode271 = new TreeNode("Node8");
			TreeNode treeNode272 = new TreeNode("BookName2", new TreeNode[] { treeNode239, treeNode266, treeNode267, treeNode268, treeNode269, treeNode270, treeNode271 });
			pnlTitle = new Panel();
			btnClose = new Button();
			lblTitle = new Label();
			pictureBox1 = new PictureBox();
			tvCollection = new TreeView();
			tableLayoutPanel1 = new TableLayoutPanel();
			panel1 = new Panel();
			btnNext = new Button();
			btnPrevious = new Button();
			rtbQuotes = new RichTextBox();
			lineNumbers_For_RichTextBox1 = new LineNumbers.LineNumbers_For_RichTextBox();
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
			lblTitle.Font = new Font("Calibri Light", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
			lblTitle.ForeColor = Color.White;
			lblTitle.Location = new Point(12, 2);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new Size(151, 26);
			lblTitle.TabIndex = 2;
			lblTitle.Text = "Collection Name";
			// 
			// pictureBox1
			// 
			pictureBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			pictureBox1.Cursor = Cursors.SizeNWSE;
			pictureBox1.Enabled = false;
			pictureBox1.Image = Resources.Resources.resize_12;
			pictureBox1.Location = new Point(785, 435);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new Size(12, 12);
			pictureBox1.TabIndex = 21;
			pictureBox1.TabStop = false;
			// 
			// tvCollection
			// 
			tvCollection.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			tvCollection.Font = new Font("Calibri", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
			tvCollection.Location = new Point(3, 3);
			tvCollection.Name = "tvCollection";
			treeNode239.Name = "Node1";
			treeNode239.Text = "BookName";
			treeNode240.Name = "Node3";
			treeNode240.Text = "Node3";
			treeNode241.Name = "Node9";
			treeNode241.Text = "Node9";
			treeNode242.Name = "Node10";
			treeNode242.Text = "Node10";
			treeNode243.Name = "Node11";
			treeNode243.Text = "Node11";
			treeNode244.Name = "Node12";
			treeNode244.Text = "Node12";
			treeNode245.Name = "Node13";
			treeNode245.Text = "Node13";
			treeNode246.Name = "Node14";
			treeNode246.Text = "Node14";
			treeNode247.Name = "Node15";
			treeNode247.Text = "Node15";
			treeNode248.Name = "Node16";
			treeNode248.Text = "Node16";
			treeNode249.Name = "Node17";
			treeNode249.Text = "Node17";
			treeNode250.Name = "Node18";
			treeNode250.Text = "Node18";
			treeNode251.Name = "Node19";
			treeNode251.Text = "Node19";
			treeNode252.Name = "Node20";
			treeNode252.Text = "Node20";
			treeNode253.Name = "Node21";
			treeNode253.Text = "Node21";
			treeNode254.Name = "Node22";
			treeNode254.Text = "Node22";
			treeNode255.Name = "Node23";
			treeNode255.Text = "Node23";
			treeNode256.Name = "Node24";
			treeNode256.Text = "Node24";
			treeNode257.Name = "Node25";
			treeNode257.Text = "Node25";
			treeNode258.Name = "Node26";
			treeNode258.Text = "Node26";
			treeNode259.Name = "Node27";
			treeNode259.Text = "Node27";
			treeNode260.Name = "Node28";
			treeNode260.Text = "Node28";
			treeNode261.Name = "Node29";
			treeNode261.Text = "Node29";
			treeNode262.Name = "Node30";
			treeNode262.Text = "Node30";
			treeNode263.Name = "Node31";
			treeNode263.Text = "Node31";
			treeNode264.Name = "Node32";
			treeNode264.Text = "Node32";
			treeNode265.Name = "Node33";
			treeNode265.Text = "Node33";
			treeNode266.Name = "Node2";
			treeNode266.Text = "BookName2";
			treeNode267.Name = "Node4";
			treeNode267.Text = "Node4";
			treeNode268.Name = "Node5";
			treeNode268.Text = "Node5";
			treeNode269.Name = "Node6";
			treeNode269.Text = "Node6";
			treeNode270.Name = "Node7";
			treeNode270.Text = "Node7";
			treeNode271.Name = "Node8";
			treeNode271.Text = "Node8";
			treeNode272.Name = "Node0";
			treeNode272.Text = "BookName2";
			tvCollection.Nodes.AddRange(new TreeNode[] { treeNode272 });
			tvCollection.Size = new Size(231, 376);
			tvCollection.TabIndex = 22;
			// 
			// tableLayoutPanel1
			// 
			tableLayoutPanel1.ColumnCount = 2;
			tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
			tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
			tableLayoutPanel1.Controls.Add(tvCollection, 0, 0);
			tableLayoutPanel1.Controls.Add(panel1, 1, 0);
			tableLayoutPanel1.Location = new Point(5, 47);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.RowCount = 1;
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			tableLayoutPanel1.Size = new Size(791, 382);
			tableLayoutPanel1.TabIndex = 23;
			// 
			// panel1
			// 
			panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			panel1.Controls.Add(lineNumbers_For_RichTextBox1);
			panel1.Controls.Add(btnNext);
			panel1.Controls.Add(btnPrevious);
			panel1.Controls.Add(rtbQuotes);
			panel1.Location = new Point(240, 3);
			panel1.Name = "panel1";
			panel1.Size = new Size(548, 376);
			panel1.TabIndex = 23;
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
			btnNext.Size = new Size(29, 370);
			btnNext.TabIndex = 6;
			btnNext.Text = "▶ ";
			btnNext.UseVisualStyleBackColor = false;
			btnNext.Click += btnNext_Click;
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
			btnPrevious.Size = new Size(32, 370);
			btnPrevious.TabIndex = 5;
			btnPrevious.Text = "◀ ";
			btnPrevious.UseVisualStyleBackColor = false;
			btnPrevious.Click += btnPrevious_Click;
			// 
			// rtbQuotes
			// 
			rtbQuotes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
			rtbQuotes.Font = new Font("Calibri", 12F, FontStyle.Regular, GraphicsUnit.Point);
			rtbQuotes.ForeColor = SystemColors.WindowText;
			rtbQuotes.Location = new Point(62, 3);
			rtbQuotes.Name = "rtbQuotes";
			rtbQuotes.Size = new Size(448, 370);
			rtbQuotes.TabIndex = 0;
			rtbQuotes.Text = "1\n2\n3\n4\n5\n6\n7\n8\n9\n10\n11\n12\n13\n14\n15";
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
			lineNumbers_For_RichTextBox1.Location = new Point(30, 3);
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
			lineNumbers_For_RichTextBox1.Size = new Size(31, 370);
			lineNumbers_For_RichTextBox1.TabIndex = 7;
			// 
			// ReaderForm
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.WhiteSmoke;
			ClientSize = new Size(800, 450);
			Controls.Add(tableLayoutPanel1);
			Controls.Add(pictureBox1);
			Controls.Add(pnlTitle);
			FormBorderStyle = FormBorderStyle.None;
			Name = "ReaderForm";
			Text = "Quoter";
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
	}
}