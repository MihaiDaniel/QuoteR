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
			this.btnOk = new System.Windows.Forms.Button();
			this.txtMessage = new System.Windows.Forms.TextBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.btnCancel = new System.Windows.Forms.Button();
			this.pnlTitle.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlTitle
			// 
			this.pnlTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlTitle.BackColor = System.Drawing.Color.DarkOrange;
			this.pnlTitle.Controls.Add(this.lblTopBar);
			this.pnlTitle.Location = new System.Drawing.Point(0, 0);
			this.pnlTitle.Name = "pnlTitle";
			this.pnlTitle.Size = new System.Drawing.Size(340, 40);
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
			// btnOk
			// 
			this.btnOk.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
			this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnOk.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnOk.Location = new System.Drawing.Point(86, 92);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(114, 31);
			this.btnOk.TabIndex = 3;
			this.btnOk.Text = "ok";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// txtMessage
			// 
			this.txtMessage.BackColor = System.Drawing.Color.WhiteSmoke;
			this.txtMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtMessage.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.txtMessage.Location = new System.Drawing.Point(12, 46);
			this.txtMessage.Multiline = true;
			this.txtMessage.Name = "txtMessage";
			this.txtMessage.Size = new System.Drawing.Size(312, 75);
			this.txtMessage.TabIndex = 4;
			this.txtMessage.Text = "A aparut un mesaj de atentionare ca sa stii ca s-a intamplat ceva anume";
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
			this.panel1.Controls.Add(this.btnOk);
			this.panel1.Controls.Add(this.btnCancel);
			this.panel1.Location = new System.Drawing.Point(4, 35);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(331, 131);
			this.panel1.TabIndex = 5;
			// 
			// btnCancel
			// 
			this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnCancel.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnCancel.Location = new System.Drawing.Point(206, 92);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(114, 31);
			this.btnCancel.TabIndex = 6;
			this.btnCancel.Text = "cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// DialogMessageForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.WhiteSmoke;
			this.ClientSize = new System.Drawing.Size(340, 170);
			this.ControlBox = false;
			this.Controls.Add(this.pnlTitle);
			this.Controls.Add(this.txtMessage);
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "DialogMessageForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "DialogForm";
			this.pnlTitle.ResumeLayout(false);
			this.pnlTitle.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Panel pnlTitle;
		private Label lblTopBar;
		private Button btnOk;
		private TextBox txtMessage;
		private Panel panel1;
		private Button btnCancel;
	}
}