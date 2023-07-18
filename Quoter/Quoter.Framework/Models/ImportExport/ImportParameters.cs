using Quoter.Framework.Enums;
using Quoter.Shared.Enums;

namespace Quoter.Framework.Models.ImportExport
{
	/// <summary>
	/// Import parameters used to know what and how to import
	/// </summary>
	public class ImportParameters
	{
		/// <summary>
		/// File paths of files to import
		/// </summary>
		public string[] Files { get; set; }

		/// <summary>
		/// Set the imported collections (books and chapters) as favourite on import, regardless of their favourite status.
		/// </summary>
		public bool IsFavourite { get; set; }

		/// <summary>
		/// If True, imported collections will be assigned the current application language, regardless of 
		/// their original language
		/// </summary>
		public bool IsIgnoreLanguage { get; set; }

		/// <summary>
		/// If IsIgnoreLanguage is set to True, this property will indicate what Language will be assigned to
		/// the imported collections. Normally this ought to be set as the current app language.
		/// </summary>
		public EnumLanguage Language { get; set; }

		/// <summary>
		/// Indicates what strategy to use when importing collections
		/// </summary>
		public EnumImportStrategy ImportStrategy { get; set; }

		/// <summary>
		/// Indicates if the user should be notified or not of this import job
		/// </summary>
		public bool NotifyUser { get; set; } = true;

	}
}
