using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Quoter.Web.Infrastructure
{
	public class SwaggerRegistrationHeaderParameter: IOperationFilter
	{
		public void Apply(OpenApiOperation operation, OperationFilterContext context)
		{
			if (operation.Parameters is null)
			{
				operation.Parameters = new List<OpenApiParameter>();
			}

			operation.Parameters.Add(new OpenApiParameter
			{
				Name = "Registration",
				In = ParameterLocation.Header,
				Description = "Registration id of the application",
				Required = true,
			});
		}
	}
}
