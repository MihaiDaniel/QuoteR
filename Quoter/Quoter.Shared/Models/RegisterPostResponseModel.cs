namespace Quoter.Shared.Models
{
	public class RegisterPostResponseModel
	{
		public string RegistrationId { get; set; }

		public RegisterPostResponseModel(string registrationId)
		{
			RegistrationId = registrationId;
		}
	}
}
