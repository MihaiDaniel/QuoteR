namespace Quoter.Framework.Models
{
	/// <summary>
	/// Object that holds argument for theOpeningForm event
	/// </summary>
	public class OpeningFormArgs
	{
		/// <summary>
		/// The type of the Form
		/// </summary>
		public Type Type { get; set; }
		/// <summary>
		/// Parameters passed to the form to open
		/// </summary>
		public object[] Parameters { get; set; }
	}
}
