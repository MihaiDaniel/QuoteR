using Quoter.Shared.Enums;

namespace Quoter.Framework.Models.ImportExport
{
    /// <summary>
    /// Parameters for the collections export service
    /// </summary>
    public class ExportParameters
    {
        /// <summary>
        /// If this is True it will export only collections marked as favourite, otherwise it will export all collections
        /// </summary>
        public bool IsExportOnlyFavouriteCollections { get; set; }
        /// <summary>
        /// Full file path of the file for export
        /// </summary>
        public string ExportFilePath { get; set; }
        /// <summary>
        /// If this has a value it will export only collections with the set language
        /// </summary>
        public EnumLanguage? Language { get; set; }
    }
}
