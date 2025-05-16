namespace Quoter.App.Forms
{
	partial class DebugForm
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
			button1 = new Button();
			btnShowQuoteWithParams = new Button();
			label1 = new Label();
			txtTitle = new TextBox();
			label2 = new Label();
			txtBody = new TextBox();
			txtFooter = new TextBox();
			label3 = new Label();
			label4 = new Label();
			label5 = new Label();
			btnShowQuoteById = new Button();
			label6 = new Label();
			txtQuoteId = new TextBox();
			btnFindQuoteId = new Button();
			label7 = new Label();
			txtSearchBar = new TextBox();
			txtQuoteIdFound = new TextBox();
			txtSearchFindContent = new TextBox();
			SuspendLayout();
			// 
			// button1
			// 
			button1.Location = new Point(12, 12);
			button1.Name = "button1";
			button1.Size = new Size(111, 42);
			button1.TabIndex = 0;
			button1.Text = "Show random quote now";
			button1.UseVisualStyleBackColor = true;
			button1.Click += button1_Click;
			// 
			// btnShowQuoteWithParams
			// 
			btnShowQuoteWithParams.Location = new Point(12, 60);
			btnShowQuoteWithParams.Name = "btnShowQuoteWithParams";
			btnShowQuoteWithParams.Size = new Size(111, 42);
			btnShowQuoteWithParams.TabIndex = 1;
			btnShowQuoteWithParams.Text = "Show quote with params";
			btnShowQuoteWithParams.UseVisualStyleBackColor = true;
			btnShowQuoteWithParams.Click += btnShowQuoteWithParams_Click;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(14, 111);
			label1.Name = "label1";
			label1.Size = new Size(29, 15);
			label1.TabIndex = 2;
			label1.Text = "Title";
			// 
			// txtTitle
			// 
			txtTitle.Location = new Point(72, 108);
			txtTitle.Name = "txtTitle";
			txtTitle.Size = new Size(288, 23);
			txtTitle.TabIndex = 3;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(14, 140);
			label2.Name = "label2";
			label2.Size = new Size(34, 15);
			label2.TabIndex = 4;
			label2.Text = "Body";
			// 
			// txtBody
			// 
			txtBody.Location = new Point(72, 137);
			txtBody.Name = "txtBody";
			txtBody.Size = new Size(288, 23);
			txtBody.TabIndex = 5;
			// 
			// txtFooter
			// 
			txtFooter.Location = new Point(72, 166);
			txtFooter.Name = "txtFooter";
			txtFooter.Size = new Size(288, 23);
			txtFooter.TabIndex = 6;
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new Point(14, 169);
			label3.Name = "label3";
			label3.Size = new Size(41, 15);
			label3.TabIndex = 7;
			label3.Text = "Footer";
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Location = new Point(19, 198);
			label4.Name = "label4";
			label4.Size = new Size(138, 15);
			label4.TabIndex = 8;
			label4.Text = "FadeInFromBottomRight";
			// 
			// label5
			// 
			label5.AutoSize = true;
			label5.Location = new Point(194, 198);
			label5.Name = "label5";
			label5.Size = new Size(52, 15);
			label5.TabIndex = 9;
			label5.Text = "FadeOut";
			// 
			// btnShowQuoteById
			// 
			btnShowQuoteById.Location = new Point(12, 232);
			btnShowQuoteById.Name = "btnShowQuoteById";
			btnShowQuoteById.Size = new Size(130, 44);
			btnShowQuoteById.TabIndex = 10;
			btnShowQuoteById.Text = "Show quote by id";
			btnShowQuoteById.UseVisualStyleBackColor = true;
			btnShowQuoteById.Click += btnShowQuoteById_Click;
			// 
			// label6
			// 
			label6.AutoSize = true;
			label6.Location = new Point(12, 292);
			label6.Name = "label6";
			label6.Size = new Size(50, 15);
			label6.TabIndex = 11;
			label6.Text = "QuoteId";
			// 
			// txtQuoteId
			// 
			txtQuoteId.Location = new Point(72, 289);
			txtQuoteId.Name = "txtQuoteId";
			txtQuoteId.Size = new Size(70, 23);
			txtQuoteId.TabIndex = 12;
			// 
			// btnFindQuoteId
			// 
			btnFindQuoteId.Location = new Point(411, 12);
			btnFindQuoteId.Name = "btnFindQuoteId";
			btnFindQuoteId.Size = new Size(130, 23);
			btnFindQuoteId.TabIndex = 13;
			btnFindQuoteId.Text = "Find quote id by text";
			btnFindQuoteId.UseVisualStyleBackColor = true;
			btnFindQuoteId.Click += btnFindQuoteId_Click;
			// 
			// label7
			// 
			label7.AutoSize = true;
			label7.Location = new Point(486, 44);
			label7.Name = "label7";
			label7.Size = new Size(55, 15);
			label7.TabIndex = 14;
			label7.Text = "Result id:";
			// 
			// txtSearchBar
			// 
			txtSearchBar.Location = new Point(547, 12);
			txtSearchBar.Name = "txtSearchBar";
			txtSearchBar.Size = new Size(241, 23);
			txtSearchBar.TabIndex = 15;
			// 
			// txtQuoteIdFound
			// 
			txtQuoteIdFound.Location = new Point(547, 41);
			txtQuoteIdFound.Name = "txtQuoteIdFound";
			txtQuoteIdFound.Size = new Size(241, 23);
			txtQuoteIdFound.TabIndex = 16;
			// 
			// txtSearchFindContent
			// 
			txtSearchFindContent.Location = new Point(547, 71);
			txtSearchFindContent.Multiline = true;
			txtSearchFindContent.Name = "txtSearchFindContent";
			txtSearchFindContent.Size = new Size(241, 60);
			txtSearchFindContent.TabIndex = 17;
			// 
			// DebugForm
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 450);
			Controls.Add(txtSearchFindContent);
			Controls.Add(txtQuoteIdFound);
			Controls.Add(txtSearchBar);
			Controls.Add(label7);
			Controls.Add(btnFindQuoteId);
			Controls.Add(txtQuoteId);
			Controls.Add(label6);
			Controls.Add(btnShowQuoteById);
			Controls.Add(label5);
			Controls.Add(label4);
			Controls.Add(label3);
			Controls.Add(txtFooter);
			Controls.Add(txtBody);
			Controls.Add(label2);
			Controls.Add(txtTitle);
			Controls.Add(label1);
			Controls.Add(btnShowQuoteWithParams);
			Controls.Add(button1);
			Name = "DebugForm";
			Text = "DebugForm";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Button button1;
		private Button btnShowQuoteWithParams;
		private Label label1;
		private TextBox txtTitle;
		private Label label2;
		private TextBox txtBody;
		private TextBox txtFooter;
		private Label label3;
		private Label label4;
		private Label label5;
		private Button btnShowQuoteById;
		private Label label6;
		private TextBox txtQuoteId;
		private Button btnFindQuoteId;
		private Label label7;
		private TextBox txtSearchBar;
		private TextBox txtQuoteIdFound;
		private TextBox txtSearchFindContent;
	}
}