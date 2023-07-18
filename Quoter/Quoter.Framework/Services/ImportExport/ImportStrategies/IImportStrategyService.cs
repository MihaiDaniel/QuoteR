using Quoter.Framework.Models.ImportExport;

namespace Quoter.Framework.Services.ImportExport.ImportStrategies
{
	/// <summary>
	/// Interface for various importing services, each with it's own strategy
	/// </summary>
	/// <remarks>
	/// Implementations:
	/// <see cref="DefaultImportStrategyService"/>
	/// <see cref="ReplaceImportStrategyService"/>
	/// <see cref="MergeImportStrategyService"/>
	/// </remarks>
	public interface IImportStrategyService
	{
		/// <summary>
		/// Import the collections, books, chapters and quotes found in the <paramref name="model"/>
		/// </summary>
		Task ImportAsync(ImportExportModel model, ImportParameters importParameters);
	}
}
