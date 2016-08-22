using System.Collections.ObjectModel;
using FreshMvvm;
using Hubbl.Core.Model;
using PropertyChanged;

namespace Hubbl.Mobile.PageModels
{
	[ImplementPropertyChanged]
	public class HubsListPageModel : FreshBasePageModel
	{
		private readonly ObservableCollection<HubblUser> _hubs;

		public HubsListPageModel()
		{
			_hubs = new ObservableCollection<HubblUser>();
		}
	}
}
