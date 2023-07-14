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
			pnlTitle = new Panel();
			pictureBox1 = new PictureBox();
			lblTitle = new Label();
			btnClose = new Button();
			tabControl = new TabControl();
			tabPage1 = new TabPage();
			label1 = new Label();
			btnLanguageFr = new Button();
			btnLanguageRo = new Button();
			btnLanguageEn = new Button();
			btnTab1Next = new Button();
			txtTab1WelcomeMsg = new TextBox();
			lblTab1Welcome = new Label();
			tabPage2 = new TabPage();
			label2 = new Label();
			btnTab2Back = new Button();
			clbTab2ImportCollections = new CheckedListBox();
			lblTab2 = new Label();
			btnTab2Next = new Button();
			txtTab2Msg = new TextBox();
			tabPage3 = new TabPage();
			label3 = new Label();
			btnTab3Back = new Button();
			btnTab3Often = new Button();
			btnTab3Rare = new Button();
			btnTab3Normal = new Button();
			btnTab3Next = new Button();
			lblTab3 = new Label();
			txtTab3Extra = new TextBox();
			txtTab3NotificationInterval = new TextBox();
			tabPage4 = new TabPage();
			label4 = new Label();
			lblTab4 = new Label();
			btnTab4Finish = new Button();
			txtTab4Msg = new TextBox();
			pnlTitle.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			tabControl.SuspendLayout();
			tabPage1.SuspendLayout();
			tabPage2.SuspendLayout();
			tabPage3.SuspendLayout();
			tabPage4.SuspendLayout();
			SuspendLayout();
			// 
			// pnlTitle
			// 
			pnlTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			pnlTitle.BackColor = Color.SlateGray;
			pnlTitle.Controls.Add(pictureBox1);
			pnlTitle.Controls.Add(lblTitle);
			pnlTitle.Controls.Add(btnClose);
			pnlTitle.Location = new Point(0, 0);
			pnlTitle.Name = "pnlTitle";
			pnlTitle.Size = new Size(683, 30);
			pnlTitle.TabIndex = 1;
			// 
			// pictureBox1
			// 
			pictureBox1.BackgroundImage = Resources.Resources.book_96;
			pictureBox1.BackgroundImageLayout = ImageLayout.Zoom;
			pictureBox1.Location = new Point(9, 6);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new Size(25, 20);
			pictureBox1.TabIndex = 3;
			pictureBox1.TabStop = false;
			// 
			// lblTitle
			// 
			lblTitle.AutoSize = true;
			lblTitle.Font = new Font("Calibri", 12F, FontStyle.Regular, GraphicsUnit.Point);
			lblTitle.ForeColor = Color.White;
			lblTitle.Location = new Point(40, 7);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new Size(54, 19);
			lblTitle.TabIndex = 2;
			lblTitle.Text = "Quoter";
			// 
			// btnClose
			// 
			btnClose.BackColor = Color.Transparent;
			btnClose.BackgroundImageLayout = ImageLayout.Zoom;
			btnClose.FlatAppearance.BorderSize = 0;
			btnClose.FlatAppearance.MouseDownBackColor = Color.FromArgb(225, 40, 40);
			btnClose.FlatAppearance.MouseOverBackColor = Color.FromArgb(225, 80, 80);
			btnClose.FlatStyle = FlatStyle.Flat;
			btnClose.Font = new Font("Calibri", 12F, FontStyle.Regular, GraphicsUnit.Point);
			btnClose.ForeColor = Color.White;
			btnClose.Location = new Point(637, 0);
			btnClose.Name = "btnClose";
			btnClose.Size = new Size(46, 34);
			btnClose.TabIndex = 1;
			btnClose.Text = "✕";
			btnClose.UseVisualStyleBackColor = false;
			btnClose.Click += btnClose_Click;
			// 
			// tabControl
			// 
			tabControl.Appearance = TabAppearance.FlatButtons;
			tabControl.Controls.Add(tabPage1);
			tabControl.Controls.Add(tabPage2);
			tabControl.Controls.Add(tabPage3);
			tabControl.Controls.Add(tabPage4);
			tabControl.ItemSize = new Size(50, 20);
			tabControl.Location = new Point(0, 32);
			tabControl.Name = "tabControl";
			tabControl.Padding = new Point(0, 0);
			tabControl.SelectedIndex = 0;
			tabControl.Size = new Size(683, 374);
			tabControl.SizeMode = TabSizeMode.Fixed;
			tabControl.TabIndex = 4;
			tabControl.TabStop = false;
			// 
			// tabPage1
			// 
			tabPage1.BackColor = Color.FromArgb(250, 250, 250);
			tabPage1.Controls.Add(label1);
			tabPage1.Controls.Add(btnLanguageFr);
			tabPage1.Controls.Add(btnLanguageRo);
			tabPage1.Controls.Add(btnLanguageEn);
			tabPage1.Controls.Add(btnTab1Next);
			tabPage1.Controls.Add(txtTab1WelcomeMsg);
			tabPage1.Controls.Add(lblTab1Welcome);
			tabPage1.Location = new Point(4, 24);
			tabPage1.Name = "tabPage1";
			tabPage1.Padding = new Padding(3);
			tabPage1.Size = new Size(675, 346);
			tabPage1.TabIndex = 0;
			tabPage1.Text = "tabPage1";
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new Font("Constantia", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
			label1.Location = new Point(300, 310);
			label1.Name = "label1";
			label1.Size = new Size(77, 23);
			label1.TabIndex = 22;
			label1.Text = "● ◌ ◌ ◌";
			// 
			// btnLanguageFr
			// 
			btnLanguageFr.BackgroundImage = Resources.Resources.flag_france_64;
			btnLanguageFr.BackgroundImageLayout = ImageLayout.Zoom;
			btnLanguageFr.FlatAppearance.BorderColor = Color.WhiteSmoke;
			btnLanguageFr.FlatAppearance.BorderSize = 2;
			btnLanguageFr.FlatStyle = FlatStyle.Flat;
			btnLanguageFr.Location = new Point(365, 214);
			btnLanguageFr.Name = "btnLanguageFr";
			btnLanguageFr.Size = new Size(50, 35);
			btnLanguageFr.TabIndex = 21;
			btnLanguageFr.UseVisualStyleBackColor = true;
			btnLanguageFr.Click += btnLanguageFr_Click;
			// 
			// btnLanguageRo
			// 
			btnLanguageRo.BackgroundImage = Resources.Resources.flag_romania_64;
			btnLanguageRo.BackgroundImageLayout = ImageLayout.Zoom;
			btnLanguageRo.FlatAppearance.BorderColor = Color.WhiteSmoke;
			btnLanguageRo.FlatAppearance.BorderSize = 2;
			btnLanguageRo.FlatStyle = FlatStyle.Flat;
			btnLanguageRo.Location = new Point(309, 214);
			btnLanguageRo.Name = "btnLanguageRo";
			btnLanguageRo.Size = new Size(50, 35);
			btnLanguageRo.TabIndex = 20;
			btnLanguageRo.UseVisualStyleBackColor = true;
			btnLanguageRo.Click += btnLanguageRo_Click;
			// 
			// btnLanguageEn
			// 
			btnLanguageEn.BackgroundImage = Resources.Resources.flag_uk_64;
			btnLanguageEn.BackgroundImageLayout = ImageLayout.Zoom;
			btnLanguageEn.FlatAppearance.BorderColor = Color.WhiteSmoke;
			btnLanguageEn.FlatAppearance.BorderSize = 2;
			btnLanguageEn.FlatStyle = FlatStyle.Flat;
			btnLanguageEn.Location = new Point(253, 214);
			btnLanguageEn.Name = "btnLanguageEn";
			btnLanguageEn.Size = new Size(50, 35);
			btnLanguageEn.TabIndex = 19;
			btnLanguageEn.UseVisualStyleBackColor = true;
			btnLanguageEn.Click += btnLanguageEn_Click;
			// 
			// btnTab1Next
			// 
			btnTab1Next.FlatStyle = FlatStyle.Flat;
			btnTab1Next.Location = new Point(560, 310);
			btnTab1Next.Name = "btnTab1Next";
			btnTab1Next.Size = new Size(100, 30);
			btnTab1Next.TabIndex = 6;
			btnTab1Next.Text = "Next";
			btnTab1Next.UseVisualStyleBackColor = true;
			btnTab1Next.Click += btnTab1Next_Click;
			// 
			// txtTab1WelcomeMsg
			// 
			txtTab1WelcomeMsg.BackColor = Color.FromArgb(250, 250, 250);
			txtTab1WelcomeMsg.BorderStyle = BorderStyle.None;
			txtTab1WelcomeMsg.Location = new Point(118, 74);
			txtTab1WelcomeMsg.Multiline = true;
			txtTab1WelcomeMsg.Name = "txtTab1WelcomeMsg";
			txtTab1WelcomeMsg.Size = new Size(434, 134);
			txtTab1WelcomeMsg.TabIndex = 5;
			txtTab1WelcomeMsg.Text = "Reminder allows you to receive\r\nnotifications about your favourite\r\nwritings. To setup the application\r\npress continue below.";
			txtTab1WelcomeMsg.TextAlign = HorizontalAlignment.Center;
			// 
			// lblTab1Welcome
			// 
			lblTab1Welcome.Font = new Font("Calibri Light", 26F, FontStyle.Regular, GraphicsUnit.Point);
			lblTab1Welcome.Location = new Point(144, 3);
			lblTab1Welcome.Name = "lblTab1Welcome";
			lblTab1Welcome.Size = new Size(388, 54);
			lblTab1Welcome.TabIndex = 2;
			lblTab1Welcome.Text = "Welcome";
			lblTab1Welcome.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// tabPage2
			// 
			tabPage2.BackColor = Color.FromArgb(250, 250, 250);
			tabPage2.Controls.Add(label2);
			tabPage2.Controls.Add(btnTab2Back);
			tabPage2.Controls.Add(clbTab2ImportCollections);
			tabPage2.Controls.Add(lblTab2);
			tabPage2.Controls.Add(btnTab2Next);
			tabPage2.Controls.Add(txtTab2Msg);
			tabPage2.Location = new Point(4, 24);
			tabPage2.Name = "tabPage2";
			tabPage2.Padding = new Padding(3);
			tabPage2.Size = new Size(675, 346);
			tabPage2.TabIndex = 1;
			tabPage2.Text = "tabPage2";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Font = new Font("Constantia", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
			label2.Location = new Point(300, 310);
			label2.Name = "label2";
			label2.Size = new Size(77, 23);
			label2.TabIndex = 23;
			label2.Text = "◌ ● ◌ ◌";
			// 
			// btnTab2Back
			// 
			btnTab2Back.FlatStyle = FlatStyle.Flat;
			btnTab2Back.Location = new Point(448, 310);
			btnTab2Back.Name = "btnTab2Back";
			btnTab2Back.Size = new Size(100, 30);
			btnTab2Back.TabIndex = 12;
			btnTab2Back.Text = "Back";
			btnTab2Back.UseVisualStyleBackColor = true;
			btnTab2Back.Click += btnTab2Back_Click;
			// 
			// clbTab2ImportCollections
			// 
			clbTab2ImportCollections.Font = new Font("Calibri", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
			clbTab2ImportCollections.FormattingEnabled = true;
			clbTab2ImportCollections.Items.AddRange(new object[] { "The New Testament - The NKJV Translation" });
			clbTab2ImportCollections.Location = new Point(125, 127);
			clbTab2ImportCollections.Name = "clbTab2ImportCollections";
			clbTab2ImportCollections.Size = new Size(419, 108);
			clbTab2ImportCollections.TabIndex = 11;
			// 
			// lblTab2
			// 
			lblTab2.Font = new Font("Calibri Light", 20F, FontStyle.Regular, GraphicsUnit.Point);
			lblTab2.Location = new Point(143, 5);
			lblTab2.Name = "lblTab2";
			lblTab2.Size = new Size(388, 45);
			lblTab2.TabIndex = 10;
			lblTab2.Text = "Welcome";
			lblTab2.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// btnTab2Next
			// 
			btnTab2Next.FlatStyle = FlatStyle.Flat;
			btnTab2Next.Location = new Point(560, 310);
			btnTab2Next.Name = "btnTab2Next";
			btnTab2Next.Size = new Size(100, 30);
			btnTab2Next.TabIndex = 9;
			btnTab2Next.Text = "Next";
			btnTab2Next.UseVisualStyleBackColor = true;
			btnTab2Next.Click += btnTab2Next_Click;
			// 
			// txtTab2Msg
			// 
			txtTab2Msg.BackColor = Color.FromArgb(250, 250, 250);
			txtTab2Msg.BorderStyle = BorderStyle.None;
			txtTab2Msg.Location = new Point(73, 53);
			txtTab2Msg.Multiline = true;
			txtTab2Msg.Name = "txtTab2Msg";
			txtTab2Msg.Size = new Size(523, 63);
			txtTab2Msg.TabIndex = 8;
			txtTab2Msg.Text = resources.GetString("txtTab2Msg.Text");
			txtTab2Msg.TextAlign = HorizontalAlignment.Center;
			// 
			// tabPage3
			// 
			tabPage3.BackColor = Color.FromArgb(250, 250, 250);
			tabPage3.Controls.Add(label3);
			tabPage3.Controls.Add(btnTab3Back);
			tabPage3.Controls.Add(btnTab3Often);
			tabPage3.Controls.Add(btnTab3Rare);
			tabPage3.Controls.Add(btnTab3Normal);
			tabPage3.Controls.Add(btnTab3Next);
			tabPage3.Controls.Add(lblTab3);
			tabPage3.Controls.Add(txtTab3Extra);
			tabPage3.Controls.Add(txtTab3NotificationInterval);
			tabPage3.Location = new Point(4, 24);
			tabPage3.Name = "tabPage3";
			tabPage3.Padding = new Padding(3);
			tabPage3.Size = new Size(675, 346);
			tabPage3.TabIndex = 2;
			tabPage3.Text = "tabPage3";
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Font = new Font("Constantia", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
			label3.Location = new Point(300, 310);
			label3.Name = "label3";
			label3.Size = new Size(77, 23);
			label3.TabIndex = 24;
			label3.Text = "◌ ◌ ● ◌";
			// 
			// btnTab3Back
			// 
			btnTab3Back.FlatStyle = FlatStyle.Flat;
			btnTab3Back.Location = new Point(448, 310);
			btnTab3Back.Name = "btnTab3Back";
			btnTab3Back.Size = new Size(100, 30);
			btnTab3Back.TabIndex = 23;
			btnTab3Back.Text = "Back";
			btnTab3Back.UseVisualStyleBackColor = true;
			btnTab3Back.Click += btnTab3Back_Click;
			// 
			// btnTab3Often
			// 
			btnTab3Often.BackColor = Color.White;
			btnTab3Often.FlatAppearance.BorderColor = Color.LightGray;
			btnTab3Often.FlatStyle = FlatStyle.Flat;
			btnTab3Often.Location = new Point(77, 87);
			btnTab3Often.Name = "btnTab3Often";
			btnTab3Often.Size = new Size(169, 54);
			btnTab3Often.TabIndex = 22;
			btnTab3Often.Text = "Often\r\n(every 15 mins)";
			btnTab3Often.UseVisualStyleBackColor = false;
			btnTab3Often.Click += btnTab3Often_Click;
			// 
			// btnTab3Rare
			// 
			btnTab3Rare.BackColor = Color.White;
			btnTab3Rare.FlatAppearance.BorderColor = Color.LightGray;
			btnTab3Rare.FlatStyle = FlatStyle.Flat;
			btnTab3Rare.Location = new Point(427, 87);
			btnTab3Rare.Name = "btnTab3Rare";
			btnTab3Rare.Size = new Size(169, 54);
			btnTab3Rare.TabIndex = 21;
			btnTab3Rare.Text = "Rare\r\n(every 15 mins)";
			btnTab3Rare.UseVisualStyleBackColor = false;
			btnTab3Rare.Click += btnTab3Rare_Click;
			// 
			// btnTab3Normal
			// 
			btnTab3Normal.BackColor = Color.White;
			btnTab3Normal.FlatStyle = FlatStyle.Flat;
			btnTab3Normal.Location = new Point(252, 87);
			btnTab3Normal.Name = "btnTab3Normal";
			btnTab3Normal.Size = new Size(169, 54);
			btnTab3Normal.TabIndex = 20;
			btnTab3Normal.Text = "Regular\r\n(every 15 mins)";
			btnTab3Normal.UseVisualStyleBackColor = false;
			btnTab3Normal.Click += btnTab3Normal_Click;
			// 
			// btnTab3Next
			// 
			btnTab3Next.FlatStyle = FlatStyle.Flat;
			btnTab3Next.Location = new Point(560, 310);
			btnTab3Next.Name = "btnTab3Next";
			btnTab3Next.Size = new Size(100, 30);
			btnTab3Next.TabIndex = 18;
			btnTab3Next.Text = "Next";
			btnTab3Next.UseVisualStyleBackColor = true;
			btnTab3Next.Click += btnTab3Next_Click;
			// 
			// lblTab3
			// 
			lblTab3.Font = new Font("Calibri Light", 20F, FontStyle.Regular, GraphicsUnit.Point);
			lblTab3.Location = new Point(143, 5);
			lblTab3.Name = "lblTab3";
			lblTab3.Size = new Size(388, 45);
			lblTab3.TabIndex = 17;
			lblTab3.Text = "Welcome";
			lblTab3.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// txtTab3Extra
			// 
			txtTab3Extra.BackColor = Color.FromArgb(250, 250, 250);
			txtTab3Extra.BorderStyle = BorderStyle.None;
			txtTab3Extra.Font = new Font("Calibri Light", 10F, FontStyle.Regular, GraphicsUnit.Point);
			txtTab3Extra.Location = new Point(77, 147);
			txtTab3Extra.Multiline = true;
			txtTab3Extra.Name = "txtTab3Extra";
			txtTab3Extra.Size = new Size(528, 28);
			txtTab3Extra.TabIndex = 16;
			txtTab3Extra.Text = "You can customize this setting later from the settings menu.\r\n";
			txtTab3Extra.TextAlign = HorizontalAlignment.Center;
			// 
			// txtTab3NotificationInterval
			// 
			txtTab3NotificationInterval.BackColor = Color.FromArgb(250, 250, 250);
			txtTab3NotificationInterval.BorderStyle = BorderStyle.None;
			txtTab3NotificationInterval.Location = new Point(70, 53);
			txtTab3NotificationInterval.Multiline = true;
			txtTab3NotificationInterval.Name = "txtTab3NotificationInterval";
			txtTab3NotificationInterval.Size = new Size(528, 28);
			txtTab3NotificationInterval.TabIndex = 15;
			txtTab3NotificationInterval.Text = "How often would you like the notifications to appear?\r\n";
			txtTab3NotificationInterval.TextAlign = HorizontalAlignment.Center;
			// 
			// tabPage4
			// 
			tabPage4.BackColor = Color.FromArgb(250, 250, 250);
			tabPage4.Controls.Add(label4);
			tabPage4.Controls.Add(lblTab4);
			tabPage4.Controls.Add(btnTab4Finish);
			tabPage4.Controls.Add(txtTab4Msg);
			tabPage4.Location = new Point(4, 24);
			tabPage4.Name = "tabPage4";
			tabPage4.Padding = new Padding(3);
			tabPage4.Size = new Size(675, 346);
			tabPage4.TabIndex = 3;
			tabPage4.Text = "tabPage4";
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Font = new Font("Constantia", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
			label4.Location = new Point(300, 310);
			label4.Name = "label4";
			label4.Size = new Size(77, 23);
			label4.TabIndex = 24;
			label4.Text = "◌ ◌ ◌ ●";
			// 
			// lblTab4
			// 
			lblTab4.Font = new Font("Calibri Light", 20F, FontStyle.Regular, GraphicsUnit.Point);
			lblTab4.Location = new Point(143, 5);
			lblTab4.Name = "lblTab4";
			lblTab4.Size = new Size(388, 45);
			lblTab4.TabIndex = 20;
			lblTab4.Text = "Welcome";
			lblTab4.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// btnTab4Finish
			// 
			btnTab4Finish.FlatStyle = FlatStyle.Flat;
			btnTab4Finish.Location = new Point(560, 310);
			btnTab4Finish.Name = "btnTab4Finish";
			btnTab4Finish.Size = new Size(100, 30);
			btnTab4Finish.TabIndex = 19;
			btnTab4Finish.Text = "Finish";
			btnTab4Finish.UseVisualStyleBackColor = true;
			btnTab4Finish.Click += btnTab4Finish_Click;
			// 
			// txtTab4Msg
			// 
			txtTab4Msg.BackColor = Color.FromArgb(250, 250, 250);
			txtTab4Msg.BorderStyle = BorderStyle.None;
			txtTab4Msg.Location = new Point(73, 66);
			txtTab4Msg.Multiline = true;
			txtTab4Msg.Name = "txtTab4Msg";
			txtTab4Msg.Size = new Size(528, 94);
			txtTab4Msg.TabIndex = 16;
			txtTab4Msg.Text = resources.GetString("txtTab4Msg.Text");
			txtTab4Msg.TextAlign = HorizontalAlignment.Center;
			// 
			// WelcomeForm
			// 
			AutoScaleDimensions = new SizeF(8F, 19F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.FromArgb(250, 250, 250);
			ClientSize = new Size(682, 406);
			ControlBox = false;
			Controls.Add(tabControl);
			Controls.Add(pnlTitle);
			Font = new Font("Calibri Light", 12F, FontStyle.Regular, GraphicsUnit.Point);
			FormBorderStyle = FormBorderStyle.None;
			Icon = (Icon)resources.GetObject("$this.Icon");
			Margin = new Padding(3, 4, 3, 4);
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "WelcomeForm";
			ShowIcon = false;
			SizeGripStyle = SizeGripStyle.Hide;
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Quoter";
			Load += WelcomeForm_Load;
			pnlTitle.ResumeLayout(false);
			pnlTitle.PerformLayout();
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			tabControl.ResumeLayout(false);
			tabPage1.ResumeLayout(false);
			tabPage1.PerformLayout();
			tabPage2.ResumeLayout(false);
			tabPage2.PerformLayout();
			tabPage3.ResumeLayout(false);
			tabPage3.PerformLayout();
			tabPage4.ResumeLayout(false);
			tabPage4.PerformLayout();
			ResumeLayout(false);
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
		private Label label1;
		private Label label2;
		private Label label4;
	}
}