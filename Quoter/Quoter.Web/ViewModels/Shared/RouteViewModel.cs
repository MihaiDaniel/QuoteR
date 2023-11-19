namespace Quoter.Web.ViewModels.Shared
{
	public class RouteViewModel
	{
		public string Name { get; set; }

		public string Route { get; set; }

		public RouteViewModel(string name, string route)
		{
			Name = name;
			Route = route;
		}
	}
}
