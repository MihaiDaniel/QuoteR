namespace Quoter.App.Forms
{
	partial class WelcomeForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WelcomeForm));
			this.pnlTitle = new System.Windows.Forms.Panel();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.lblTitle = new System.Windows.Forms.Label();
			this.btnClose = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.lblPage1Welcome = new System.Windows.Forms.Label();
			this.btnContinueToTab2 = new System.Windows.Forms.Button();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.label3 = new System.Windows.Forms.Label();
			this.btnContinueToTab3 = new System.Windows.Forms.Button();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.btnFolder = new System.Windows.Forms.Button();
			this.txtFolder = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.rbOften = new System.Windows.Forms.RadioButton();
			this.rbRegular = new System.Windows.Forms.RadioButton();
			this.rbRare = new System.Windows.Forms.RadioButton();
			this.btnContinueToTab4 = new System.Windows.Forms.Button();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.label6 = new System.Windows.Forms.Label();
			this.btnFinishSetup = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.textBox5 = new System.Windows.Forms.TextBox();
			this.pnlTitle.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.tabControl.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.tabPage4.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlTitle
			// 
			this.pnlTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlTitle.BackColor = System.Drawing.Color.SlateGray;
			this.pnlTitle.Controls.Add(this.pictureBox1);
			this.pnlTitle.Controls.Add(this.lblTitle);
			this.pnlTitle.Controls.Add(this.btnClose);
			this.pnlTitle.Location = new System.Drawing.Point(0, 0);
			this.pnlTitle.Name = "pnlTitle";
			this.pnlTitle.Size = new System.Drawing.Size(683, 40);
			this.pnlTitle.TabIndex = 1;
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackgroundImage = global::Quoter.App.Resources.Resources.book_96;
			this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.pictureBox1.Location = new System.Drawing.Point(22, 6);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(31, 28);
			this.pictureBox1.TabIndex = 3;
			this.pictureBox1.TabStop = false;
			// 
			// lblTitle
			// 
			this.lblTitle.AutoSize = true;
			this.lblTitle.Font = new System.Drawing.Font("Calibri Light", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblTitle.ForeColor = System.Drawing.Color.White;
			this.lblTitle.Location = new System.Drawing.Point(68, 5);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(94, 26);
			this.lblTitle.TabIndex = 2;
			this.lblTitle.Text = "Reminder";
			// 
			// btnClose
			// 
			this.btnClose.BackColor = System.Drawing.Color.Transparent;
			this.btnClose.BackgroundImage = global::Quoter.App.Resources.Resources.icons8_close_50;
			this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.btnClose.FlatAppearance.BorderSize = 0;
			this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
			this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnClose.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			this.btnClose.ForeColor = System.Drawing.Color.White;
			this.btnClose.Location = new System.Drawing.Point(637, 0);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(46, 40);
			this.btnClose.TabIndex = 1;
			this.btnClose.UseVisualStyleBackColor = false;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.SlateGray;
			this.panel1.Location = new System.Drawing.Point(14, 36);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(44, 372);
			this.panel1.TabIndex = 2;
			// 
			// tabControl
			// 
			this.tabControl.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
			this.tabControl.Controls.Add(this.tabPage1);
			this.tabControl.Controls.Add(this.tabPage2);
			this.tabControl.Controls.Add(this.tabPage3);
			this.tabControl.Controls.Add(this.tabPage4);
			this.tabControl.ItemSize = new System.Drawing.Size(50, 20);
			this.tabControl.Location = new System.Drawing.Point(64, 46);
			this.tabControl.Name = "tabControl";
			this.tabControl.Padding = new System.Drawing.Point(0, 0);
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(606, 348);
			this.tabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			this.tabControl.TabIndex = 4;
			this.tabControl.TabStop = false;
			// 
			// tabPage1
			// 
			this.tabPage1.BackColor = System.Drawing.Color.White;
			this.tabPage1.Controls.Add(this.textBox1);
			this.tabPage1.Controls.Add(this.lblPage1Welcome);
			this.tabPage1.Controls.Add(this.btnContinueToTab2);
			this.tabPage1.Location = new System.Drawing.Point(4, 24);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(598, 320);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "tabPage1";
			// 
			// lblPage1Welcome
			// 
			this.lblPage1Welcome.AutoSize = true;
			this.lblPage1Welcome.Font = new System.Drawing.Font("Calibri Light", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblPage1Welcome.Location = new System.Drawing.Point(220, 30);
			this.lblPage1Welcome.Name = "lblPage1Welcome";
			this.lblPage1Welcome.Size = new System.Drawing.Size(151, 42);
			this.lblPage1Welcome.TabIndex = 2;
			this.lblPage1Welcome.Text = "Welcome";
			// 
			// btnContinueToTab2
			// 
			this.btnContinueToTab2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(0)))));
			this.btnContinueToTab2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
			this.btnContinueToTab2.FlatAppearance.BorderSize = 2;
			this.btnContinueToTab2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(220)))), ((int)(((byte)(0)))));
			this.btnContinueToTab2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(208)))), ((int)(((byte)(0)))));
			this.btnContinueToTab2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnContinueToTab2.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnContinueToTab2.ForeColor = System.Drawing.Color.White;
			this.btnContinueToTab2.Location = new System.Drawing.Point(230, 260);
			this.btnContinueToTab2.Name = "btnContinueToTab2";
			this.btnContinueToTab2.Size = new System.Drawing.Size(115, 41);
			this.btnContinueToTab2.TabIndex = 0;
			this.btnContinueToTab2.Text = "CONTINUE";
			this.btnContinueToTab2.UseVisualStyleBackColor = false;
			this.btnContinueToTab2.Click += new System.EventHandler(this.btnContinueToTab2_Click);
			// 
			// tabPage2
			// 
			this.tabPage2.BackColor = System.Drawing.Color.White;
			this.tabPage2.Controls.Add(this.btnFolder);
			this.tabPage2.Controls.Add(this.textBox2);
			this.tabPage2.Controls.Add(this.txtFolder);
			this.tabPage2.Controls.Add(this.btnContinueToTab3);
			this.tabPage2.Controls.Add(this.label3);
			this.tabPage2.Location = new System.Drawing.Point(4, 24);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(598, 320);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "tabPage2";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Calibri Light", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label3.Location = new System.Drawing.Point(121, 16);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(336, 42);
			this.label3.TabIndex = 3;
			this.label3.Text = "Select a working folder";
			// 
			// btnContinueToTab3
			// 
			this.btnContinueToTab3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(0)))));
			this.btnContinueToTab3.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
			this.btnContinueToTab3.FlatAppearance.BorderSize = 2;
			this.btnContinueToTab3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(220)))), ((int)(((byte)(0)))));
			this.btnContinueToTab3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(208)))), ((int)(((byte)(0)))));
			this.btnContinueToTab3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnContinueToTab3.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnContinueToTab3.ForeColor = System.Drawing.Color.White;
			this.btnContinueToTab3.Location = new System.Drawing.Point(230, 260);
			this.btnContinueToTab3.Name = "btnContinueToTab3";
			this.btnContinueToTab3.Size = new System.Drawing.Size(115, 41);
			this.btnContinueToTab3.TabIndex = 4;
			this.btnContinueToTab3.Text = "CONTINUE";
			this.btnContinueToTab3.UseVisualStyleBackColor = false;
			this.btnContinueToTab3.Click += new System.EventHandler(this.btnContinueToTab3_Click);
			// 
			// tabPage3
			// 
			this.tabPage3.BackColor = System.Drawing.Color.White;
			this.tabPage3.Controls.Add(this.textBox4);
			this.tabPage3.Controls.Add(this.textBox3);
			this.tabPage3.Controls.Add(this.btnContinueToTab4);
			this.tabPage3.Controls.Add(this.rbRare);
			this.tabPage3.Controls.Add(this.rbRegular);
			this.tabPage3.Controls.Add(this.rbOften);
			this.tabPage3.Controls.Add(this.label2);
			this.tabPage3.Location = new System.Drawing.Point(4, 24);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage3.Size = new System.Drawing.Size(598, 320);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "tabPage3";
			// 
			// btnFolder
			// 
			this.btnFolder.BackgroundImage = global::Quoter.App.Resources.Resources.folder_96;
			this.btnFolder.FlatAppearance.BorderSize = 0;
			this.btnFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnFolder.Location = new System.Drawing.Point(239, 123);
			this.btnFolder.Name = "btnFolder";
			this.btnFolder.Size = new System.Drawing.Size(94, 92);
			this.btnFolder.TabIndex = 5;
			this.btnFolder.UseVisualStyleBackColor = true;
			// 
			// txtFolder
			// 
			this.txtFolder.Location = new System.Drawing.Point(112, 221);
			this.txtFolder.Name = "txtFolder";
			this.txtFolder.Size = new System.Drawing.Size(363, 27);
			this.txtFolder.TabIndex = 6;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Calibri Light", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label2.Location = new System.Drawing.Point(189, 18);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(197, 42);
			this.label2.TabIndex = 4;
			this.label2.Text = "Almost done";
			// 
			// rbOften
			// 
			this.rbOften.AutoSize = true;
			this.rbOften.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
			this.rbOften.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.rbOften.Location = new System.Drawing.Point(77, 120);
			this.rbOften.Name = "rbOften";
			this.rbOften.Size = new System.Drawing.Size(124, 55);
			this.rbOften.TabIndex = 10;
			this.rbOften.Text = "Often\r\nEvery 15 minutes";
			this.rbOften.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.rbOften.UseVisualStyleBackColor = true;
			this.rbOften.CheckedChanged += new System.EventHandler(this.rbOften_CheckedChanged);
			// 
			// rbRegular
			// 
			this.rbRegular.AutoSize = true;
			this.rbRegular.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
			this.rbRegular.Checked = true;
			this.rbRegular.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.rbRegular.Location = new System.Drawing.Point(230, 120);
			this.rbRegular.Name = "rbRegular";
			this.rbRegular.Size = new System.Drawing.Size(124, 55);
			this.rbRegular.TabIndex = 11;
			this.rbRegular.TabStop = true;
			this.rbRegular.Text = "Regular\r\nEvery 30 minutes";
			this.rbRegular.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.rbRegular.UseVisualStyleBackColor = true;
			this.rbRegular.CheckedChanged += new System.EventHandler(this.rbRegular_CheckedChanged);
			// 
			// rbRare
			// 
			this.rbRare.AutoSize = true;
			this.rbRare.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
			this.rbRare.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.rbRare.Location = new System.Drawing.Point(394, 120);
			this.rbRare.Name = "rbRare";
			this.rbRare.Size = new System.Drawing.Size(93, 55);
			this.rbRare.TabIndex = 12;
			this.rbRare.Text = "Rare\r\nEvery 1 hour";
			this.rbRare.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.rbRare.UseVisualStyleBackColor = true;
			this.rbRare.CheckedChanged += new System.EventHandler(this.rbRare_CheckedChanged);
			// 
			// btnContinueToTab4
			// 
			this.btnContinueToTab4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(0)))));
			this.btnContinueToTab4.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
			this.btnContinueToTab4.FlatAppearance.BorderSize = 2;
			this.btnContinueToTab4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(220)))), ((int)(((byte)(0)))));
			this.btnContinueToTab4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(208)))), ((int)(((byte)(0)))));
			this.btnContinueToTab4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnContinueToTab4.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnContinueToTab4.ForeColor = System.Drawing.Color.White;
			this.btnContinueToTab4.Location = new System.Drawing.Point(230, 260);
			this.btnContinueToTab4.Name = "btnContinueToTab4";
			this.btnContinueToTab4.Size = new System.Drawing.Size(115, 41);
			this.btnContinueToTab4.TabIndex = 13;
			this.btnContinueToTab4.Text = "CONTINUE";
			this.btnContinueToTab4.UseVisualStyleBackColor = false;
			this.btnContinueToTab4.Click += new System.EventHandler(this.btnContinueToTab4_Click);
			// 
			// tabPage4
			// 
			this.tabPage4.BackColor = System.Drawing.Color.White;
			this.tabPage4.Controls.Add(this.textBox5);
			this.tabPage4.Controls.Add(this.btnFinishSetup);
			this.tabPage4.Controls.Add(this.label6);
			this.tabPage4.Location = new System.Drawing.Point(4, 24);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage4.Size = new System.Drawing.Size(598, 320);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "tabPage4";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Calibri Light", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label6.Location = new System.Drawing.Point(221, 25);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(135, 42);
			this.label6.TabIndex = 5;
			this.label6.Text = "Finished";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnFinishSetup
			// 
			this.btnFinishSetup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(0)))));
			this.btnFinishSetup.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
			this.btnFinishSetup.FlatAppearance.BorderSize = 2;
			this.btnFinishSetup.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(220)))), ((int)(((byte)(0)))));
			this.btnFinishSetup.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(208)))), ((int)(((byte)(0)))));
			this.btnFinishSetup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnFinishSetup.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnFinishSetup.ForeColor = System.Drawing.Color.White;
			this.btnFinishSetup.Location = new System.Drawing.Point(230, 260);
			this.btnFinishSetup.Name = "btnFinishSetup";
			this.btnFinishSetup.Size = new System.Drawing.Size(115, 41);
			this.btnFinishSetup.TabIndex = 14;
			this.btnFinishSetup.Text = "CLOSE";
			this.btnFinishSetup.UseVisualStyleBackColor = false;
			this.btnFinishSetup.Click += new System.EventHandler(this.btnFinishSetup_Click);
			// 
			// textBox1
			// 
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox1.Location = new System.Drawing.Point(144, 86);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(307, 96);
			this.textBox1.TabIndex = 5;
			this.textBox1.Text = "Reminder allows you to receive\r\nnotifications about your favourite\r\nwritings. To " +
    "setup the application\r\npress continue below.";
			this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// textBox2
			// 
			this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox2.Location = new System.Drawing.Point(33, 61);
			this.textBox2.Multiline = true;
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(523, 96);
			this.textBox2.TabIndex = 8;
			this.textBox2.Text = resources.GetString("textBox2.Text");
			this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// textBox3
			// 
			this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox3.Location = new System.Drawing.Point(34, 73);
			this.textBox3.Multiline = true;
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(528, 28);
			this.textBox3.TabIndex = 15;
			this.textBox3.Text = "How often would you like the notifications to appear?\r\n";
			this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// textBox4
			// 
			this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox4.Font = new System.Drawing.Font("Calibri Light", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.textBox4.Location = new System.Drawing.Point(34, 198);
			this.textBox4.Multiline = true;
			this.textBox4.Name = "textBox4";
			this.textBox4.Size = new System.Drawing.Size(528, 28);
			this.textBox4.TabIndex = 16;
			this.textBox4.Text = "You can customize this setting later from the settings menu.\r\n";
			this.textBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// textBox5
			// 
			this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox5.Location = new System.Drawing.Point(37, 70);
			this.textBox5.Multiline = true;
			this.textBox5.Name = "textBox5";
			this.textBox5.Size = new System.Drawing.Size(528, 94);
			this.textBox5.TabIndex = 16;
			this.textBox5.Text = resources.GetString("textBox5.Text");
			this.textBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// WelcomeForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(682, 406);
			this.ControlBox = false;
			this.Controls.Add(this.tabControl);
			this.Controls.Add(this.pnlTitle);
			this.Controls.Add(this.panel1);
			this.Font = new System.Drawing.Font("Calibri Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "WelcomeForm";
			this.ShowIcon = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Reminder";
			this.Load += new System.EventHandler(this.WelcomeForm_Load);
			this.pnlTitle.ResumeLayout(false);
			this.pnlTitle.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.tabControl.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			this.tabPage3.ResumeLayout(false);
			this.tabPage3.PerformLayout();
			this.tabPage4.ResumeLayout(false);
			this.tabPage4.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private Panel pnlTitle;
		private Label lblTitle;
		private Button btnClose;
		private Panel panel1;
		private TabControl tabControl;
		private TabPage tabPage1;
		private TabPage tabPage2;
		private Label lblPage1Welcome;
		private Button btnContinueToTab2;
		private PictureBox pictureBox1;
		private Button btnContinueToTab3;
		private Label label3;
		private TabPage tabPage3;
		private TextBox txtFolder;
		private Button btnFolder;
		private RadioButton rbRare;
		private RadioButton rbRegular;
		private RadioButton rbOften;
		private Label label2;
		private Button btnContinueToTab4;
		private TabPage tabPage4;
		private Button btnFinishSetup;
		private Label label6;
		private TextBox textBox1;
		private TextBox textBox2;
		private TextBox textBox4;
		private TextBox textBox3;
		private TextBox textBox5;
	}
}