using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quoter.App.Helpers
{
	public static class LoadingTabHelper
	{
		public static void ShowLoadingSpinner(Control control)
		{
			Panel loadPanel = new Panel()
			{
				Name = "pnlLoading-"+control.Name,
				Location = new Point(0, 0),
				Width = control.Width,
				Height = control.Height,
			};
			loadPanel.Controls.Add(new PictureBox()
			{
				Location = new Point(350, 170),
				Width = 128,
				Height = 128,
				Image = global::Quoter.App.Resources.Resources.loading_transparent_128
			});
			control.Controls.Add(loadPanel);
			loadPanel.BringToFront();
		}

		public static void HideLoadingSpinner(Control control)
		{
			Control[] found = control.Controls.Find("pnlLoading-" + control.Name, false);
			if(found != null && found.Length > 0)
			{
				control.Controls.Remove(found[0]);
			}
		}
	}
}
