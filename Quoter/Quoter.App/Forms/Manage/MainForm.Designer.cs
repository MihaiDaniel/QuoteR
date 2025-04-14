namespace Quoter.App.Forms.Manage
{
	partial class MainForm
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
			poisonTabControl1 = new ReaLTaiizor.Controls.PoisonTabControl();
			tabPage1 = new TabPage();
			tabPage2 = new TabPage();
			thunderGroupBox1 = new ReaLTaiizor.Controls.ThunderGroupBox();
			poisonTabControl1.SuspendLayout();
			tabPage1.SuspendLayout();
			SuspendLayout();
			// 
			// poisonTabControl1
			// 
			poisonTabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			poisonTabControl1.Controls.Add(tabPage1);
			poisonTabControl1.Controls.Add(tabPage2);
			poisonTabControl1.Location = new Point(23, 63);
			poisonTabControl1.Name = "poisonTabControl1";
			poisonTabControl1.Padding = new Point(6, 8);
			poisonTabControl1.SelectedIndex = 0;
			poisonTabControl1.Size = new Size(754, 364);
			poisonTabControl1.TabIndex = 0;
			poisonTabControl1.UseSelectable = true;
			// 
			// tabPage1
			// 
			tabPage1.Controls.Add(thunderGroupBox1);
			tabPage1.Location = new Point(4, 38);
			tabPage1.Name = "tabPage1";
			tabPage1.Padding = new Padding(3);
			tabPage1.Size = new Size(746, 322);
			tabPage1.TabIndex = 0;
			tabPage1.Text = "tabPage1";
			tabPage1.UseVisualStyleBackColor = true;
			// 
			// tabPage2
			// 
			tabPage2.Location = new Point(4, 35);
			tabPage2.Name = "tabPage2";
			tabPage2.Padding = new Padding(3);
			tabPage2.Size = new Size(509, 209);
			tabPage2.TabIndex = 1;
			tabPage2.Text = "tabPage2";
			tabPage2.UseVisualStyleBackColor = true;
			// 
			// thunderGroupBox1
			// 
			thunderGroupBox1.BackColor = Color.Transparent;
			thunderGroupBox1.BodyColorA = Color.FromArgb(26, 26, 26);
			thunderGroupBox1.BodyColorB = Color.FromArgb(30, 30, 30);
			thunderGroupBox1.BodyColorC = Color.FromArgb(46, 46, 46);
			thunderGroupBox1.BodyColorD = Color.FromArgb(50, 55, 58);
			thunderGroupBox1.ForeColor = Color.WhiteSmoke;
			thunderGroupBox1.Location = new Point(6, 6);
			thunderGroupBox1.Name = "thunderGroupBox1";
			thunderGroupBox1.Size = new Size(379, 165);
			thunderGroupBox1.TabIndex = 0;
			thunderGroupBox1.Text = "thunderGroupBox1";
			// 
			// MainForm
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 450);
			Controls.Add(poisonTabControl1);
			Name = "MainForm";
			Text = "MainForm";
			poisonTabControl1.ResumeLayout(false);
			tabPage1.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion

		private ReaLTaiizor.Controls.PoisonTabControl poisonTabControl1;
		private TabPage tabPage1;
		private TabPage tabPage2;
		private ReaLTaiizor.Controls.ThunderGroupBox thunderGroupBox1;
	}
}