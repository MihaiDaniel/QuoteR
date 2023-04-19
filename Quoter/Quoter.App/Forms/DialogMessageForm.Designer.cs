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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogMessageForm));
			pnlTitle = new Panel();
			lblTopBar = new Label();
			btnOk = new Button();
			txtMessage = new TextBox();
			panel1 = new Panel();
			btnCancel = new Button();
			pnlTitle.SuspendLayout();
			panel1.SuspendLayout();
			SuspendLayout();
			// 
			// pnlTitle
			// 
			pnlTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			pnlTitle.BackColor = Color.DarkOrange;
			pnlTitle.Controls.Add(lblTopBar);
			pnlTitle.Location = new Point(0, 0);
			pnlTitle.Name = "pnlTitle";
			pnlTitle.Size = new Size(340, 30);
			pnlTitle.TabIndex = 2;
			// 
			// lblTopBar
			// 
			lblTopBar.AutoSize = true;
			lblTopBar.Font = new Font("Calibri", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
			lblTopBar.ForeColor = Color.White;
			lblTopBar.Location = new Point(6, 4);
			lblTopBar.Name = "lblTopBar";
			lblTopBar.Size = new Size(129, 23);
			lblTopBar.TabIndex = 5;
			lblTopBar.Text = "Manage quotes";
			// 
			// btnOk
			// 
			btnOk.FlatAppearance.BorderColor = Color.DimGray;
			btnOk.FlatStyle = FlatStyle.Flat;
			btnOk.Font = new Font("Calibri", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
			btnOk.Location = new Point(94, 101);
			btnOk.Name = "btnOk";
			btnOk.Size = new Size(114, 31);
			btnOk.TabIndex = 1;
			btnOk.Text = "ok";
			btnOk.UseVisualStyleBackColor = true;
			btnOk.Click += btnOk_Click;
			btnOk.KeyDown += btnOk_KeyDown;
			// 
			// txtMessage
			// 
			txtMessage.BackColor = Color.WhiteSmoke;
			txtMessage.BorderStyle = BorderStyle.None;
			txtMessage.Font = new Font("Calibri", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
			txtMessage.Location = new Point(12, 46);
			txtMessage.Multiline = true;
			txtMessage.Name = "txtMessage";
			txtMessage.Size = new Size(312, 75);
			txtMessage.TabIndex = 4;
			txtMessage.TabStop = false;
			txtMessage.Text = "A aparut un mesaj de atentionare ca sa stii ca s-a intamplat ceva anume";
			// 
			// panel1
			// 
			panel1.BackColor = Color.WhiteSmoke;
			panel1.Controls.Add(btnOk);
			panel1.Controls.Add(btnCancel);
			panel1.Location = new Point(4, 31);
			panel1.Name = "panel1";
			panel1.Size = new Size(331, 135);
			panel1.TabIndex = 5;
			// 
			// btnCancel
			// 
			btnCancel.FlatAppearance.BorderColor = Color.DimGray;
			btnCancel.FlatStyle = FlatStyle.Flat;
			btnCancel.Font = new Font("Calibri", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
			btnCancel.Location = new Point(214, 101);
			btnCancel.Name = "btnCancel";
			btnCancel.Size = new Size(114, 31);
			btnCancel.TabIndex = 2;
			btnCancel.Text = "cancel";
			btnCancel.UseVisualStyleBackColor = true;
			btnCancel.Click += btnCancel_Click;
			btnCancel.KeyDown += btnCancel_KeyDown;
			// 
			// DialogMessageForm
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.WhiteSmoke;
			ClientSize = new Size(340, 170);
			ControlBox = false;
			Controls.Add(pnlTitle);
			Controls.Add(txtMessage);
			Controls.Add(panel1);
			FormBorderStyle = FormBorderStyle.None;
			Icon = (Icon)resources.GetObject("$this.Icon");
			Name = "DialogMessageForm";
			SizeGripStyle = SizeGripStyle.Hide;
			StartPosition = FormStartPosition.CenterParent;
			Text = "DialogForm";
			TopMost = true;
			pnlTitle.ResumeLayout(false);
			pnlTitle.PerformLayout();
			panel1.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
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