namespace Quoter.Update
{
	partial class Main
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
			progressBar1 = new ProgressBar();
			label1 = new Label();
			SuspendLayout();
			// 
			// progressBar1
			// 
			progressBar1.Location = new Point(12, 41);
			progressBar1.Margin = new Padding(3, 2, 3, 2);
			progressBar1.MarqueeAnimationSpeed = 40;
			progressBar1.Name = "progressBar1";
			progressBar1.Size = new Size(260, 22);
			progressBar1.Style = ProgressBarStyle.Marquee;
			progressBar1.TabIndex = 0;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new Font("Calibri", 12F, FontStyle.Regular, GraphicsUnit.Point);
			label1.Location = new Point(12, 18);
			label1.Name = "label1";
			label1.Size = new Size(80, 19);
			label1.TabIndex = 1;
			label1.Text = "Updating...";
			// 
			// Main
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(284, 101);
			Controls.Add(label1);
			Controls.Add(progressBar1);
			Icon = (Icon)resources.GetObject("$this.Icon");
			Margin = new Padding(3, 2, 3, 2);
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "Main";
			ShowInTaskbar = false;
			SizeGripStyle = SizeGripStyle.Hide;
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Quoter";
			FormClosing += Main_FormClosing;
			Load += Main_Load;
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private ProgressBar progressBar1;
		private Label label1;
	}
}