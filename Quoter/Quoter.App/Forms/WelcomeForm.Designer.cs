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
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.btnLanguageFr = new System.Windows.Forms.Button();
			this.btnLanguageRo = new System.Windows.Forms.Button();
			this.btnLanguageEn = new System.Windows.Forms.Button();
			this.btnTab1Next = new System.Windows.Forms.Button();
			this.txtTab1WelcomeMsg = new System.Windows.Forms.TextBox();
			this.lblTab1Welcome = new System.Windows.Forms.Label();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.btnTab2Back = new System.Windows.Forms.Button();
			this.clbTab2ImportCollections = new System.Windows.Forms.CheckedListBox();
			this.lblTab2 = new System.Windows.Forms.Label();
			this.btnTab2Next = new System.Windows.Forms.Button();
			this.txtTab2Msg = new System.Windows.Forms.TextBox();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.btnTab3Back = new System.Windows.Forms.Button();
			this.btnTab3Often = new System.Windows.Forms.Button();
			this.btnTab3Rare = new System.Windows.Forms.Button();
			this.btnTab3Normal = new System.Windows.Forms.Button();
			this.btnTab3Next = new System.Windows.Forms.Button();
			this.lblTab3 = new System.Windows.Forms.Label();
			this.txtTab3Extra = new System.Windows.Forms.TextBox();
			this.txtTab3NotificationInterval = new System.Windows.Forms.TextBox();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.lblTab4 = new System.Windows.Forms.Label();
			this.btnTab4Finish = new System.Windows.Forms.Button();
			this.txtTab4Msg = new System.Windows.Forms.TextBox();
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
			this.pictureBox1.Location = new System.Drawing.Point(9, 6);
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
			this.lblTitle.Location = new System.Drawing.Point(48, 7);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(72, 26);
			this.lblTitle.TabIndex = 2;
			this.lblTitle.Text = "Quoter";
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
			this.btnClose.Location = new System.Drawing.Point(637, 0);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(46, 40);
			this.btnClose.TabIndex = 1;
			this.btnClose.Text = "X";
			this.btnClose.UseVisualStyleBackColor = false;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// tabControl
			// 
			this.tabControl.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
			this.tabControl.Controls.Add(this.tabPage1);
			this.tabControl.Controls.Add(this.tabPage2);
			this.tabControl.Controls.Add(this.tabPage3);
			this.tabControl.Controls.Add(this.tabPage4);
			this.tabControl.ItemSize = new System.Drawing.Size(50, 20);
			this.tabControl.Location = new System.Drawing.Point(0, 46);
			this.tabControl.Name = "tabControl";
			this.tabControl.Padding = new System.Drawing.Point(0, 0);
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(683, 360);
			this.tabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			this.tabControl.TabIndex = 4;
			this.tabControl.TabStop = false;
			// 
			// tabPage1
			// 
			this.tabPage1.BackColor = System.Drawing.Color.White;
			this.tabPage1.Controls.Add(this.btnLanguageFr);
			this.tabPage1.Controls.Add(this.btnLanguageRo);
			this.tabPage1.Controls.Add(this.btnLanguageEn);
			this.tabPage1.Controls.Add(this.btnTab1Next);
			this.tabPage1.Controls.Add(this.txtTab1WelcomeMsg);
			this.tabPage1.Controls.Add(this.lblTab1Welcome);
			this.tabPage1.Location = new System.Drawing.Point(4, 24);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(675, 332);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "tabPage1";
			// 
			// btnLanguageFr
			// 
			this.btnLanguageFr.BackgroundImage = global::Quoter.App.Resources.Resources.flag_france_64;
			this.btnLanguageFr.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.btnLanguageFr.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
			this.btnLanguageFr.FlatAppearance.BorderSize = 2;
			this.btnLanguageFr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnLanguageFr.Location = new System.Drawing.Point(365, 214);
			this.btnLanguageFr.Name = "btnLanguageFr";
			this.btnLanguageFr.Size = new System.Drawing.Size(50, 35);
			this.btnLanguageFr.TabIndex = 21;
			this.btnLanguageFr.UseVisualStyleBackColor = true;
			this.btnLanguageFr.Click += new System.EventHandler(this.btnLanguageFr_Click);
			// 
			// btnLanguageRo
			// 
			this.btnLanguageRo.BackgroundImage = global::Quoter.App.Resources.Resources.flag_romania_64;
			this.btnLanguageRo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.btnLanguageRo.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
			this.btnLanguageRo.FlatAppearance.BorderSize = 2;
			this.btnLanguageRo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnLanguageRo.Location = new System.Drawing.Point(309, 214);
			this.btnLanguageRo.Name = "btnLanguageRo";
			this.btnLanguageRo.Size = new System.Drawing.Size(50, 35);
			this.btnLanguageRo.TabIndex = 20;
			this.btnLanguageRo.UseVisualStyleBackColor = true;
			this.btnLanguageRo.Click += new System.EventHandler(this.btnLanguageRo_Click);
			// 
			// btnLanguageEn
			// 
			this.btnLanguageEn.BackgroundImage = global::Quoter.App.Resources.Resources.flag_uk_64;
			this.btnLanguageEn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.btnLanguageEn.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
			this.btnLanguageEn.FlatAppearance.BorderSize = 2;
			this.btnLanguageEn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnLanguageEn.Location = new System.Drawing.Point(253, 214);
			this.btnLanguageEn.Name = "btnLanguageEn";
			this.btnLanguageEn.Size = new System.Drawing.Size(50, 35);
			this.btnLanguageEn.TabIndex = 19;
			this.btnLanguageEn.UseVisualStyleBackColor = true;
			this.btnLanguageEn.Click += new System.EventHandler(this.btnLanguageEn_Click);
			// 
			// btnTab1Next
			// 
			this.btnTab1Next.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnTab1Next.Location = new System.Drawing.Point(560, 289);
			this.btnTab1Next.Name = "btnTab1Next";
			this.btnTab1Next.Size = new System.Drawing.Size(106, 41);
			this.btnTab1Next.TabIndex = 6;
			this.btnTab1Next.Text = "Next";
			this.btnTab1Next.UseVisualStyleBackColor = true;
			this.btnTab1Next.Click += new System.EventHandler(this.btnTab1Next_Click);
			// 
			// txtTab1WelcomeMsg
			// 
			this.txtTab1WelcomeMsg.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtTab1WelcomeMsg.Location = new System.Drawing.Point(118, 74);
			this.txtTab1WelcomeMsg.Multiline = true;
			this.txtTab1WelcomeMsg.Name = "txtTab1WelcomeMsg";
			this.txtTab1WelcomeMsg.Size = new System.Drawing.Size(434, 134);
			this.txtTab1WelcomeMsg.TabIndex = 5;
			this.txtTab1WelcomeMsg.Text = "Reminder allows you to receive\r\nnotifications about your favourite\r\nwritings. To " +
    "setup the application\r\npress continue below.";
			this.txtTab1WelcomeMsg.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// lblTab1Welcome
			// 
			this.lblTab1Welcome.Font = new System.Drawing.Font("Calibri Light", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblTab1Welcome.Location = new System.Drawing.Point(144, 3);
			this.lblTab1Welcome.Name = "lblTab1Welcome";
			this.lblTab1Welcome.Size = new System.Drawing.Size(388, 54);
			this.lblTab1Welcome.TabIndex = 2;
			this.lblTab1Welcome.Text = "Welcome";
			this.lblTab1Welcome.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// tabPage2
			// 
			this.tabPage2.BackColor = System.Drawing.Color.White;
			this.tabPage2.Controls.Add(this.btnTab2Back);
			this.tabPage2.Controls.Add(this.clbTab2ImportCollections);
			this.tabPage2.Controls.Add(this.lblTab2);
			this.tabPage2.Controls.Add(this.btnTab2Next);
			this.tabPage2.Controls.Add(this.txtTab2Msg);
			this.tabPage2.Location = new System.Drawing.Point(4, 24);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(675, 332);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "tabPage2";
			// 
			// btnTab2Back
			// 
			this.btnTab2Back.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnTab2Back.Location = new System.Drawing.Point(448, 289);
			this.btnTab2Back.Name = "btnTab2Back";
			this.btnTab2Back.Size = new System.Drawing.Size(106, 41);
			this.btnTab2Back.TabIndex = 12;
			this.btnTab2Back.Text = "Back";
			this.btnTab2Back.UseVisualStyleBackColor = true;
			this.btnTab2Back.Click += new System.EventHandler(this.btnTab2Back_Click);
			// 
			// clbTab2ImportCollections
			// 
			this.clbTab2ImportCollections.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.clbTab2ImportCollections.FormattingEnabled = true;
			this.clbTab2ImportCollections.Items.AddRange(new object[] {
            "The New Testament - The NKJV Translation"});
			this.clbTab2ImportCollections.Location = new System.Drawing.Point(125, 127);
			this.clbTab2ImportCollections.Name = "clbTab2ImportCollections";
			this.clbTab2ImportCollections.Size = new System.Drawing.Size(419, 108);
			this.clbTab2ImportCollections.TabIndex = 11;
			// 
			// lblTab2
			// 
			this.lblTab2.Font = new System.Drawing.Font("Calibri Light", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblTab2.Location = new System.Drawing.Point(143, 5);
			this.lblTab2.Name = "lblTab2";
			this.lblTab2.Size = new System.Drawing.Size(388, 45);
			this.lblTab2.TabIndex = 10;
			this.lblTab2.Text = "Welcome";
			this.lblTab2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnTab2Next
			// 
			this.btnTab2Next.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnTab2Next.Location = new System.Drawing.Point(560, 289);
			this.btnTab2Next.Name = "btnTab2Next";
			this.btnTab2Next.Size = new System.Drawing.Size(106, 41);
			this.btnTab2Next.TabIndex = 9;
			this.btnTab2Next.Text = "Next";
			this.btnTab2Next.UseVisualStyleBackColor = true;
			this.btnTab2Next.Click += new System.EventHandler(this.btnTab2Next_Click);
			// 
			// txtTab2Msg
			// 
			this.txtTab2Msg.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtTab2Msg.Location = new System.Drawing.Point(73, 53);
			this.txtTab2Msg.Multiline = true;
			this.txtTab2Msg.Name = "txtTab2Msg";
			this.txtTab2Msg.Size = new System.Drawing.Size(523, 63);
			this.txtTab2Msg.TabIndex = 8;
			this.txtTab2Msg.Text = resources.GetString("txtTab2Msg.Text");
			this.txtTab2Msg.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// tabPage3
			// 
			this.tabPage3.BackColor = System.Drawing.Color.White;
			this.tabPage3.Controls.Add(this.btnTab3Back);
			this.tabPage3.Controls.Add(this.btnTab3Often);
			this.tabPage3.Controls.Add(this.btnTab3Rare);
			this.tabPage3.Controls.Add(this.btnTab3Normal);
			this.tabPage3.Controls.Add(this.btnTab3Next);
			this.tabPage3.Controls.Add(this.lblTab3);
			this.tabPage3.Controls.Add(this.txtTab3Extra);
			this.tabPage3.Controls.Add(this.txtTab3NotificationInterval);
			this.tabPage3.Location = new System.Drawing.Point(4, 24);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage3.Size = new System.Drawing.Size(675, 332);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "tabPage3";
			// 
			// btnTab3Back
			// 
			this.btnTab3Back.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnTab3Back.Location = new System.Drawing.Point(448, 289);
			this.btnTab3Back.Name = "btnTab3Back";
			this.btnTab3Back.Size = new System.Drawing.Size(106, 41);
			this.btnTab3Back.TabIndex = 23;
			this.btnTab3Back.Text = "Back";
			this.btnTab3Back.UseVisualStyleBackColor = true;
			this.btnTab3Back.Click += new System.EventHandler(this.btnTab3Back_Click);
			// 
			// btnTab3Often
			// 
			this.btnTab3Often.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			this.btnTab3Often.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnTab3Often.Location = new System.Drawing.Point(77, 87);
			this.btnTab3Often.Name = "btnTab3Often";
			this.btnTab3Often.Size = new System.Drawing.Size(169, 54);
			this.btnTab3Often.TabIndex = 22;
			this.btnTab3Often.Text = "Often\r\n(every 15 mins)";
			this.btnTab3Often.UseVisualStyleBackColor = true;
			this.btnTab3Often.Click += new System.EventHandler(this.btnTab3Often_Click);
			// 
			// btnTab3Rare
			// 
			this.btnTab3Rare.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			this.btnTab3Rare.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnTab3Rare.Location = new System.Drawing.Point(427, 87);
			this.btnTab3Rare.Name = "btnTab3Rare";
			this.btnTab3Rare.Size = new System.Drawing.Size(169, 54);
			this.btnTab3Rare.TabIndex = 21;
			this.btnTab3Rare.Text = "Rare\r\n(every 15 mins)";
			this.btnTab3Rare.UseVisualStyleBackColor = true;
			this.btnTab3Rare.Click += new System.EventHandler(this.btnTab3Rare_Click);
			// 
			// btnTab3Normal
			// 
			this.btnTab3Normal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnTab3Normal.Location = new System.Drawing.Point(252, 87);
			this.btnTab3Normal.Name = "btnTab3Normal";
			this.btnTab3Normal.Size = new System.Drawing.Size(169, 54);
			this.btnTab3Normal.TabIndex = 20;
			this.btnTab3Normal.Text = "Regular\r\n(every 15 mins)";
			this.btnTab3Normal.UseVisualStyleBackColor = true;
			this.btnTab3Normal.Click += new System.EventHandler(this.btnTab3Normal_Click);
			// 
			// btnTab3Next
			// 
			this.btnTab3Next.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnTab3Next.Location = new System.Drawing.Point(560, 289);
			this.btnTab3Next.Name = "btnTab3Next";
			this.btnTab3Next.Size = new System.Drawing.Size(106, 41);
			this.btnTab3Next.TabIndex = 18;
			this.btnTab3Next.Text = "Next";
			this.btnTab3Next.UseVisualStyleBackColor = true;
			this.btnTab3Next.Click += new System.EventHandler(this.btnTab3Next_Click);
			// 
			// lblTab3
			// 
			this.lblTab3.Font = new System.Drawing.Font("Calibri Light", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblTab3.Location = new System.Drawing.Point(143, 5);
			this.lblTab3.Name = "lblTab3";
			this.lblTab3.Size = new System.Drawing.Size(388, 45);
			this.lblTab3.TabIndex = 17;
			this.lblTab3.Text = "Welcome";
			this.lblTab3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// txtTab3Extra
			// 
			this.txtTab3Extra.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtTab3Extra.Font = new System.Drawing.Font("Calibri Light", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.txtTab3Extra.Location = new System.Drawing.Point(77, 147);
			this.txtTab3Extra.Multiline = true;
			this.txtTab3Extra.Name = "txtTab3Extra";
			this.txtTab3Extra.Size = new System.Drawing.Size(528, 28);
			this.txtTab3Extra.TabIndex = 16;
			this.txtTab3Extra.Text = "You can customize this setting later from the settings menu.\r\n";
			this.txtTab3Extra.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// txtTab3NotificationInterval
			// 
			this.txtTab3NotificationInterval.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtTab3NotificationInterval.Location = new System.Drawing.Point(70, 53);
			this.txtTab3NotificationInterval.Multiline = true;
			this.txtTab3NotificationInterval.Name = "txtTab3NotificationInterval";
			this.txtTab3NotificationInterval.Size = new System.Drawing.Size(528, 28);
			this.txtTab3NotificationInterval.TabIndex = 15;
			this.txtTab3NotificationInterval.Text = "How often would you like the notifications to appear?\r\n";
			this.txtTab3NotificationInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// tabPage4
			// 
			this.tabPage4.BackColor = System.Drawing.Color.White;
			this.tabPage4.Controls.Add(this.lblTab4);
			this.tabPage4.Controls.Add(this.btnTab4Finish);
			this.tabPage4.Controls.Add(this.txtTab4Msg);
			this.tabPage4.Location = new System.Drawing.Point(4, 24);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage4.Size = new System.Drawing.Size(675, 332);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "tabPage4";
			// 
			// lblTab4
			// 
			this.lblTab4.Font = new System.Drawing.Font("Calibri Light", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblTab4.Location = new System.Drawing.Point(143, 5);
			this.lblTab4.Name = "lblTab4";
			this.lblTab4.Size = new System.Drawing.Size(388, 45);
			this.lblTab4.TabIndex = 20;
			this.lblTab4.Text = "Welcome";
			this.lblTab4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnTab4Finish
			// 
			this.btnTab4Finish.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnTab4Finish.Location = new System.Drawing.Point(560, 289);
			this.btnTab4Finish.Name = "btnTab4Finish";
			this.btnTab4Finish.Size = new System.Drawing.Size(106, 41);
			this.btnTab4Finish.TabIndex = 19;
			this.btnTab4Finish.Text = "Finish";
			this.btnTab4Finish.UseVisualStyleBackColor = true;
			this.btnTab4Finish.Click += new System.EventHandler(this.btnTab4Finish_Click);
			// 
			// txtTab4Msg
			// 
			this.txtTab4Msg.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtTab4Msg.Location = new System.Drawing.Point(73, 66);
			this.txtTab4Msg.Multiline = true;
			this.txtTab4Msg.Name = "txtTab4Msg";
			this.txtTab4Msg.Size = new System.Drawing.Size(528, 94);
			this.txtTab4Msg.TabIndex = 16;
			this.txtTab4Msg.Text = resources.GetString("txtTab4Msg.Text");
			this.txtTab4Msg.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// WelcomeForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.WhiteSmoke;
			this.ClientSize = new System.Drawing.Size(682, 406);
			this.ControlBox = false;
			this.Controls.Add(this.tabControl);
			this.Controls.Add(this.pnlTitle);
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
		private Label lblTab1Welcome;
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
		private Button btnContinueToTab4;
		private TabPage tabPage4;
		private TextBox txtTab1WelcomeMsg;
		private TextBox txtTab2Msg;
		private TextBox txtTab3Extra;
		private TextBox txtTab3NotificationInterval;
		private TextBox txtTab4Msg;
		private Button btnTab1Next;
		private Button btnTab2Next;
		private Button btnLanguageFr;
		private Button btnLanguageRo;
		private Button btnLanguageEn;
		private CheckedListBox clbTab2ImportCollections;
		private Label lblTab2;
		private Button btnTab3Often;
		private Button btnTab3Rare;
		private Button btnTab3Normal;
		private Button btnTab3Next;
		private Label lblTab3;
		private Button btnTab4Finish;
		private Label lblTab4;
		private Button btnTab2Back;
		private Button btnTab3Back;
	}
}