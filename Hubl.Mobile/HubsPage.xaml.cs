using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Xamarin.Forms;

namespace Hubl.Mobile
{
	public class Hub 
	{
		public string Name {get; set; }
		public string Admin {get; set; }
		public string CurrentSong { get; set; }
	}

	public partial class HubsPage : ContentPage
	{
		ObservableCollection<Hub> hubs = new ObservableCollection<Hub>();
		public HubsPage ()
		{
			InitializeComponent ();

			hubs.Add (new Hub{ Name = "Rap", Admin = "Pasha", CurrentSong = "Eminem - Lose yourself" });
			hubs.Add(new Hub{Name = "Rock", Admin = "Danil", CurrentSong = "Scorpions - Wind of change"});
			HubsView.ItemsSource = hubs;

			HubsView.ItemSelected += (sender, e) => {
				Navigation.PushAsync(new OneHubPage());
			};
		}
	}
}

