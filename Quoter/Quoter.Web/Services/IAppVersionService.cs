namespace Quoter.Web.Services
{
	public interface IAppVersionService
	{
		Task<bool> IsAppVersionIdValid(int? appVersionId);
	}
}
