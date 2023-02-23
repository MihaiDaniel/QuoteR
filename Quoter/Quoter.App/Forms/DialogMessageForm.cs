using Quoter.App.Models;
using Quoter.App.Services.FormAnimation;
using Quoter.App.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Quoter.App.Helpers;

namespace Quoter.App.Forms
{
	public partial class DialogMessageForm : Form, IDialogReturnable
	{
		private readonly IFormsManager _formsManager;

		public string StringResult { get; private set; }

		public DialogMessageForm(IFormsManager formsManager,
							   IFormPositioningService formPositioningService,
							   IStringResources stringResources,
							   DialogModel dialogModel)
		{
			InitializeComponent();
			formPositioningService.RegisterFormDragableByControl(this, pnlTitle);

			pnlTitle.BackColor = dialogModel.TitleColor;
			lblTopBar.Text = dialogModel.Title;
			txtMessage.Text = dialogModel.Message;



			_formsManager = formsManager;
		}

		private void btnRight_Click(object sender, EventArgs e)
		{

		}

		private void btnLeft_Click(object sender, EventArgs e)
		{

		}
	}
}
