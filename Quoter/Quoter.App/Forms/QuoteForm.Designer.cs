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
			this.pnlTitle = new System.Windows.Forms.Panel();
			this.btnClose = new System.Windows.Forms.Button();
			this.lblTitle = new System.Windows.Forms.Label();
			this.txtBody = new System.Windows.Forms.TextBox();
			this.txtFooter = new System.Windows.Forms.TextBox();
			this.pnlTitle.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlTitle
			// 
			this.pnlTitle.BackColor = System.Drawing.Color.SlateGray;
			this.pnlTitle.Controls.Add(this.btnClose);
			this.pnlTitle.Controls.Add(this.lblTitle);
			this.pnlTitle.Location = new System.Drawing.Point(0, 0);
			this.pnlTitle.Name = "pnlTitle";
			this.pnlTitle.Size = new System.Drawing.Size(380, 30);
			this.pnlTitle.TabIndex = 0;
			// 
			// btnClose
			// 
			this.btnClose.BackColor = System.Drawing.Color.Transparent;
			this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.Black;
			this.btnClose.FlatAppearance.BorderSize = 0;
			this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
			this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnClose.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			this.btnClose.ForeColor = System.Drawing.Color.White;
			this.btnClose.Location = new System.Drawing.Point(330, 0);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(50, 30);
			this.btnClose.TabIndex = 2;
			this.btnClose.Text = "X";
			this.btnClose.UseVisualStyleBackColor = false;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// lblTitle
			// 
			this.lblTitle.AutoSize = true;
			this.lblTitle.Font = new System.Drawing.Font("Calibri Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblTitle.ForeColor = System.Drawing.Color.White;
			this.lblTitle.Location = new System.Drawing.Point(11, 6);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(329, 19);
			this.lblTitle.TabIndex = 0;
			this.lblTitle.Text = "1234567890123456789012345678901234567890";
			// 
			// txtBody
			// 
			this.txtBody.BackColor = System.Drawing.Color.White;
			this.txtBody.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtBody.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
			this.txtBody.Location = new System.Drawing.Point(11, 36);
			this.txtBody.Multiline = true;
			this.txtBody.Name = "txtBody";
			this.txtBody.ReadOnly = true;
			this.txtBody.Size = new System.Drawing.Size(357, 84);
			this.txtBody.TabIndex = 1;
			this.txtBody.Text = "123456789012345678901234567890123456789012345678901234567890123456789012345678901" +
    "23456789012345678901234567890";
			this.txtBody.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// txtFooter
			// 
			this.txtFooter.BackColor = System.Drawing.Color.White;
			this.txtFooter.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtFooter.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.txtFooter.Location = new System.Drawing.Point(12, 128);
			this.txtFooter.Name = "txtFooter";
			this.txtFooter.ReadOnly = true;
			this.txtFooter.Size = new System.Drawing.Size(356, 20);
			this.txtFooter.TabIndex = 2;
			this.txtFooter.Text = "1234567890123456789012345678901234567890";
			this.txtFooter.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// MessageForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(380, 160);
			this.ControlBox = false;
			this.Controls.Add(this.txtFooter);
			this.Controls.Add(this.txtBody);
			this.Controls.Add(this.pnlTitle);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MdiChildrenMinimizedAnchorBottom = false;
			this.MinimizeBox = false;
			this.Name = "MessageForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "MessageForm";
			this.Load += new System.EventHandler(this.MessageForm_Load);
			this.pnlTitle.ResumeLayout(false);
			this.pnlTitle.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Panel pnlTitle;
		private Label lblTitle;
		private TextBox txtBody;
		private TextBox txtFooter;
		private Button btnClose;
	}
}