namespace Quoter.Web.ViewModels.Shared
{
	/// <summary>
	/// View model for a route that the user can access
	/// </summary>
	public class RouteViewModel
	{
		/// <summary>
		/// Displayed name of the route
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The route of the page that can be accessed
		/// Ex: /Dashboard/AppVersions/Index
		/// </summary>
		public string Route { get; set; }

		public RouteViewModel(string name, string route)
		{
			Name = name;
			Route = route;
		}
	}
}
