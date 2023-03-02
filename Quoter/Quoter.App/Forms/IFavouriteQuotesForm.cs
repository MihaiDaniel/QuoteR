using Quoter.Framework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.App.Forms
{
	public interface IFavouriteQuotesForm
	{
		void SetChecksFavourites();
		void SetCollections(List<Collection> lstCollections);

	}
}
