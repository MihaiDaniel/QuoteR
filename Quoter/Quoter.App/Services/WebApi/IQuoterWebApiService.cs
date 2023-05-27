namespace Quoter.App.Services.WebApi
{
	public interface IQuoterWebApiService
	{
		Task<Guid> RegisterApplication(Guid installGuid);

	}
}
