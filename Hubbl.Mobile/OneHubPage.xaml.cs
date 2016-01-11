using System;
using System.Collections.ObjectModel;
using Autofac;
using Xamarin.Forms;
using Hubbl.Core.Model;
using Hubbl.Core.Service;
using Hubbl.Core.Messages;

namespace Hubbl.Mobile
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
		HubblUser hub;
		bool isPlaying;
		ObservableCollection<PlaylistEntry> tracks;
		PlaylistEntry currentTrack;
		public OneHubPage (HubblUser hub)
		{
			this.hub = hub;
			InitializeComponent ();
			BindingContext = this;
			Play.Clicked += Play_Clicked;
			tracks = new ObservableCollection<PlaylistEntry> (App.Container.Resolve<ISession> ().Playlist);
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
				SetCurrentTrack ();
			});
			base.OnAppearing ();
		}

		void SetCurrentTrack()
		{
			CurrentSongLabel.Text = currentTrack.Track.Name;
			CurrentSongAuthor.Text = currentTrack.Track.Artist;
			ElapsedTime.Text = currentTrack.Track.Current.ToString (@"mm\:ss");
			RemainingTime.Text = currentTrack.Track.Duration.Subtract (currentTrack.Track.Current).ToString (@"mm\:ss");
			var progress = (double)currentTrack.Track.Current.TotalSeconds / currentTrack.Track.Duration.TotalSeconds;
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
			
		}
	}
}

