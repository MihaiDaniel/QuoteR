using Quoter.Framework.Enums;

namespace Quoter.Framework.Services.ImportExport.ImportStrategies
{
	/// <summary>
	/// Interface for service responsible for creating services depending on import strategy
	/// </summary>
	public interface IImportStrategyServiceFactory
	{
		/// <summary>
		/// Get a import strategy service based on <paramref name="enumImportStrategy"/>
		/// </summary>
		IImportStrategyService GetImportStrategyService(EnumImportStrategy enumImportStrategy);
	}
}
