namespace Quoter.Framework.Enums
{
	/// <summary>
	/// Describes supported strategies for importing collections in the application
	/// </summary>
	public enum EnumImportStrategy
	{
		/// <summary>
		/// Default strategy will always Add. If there is already a collection with the same name
		/// it will be left as is, and a new one will be added with a number appended to the end
		/// </summary>
		Default = 0,

		/// <summary>
		/// This strategy will attempt to combine data. If a collection does not exist it will simply add it.
		/// If it does exist it will merge the data into it. It will treat books and chapters similary inside
		/// the collection. Quotes will always be replaced with the ones in the imported collection for existing books and chapters.
		/// </summary>
		Merge = 1,

		/// <summary>
		/// If another collection with the same name already exist it will delete it entierly and add new data 
		/// from the import effectively replacing it. Otherwise this strategy will simply add a collection if it does not exist.
		/// </summary>
		Replace = 2,
	}
}
