namespace Quoter.App.Forms
{
	partial class DialogExportFinishedForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogExportFinishedForm));
			pnlTitle = new Panel();
			lblTitle = new Label();
			btnOk = new Button();
			btnOpenExplorer = new Button();
			txtMessage = new TextBox();
			panel1 = new Panel();
			pnlTitle.SuspendLayout();
			panel1.SuspendLayout();
			SuspendLayout();
			// 
			// pnlTitle
			// 
			pnlTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			pnlTitle.BackColor = Color.SlateGray;
			pnlTitle.Controls.Add(lblTitle);
			pnlTitle.Location = new Point(0, 0);
			pnlTitle.Name = "pnlTitle";
			pnlTitle.Size = new Size(341, 30);
			pnlTitle.TabIndex = 3;
			// 
			// lblTitle
			// 
			lblTitle.AutoSize = true;
			lblTitle.Font = new Font("Calibri", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
			lblTitle.ForeColor = Color.White;
			lblTitle.Location = new Point(4, 3);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new Size(129, 23);
			lblTitle.TabIndex = 2;
			lblTitle.Text = "Manage quotes";
			// 
			// btnOk
			// 
			btnOk.FlatAppearance.BorderColor = Color.DimGray;
			btnOk.FlatStyle = FlatStyle.Flat;
			btnOk.Location = new Point(238, 131);
			btnOk.Name = "btnOk";
			btnOk.Size = new Size(90, 30);
			btnOk.TabIndex = 3;
			btnOk.Text = "Ok";
			btnOk.UseVisualStyleBackColor = true;
			btnOk.Click += btnOk_Click;
			// 
			// btnOpenExplorer
			// 
			btnOpenExplorer.FlatAppearance.BorderColor = Color.DimGray;
			btnOpenExplorer.FlatStyle = FlatStyle.Flat;
			btnOpenExplorer.Location = new Point(115, 131);
			btnOpenExplorer.Name = "btnOpenExplorer";
			btnOpenExplorer.Size = new Size(117, 30);
			btnOpenExplorer.TabIndex = 2;
			btnOpenExplorer.Text = "Open folder";
			btnOpenExplorer.UseVisualStyleBackColor = true;
			btnOpenExplorer.Click += btnOpenExplorer_Click;
			// 
			// txtMessage
			// 
			txtMessage.BorderStyle = BorderStyle.None;
			txtMessage.Location = new Point(10, 5);
			txtMessage.Multiline = true;
			txtMessage.Name = "txtMessage";
			txtMessage.Size = new Size(316, 32);
			txtMessage.TabIndex = 8;
			txtMessage.Text = "Introduceti mai jos ceva nume ca sa stim ce facem mai departe";
			// 
			// panel1
			// 
			panel1.BackColor = Color.White;
			panel1.Controls.Add(txtMessage);
			panel1.Location = new Point(2, 31);
			panel1.Name = "panel1";
			panel1.Size = new Size(336, 137);
			panel1.TabIndex = 10;
			// 
			// DialogExportFinishedForm
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.WhiteSmoke;
			ClientSize = new Size(340, 170);
			ControlBox = false;
			Controls.Add(btnOpenExplorer);
			Controls.Add(btnOk);
			Controls.Add(pnlTitle);
			Controls.Add(panel1);
			Font = new Font("Calibri", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
			FormBorderStyle = FormBorderStyle.None;
			Icon = (Icon)resources.GetObject("$this.Icon");
			Name = "DialogExportFinishedForm";
			StartPosition = FormStartPosition.CenterParent;
			Text = "DialogExportFinishedForm";
			TopMost = true;
			pnlTitle.ResumeLayout(false);
			pnlTitle.PerformLayout();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private Panel pnlTitle;
		private Label lblTitle;
		private Button btnOpenExplorer;
		private Button btnOk;
		private TextBox txtMessage;
		private Panel panel1;
	}
}