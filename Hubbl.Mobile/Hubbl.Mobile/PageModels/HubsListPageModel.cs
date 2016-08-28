using System.Collections.ObjectModel;
using FreshMvvm;
using Hubbl.Core.Model;
using PropertyChanged;
using Xamarin.Forms;

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

		public Command SettingsCommand
		{
		    get { return new Command(() => CoreMethods.PushPageModel<SettingsPageModel>()); }
		}

	    public Command CreateNewHubCommand
	    {
	        get { return new Command(() => CoreMethods.PushPageModel<CreateNewHubPageModel>()); }
	    }
	}
}
