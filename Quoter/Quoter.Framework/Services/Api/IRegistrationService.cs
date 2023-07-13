namespace Quoter.Framework.Services.Api
{
	/// <summary>
	/// Interface for service responsible for managing the registration of the application
	/// </summary>
	public interface IRegistrationService
	{
		/// <summary>
		/// Verifies if the application is registered by checking if the app has a stored registrationId
		/// </summary>
		/// <returns></returns>
		bool IsRegistered();

		/// <summary>
		/// Returns the current registrationId if the app is registered. Otherwise it tries to register
		/// the application by calling the server, stores the received registrationId and then returns it.
		/// </summary>
		/// <returns></returns>
		Task<Guid> GetRegistrationId();
	}
}
