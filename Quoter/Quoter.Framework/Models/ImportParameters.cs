using Quoter.Shared.Enums;

namespace Quoter.Framework.Models
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
		/// Set the imported collections as favourite on import.
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
		/// Parameter that indicates what happens when an imported collection has the same name as an
		/// existing collection. If True, all the books, chapters, and quotes will be merged into the
		/// existing collection. If False they will be simply added (in case of name conflict, collections will 
		/// be renamed)
		/// </summary>
		public bool IsMergeCollections { get; set; }
	
	}
}
