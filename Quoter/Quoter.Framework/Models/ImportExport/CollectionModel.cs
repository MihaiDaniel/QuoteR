using Newtonsoft.Json;
using Quoter.Shared.Enums;

namespace Quoter.Framework.Models.ImportExport
{
	/// <summary>
	/// Model used for exporting and importing collections
	/// </summary>
	public class CollectionModel
	{
		[JsonProperty("CollectionId")]
		public int CollectionId { get; set; }

		[JsonProperty("Name")]
		public string Name { get; set; }

		[JsonProperty("Description")]
		public string? Description { get; set; }

		[JsonProperty("IsFavourite")]
		public bool? IsFavourite { get; set; }

		[JsonProperty("Language")]
		public EnumLanguage? Language { get; set; }
	}
}
