namespace Quoter.App.Forms
{
	partial class DialogMessageForm
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
			this.lblTopBar = new System.Windows.Forms.Label();
			this.btnLeft = new System.Windows.Forms.Button();
			this.txtMessage = new System.Windows.Forms.TextBox();
			this.btnRight = new System.Windows.Forms.Button();
			this.pnlTitle.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlTitle
			// 
			this.pnlTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlTitle.BackColor = System.Drawing.Color.SlateGray;
			this.pnlTitle.Controls.Add(this.lblTopBar);
			this.pnlTitle.Location = new System.Drawing.Point(0, 0);
			this.pnlTitle.Name = "pnlTitle";
			this.pnlTitle.Size = new System.Drawing.Size(336, 40);
			this.pnlTitle.TabIndex = 2;
			// 
			// lblTopBar
			// 
			this.lblTopBar.AutoSize = true;
			this.lblTopBar.Font = new System.Drawing.Font("Calibri Light", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblTopBar.ForeColor = System.Drawing.Color.White;
			this.lblTopBar.Location = new System.Drawing.Point(12, 7);
			this.lblTopBar.Name = "lblTopBar";
			this.lblTopBar.Size = new System.Drawing.Size(144, 26);
			this.lblTopBar.TabIndex = 2;
			this.lblTopBar.Text = "Manage quotes";
			// 
			// btnLeft
			// 
			this.btnLeft.Location = new System.Drawing.Point(90, 119);
			this.btnLeft.Name = "btnLeft";
			this.btnLeft.Size = new System.Drawing.Size(114, 31);
			this.btnLeft.TabIndex = 3;
			this.btnLeft.Text = "button1";
			this.btnLeft.UseVisualStyleBackColor = true;
			this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
			// 
			// txtMessage
			// 
			this.txtMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtMessage.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.txtMessage.Location = new System.Drawing.Point(12, 46);
			this.txtMessage.Multiline = true;
			this.txtMessage.Name = "txtMessage";
			this.txtMessage.Size = new System.Drawing.Size(312, 67);
			this.txtMessage.TabIndex = 4;
			this.txtMessage.Text = "A aparut un mesaj de atentionare ca sa stii ca s-a intamplat ceva anume";
			// 
			// btnRight
			// 
			this.btnRight.Location = new System.Drawing.Point(210, 119);
			this.btnRight.Name = "btnRight";
			this.btnRight.Size = new System.Drawing.Size(114, 31);
			this.btnRight.TabIndex = 5;
			this.btnRight.Text = "button2";
			this.btnRight.UseVisualStyleBackColor = true;
			this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
			// 
			// DialogForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(336, 162);
			this.ControlBox = false;
			this.Controls.Add(this.btnRight);
			this.Controls.Add(this.txtMessage);
			this.Controls.Add(this.btnLeft);
			this.Controls.Add(this.pnlTitle);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "DialogForm";
			this.Text = "DialogForm";
			this.pnlTitle.ResumeLayout(false);
			this.pnlTitle.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Panel pnlTitle;
		private Label lblTopBar;
		private Button btnLeft;
		private TextBox txtMessage;
		private Button btnRight;
	}
}