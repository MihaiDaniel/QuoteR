namespace Quoter.App
{
	partial class SettingsForm
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
			this.pnlTitle = new System.Windows.Forms.Panel();
			this.lblTopBar = new System.Windows.Forms.Label();
			this.btnClose = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.txtFolder = new System.Windows.Forms.TextBox();
			this.lblFolder = new System.Windows.Forms.Label();
			this.btnBrowse = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.pnlTitle.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlTitle
			// 
			this.pnlTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlTitle.BackColor = System.Drawing.Color.SlateGray;
			this.pnlTitle.Controls.Add(this.lblTopBar);
			this.pnlTitle.Controls.Add(this.btnClose);
			this.pnlTitle.Location = new System.Drawing.Point(0, 0);
			this.pnlTitle.Name = "pnlTitle";
			this.pnlTitle.Size = new System.Drawing.Size(400, 40);
			this.pnlTitle.TabIndex = 0;
			// 
			// lblTopBar
			// 
			this.lblTopBar.AutoSize = true;
			this.lblTopBar.Font = new System.Drawing.Font("Calibri Light", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblTopBar.ForeColor = System.Drawing.Color.White;
			this.lblTopBar.Location = new System.Drawing.Point(12, 7);
			this.lblTopBar.Name = "lblTopBar";
			this.lblTopBar.Size = new System.Drawing.Size(79, 26);
			this.lblTopBar.TabIndex = 2;
			this.lblTopBar.Text = "Settings";
			// 
			// btnClose
			// 
			this.btnClose.BackColor = System.Drawing.Color.Transparent;
			this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.btnClose.FlatAppearance.BorderSize = 0;
			this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
			this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnClose.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			this.btnClose.ForeColor = System.Drawing.Color.White;
			this.btnClose.Location = new System.Drawing.Point(351, 0);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(46, 40);
			this.btnClose.TabIndex = 1;
			this.btnClose.Text = "X";
			this.btnClose.UseVisualStyleBackColor = false;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(222, 63);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "button1";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click_1);
			// 
			// txtFolder
			// 
			this.txtFolder.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.txtFolder.Location = new System.Drawing.Point(12, 161);
			this.txtFolder.Name = "txtFolder";
			this.txtFolder.Size = new System.Drawing.Size(333, 27);
			this.txtFolder.TabIndex = 2;
			// 
			// lblFolder
			// 
			this.lblFolder.AutoSize = true;
			this.lblFolder.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblFolder.Location = new System.Drawing.Point(12, 139);
			this.lblFolder.Name = "lblFolder";
			this.lblFolder.Size = new System.Drawing.Size(111, 19);
			this.lblFolder.TabIndex = 3;
			this.lblFolder.Text = "Folder scanned:";
			// 
			// btnBrowse
			// 
			this.btnBrowse.BackgroundImage = global::Quoter.App.Resources.Resources.folder_96;
			this.btnBrowse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.btnBrowse.FlatAppearance.BorderSize = 0;
			this.btnBrowse.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
			this.btnBrowse.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
			this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnBrowse.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			this.btnBrowse.Location = new System.Drawing.Point(351, 153);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new System.Drawing.Size(40, 40);
			this.btnBrowse.TabIndex = 4;
			this.btnBrowse.UseVisualStyleBackColor = true;
			this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(222, 92);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 5;
			this.button2.Text = "button2";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(58, 63);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(75, 23);
			this.button3.TabIndex = 6;
			this.button3.Text = "button3";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(58, 98);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(100, 23);
			this.textBox1.TabIndex = 7;
			// 
			// SettingsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(400, 200);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.btnBrowse);
			this.Controls.Add(this.lblFolder);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.txtFolder);
			this.Controls.Add(this.pnlTitle);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "SettingsForm";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.SettingsForm_Load);
			this.pnlTitle.ResumeLayout(false);
			this.pnlTitle.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Panel pnlTitle;
		private Button btnClose;
		private Label lblTopBar;
		private Button button1;
		private TextBox txtFolder;
		private Label lblFolder;
		private Button btnBrowse;
		private Button button2;
		private Button button3;
		private TextBox textBox1;
	}
}