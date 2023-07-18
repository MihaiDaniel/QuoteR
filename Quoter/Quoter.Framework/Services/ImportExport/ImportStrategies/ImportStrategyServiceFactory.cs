using Quoter.Framework.Enums;

namespace Quoter.Framework.Services.ImportExport.ImportStrategies
{
	public class ImportStrategyServiceFactory : IImportStrategyServiceFactory
	{
		private readonly DefaultImportStrategyService _defaultStrategyService;
		private readonly MergeImportStrategyService _mergeStrategyService;
		private readonly ReplaceImportStrategyService _replaceStrategyService;

		public ImportStrategyServiceFactory(
			DefaultImportStrategyService defaultImportStrategyService,
			MergeImportStrategyService mergeStrategyService,
			ReplaceImportStrategyService replaceStrategyService)
		{
			_defaultStrategyService = defaultImportStrategyService;
			_mergeStrategyService = mergeStrategyService;
			_replaceStrategyService = replaceStrategyService;
		}

		public IImportStrategyService GetImportStrategyService(EnumImportStrategy enumImportStrategy)
		{
			switch (enumImportStrategy)
			{
				case EnumImportStrategy.Default: return _defaultStrategyService;
				case EnumImportStrategy.Merge: return _mergeStrategyService;
				case EnumImportStrategy.Replace: return _replaceStrategyService;
				default:
					throw new NotImplementedException();
			}
		}
	}
}
