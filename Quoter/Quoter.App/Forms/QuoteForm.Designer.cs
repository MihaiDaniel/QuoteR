namespace Quoter.App.Views
{
	partial class QuoteForm
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
			pnlTitle = new Panel();
			btnClose = new Button();
			lblTitle = new Label();
			txtBody = new TextBox();
			txtFooter = new TextBox();
			btnNextQuote = new Button();
			btnPreviousQuote = new Button();
			pnlTitle.SuspendLayout();
			SuspendLayout();
			// 
			// pnlTitle
			// 
			pnlTitle.BackColor = Color.SlateGray;
			pnlTitle.Controls.Add(btnClose);
			pnlTitle.Controls.Add(lblTitle);
			pnlTitle.Location = new Point(0, 0);
			pnlTitle.Name = "pnlTitle";
			pnlTitle.Size = new Size(380, 30);
			pnlTitle.TabIndex = 0;
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
			btnClose.Location = new Point(330, 0);
			btnClose.Name = "btnClose";
			btnClose.Size = new Size(50, 30);
			btnClose.TabIndex = 2;
			btnClose.Text = "X";
			btnClose.UseVisualStyleBackColor = false;
			btnClose.Click += btnClose_Click;
			// 
			// lblTitle
			// 
			lblTitle.AutoSize = true;
			lblTitle.Font = new Font("Calibri", 12F, FontStyle.Bold, GraphicsUnit.Point);
			lblTitle.ForeColor = Color.White;
			lblTitle.Location = new Point(11, 6);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new Size(329, 19);
			lblTitle.TabIndex = 0;
			lblTitle.Text = "1234567890123456789012345678901234567890";
			// 
			// txtBody
			// 
			txtBody.BackColor = Color.White;
			txtBody.BorderStyle = BorderStyle.None;
			txtBody.Font = new Font("Calibri", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
			txtBody.Location = new Point(11, 36);
			txtBody.Multiline = true;
			txtBody.Name = "txtBody";
			txtBody.ReadOnly = true;
			txtBody.Size = new Size(357, 94);
			txtBody.TabIndex = 1;
			txtBody.Text = "0123456789001234567890012345678900123456789001234567890012345678900123456789001234567890";
			txtBody.TextAlign = HorizontalAlignment.Center;
			// 
			// txtFooter
			// 
			txtFooter.BackColor = Color.White;
			txtFooter.BorderStyle = BorderStyle.None;
			txtFooter.Cursor = Cursors.Hand;
			txtFooter.Font = new Font("Calibri", 12F, FontStyle.Bold, GraphicsUnit.Point);
			txtFooter.Location = new Point(12, 135);
			txtFooter.Name = "txtFooter";
			txtFooter.ReadOnly = true;
			txtFooter.Size = new Size(356, 20);
			txtFooter.TabIndex = 2;
			txtFooter.Text = "123456789012345678901234567890123456789012345678901234567890";
			txtFooter.TextAlign = HorizontalAlignment.Center;
			txtFooter.Click += txtFooter_Click;
			txtFooter.MouseEnter += txtFooter_MouseEnter;
			txtFooter.MouseLeave += txtFooter_MouseLeave;
			// 
			// btnNextQuote
			// 
			btnNextQuote.BackColor = Color.Transparent;
			btnNextQuote.FlatAppearance.BorderSize = 0;
			btnNextQuote.FlatStyle = FlatStyle.Flat;
			btnNextQuote.Font = new Font("Calibri", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
			btnNextQuote.ForeColor = Color.Silver;
			btnNextQuote.Location = new Point(364, 36);
			btnNextQuote.Name = "btnNextQuote";
			btnNextQuote.Size = new Size(17, 94);
			btnNextQuote.TabIndex = 3;
			btnNextQuote.Text = "▶ ";
			btnNextQuote.UseVisualStyleBackColor = false;
			btnNextQuote.Click += btnNextQuote_Click;
			// 
			// btnPreviousQuote
			// 
			btnPreviousQuote.BackColor = Color.Transparent;
			btnPreviousQuote.FlatAppearance.BorderSize = 0;
			btnPreviousQuote.FlatStyle = FlatStyle.Flat;
			btnPreviousQuote.Font = new Font("Calibri", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
			btnPreviousQuote.ForeColor = Color.Silver;
			btnPreviousQuote.Location = new Point(-2, 36);
			btnPreviousQuote.Name = "btnPreviousQuote";
			btnPreviousQuote.Size = new Size(17, 94);
			btnPreviousQuote.TabIndex = 4;
			btnPreviousQuote.Text = "◀ ";
			btnPreviousQuote.UseVisualStyleBackColor = false;
			btnPreviousQuote.Click += btnPreviousQuote_Click;
			// 
			// QuoteForm
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.White;
			ClientSize = new Size(380, 160);
			ControlBox = false;
			Controls.Add(btnPreviousQuote);
			Controls.Add(btnNextQuote);
			Controls.Add(txtFooter);
			Controls.Add(txtBody);
			Controls.Add(pnlTitle);
			DoubleBuffered = true;
			FormBorderStyle = FormBorderStyle.None;
			MaximizeBox = false;
			MdiChildrenMinimizedAnchorBottom = false;
			MinimizeBox = false;
			Name = "QuoteForm";
			ShowIcon = false;
			ShowInTaskbar = false;
			SizeGripStyle = SizeGripStyle.Hide;
			Text = "MessageForm";
			Load += MessageForm_Load;
			pnlTitle.ResumeLayout(false);
			pnlTitle.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Panel pnlTitle;
		private Label lblTitle;
		private TextBox txtBody;
		private TextBox txtFooter;
		private Button btnClose;
		private Button btnNextQuote;
		private Button btnPreviousQuote;
	}
}