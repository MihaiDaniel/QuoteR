using Quoter.App.Forms;
using Quoter.App.Models;
using Quoter.App.Services.Forms;
using Quoter.Framework.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.App.Helpers.Extensions
{
	public static class Extensions
	{
		public static T InvokeIfRequiredReturn<T>(this ISynchronizeInvoke control, Func<T> function)
		{
			if (control.InvokeRequired)
			{
				var args = new object[0];
				return (T)control.Invoke(function, args);
			}
			else
			{
				return function();
			}
		}

		/// <summary>
		/// Checks if the UI object needs an Invoke before making any changes.
		/// In case the call is made from a separate thread
		/// </summary>
		/// <param name="obj">Object, usually a control</param>
		/// <param name="action">Action to invoke</param>
		public static void InvokeIfRequired(this ISynchronizeInvoke obj, MethodInvoker action)
		{
			if (obj.InvokeRequired)
			{
				var args = new object[0];
				obj.Invoke(action, args);
			}
			else
			{
				action();
			}
		}

		/// <summary>
		/// Create a label below the control with red text. If the label does not exist it's created.
		/// If the label exists but error is empty it's hidden.
		/// </summary>
		/// <param name="control">The control for which the label message is created</param>
		/// <param name="error">The error message</param>
		public static void SetValidationError(this Control control, string error)
		{
			Label validationLabel = GetControlValidationLabel(control);
			SetValidationLabelError(validationLabel, error);
		}

		private static Label GetControlValidationLabel(Control control)
		{
			string validationLabelName = $"{control.Name}ValidationLabelMessage";
			Control[] arrExistingValidationControls = control.Parent.Controls.Find(validationLabelName, true);
			foreach (Control existingValidationControl in arrExistingValidationControls)
			{
				if (existingValidationControl.Name == validationLabelName && existingValidationControl is Label)
				{
					return existingValidationControl as Label;
				}
			}
			return CreateValidationLabelError(control, validationLabelName);
		}

		private static Label CreateValidationLabelError(Control control,string name)
		{
			Label label = new Label();
			label.Name = name;
			label.Visible = true;
			label.Font = new Font(label.Font.FontFamily, 9, FontStyle.Regular);
			label.ForeColor = Color.Red;
			label.Location = new Point(control.Location.X, control.Location.Y + control.Size.Height + 1);
			label.Size = new Size(control.Size.Width, 20);
			control.Parent.Controls.Add(label);
			return label;
		}

		private static void SetValidationLabelError(Label label, string error)
		{
			if (string.IsNullOrWhiteSpace(error))
			{
				label.Visible = false;
			}
			else
			{
				if (!label.Visible)
				{
					label.Visible = true;
				}
				label.Text = error;
			}
		}

		public static void ShowDialogOk(this IFormsManager formsManager, string title, string message)
		{
			DialogMessageFormOptions dialogModel = new DialogMessageFormOptions()
			{
				Title = title,
				Message = message,
				MessageBoxButtons = EnumDialogButtons.Ok
			};
			formsManager.ShowDialog<DialogMessageForm>(dialogModel);
		}

		public static void ShowDialogErr(this IFormsManager formsManager, string title, string message)
		{
			DialogMessageFormOptions dialogModel = new DialogMessageFormOptions()
			{
				Title = title,
				TitleColor = Const.ColorError,
				Message = message,
				MessageBoxButtons = EnumDialogButtons.Ok
			};
			formsManager.ShowDialog<DialogMessageForm>(dialogModel);
		}
	}
}
