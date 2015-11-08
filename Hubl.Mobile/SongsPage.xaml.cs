using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using Hubl.Core.Model;
using Hubl.Core.Messages;
using Autofac;
using Hubl.Core.Service;
using PCLCore;

namespace Hubl.Mobile
{
	public partial class SongsPage : ContentPage
	{
		ObservableCollection<Song> songs = new ObservableCollection<Song>();
		User hub;
		public SongsPage (User hub)
		{
			this.hub = hub;
			InitializeComponent ();
			Title = "Добавить песни";
		}

		protected override void OnAppearing ()
		{
			LoadSongsFromVK ();
			SongsView.ItemsSource = songs;
			//songs.Add (new Song(){Track = new Track{Name = "123"}});
			base.OnAppearing ();
		}

		async void LoadSongsFromVK()
		{
			var user = App.Container.Resolve<ISession> ().CurrentUser;
			if (user.VkUserInfo == null) {
				Navigation.PushAsync (new AuthPage());
			}
			else {
				var service = new VkApiService (user);
				var tracks = await service.GetUserAudioList (0, 25);
				songs.Clear ();
				foreach (var t in tracks)
					songs.Add (new Song{Track = t});
			}
		}

		protected override void OnDisappearing ()
		{	
			foreach (var song in songs.Where (s => s.Selected)) {
				var msg = new AddCloudTrackMessage ();
				msg.Track = song.Track;
				msg.Sender = App.Container.Resolve<ISession> ().CurrentUser;
				App.Router.PublishFor (new string[]{hub.Id}, msg).First ().Run ();
			}

			base.OnDisappearing ();
		}
	}
}

