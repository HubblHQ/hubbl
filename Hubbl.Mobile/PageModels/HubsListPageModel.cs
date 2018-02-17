using System.Collections.ObjectModel;
using Hubbl.Core.Model;
using Xamarin.Forms;

namespace Hubbl.Mobile.PageModels
{
	public class HubsListPageModel : HubblViewModel
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
