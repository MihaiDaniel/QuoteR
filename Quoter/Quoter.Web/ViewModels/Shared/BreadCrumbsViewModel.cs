namespace Quoter.Web.ViewModels.Shared
{
	/// <summary>
	/// ViewModel for displaying breadcrumbs and their names and routes
	/// </summary>
	public class BreadCrumbsViewModel
	{
		/// <summary>
		/// The current page name (the last "crumb" in the bread crumbs unclickable)
		/// </summary>
		public string CurrentPageName { get; set; }

		/// <summary>
		/// The routes that the user can go to between Home and Current page
		/// </summary>
		public IEnumerable<RouteViewModel> Routes { get; set; }

		public BreadCrumbsViewModel(string currentPageName)
		{
			CurrentPageName = currentPageName;
			Routes = new List<RouteViewModel>();
		}

		public BreadCrumbsViewModel(string currentPageName, params RouteViewModel[] routes)
		{
			CurrentPageName = currentPageName;
			Routes = routes;
		}
	}
}
