namespace Quoter.Web.ViewModels.Shared
{
	/// <summary>
	/// View model for a delete modal with delete and cancel options
	/// </summary>
	public class ModalDeleteViewModel
	{
		/// <summary>
		/// Title of the modal.  Should be localized beforehand
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Message of the modal. Should be localized beforehand
		/// </summary>
		public string Message { get; set; }

		public ModalDeleteViewModel(string title, string message)
		{
			Title = title;
			Message = message;
		}
	}
}
