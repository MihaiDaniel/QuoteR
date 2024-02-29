using Quoter.Framework.Data.Entities;

namespace Quoter.App.Forms.Manage
{
    /// <summary>
    /// Handles the favourite quotes form
    /// </summary>
    /// <remarks>
    /// This is actually a tab in the <see cref="ManageForm"/>
    /// </remarks>
    public interface IFavouriteQuotesForm
	{
		void SetChecksFavourites();

		void SetCollections(List<Collection> lstCollections);

	}
}
