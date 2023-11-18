namespace Quoter.Web.Services
{
	public interface IAppVersionService
	{
		Task<bool> IsAppVersionIdValid(Guid? appVersionId);
	}
}
