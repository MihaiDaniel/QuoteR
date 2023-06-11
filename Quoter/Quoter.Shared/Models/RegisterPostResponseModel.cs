namespace Quoter.Shared.Models
{
	public class RegisterPostResponseModel
	{
		public Guid RegistrationId { get; set; }

		public RegisterPostResponseModel(Guid registrationId)
		{
			RegistrationId = registrationId;
		}
	}
}
