using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Autofac;
using Xamarin.Forms;
using Hubl.Core.Model;
using Hubl.Core.Service;
using Autofac.Core.Lifetime;
using System.Linq;
using System.Diagnostics;
using Hubl.Core;

namespace Hubl.Mobile
{	
	public class Song {
		public Track Track { get; set; }
		public bool Selected {get; set; }
		public override string ToString ()
		{
			return Track.Name;
		}
	}

	public class State {
		public double Progress {get; set; }
		public string Current { get; set; }
		public DateTime Remaining {get; set; }
	}

	public partial class OneHubPage : ContentPage
	{
		User hub;
		bool isPlaying;
		ObservableCollection<Track> tracks;
		Track currentTrack;
		public OneHubPage (User hub)
		{
			this.hub = hub;
			InitializeComponent ();
			BindingContext = this;
			Play.Clicked += Play_Clicked;
			tracks = App.Container.Resolve<ISession> ().Playlist;
			SongsView.ItemsSource = tracks;
			tracks.CollectionChanged += Tracks_CollectionChanged;
			this.Title = hub.Hub;
			//CurrentSong.Text = "Current song";
			//ElapsedTime.Text = 
			//RemainingTime.Text = TimeSpan.FromSeconds (150).ToString (@"mm\:ss");
			//SongProgress.ProgressTo (100.0/250, 5, Easing.CubicOut);

			ToolbarItems.Add (new ToolbarItem("", "add-track-blue@2x.png", async () => {
				Navigation.PushAsync (new SongsPage(hub));
			}));
			ElapsedTime.Text = TimeSpan.FromSeconds (0).ToString (@"mm\:ss");
			RemainingTime.Text = TimeSpan.FromSeconds (0).ToString (@"mm\:ss");
		}

		protected override void OnAppearing ()
		{
			App.Router.Subscribe<HubMessagePlaylistWasUpdated> ().OnSuccess ((ep, m) => {
				tracks.Clear ();
				foreach (var t in m.Playlist) {
					tracks.Add (t);
				}
				currentTrack = m.PlayingTrack;

			});
			base.OnAppearing ();
		}

		void SetCurrentTrack()
		{
			CurrentSongLabel.Text = currentTrack.Name;
			CurrentSongAuthor.Text = currentTrack.Artist;
			ElapsedTime.Text = currentTrack.Current.ToString (@"mm\:ss");
			RemainingTime.Text = currentTrack.Duration.Subtract (currentTrack.Current).ToString (@"mm\:ss");
			var progress = (double)currentTrack.Current.TotalSeconds / currentTrack.Duration.TotalSeconds;
			SongProgress.ProgressTo (progress, 1, Easing.SinOut);
		}

		void Tracks_CollectionChanged (object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			/*if (tracks.Count == 1) {
				currentTrack = tracks.First ();
				DependencyService.Get<IMusicPlayerBackend> ().PlayTrack (currentTrack);
				var player = DependencyService.Get<IMusicPlayerBackend> ();
			}*/
		}

		void Play_Clicked (object sender, EventArgs e)
		{
			if (isPlaying) {
				DependencyService.Get<IMusicPlayerBackend> ().PauseCurrentTrack ();
			} else {
				DependencyService.Get<IMusicPlayerBackend> ().PlayTrack (currentTrack);
			}
		}
	}
}

