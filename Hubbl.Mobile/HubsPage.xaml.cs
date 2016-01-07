﻿using System.Collections.ObjectModel;
using Hubbl.Core.Messages;
using Autofac;
using Hubbl.Core.Service;

using Xamarin.Forms;
using Hubbl.Core.Model;
using System.Diagnostics;
using MessageRouter.Network;

namespace Hubbl.Mobile
{
	public class Hub 
	{
		public string Name {get; set; }
		public string Admin {get; set; }
		public string CurrentSong { get; set; }
	}

	public partial class HubsPage : ContentPage
	{
		ObservableCollection<User> hubs = new ObservableCollection<User>();
		IMessageReceiverConfig<EchoMessage> subscription;
		public HubsPage ()
		{
			InitializeComponent ();
			//var message = new HelloMessage (App.Container.Resolve<ISession> ().CurrentUser);

			HubsView.ItemsSource = hubs;
			this.Icon = new FileImageSource (){ File = "app-logo-square-black@2x.png" };
			HubsView.ItemSelected += (sender, e) => {
				Navigation.PushAsync(new OneHubPage((User)e.SelectedItem));
			};
			AddHub.Clicked += (sender, e) => {
				Navigation.PushAsync(new NewHubPage(), true);
			};
			ToolbarItems.Add (new ToolbarItem ("Обновить", "", () => {
				LoadHubs ();
			}));
		}
		void LoadHubs()
		{
			hubs.Clear ();
			subscription = App.Router.Subscribe<EchoMessage> ();
			subscription.OnSuccess ((ep, m) => {				
				m.Sender.IpAddress = ep.Address;
				if (m.Sender.IsHub) {
					App.Container.Resolve<UsersService>().Add(m.Sender);
					hubs.Add (m.Sender);
				}
			});
			subscription.OnException ((ep, ex) => {
				Debug.WriteLine(ex.Message);
			});
			var msg = new HelloMessage (App.Container.Resolve<ISession> ().CurrentUser);
			var hello = App.Router.Publish (msg);
			hello.Run ();
		}
		protected override void OnAppearing ()
		{
			LoadHubs ();
			base.OnAppearing ();
		}
		protected override void OnDisappearing ()
		{
			subscription.Dispose ();
			base.OnDisappearing ();
		}
	}
}

